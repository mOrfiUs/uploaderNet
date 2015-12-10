using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PicloadNet
{
    #region Interfaces
    /// <summary>
    /// el interface permite que la función subsecuente implementada sea accesible desde COM
    /// </summary>
    /// 

    //[ComVisible(false)]
    public interface IFunctions
    {
        [Description("obtiene el enlace de la imagen subida fLocal")]
        string picloadLink(string idPicload, string hashPicload, string fLocal);
    }
    #endregion interfaces

    [ClassInterface(ClassInterfaceType.None)]
    [Description("Funciones para subir una imagen a Picload")]
    public class picload : IFunctions, IDisposable
    {
        [ComVisible(false)]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~picload()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public picload()
        {

        }

        internal string MD5File(string sPath)
        {
            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(File.ReadAllBytes(sPath)));
        }

        public string picloadLink(string idPicload, string hashPicload, string fLocal)
        {
            string sLink = string.Empty;
            string fNameLocal = Path.GetFileName(fLocal);
            var values = new NameValueCollection {
            { "user", idPicload },
            { "hash", hashPicload },
            { "real", "1"},
            //{ "check", "1"},
            //{ "folderid", ""},
            { "imagename", fNameLocal },
            { "imagedata", Convert.ToBase64String(File.ReadAllBytes(fLocal)) }
            };

            using (var w = new WebClient())//"http://api.picload.org/json/api.test"
            using (StreamReader sr = new StreamReader(new MemoryStream(w.UploadValues(new Uri("http://api.picload.org/json/images.upload"), values))))
                sLink = sr.ReadToEnd();
            Dictionary<string, object> jPicload = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(sLink);
            if (jPicload.Count == 0)
                return null;
            if (jPicload["ret_code"].ToString() != "200")
                throw new ArgumentException("Return Code Fail!");

            Dictionary<string, object> links = (Dictionary<string, object>)jPicload["links"];
            Dictionary<string, object> image = (Dictionary<string, object>)jPicload["image"];
            if (image.Count == 0)
                return null;
            if (links.Count == 0)
                return null;
            //string sh = links["short"].ToString(); string fNameWeb = image["name"].ToString();

            if (image["checksum"].ToString() != MD5File(fLocal).Replace("-", "").ToLower())
                throw new ArgumentException("MD5 Fail!");
            return links["image"].ToString();
        }
    }
}
