using Contracts.DTOs.User;
using Contracts.DTOs;


namespace Services.Interfaces
{
    public interface IUserServices
    {
        ResultDto<List<FetchDto>> FetchList();
        Task<ResultDto<FetchDto>> FetchByIdAsync(string id);
    }
}
