using Contracts.DTOs.Test;
using Contracts.DTOs;


namespace Services.Interfaces
{
    public interface IAnswerTestServices
    {
        Task<ResultDto<FetchTestDto>> FetchTestQuestions(int testId, CancellationToken cancellationToken = default);
    }
}
