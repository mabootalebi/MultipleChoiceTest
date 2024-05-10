using Contracts.DTOs;
using Contracts.DTOs.Test;
using Contracts.DTOs.Test.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using System.Security.Claims;

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
            var result = await _answerTestServices.FetchTestQuestionsAsync(testId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserAnswers(CreateAnswerDto dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
                return Ok(new ResultDto { Status = CustomStatuses.UserWithGivenUsernameNotFound });

            IEnumerable<Claim> claims = identity.Claims;
            var username = claims.Where(t => t.Type.Contains("name")).Select(t => t.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(username))
                return Ok(new ResultDto { Status = CustomStatuses.UserWithGivenUsernameNotFound });

            var result = await _answerTestServices.SaveUserAnswers(username, dto);
            return Ok(result);
        }
    }
}
