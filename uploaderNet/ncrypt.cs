using System;
using System.IO;
using System.Net;
using System.Text;

namespace uploaderNet
{
    internal sealed class ncrypt
    {
        private void finishWReq(IAsyncResult wRes, HttpWebRequest wReq, out string lNCrypt)
        {
            lNCrypt = string.Empty;
            using (HttpWebResponse wr = (HttpWebResponse)wReq.EndGetResponse(wRes))
                if (wr.StatusCode == HttpStatusCode.OK)
                    using (Stream st = wr.GetResponseStream())
                        lNCrypt = new util().getStream(st, false);

            if (!lNCrypt.StartsWith("http://ncrypt.in/"))
            {
                lNCrypt = string.Empty;
                throw new ArgumentException(lNCrypt.StartsWith("encrypt_limit") ? "límte alcanzado espera un minuto" : "error generando link", "nCrypt");
            }
        }
        
        /// <summary>
        /// Get ncrypt link from any link(s)
        /// </summary>
        /// <param name="idNCrypt">API id</param>
        /// <param name="title">Title</param>
        /// <param name="links">link(s) separated by Environment.NewLine</param>
        /// <returns>Link to new ncrypt folder, and other data for edit</returns>
        public string ncryptLink(string idNCrypt, string title, string links)
        {
            string s = "auth_code=" + idNCrypt;

            s += "&links=" + links;
            s += "&foldername=" + title;
            s += "&show_container=1";
            s += "&show_links=0";
            s += "&ccf=0";
            s += "&rsdf=0";
            s += "&dlc=0";
            s += "&cnl=1";
            s += "&show_views=0";
            s += "&captcha=2";

            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(new Uri("http://ncrypt.in/api.php"));

            wReq.Timeout = 200000;
            wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.Method = "POST";
            wReq.UserAgent = new util().UserAgent;
            wReq.Accept = "*/*";
            byte[] b = Encoding.UTF8.GetBytes(s);
            wReq.ContentLength = b.Length;

            using (Stream ds = wReq.GetRequestStream())
                ds.Write(b, 0, b.Length);
            string lNCrypt = string.Empty;
            wReq.BeginGetResponse(wRes => { finishWReq(wRes, wReq, out lNCrypt); }, null);
            return lNCrypt;
        }
    }
}
