using Contracts.DTOs.Test;
using Contracts.DTOs;
using Contracts.DTOs.Test.Answer;


namespace Services.Interfaces
{
    public interface IAnswerTestServices
    {
        Task<ResultDto<FetchTestDto>> FetchTestQuestionsAsync(int testId, CancellationToken cancellationToken = default);
        Task<ResultDto<TestResultDto>> SaveUserAnswers(string username, CreateAnswerDto answerDto, CancellationToken cancellationToken = default);
    }
}
