using Contracts.DTOs.Authentication;
using Contracts.DTOs;


namespace Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<ResultDto<string>> RegisterAsync(RegisterDto model);
    }
}
