using System;
using System.Net;
using System.IO;
using System.Text;

namespace uploaderNet
{
    internal sealed class relink
    {
        private void finishWReq(IAsyncResult wRes, HttpWebRequest wReq, out string lRelink)
        {
            string s = string.Empty;
            lRelink = string.Empty;
            using (HttpWebResponse wr = (HttpWebResponse)wReq.EndGetResponse(wRes))
                if (wr.StatusCode == HttpStatusCode.OK)
                    using (Stream st = wr.GetResponseStream())
                        s = new util().getStream(st, true);
            if (s.StartsWith("1 -"))
                lRelink = s.Substring(4);
        }

        public string relinkLink(string idRelink, string title, string links)
        {
            string s = "api=" + idRelink;

            s += "&url=" + links;
            s += "&title=" + title;
            s += "&web=no";
            s += "&dlc=no";
            s += "&cnl=yes";
            s += "&captcha=yes";
            //s += "&comment=";

            Uri RequestUri = new Uri("http://api.relink.us/api.php");
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(RequestUri);

            wReq.Timeout = 200000;
            wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.Method = "POST";
            wReq.UserAgent = new util().UserAgent;
            wReq.Accept = "*/*";
            byte[] b = Encoding.UTF8.GetBytes(s);
            wReq.ContentLength = b.Length;

            using (Stream dataStream = wReq.GetRequestStream())
                dataStream.Write(b, 0, b.Length);
            string lRelink = string.Empty;
            wReq.BeginGetResponse(wRes => { finishWReq(wRes, wReq, out lRelink); }, null);
            return lRelink;
        }

    }
}
