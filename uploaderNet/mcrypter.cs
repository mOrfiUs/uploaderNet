using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace uploaderNet
{
    internal sealed class mcrypter
    {
        public class infoLink
        {
            public string name { get; set; }
            public string key { get; set; }
            public string size { get; set; }
            public string extra { get; set; }
            public string expire { get; set; }
            public string pass { get; set; }
        }

        private void finishWReq(IAsyncResult wRes, HttpWebRequest wReq, out string[] lOutLinks, bool bInfo = false)
        {
            string s = string.Empty;
            lOutLinks = null;
            using (HttpWebResponse wr = (HttpWebResponse)wReq.EndGetResponse(wRes))
                if (wr.StatusCode == HttpStatusCode.OK)
                    using (Stream st = wr.GetResponseStream())
                        s = new util().getStream(st, true);
            if (s.Contains("error"))
                return;
            if (bInfo == false)
            {
                Dictionary<string, string[]> r = new JavaScriptSerializer().Deserialize<Dictionary<string, string[]>>(s);
                if (r["links"] != null)
                    lOutLinks = r["links"];
            }
            else
            {
                Dictionary<string, string> r = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(s);
                infoLink il = new JavaScriptSerializer().Deserialize<infoLink>(s);
                IEnumerable<string> e = r.Values;
                lOutLinks = new string[r.Count];
                r.Values.CopyTo(lOutLinks, 0);
            }
        }

        public string[] getCryptLinks(string urlMCrypter, string[] links, bool getNames = false)
        {
            if (links == null)
                return null;
            if (links.Length == 0)
                return null;

            Dictionary<string, object> dictJson = new Dictionary<string, object>();
            dictJson.Add(@"""m""", @"""crypt""");
            //dictJson.Add(@"""hide_name""", @"""true""");
            dictJson.Add(@"""links""", new JavaScriptSerializer().Serialize(links));
            string referer = string.Empty;
            //referer = "http://www.yyy.com";
            if (!string.IsNullOrEmpty(referer))
                dictJson.Add(@"""referer""", @"""" + referer + @"""");
            string jsonPost = string.Empty;
            foreach (KeyValuePair<string, object> kvp in dictJson)
                jsonPost += kvp.Key + ":" + kvp.Value + ",";
            jsonPost = "{" + jsonPost.TrimEnd(',') + "}";

            Uri RequestUri = new Uri(urlMCrypter);
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(RequestUri);
            wReq.Timeout = 200000;
            wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            wReq.ContentType = "application/json";
            wReq.Method = "POST";
            wReq.UserAgent = new util().UserAgent;
            wReq.Accept = "*/*";

            byte[] b = Encoding.UTF8.GetBytes(jsonPost);
            wReq.ContentLength = b.Length;
            using (Stream dataStream = wReq.GetRequestStream())
                dataStream.Write(b, 0, b.Length);

            wReq.BeginGetResponse(wRes => { finishWReq(wRes, wReq, out links); }, null);//reuse links[]
            if (getNames)
                for (int i = 0; i < links.Length; i++)
                    links[i] += "||" + getInfoLink(urlMCrypter, links[i])[0];
            return links;
        }

        private string[] getInfoLink(string urlMCrypter, string link)
        {
            if (string.IsNullOrEmpty(link))
                return null;

            Dictionary<string, string> dictJson = new Dictionary<string, string>();
            dictJson.Add(@"""m""", @"""info""");
            dictJson.Add(@"""link""", @"""" + link + @"""");

            string jsonPost = string.Empty;
            foreach (var item in dictJson)
                jsonPost += item.Key + ":" + item.Value + ",";
            jsonPost = "{" + jsonPost.TrimEnd(',') + "}";

            Uri RequestUri = new Uri(urlMCrypter);
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(RequestUri);
            wReq.Timeout = 200000;
            wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            wReq.ContentType = "application/json";
            wReq.Method = "POST";
            wReq.UserAgent = new util().UserAgent;
            wReq.Accept = "*/*";

            byte[] b = Encoding.UTF8.GetBytes(jsonPost);
            wReq.ContentLength = b.Length;
            using (Stream dataStream = wReq.GetRequestStream())
                dataStream.Write(b, 0, b.Length);
            string[] lRelink = null;
            wReq.BeginGetResponse(wRes => { finishWReq(wRes, wReq, out lRelink, true); }, null);
            return lRelink;
        }
    }
}
