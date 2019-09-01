using Microsoft.AspNetCore.Mvc;
using Shadow.Infrastructure.Runtime.Caching;

namespace Shadow.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : Controller
    {
        private readonly ICache _cache;

        public RedisController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        [Route("conn")]
        public IActionResult Connect()
        {
            return Ok("conn");
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            var value = _cache.GetOrDefault<string>(key);
            return Ok(value ?? "not value.");
        }
    }
}
