using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System;

namespace ProxySuper.Core.Helpers
{
    public class HttpClientUtil
    {
        /// <summary>
        /// ProxyServer Environment Var.
        /// </summary>
        public const string ProxyServerVar = "DOTNET_PROXY_SERVER";

        /// <summary>
        /// 创建HttpClient.
        /// </summary>
        /// <param name="timeoutMilliseconds">timeout milliseconds.</param>
        /// <returns><see cref="HttpClient"/>.</returns>
        public static HttpClient CreateHttpClient(double? timeoutMilliseconds = null)
        {
            var defaultHttpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,

                // allow concurrent request
                MaxConnectionsPerServer = 2000,

                // allow all ssl cert
                ServerCertificateCustomValidationCallback = (message, certificate2, sslPolicyError, arg4) => true,
                UseCookies = false,
                UseProxy = false,
                Proxy = null,
            };

            var proxyServer = Environment.GetEnvironmentVariable(ProxyServerVar);
            if (proxyServer != null)
            {
                try
                {
                    var proxyUri = new Uri(proxyServer);

                    // has proxy server
                    defaultHttpClientHandler.UseProxy = true;
                    defaultHttpClientHandler.Proxy = new WebProxy(proxyUri);
                }
                catch (UriFormatException)
                {
                    // ignore
                }
            }

            var httpClient = new HttpClient(defaultHttpClientHandler);

            if (timeoutMilliseconds.HasValue)
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds.Value);
            }

            httpClient.DefaultRequestHeaders.ConnectionClose = false;
            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            httpClient.MaxResponseContentBufferSize = 1024 * 1024 * 1024;

            return httpClient;
        }
    }
}