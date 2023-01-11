using System;
using Newtonsoft.Json;
using ProxySuper.Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProxySuper.Core.Models.External;
using ProxySuper.Core.Models.Projects;

namespace ProxySuper.Core.Extension
{
    public static class XrayExtension
    {
        private static readonly HttpClient _httpClient;

        static XrayExtension()
        {
            _httpClient = HttpClientUtil.CreateHttpClient(5000);
        }

        public static async Task InitXrayCoreVersionListAsync()
        {
            var httpRequestMessage =
                new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/XTLS/Xray-core/releases");
            httpRequestMessage.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
            httpRequestMessage.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
            httpRequestMessage.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.42");

            try
            {
                var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                XraySettings.XrayCoreVersionList = JsonConvert
                    .DeserializeObject<List<XrayVersionResponse>>(await response.Content.ReadAsStringAsync())
                    ?.Select(e => $"{e.Version} {(e.IsPreRelease ? "[PRE]" : string.Empty)}")
                    .ToList();
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}