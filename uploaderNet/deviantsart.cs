using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;


namespace uploaderNet
{
    internal sealed class deviantsart
    {
        public string deviantsartLink(string fLocal)
        {
            string sLink = string.Empty;
            using (var w = new WebClient())
                using (StreamReader sr = new StreamReader(new MemoryStream(w.UploadFile(new Uri("http://deviantsart.com"), "POST", fLocal))))
                    sLink = sr.ReadToEnd();
            Dictionary<string, string> jUpGo4up = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(sLink);
            sLink = jUpGo4up["url"];
            return sLink;
        }
    }
}