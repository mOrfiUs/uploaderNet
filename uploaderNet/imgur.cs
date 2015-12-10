using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace uploaderNet
{
    class imgur
    {
        private bool uploadValues(WebClient w, string idImgur)
        {
            w.Headers.Add("Authorization", "Client-ID " + idImgur);
            return true;
        }

        public string imgurLink(string idImgur, string fLocal)
        {
            string sLink = string.Empty;
            string sHash = string.Empty;
            var values = new NameValueCollection {
            { "image", Convert.ToBase64String(File.ReadAllBytes(fLocal)) },
            { "title", Path.GetFileNameWithoutExtension(fLocal) }
            };
            
            using (var w = new WebClient())
                if (uploadValues(w, idImgur))
                    using (StreamReader sr = new StreamReader(new MemoryStream(w.UploadValues(new Uri("https://api.imgur.com/3/upload.xml"), values))))
                        sLink = sr.ReadToEnd();
            if (!string.IsNullOrEmpty(sLink))
            {
                sHash = new Regex(@"<deletehash>(.*?)</deletehash>", RegexOptions.Multiline).Match(sLink).Groups[1].Value.Trim();
                sLink = new Regex(@"<link>(.*?)</link>", RegexOptions.Multiline).Match(sLink).Groups[1].Value.Trim();
            }
            return sLink + "#http://imgur.com/delete/" + sHash;
        }
    }
}
