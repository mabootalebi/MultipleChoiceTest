using Contracts.DTOs.Authentication;
using Contracts.DTOs.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = UserRolesDto.Admin)]
    [ApiController]
    public class TestCreatorController : ControllerBase
    {
        private readonly ITestCreatorServices _testCreatorServices;
        private readonly ILogger<TestCreatorController> _logger;

        public TestCreatorController(ITestCreatorServices testCreatorServices,
                                     ILogger<TestCreatorController> logger)
        {
            _testCreatorServices = testCreatorServices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDto createDto)
        {
            var result = await _testCreatorServices.CreateTestAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, CreateTestDto dto)
        {
            var result = await _testCreatorServices.UpdateTestAsync(id, dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchTestList()
        {
            var result = await _testCreatorServices.FetchTestListAsync();
            return Ok(result);
        }

    }
}
