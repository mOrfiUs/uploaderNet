using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace uploaderNet
{
    internal sealed class binbox
    {
        public string binboxLink(string idBinbox, string title, string links)
        {
            string sLink = string.Empty;
            string[] dataPass = dataJson(links);
            if (dataPass.Length != 3)
                return sLink;
            string data = dataPass[0];
            string pass = dataPass[1];

            var values = new NameValueCollection
            {
                { "data", data },
                { "title", title },
                { "folder", "12" }
            };

            using (var w = new WebClient())
                using (StreamReader sr = new StreamReader(new MemoryStream(w.UploadValues(new Uri("http://" + idBinbox + ".binbox.io/submit.json"), values))))
                    sLink = sr.ReadToEnd();

            Dictionary<string, string> idsResult = new Dictionary<string, string>();
            Dictionary<string, string> jUpGo4up = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(sLink);

            if ((jUpGo4up["ok"] == null) || (jUpGo4up["id"] == null))
                if (jUpGo4up["ok"].ToString() != "true")
                    Debug.WriteLine(pass);
            sLink = jUpGo4up["id"].ToString() + "#" + pass;
            
            //finally save the pass in "binboxPass.txt"
            string fileBinboxPass = Path.Combine(Application.StartupPath, "binboxPass.txt");
            string sBinboxPass = string.Empty;
            if (File.Exists(fileBinboxPass))
                sBinboxPass = File.ReadAllText(fileBinboxPass, Encoding.Default);
            if (!string.IsNullOrEmpty(sLink))
                File.WriteAllText(fileBinboxPass, sLink + Environment.NewLine + sBinboxPass, Encoding.Default);
            return sLink;
        }

        private string[] dataJson(string links)
        {
            string[] dJson = new string[] { string.Empty, string.Empty };            
            var engine = new Jurassic.ScriptEngine();

            // Export the print method to the JavaScript world, just debug
            engine.SetGlobalFunction("print", new Action<object>((o) =>
            {
                Debug.WriteLine(o == null ? "null" : o.ToString());
            }));

            // Export binbox api to the JavaScript world
            engine.SetGlobalFunction("__encrypt", new Func<string, string, bool>((data64, pass) =>
            {
                string jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(data64));
                dJson = new string[] { data64, pass, jsonData };
                return true;
            }));

            var jsSource = new StringBuilder(32000);
            var a = Assembly.GetExecutingAssembly();
            //se crea un jsSource.js virtual que contiene todos los resources .js, salvo los que empiezan por guión bajo
            foreach (string rName in a.GetManifestResourceNames())
                if(rName.EndsWith(".js") && !rName.Contains("_"))
                    using (Stream st = a.GetManifestResourceStream(rName))
                        using (StreamReader rd = new StreamReader(st))
                            jsSource.Append(rd.ReadToEnd()).AppendLine();

            jsSource.Append(@"bbMain();");
            //String literals can't contain newlines in javascript. So pass GlobalValue
            engine.SetGlobalValue("textWithCrLf", links);

            engine.SetGlobalValue("ExitCode", 0);

            try
            {
                engine.Execute(jsSource.ToString());
            }
            catch(Jurassic.JavaScriptException ex)
            {
                string sOut = string.Empty;
                new util().modernInputBox(null, "Go4Up devolvió el error " + ex.Message, sOut, SystemIcons.Error, out sOut, true);
                Debug.WriteLine("Error:{0}, Line:{1}\r\n{2}", ex.Message, ex.LineNumber, ex.Source);
            }
            return dJson;
        }
    }
}
