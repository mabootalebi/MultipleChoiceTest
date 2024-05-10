using Contracts.DTOs.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AnswerTestController : ControllerBase
    {
        private readonly IAnswerTestServices _answerTestServices;
        private readonly ILogger<AnswerTestController> _logger;

        public AnswerTestController(IAnswerTestServices answerTestServices,
                                    ILogger<AnswerTestController> logger)
        {
            _answerTestServices = answerTestServices;
            _logger = logger;
        }

        [HttpGet]
        [Route("{testId}")]
        public async Task<IActionResult> FetchTestQuestionsList(int testId)
        {
            var result = await _answerTestServices.FetchTestQuestions(testId);
            return Ok(result);
        }
    }
}
