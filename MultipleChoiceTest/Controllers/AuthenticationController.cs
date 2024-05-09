using Contracts.DTOs;
using Contracts.DTOs.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationServices authenticationServices, 
                                        ILogger<AuthenticationController> logger,
                                        IConfiguration configuration)
        {
            _authenticationServices = authenticationServices;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authenticationServices.RegisterAsync(registerDto, UserRolesDto.User);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAdmin(RegisterDto registerDto)
        {
            var result = await _authenticationServices.RegisterAsync(registerDto, UserRolesDto.Admin);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var jwtDto = new JWTDto
            {
                Secret = _configuration["JWT:Secret"]!,
                Issuer = _configuration["JWT:ValidIssuer"]!,
                Audience = _configuration["JWT:ValidAudience"]!
            };

            var result = await _authenticationServices.LoginAsync(loginDto, jwtDto);
            return Ok(result);
        }

    }
}
