using Contracts.DTOs.Authentication;
using Contracts.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserServices _userServices;
        private readonly ILogger<AuthenticationController> _logger;

        public UsersController(IUserServices userServices, ILogger<AuthenticationController> logger)
        {
            _userServices = userServices;
            _logger = logger;            
        }

        [HttpGet]
        [Authorize(Roles = UserRolesDto.Admin)]
        public ActionResult FetchList()
        {
            var result = _userServices.FetchList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FetchById(string id)
        {
            var result = await _userServices.FetchByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdateDto updateDto)
        {
            var result = await _userServices.UpdateUserInfoAsync(id, updateDto);
            return Ok(result);
        }
    }
}
