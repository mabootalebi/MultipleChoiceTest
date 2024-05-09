using Contracts.DTOs.User;
using Contracts.DTOs;
using Microsoft.AspNetCore.Identity;


namespace Services.Interfaces
{
    public interface IUserServices
    {
        ResultDto<List<FetchDto>> FetchList();
        Task<ResultDto<FetchDto>> FetchByIdAsync(string id);
        Task<ResultDto<List<IdentityError>>> UpdateUserInfoAsync(string id, UpdateDto updateDto);
    }
}
