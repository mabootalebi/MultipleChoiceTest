using Contracts.DTOs;
using Contracts.DTOs.Test;
using Contracts.DTOs.Test.Answer;
using Contracts.DTOs.Test.Question;
using Contracts.DTOs.Test.Question.Choice;
using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Interfaces;


namespace Services
{
    public class AnswerTestServices: IAnswerTestServices
    {
        private readonly ILogger<AnswerTestServices> _logger;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IChoiceRepository _choiceRepository;
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IAnsweredTestRepository _answeredTestRepository;
        private readonly IAnsweredTestDetailRepository _answeredTestDetailRepository;
        private readonly UserManager<User> _userManager;


        public AnswerTestServices(ILogger<AnswerTestServices> logger,
                                  ITestRepository testRepository,
                                  IQuestionRepository questionRepository,
                                  IChoiceRepository choiceRepository,
                                  IAnalysisRepository analysisRepository,
                                  IAnsweredTestRepository answeredTestRepository,
                                  IAnsweredTestDetailRepository answeredTestDetailRepository,
                                  UserManager<User> userManager)
        {
            _logger = logger;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
            _analysisRepository = analysisRepository;
            _answeredTestRepository = answeredTestRepository;
            _answeredTestDetailRepository = answeredTestDetailRepository;
            _userManager = userManager;
        }

        public async Task<ResultDto<FetchTestDto>> FetchTestQuestionsAsync(int testId, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(testId, cancellationToken);
            if (test is null)
                return new ResultDto<FetchTestDto>
                {
                    Status = CustomStatuses.TestNotFound
                };

            var questions = await _questionRepository.FetchAllQuestionsByTestId(testId, cancellationToken);

            return new ResultDto<FetchTestDto>
            {
                Status = CustomStatuses.Success,
                Parameter = new FetchTestDto
                {
                    Id = test.Id,
                    Name = test.Name,
                    Description = test.Description,
                    Questions = questions?.Select(t => new FetchQuestionDto
                    {
                        Id = t.Id,
                        Description = t.Description,
                        Order = t.Order,
                        Choices = t.Choices?.Select(x => new FetchChoiceDto
                        {
                            Id = x.Id,
                            Order = x.Order,
                            Score = x.Score,
                            Title = x.Title
                        }).OrderBy(x => x.Order)
                          .ToList()
                    }).OrderBy(t => t.Order)
                      .ToList()
                }
            };
        }

        public async Task<ResultDto<TestResultDto>> SaveUserAnswers(string username, CreateAnswerDto answerDto, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(answerDto.TestId, cancellationToken);
            if (test is null)
                return new ResultDto<TestResultDto>
                {
                    Status = CustomStatuses.TestNotFound
                };

            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return new ResultDto<TestResultDto>
                {
                    Status = CustomStatuses.UserWithGivenUsernameNotFound
                };

            var userTotalScore = await _choiceRepository.CalculateScoreAsync(answerDto.ChoiceIds!, cancellationToken);

            var answer = new AnsweredTest
            {
                Test = test,
                TestId = test.Id,
                User = user,
                UserId = user.Id,
                TotalScore = userTotalScore
            };

            var details = new List<AnsweredTestDetail>();

            foreach(var choiceId in answerDto.ChoiceIds!)
            {
                var choice = await _choiceRepository.FetchByIdAsync(choiceId);
                if (choice is null)
                    return new ResultDto<TestResultDto>
                    {
                        Status = CustomStatuses.ChoiceNotFound
                    };

                details.Add(new AnsweredTestDetail
                {
                    AnsweredTest = answer,
                    AnsweredTestId = answer.Id,
                    Choice = choice,
                    ChoiceId = choice.Id
                });
            }

            await _answeredTestRepository.CreateAsync(answer, cancellationToken);
            await _answeredTestDetailRepository.CreateRangeAsync(details, cancellationToken);

            var fetchAnalysis = await _analysisRepository.FetchAnalysisBasedOnScoreAsync(answerDto.TestId, userTotalScore);

            return new ResultDto<TestResultDto>
            {
                Status = CustomStatuses.Success,
                Parameter = new TestResultDto { TestResultDescription = fetchAnalysis?.Description }
            };
        }

    }
}
