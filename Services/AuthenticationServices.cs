using Contracts.DTOs;
using Contracts.DTOs.Authentication;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthenticationServices> _logger;

        public AuthenticationServices(UserManager<User> userManager,
                                      RoleManager<IdentityRole> roleManager,
                                      SignInManager<User> signInManager,
                                      ILogger<AuthenticationServices> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<ResultDto<List<IdentityError>>> RegisterAsync(RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new ResultDto<List<IdentityError>> { Status = CustomStatuses.CannotRegisterUserDueToUserWithSameUserNameAlreadyExists };

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

                return new ResultDto<List<IdentityError>>
                {
                    Status = CustomStatuses.ErrorOccuredWhileRegisteringUser,
                    Parameter = result.Errors.ToList()
                };
            }

            return new ResultDto<List<IdentityError>> { Status = CustomStatuses.Success };
        }

        public async Task<ResultDto<LoginResultDto>> LoginAsync(LoginDto loginDto, JWTDto jWTDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null)
                return new ResultDto<LoginResultDto>
                {
                    Status = CustomStatuses.IncorrectUsernameOrPassword
                };

            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
                return new ResultDto<LoginResultDto>
                {
                    Status = CustomStatuses.IncorrectUsernameOrPassword
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTDto.Secret));

            var token = new JwtSecurityToken(
                issuer: jWTDto.Issuer,
                audience: jWTDto.Audience,
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new ResultDto<LoginResultDto>
            {
                Status = CustomStatuses.Success,
                Parameter = new LoginResultDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                }
            };
        }

    }
}
