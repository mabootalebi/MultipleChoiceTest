using Contracts.DTOs;
using Contracts.DTOs.Authentication;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Text.Json;


namespace Services
{
    public class AuthenticationServices: IAuthenticationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationServices> _logger;

        public AuthenticationServices(UserManager<User> userManager, 
                                      RoleManager<IdentityRole> roleManager,
                                      ILogger<AuthenticationServices> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResultDto<string>> RegisterAsync(RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new ResultDto<string> { Status = CustomStatuses.CannotRegisterUserDueToUserWithSameUserNameAlreadyExists };

            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalCode = model.NationalCode
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var userDtoSerialized = JsonSerializer.Serialize(model);
                var errors = string.Join(" / ", result.Errors.Select(t => $"code: {t.Code}, description: {t.Description}"));
                _logger.LogError($"Error Occured while registering user. Entered Data: {userDtoSerialized}. ErrorsList: {errors}");

                return new ResultDto<string> 
                { 
                    Status = CustomStatuses.ErrorOccuredWhileRegisteringUser,
                    Parameter = errors
                };
            }

            return new ResultDto<string> { Status = CustomStatuses.Success };
        }
    }
}
