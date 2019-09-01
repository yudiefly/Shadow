using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shadow.Tool.Http;
using Shadow.Tool.Http.Filters;
using Shadow.Tool.Web;
using System.ComponentModel.DataAnnotations;

namespace Shadow.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValueController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ValueController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<ValueController>();
        }

        public IActionResult Get()
        {
            //_nlogger.Info("shadow test.");
            var result = new
            {
                Request.HttpContext.TraceIdentifier,
                HttpContextGlobal.CurrentTraceId,
                RequestUrl = $"{ HttpContextGlobal.Current.Request.Path}{HttpContextGlobal.Current.Request.QueryString}"
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("cmd")]
        public IActionResult CmdConfing(string key)
        {
            return Ok(_configuration[key]);
        }

        [HttpPost]
        public IActionResult Post(string arg)
        {
            var result = new
            {
                Request.HttpContext.TraceIdentifier,
                HttpContextGlobal.CurrentTraceId,
                RequestUrl = $"{ HttpContextGlobal.Current.Request.Path}{HttpContextGlobal.Current.Request.QueryString}"
            };
            _logger.LogInformation(arg);

            return Ok(result);
        }

        [ValidateRequestModel]
        [Route("requestmodel")]
        [HttpPost]
        public IActionResult PostRquestModel([FromBody]RequsetModel<RequsetModelTest> model)
        {
            return Ok(model);
        }

        [ValidateRequestModel]
        [Route("requestmodel2")]
        [HttpPost]
        public IActionResult PostRquestModel()
        {
            return Ok();
        }
    }

    public class RequsetModelTest
    {
        [Required]
        public string Name { get; set; }
    }
}

