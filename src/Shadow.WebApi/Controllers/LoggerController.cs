using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shadow.Tool.Http.Filters;

namespace Shadow.WebApi.Controllers
{
    [NoLog]
    [Route("api/[controller]")]
    public class LoggerController : Controller
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        [Route("log")]
        [HttpGet]
        public IActionResult Log()
        {
            _logger.LogInformation("use the kafka logger.");

            return Ok("log");
        }

        [Route("log2")]
        [HttpGet]
        public IActionResult Log2(int id)
        {
            _logger.LogInformation("use the kafka logger2.");

            return Ok("log2 -- " + id);
        }

        [Route("log3")]
        [HttpGet]
        public IActionResult Log3()
        {
            throw new System.InvalidOperationException("log3");
        }
    }
}
