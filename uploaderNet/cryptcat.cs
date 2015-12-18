using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace uploaderNet
{
    class cryptcat
    {

        public string cryptcatLink(KeyValuePair<string, string> linksPass)
        {
            string s = new WebClient().DownloadString(new Uri("https://crypt.cat/api.php?" + "link=" + linksPass.Key + (!string.IsNullOrEmpty(linksPass.Value) ? "&passwd=" + linksPass.Value : string.Empty)));
            return ((!string.IsNullOrEmpty(s)) && (s.StartsWith("https://crypt.cat/"))) ? s : string.Empty;
        }

        public string cryptcatLink(string[] arrS, string pass = "")
        {
            string link = "link=" + string.Join("|", arrS).TrimEnd('|') + (!string.IsNullOrEmpty(pass) ? "&passwd=" + pass : string.Empty);
            string s = new WebClient().DownloadString(new Uri("https://crypt.cat/api.php?" + link));
            return (!string.IsNullOrEmpty(s)) && (s.StartsWith("https://crypt.cat/")) ? s : string.Empty;
        }
    }
}
