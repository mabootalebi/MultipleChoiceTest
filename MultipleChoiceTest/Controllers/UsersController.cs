using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
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
        //[Authorize(Roles = UserRolesDto.Admin)]
        public ActionResult FetchList()
        {
            var result = _userServices.FetchList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        
        //[Authorize]
        public async Task<ActionResult> FetchById(string id)
        {
            var result = await _userServices.FetchByIdAsync(id);
            return Ok(result);
        }

    }
}
