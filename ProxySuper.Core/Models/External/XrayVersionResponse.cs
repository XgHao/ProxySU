using Newtonsoft.Json;

namespace ProxySuper.Core.Models.External
{
    public class XrayVersionResponse
    {
        [JsonProperty("prerelease")]
        public bool IsPreRelease { get; set; }

        [JsonProperty("tag_name")]
        public string Version { get; set; }
    }
}