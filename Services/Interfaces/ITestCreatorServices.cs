using Contracts.DTOs.Test;
using Contracts.DTOs;
using Contracts.DTOs.Test.Question;
using Contracts.DTOs.Test.Analysis;

namespace Services.Interfaces
{
    public interface ITestCreatorServices
    {
        Task<ResultDto<FetchTestDto>> CreateTestAsync(CreateTestDto dto, CancellationToken cancellationToken = default);
        Task<ResultDto<FetchTestDto>> UpdateTestAsync(int id, CreateTestDto dto, CancellationToken cancellationToken = default);
        Task<ResultDto<List<FetchTestDto>>> FetchTestListAsync(CancellationToken cancellationToken = default);

        Task<ResultDto> CreateTestQuestionAsync(int testId, CreateQuestionDto dto, CancellationToken cancellationToken = default);
        Task<ResultDto> CreateTestAnalysisAsync(int testId, CreateAnalysisDto dto, CancellationToken cancellationToken = default);
    }
}
