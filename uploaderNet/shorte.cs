using System;
using System.Net;

namespace uploaderNet
{
    internal sealed class shorte
    {
        public string shorteLink(string idShorte, string link)
        {
            string s = new WebClient().DownloadString(new Uri("http://api.shorte.st/s/" + idShorte + "/" + link));
            if (!string.IsNullOrEmpty(s))
                if (s.Contains("status\":\"ok\""))
                    s = "http://sh.st/" + s.Substring(s.Length - 7, 5);
            return s;
        }
    }
}
