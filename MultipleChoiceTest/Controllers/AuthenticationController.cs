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

        public AuthenticationController(IAuthenticationServices authenticationServices, ILogger<AuthenticationController> logger)
        {
            _authenticationServices = authenticationServices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authenticationServices.RegisterAsync(registerDto);
            return Ok(result);
        } 
    }
}
