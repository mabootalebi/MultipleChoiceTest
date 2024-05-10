using Contracts.DTOs.Authentication;
using Contracts.DTOs.Test;
using Contracts.DTOs.Test.Analysis;
using Contracts.DTOs.Test.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
        [Route("Create")]
        public async Task<IActionResult> Create(CreateTestDto createDto)
        {
            var result = await _testCreatorServices.CreateTestAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, CreateTestDto dto)
        {
            var result = await _testCreatorServices.UpdateTestAsync(id, dto);
            return Ok(result);
        }

        [HttpGet]
        [Route("FetchTestList")]
        public async Task<IActionResult> FetchTestList()
        {
            var result = await _testCreatorServices.FetchTestListAsync();
            return Ok(result);
        }


        [HttpPost]
        [Route("{testId}/CreateQuestion")]
        public async Task<IActionResult> CreateQuestion(int testId, CreateQuestionDto dto)
        {
            var result = await _testCreatorServices.CreateTestQuestionAsync(testId, dto);
            return Ok(result);
        }

        //[HttpPost]
        //[Route("{testId}/UpdateQuestion/{questionId}")]
        //public async Task<IActionResult> UpdateQuestion(int testId, long questionId, CreateQuestionDto dto)
        //{
        //    var result = await _testCreatorServices.UpdateTestQuestionAsync(testId, questionId, dto);
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("{testId}/CreateAnalysis")]
        public async Task<IActionResult> CreateAnalysis(int testId, CreateAnalysisDto dto)
        {
            var result = await _testCreatorServices.CreateTestAnalysisAsync(testId, dto);
            return Ok(result);
        }

    }
}
