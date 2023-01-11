using ProxySuper.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace ProxySuper.Core.Models.Projects
{
    public class XraySettings : V2raySettings
    {
        public static List<string> FlowList = new List<string> { "xtls-rprx-vision", "xtls-rprx-vision-udp443", "xtls-rprx-origin", "xtls-rprx-origin-udp443", "xtls-rprx-direct", "xtls-rprx-direct-udp443", "xtls-rprx-splice", "xtls-rprx-splice-udp443" };
        public static List<string> UTLSList = new List<string> { "", "chrome", "firefox", "safari", "randomized" };
        public static List<string> XrayCoreVersionList = new List<string> { string.Empty };

        public string UTLS { get; set; } = UTLSList[1];

        public string Flow { get; set; } = FlowList[0];

        public string XrayCoreVersion { get; set; } = XrayCoreVersionList.FirstOrDefault();

        /// <summary>
        /// vless xtls shareLink
        /// </summary>
        public string VLESS_TCP_XTLS_ShareLink
        {
            get
            {
                return ShareLink.Build(RayType.VLESS_TCP_XTLS, this);
            }
        }

        public string GetXrayCoreVersion()
        {
            return string.IsNullOrEmpty(XrayCoreVersion)
                ? string.Empty
                : $" --version {XrayCoreVersion.Replace("[PRE]", string.Empty).Replace("v", string.Empty)}";
        }
    }
}
