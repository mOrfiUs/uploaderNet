using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

//jdonwloader br.getPage("http://go4up.com/download/gethosts/" + new Regex(parameter, "(\\w{1,15})/?$").getMatch(0));
//Regex rx = new Regex(@"(\w{1,15})/?$", RegexOptions.None);

namespace uploaderNet
{
    class go4up : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~go4up()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_cts != null)
                {
                    _cts.Cancel();
                    Application.DoEvents();
                    _cts.Dispose();
                    _cts = null;
                }
            }
        }

        public class go4upEventArgs : EventArgs
        {
            private object _oLinkGo4up;
            private object _oLinksHosts;

            public go4upEventArgs(object oLinksHosts, object oLinkGo4up)
            {
                _oLinkGo4up = oLinkGo4up;
                _oLinksHosts = oLinksHosts;
            }

            public object LinkGo4up { get { return _oLinkGo4up; } }

            public object LinksHosts { get { return _oLinksHosts; } }
        }

        public delegate void finishWREventH(object sender, go4upEventArgs e);
        public event finishWREventH FinishWebReq;
        protected virtual void OnFinishWebReq(go4upEventArgs e)
        {
            finishWREventH h = FinishWebReq;
            if (h != null)
                h(this, e);
        }

        private string _linkGo4up;
        private bool _isFinished;
        private string[] _linksHosts;
        private string strlinksHosts = string.Empty;
        private int _linkCount = 0;
        private int _nLinks = 0;
        private Queue<string[]> _linksGo4up = new Queue<string[]>();
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public go4up(string link) { this._linkGo4up = link; }

        private void go4UpFinishWebRequest(IAsyncResult wRes, HttpWebRequest wReq, string linkOrigin, bool fHost)
        {
            string s = string.Empty;
            using (HttpWebResponse wr = (HttpWebResponse)wReq.EndGetResponse(wRes))
                if (wr.StatusCode == HttpStatusCode.OK)
                    using (Stream st = wr.GetResponseStream())
                        s = new util().getStream(st, true);
            if (string.IsNullOrEmpty(s))
                return;
            if (fHost)
            {
                foreach (var link in new util().matchAll(@"<b><a href=\""(.*?)""", s))
                    strlinksHosts += link.ToString() + Environment.NewLine;
                _linkCount++;
            }
            else
            {
                //cambiado por go4up el 2015-04-25 Link checker added on download pages foreach (string link in new util().matchAll(@"http://go4up.com/rd/(.*?)</a>", s))
                foreach (string link in new util().matchAll(@"<b><a href=\\""\\/rd\\/(.*?)\\"">", s))
                    _linksGo4up.Enqueue(new string[] { "http://go4up.com/rd/" + link.Replace(@"\\/", "/"), linkOrigin });
                _nLinks = _linksGo4up.Count;
                go4Up2Host();
            }

            if ((_nLinks - _linkCount) == 0)
            {
                this._linksHosts = strlinksHosts.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                this._isFinished = true;
                FinishWebReq(this, new go4upEventArgs(this._linksHosts, this._linkGo4up));
            }
        }

        private void go4Up2Host()
        {
            if (_linksGo4up.Any())
            {
                var url = _linksGo4up.Dequeue();
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    CancellationToken token = (CancellationToken)_;
                    if (token.IsCancellationRequested)
                        return;
                    Uri uri = new Uri(url[0]);
                    HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(uri);
                    wReq.Timeout = 100000;
                    wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    wReq.Method = "GET";
                    wReq.UserAgent = new util().UserAgent;
                    wReq.Accept = "text/xml";
                    wReq.Referer = "http://go4up.com/";
                    //wReq.Accept = "*/*";
                    wReq.BeginGetResponse(wRes => { go4UpFinishWebRequest(wRes, wReq, url[1], true); }, null);
                }, _cts.Token);
                Application.DoEvents();
                go4Up2Host();
            }
        }

        protected virtual void getLinksGo4up()
        {
            Uri RequestUri = new Uri(_linkGo4up);
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(RequestUri);

            wReq.Timeout = 100000;
            wReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            wReq.Method = "GET";
            wReq.UserAgent = new util().UserAgent;
            wReq.Accept = "text/xml";
            wReq.Referer = "http://go4up.com/";
            wReq.BeginGetResponse(wRes => { go4UpFinishWebRequest(wRes, wReq, _linkGo4up, false); }, null);
        }

        public bool IsFinished { get { return this._isFinished; } }

        public string[] linksHosts
        {
            get
            {
                if (!this._isFinished)
                    throw new InvalidOperationException("Operación aún no finalizada");
                return this._linksHosts;
            }
        }

        public void getLinksHosts()
        {
            if (this._isFinished)
                throw new InvalidOperationException("Operación terminada");
            getLinksGo4up();
        }

    }

}
