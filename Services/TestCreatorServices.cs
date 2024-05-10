using Contracts.DTOs;
using Contracts.DTOs.Test;
using Contracts.DTOs.Test.Analysis;
using Contracts.DTOs.Test.Question;
using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;


namespace Services
{
    public class TestCreatorServices : ITestCreatorServices
    {
        private readonly ILogger<TestCreatorServices> _logger;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IChoiceRepository _choiceRepository;
        private readonly IAnalysisRepository _analysisRepository;

        public TestCreatorServices(ILogger<TestCreatorServices> logger, 
                                   ITestRepository testRepository,
                                   IQuestionRepository questionRepository,
                                   IChoiceRepository choiceRepository,
                                   IAnalysisRepository analysisRepository)
        {
            _logger = logger;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
            _analysisRepository = analysisRepository;
        }

        public async Task<ResultDto<FetchTestDto>> CreateTestAsync(CreateTestDto dto, CancellationToken cancellationToken = default)
        {
            var test = new Test
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _testRepository.CreateAsync(test, cancellationToken);
            return new ResultDto<FetchTestDto>
            {
                Status = CustomStatuses.Success,
                Parameter = MakeFetchTestDtoInstance(test)
            };
        }

        private FetchTestDto MakeFetchTestDtoInstance(Test test) =>
            new FetchTestDto
            {
                Name = test.Name,
                Description = test.Description,
                Id = test.Id
            };

        public async Task<ResultDto<FetchTestDto>> UpdateTestAsync(int id, CreateTestDto dto, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(id);
            if (test is null)
                return new ResultDto<FetchTestDto>
                {
                    Status = CustomStatuses.TestNotFound
                };

            await _testRepository.UpdateAsync(test, dto, cancellationToken);

            return new ResultDto<FetchTestDto>
            {
                Status = CustomStatuses.Success,
                Parameter = MakeFetchTestDtoInstance(test)
            };
        }

        public async Task<ResultDto<List<FetchTestDto>>> FetchTestListAsync(CancellationToken cancellationToken = default)
        {
            var list = (await _testRepository.FetchTestListAsync())
                .Select(t => new FetchTestDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToList();

            return new ResultDto<List<FetchTestDto>>
            {
                Status = CustomStatuses.Success,
                Parameter = list
            };
        }

        public async Task<ResultDto> CreateTestQuestionAsync(int testId, CreateQuestionDto dto, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(testId, cancellationToken);
            if (test is null)
                return new ResultDto
                {
                    Status = CustomStatuses.TestNotFound
                };

            var question = new Question
            {
                Description = dto.Description,
                Order = dto.Order,
                Test = test,
                TestId = test.Id
            };

            var choices = dto.Choices?.Select(t => new Choice
            {
                Order = t.Order,
                Title = t.Title,
                Score = t.Score,
                Question = question,
                QuestionId = question.Id
            }).ToList();

            await _questionRepository.CreateAsync(question, cancellationToken);
            await _choiceRepository.CreateRangeAsync(choices, cancellationToken);

            return new ResultDto
            {
                Status = CustomStatuses.Success,
            };
        }

        //public async Task<ResultDto> UpdateTestQuestionAsync(int testId, long questionId, CreateQuestionDto dto, CancellationToken cancellationToken = default)
        //{
        //    var test = await _testRepository.FetchByIdAsync(testId, cancellationToken);
        //    if (test is null)
        //        return new ResultDto
        //        {
        //            Status = CustomStatuses.TestNotFound
        //        };

        //    var question = await _questionRepository.FindById(questionId, cancellationToken);
        //    if (question is null)
        //        return new ResultDto
        //        {
        //            Status = CustomStatuses.QuestionNotFound
        //        };

        //    var question = new Question
        //    {
        //        Description = dto.Description,
        //        Order = dto.Order,
        //        Test = test,
        //        TestId = test.Id
        //    };

        //    var choices = dto.Choices?.Select(t => new Choice
        //    {
        //        Order = t.Order,
        //        Title = t.Title,
        //        Score = t.Score,
        //        Question = question,
        //        QuestionId = question.Id
        //    }).ToList();

        //    await _questionRepository.CreateAsync(question, cancellationToken);
        //    await _choiceRepository.CreateRangeAsync(choices, cancellationToken);

        //    return new ResultDto
        //    {
        //        Status = CustomStatuses.Success,
        //    };
        //}


        public async Task<ResultDto> CreateTestAnalysisAsync(int testId, CreateAnalysisDto dto, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(testId, cancellationToken);
            if (test is null)
                return new ResultDto
                {
                    Status = CustomStatuses.TestNotFound
                };

            var analysis = new Analysis
            {
                Description = dto.Description,
                MaxScore = dto.MaxScore,
                MinScore = dto.MinScore,
                Test = test,
                TestId = test.Id
            };

            await _analysisRepository.CreateAsync(analysis, cancellationToken);

            return new ResultDto
            {
                Status = CustomStatuses.Success,
            };
        }
    }
}
