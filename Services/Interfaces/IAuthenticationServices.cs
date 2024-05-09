using Contracts.DTOs.Authentication;
using Contracts.DTOs;
using Microsoft.AspNetCore.Identity;


namespace Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<ResultDto<List<IdentityError>>> RegisterAsync(RegisterDto model, string role);
        Task<ResultDto<LoginResultDto>> LoginAsync(LoginDto loginDto, JWTDto jWTDto);
    }
}
