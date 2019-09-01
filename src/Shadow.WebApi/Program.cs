using Microsoft.AspNetCore;
//using Steeltoe.Extensions.Configuration.CloudFoundry;

using Microsoft.AspNetCore.Hosting;

namespace Shadow.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseCloudFoundryHosting(5000)
                //.AddCloudFoundry()
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000");
    }
}
