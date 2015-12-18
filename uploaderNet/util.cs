using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace uploaderNet
{
    internal sealed class util
    {
        public bool getKeyFromValue(Dictionary<int, string> d, string v, out int iKey)
        {
            iKey = 0;
            foreach (KeyValuePair<int, string> kv in d)
                if (kv.Value.ToLower() == v.ToLower())
                {
                    iKey = kv.Key;
                    return true;
                }
            return false;
        }

        public string GetMachineGuid()
        {//http://hazardedit.com/downloads/

            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";
            object machineGuid = null;

            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                if (rk == null)
                    throw new KeyNotFoundException(string.Format("Key Not Found: {0}", location));
                else
                    machineGuid = rk.GetValue(name);
            if (machineGuid == null)
                throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", name));
            return machineGuid.ToString();
        }

        public string SHA1HashFile(string sPath)
        {
            using (StreamReader sr = new StreamReader(sPath))
                return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(sr.BaseStream));
        }

        public string MD5File(string sPath)
        {
            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(File.ReadAllBytes(sPath)));
        }

        public string UserAgent { get { return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36"; } }

        public string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;
            return body.Member.Name;
        }

        public string getStream(Stream st, bool fPlainHTML)
        {
            StreamReader sr = null;
            string sDecode = string.Empty;
            using (sr = new StreamReader(st))
            {
                StringBuilder sb = new StringBuilder();
                while (!sr.EndOfStream)
                    sb.Append(sr.ReadLine());
                sDecode = sb.ToString();
            }
            if (!fPlainHTML)
                sDecode = HttpUtility.HtmlDecode(sDecode);
            return sDecode;
        }

        public ArrayList matchAll(string regex, string html, int i = 1, int iMaxReturns = 1000)
        {
            ArrayList list = new ArrayList();
            int iCount = 0;
            foreach (Match m in new Regex(regex, RegexOptions.Multiline).Matches(html))
            {
                if (++iCount > iMaxReturns)
                    break;
                list.Add(m.Groups[i].Value.Trim());
            }
            return list;
        }

        private DialogResult showDialog(Form f, Form frm)
        {
            if (frm.InvokeRequired)
            {
                DialogResult dr = DialogResult.Cancel;
                frm.Invoke((MethodInvoker)delegate() { dr = showDialog(f, frm); });
                return dr;
            }
            else
                return f.ShowDialog((IWin32Window)frm);
        }

        private void mDown(object sender, MouseEventArgs e)
        {
            const int HT_CAPTION = 0x2;
            const int WM_NCLBUTTONDOWN = 0xA1;

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(new HandleRef(null, ((Control)sender).Handle), (IntPtr)WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        public DialogResult modernInputBox(Form frm, string sTitle, string sIn, Icon inpuBoxIcon, out string sOut, bool isMsgBox = false)
        {
            string fName = "frmInputBox";
            DialogResult dr = DialogResult.Cancel;
            sOut = string.Empty;
            string[] sFactorWH = sTitle.Split(Environment.NewLine.ToCharArray());
            int factorH = sFactorWH.Length;
            int factorW = 0;
            foreach (string item in sFactorWH)
                if (factorW < item.Length)
                    factorW = item.Length;
            int frmHeight = 200 + (factorH * 16);
            int frmWidth = 400 + (factorW * 4);
            if (frmHeight > 1000)
                frmHeight = 1000;
            if (frmWidth > 800)
                frmWidth = 800;
            using (Form f = new Form() { Icon = frm.Icon, ControlBox = false, Name = fName, BackColor = Color.White, Font = new Font("Segoe UI", 11F), ShowIcon = false, ShowInTaskbar = false, MinimizeBox = false, MaximizeBox = false, FormBorderStyle = FormBorderStyle.FixedToolWindow, Width = frmWidth, Height = frmHeight, StartPosition = FormStartPosition.CenterParent })
            {                
                Label l = new Label() { Left = 30, Top = 20, Text = sTitle, BackColor = f.BackColor, AutoSize = true };
                Button b = new Button() { Text = "Ok", Left = f.Width - 260, Width = 100, Top = frmHeight - 50, Height = 30, FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = true };
                Button c = new Button() { Text = "Cancel", Left = b.Left + b.Width + 30, Width = 100, Top = b.Top, Height = 30, FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = true };
                TextBox t = new TextBox() { Left = 40, Top = frmHeight - 100, Width = c.Left + c.Width - 40, Height = 40, Text = sIn, BackColor = f.BackColor };
                PictureBox pb = new PictureBox() { SizeMode = PictureBoxSizeMode.AutoSize, Left = f.Width - 50, Top = 20, Image = (Image)inpuBoxIcon.ToBitmap() };
                b.Click += (sender, e) => { f.DialogResult = DialogResult.OK; f.Close(); };
                c.Click += (sender, e) => { f.Close(); };
                f.MouseDown += (sender, e) => { mDown(sender, e); };
                l.MouseDown += (sender, e) => { mDown(f, e); };
                pb.MouseDown += (sender, e) => { mDown(f, e); };
                f.Paint += (sender, e) => {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(255, 0, 174, 219)))
                        e.Graphics.FillRectangle(sb, new Rectangle(0, 0, ((Control)sender).Width, 5)); };
                EventHandler ehActivated = null;
                ehActivated = (sender, e) => {
                    f.Activated -= ehActivated;
                    ForceForegroundWindow(f.Handle);
                    //FlashWindowEx(f);
                };
                f.Activated += ehActivated;

                f.Controls.AddRange(new Control[] { t, b, c, l, pb });
                f.AcceptButton = b;
                f.CancelButton = c;
                if (isMsgBox)
                    t.Visible = false;
                pb.BringToFront();
                dr = showDialog(f, frm);
                //if (dr == DialogResult.OK) incluso en Cancel devuelve el valor del texto
                    sOut = t.Text;
            }
            return dr;
        }

        public void ForceForegroundWindow(IntPtr hWnd)
        {
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(),
                IntPtr.Zero);
            uint appThread = GetCurrentThreadId();
            const uint SW_SHOW = 5;

            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
                AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, SW_SHOW);
            }
        }

        public IEnumerable<string> splitInParts(string s, int partLength)
        {
            if (!string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");
            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        #region NativeWindows

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool ReleaseCapture();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, IntPtr msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        [ComVisible(false)]
        internal struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [ComVisible(false)]
        internal enum FlashWindow : int
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state. 
            /// </summary>    
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption 
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button. 
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground. 
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass 
        // IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();

        /// The GetForegroundWindow function returns a handle to the 
        /// foreground window.
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool BringWindowToTop(HandleRef hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, Int32 hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, uint uFlags);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        #endregion NativeWindows

    }
}
