using Shadow.Infrastructure.Extensions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shadow.Tool.Http
{
    /// <summary>
    /// Http 请求 Client
    /// </summary>
    public class HttpRequestClient
    {
        public HttpClient Client { get; }

        public HttpRequestClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.GetAsync(url, cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.PostAsync(url, content, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return Client.SendAsync(request);
        }

        public async Task<Response<T>> GetAsync<T>(string url)
        {
            try
            {
                var result = await Client.GetStringAsync(url);
                return new Response<T>(result.ToObject<T>());
            }
            catch (HttpRequestException ex)
            {
                return new Response<T>(ex);
            }
        }

        public async Task<Response<string>> GetStringAsync(string url)
        {
            try
            {
                var result = await Client.GetStringAsync(url);
                return new Response<string>(result);
            }
            catch (HttpRequestException ex)
            {
                return new Response<string>(ex);
            }
        }

        public async Task<Response<T>> PostAsync<T>(string url, HttpContent content)
        {
            try
            {
                var response = await Client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content?.ReadAsStringAsync();
                return new Response<T>(result.AsObject<T>());
            }
            catch (HttpRequestException ex)
            {
                return new Response<T>(ex);
            }
        }

        public async Task<Response<string>> PostStringAsync(string url, HttpContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await Client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content?.ReadAsStringAsync();
                return new Response<string>(result);
            }
            catch (HttpRequestException ex)
            {
                return new Response<string>(ex);
            }
        }
    }
}
