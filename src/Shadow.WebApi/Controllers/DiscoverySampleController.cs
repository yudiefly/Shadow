using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Shadow.WebApi.Controllers
{
    /// <summary>
    /// 服务发现使用示例
    /// </summary>
    public class DiscoverySampleController : Controller
    {
        private readonly HttpClient _httpClient;
        
        public DiscoverySampleController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
