using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shadow.Infrastructure.Extensions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shadow.Tool.Http
{
    public class TraceDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var loggerFactory = HttpContextGlobal.Current.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<TraceDelegatingHandler>();

            // add TraceId to header.
            request.Headers.TryAddWithoutValidation(Constants.RESTfulTraceId, HttpContextGlobal.CurrentTraceId);

            // think: how to exclude the file content， like MultipartFormDataContent
            var requestContent = await request.Content.ReadAsStringAsync();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            HttpResponseMessage response;
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, new { Request = requestContent }.ToJson());
                throw;
            }
            finally
            {
                watch.Stop();
            }

            // 响应
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                logger.LogInformation(new { ElapsedTime = watch.ElapsedMilliseconds, Request = requestContent, Responese = responseContent }.ToJson());
            }
            else
            {
                var responseContent = await response.Content?.ReadAsStringAsync();
                logger.LogWarning(new { ElapsedTime = watch.ElapsedMilliseconds, StatusCode = (int)response.StatusCode, Request = requestContent, Responese = responseContent }.ToJson());
            }

            return response;
        }
    }
}
