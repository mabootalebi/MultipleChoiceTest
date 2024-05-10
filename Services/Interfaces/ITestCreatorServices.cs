using Contracts.DTOs.Test;
using Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITestCreatorServices
    {
        Task<ResultDto<FetchTestDto>> CreateTestAsync(CreateTestDto dto, CancellationToken cancellationToken = default);
        Task<ResultDto<FetchTestDto>> UpdateTestAsync(int id, CreateTestDto dto, CancellationToken cancellationToken = default);
        Task<ResultDto<List<FetchTestDto>>> FetchTestListAsync(CancellationToken cancellationToken = default);
    }
}
