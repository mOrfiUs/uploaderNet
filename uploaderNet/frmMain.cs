using PicloadNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace uploaderNet
{
    public partial class frmMain : Form
    {
        private Button bGo;
        private Button bClose;
        private Button bMinimize;
        private ProgressBar pbAction;
        private metroTabCtrl.metroTabCtrl mtc;
        private System.ComponentModel.IContainer components = null;
        private TabPage tpImgur;
        private TabPage tpDlc;
        private TabPage tpCNL2;
        private TabPage tpBinbox;
        private TabPage tpMCrypter;
        private TabPage tpNCrypt;
        private TabPage tpDeviantsart;
        private TabPage tpPicload;
        private TabPage tpGo4up;

        private System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.bGo = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.pbAction = new System.Windows.Forms.ProgressBar();
            this.mtc = new metroTabCtrl.metroTabCtrl();
            this.tpImgur = new System.Windows.Forms.TabPage();
            this.tpDlc = new System.Windows.Forms.TabPage();
            this.tpCNL2 = new System.Windows.Forms.TabPage();
            this.tpBinbox = new System.Windows.Forms.TabPage();
            this.tpMCrypter = new System.Windows.Forms.TabPage();
            this.tpNCrypt = new System.Windows.Forms.TabPage();
            this.tpDeviantsart = new System.Windows.Forms.TabPage();
            this.tpGo4up = new System.Windows.Forms.TabPage();
            this.tpPicload = new System.Windows.Forms.TabPage();
            this.bMinimize = new System.Windows.Forms.Button();
            this.mtc.SuspendLayout();
            this.SuspendLayout();
            // 
            // bGo
            // 
            this.bGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGo.Location = new System.Drawing.Point(779, 57);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(79, 30);
            this.bGo.TabIndex = 0;
            this.bGo.Text = "Go!";
            this.bGo.UseVisualStyleBackColor = true;
            // 
            // bClose
            // 
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Location = new System.Drawing.Point(783, 308);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 30);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            // 
            // pbAction
            // 
            this.pbAction.Location = new System.Drawing.Point(12, 308);
            this.pbAction.Name = "pbAction";
            this.pbAction.Size = new System.Drawing.Size(684, 30);
            this.pbAction.Step = 1;
            this.pbAction.TabIndex = 3;
            // 
            // mtc
            // 
            this.mtc.Controls.Add(this.tpImgur);
            this.mtc.Controls.Add(this.tpDlc);
            this.mtc.Controls.Add(this.tpCNL2);
            this.mtc.Controls.Add(this.tpBinbox);
            this.mtc.Controls.Add(this.tpMCrypter);
            this.mtc.Controls.Add(this.tpNCrypt);
            this.mtc.Controls.Add(this.tpDeviantsart);
            this.mtc.Controls.Add(this.tpGo4up);
            this.mtc.Controls.Add(this.tpPicload);
            this.mtc.ItemSize = new System.Drawing.Size(0, 21);
            this.mtc.Location = new System.Drawing.Point(12, 32);
            this.mtc.Multiline = true;
            this.mtc.Name = "mtc";
            this.mtc.SelectedIndex = 0;
            this.mtc.Size = new System.Drawing.Size(684, 252);
            this.mtc.TabIndex = 4;
            // 
            // tpImgur
            // 
            this.tpImgur.Location = new System.Drawing.Point(4, 25);
            this.tpImgur.Name = "tpImgur";
            this.tpImgur.Padding = new System.Windows.Forms.Padding(3);
            this.tpImgur.Size = new System.Drawing.Size(676, 223);
            this.tpImgur.TabIndex = 4;
            this.tpImgur.Text = "Imgur";
            this.tpImgur.UseVisualStyleBackColor = true;
            // 
            // tpDlc
            // 
            this.tpDlc.Location = new System.Drawing.Point(4, 25);
            this.tpDlc.Name = "tpDlc";
            this.tpDlc.Padding = new System.Windows.Forms.Padding(3);
            this.tpDlc.Size = new System.Drawing.Size(949, 223);
            this.tpDlc.TabIndex = 5;
            this.tpDlc.Text = "Contenedor dlc";
            this.tpDlc.UseVisualStyleBackColor = true;
            // 
            // tpCNL2
            // 
            this.tpCNL2.Location = new System.Drawing.Point(4, 25);
            this.tpCNL2.Name = "tpCNL2";
            this.tpCNL2.Padding = new System.Windows.Forms.Padding(3);
            this.tpCNL2.Size = new System.Drawing.Size(949, 223);
            this.tpCNL2.TabIndex = 6;
            this.tpCNL2.Text = "Servidor cnl2";
            this.tpCNL2.UseVisualStyleBackColor = true;
            // 
            // tpBinbox
            // 
            this.tpBinbox.Location = new System.Drawing.Point(4, 25);
            this.tpBinbox.Name = "tpBinbox";
            this.tpBinbox.Padding = new System.Windows.Forms.Padding(3);
            this.tpBinbox.Size = new System.Drawing.Size(949, 223);
            this.tpBinbox.TabIndex = 7;
            this.tpBinbox.Text = "Binbox";
            this.tpBinbox.UseVisualStyleBackColor = true;
            // 
            // tpMCrypter
            // 
            this.tpMCrypter.Location = new System.Drawing.Point(4, 25);
            this.tpMCrypter.Name = "tpMCrypter";
            this.tpMCrypter.Padding = new System.Windows.Forms.Padding(3);
            this.tpMCrypter.Size = new System.Drawing.Size(949, 223);
            this.tpMCrypter.TabIndex = 8;
            this.tpMCrypter.Text = "MCrypter";
            this.tpMCrypter.UseVisualStyleBackColor = true;
            // 
            // tpNCrypt
            // 
            this.tpNCrypt.Location = new System.Drawing.Point(4, 25);
            this.tpNCrypt.Name = "tpNCrypt";
            this.tpNCrypt.Padding = new System.Windows.Forms.Padding(3);
            this.tpNCrypt.Size = new System.Drawing.Size(949, 223);
            this.tpNCrypt.TabIndex = 9;
            this.tpNCrypt.Text = "nCrypt";
            this.tpNCrypt.UseVisualStyleBackColor = true;
            // 
            // tpDeviantsart
            // 
            this.tpDeviantsart.Location = new System.Drawing.Point(4, 25);
            this.tpDeviantsart.Name = "tpDeviantsart";
            this.tpDeviantsart.Padding = new System.Windows.Forms.Padding(3);
            this.tpDeviantsart.Size = new System.Drawing.Size(949, 223);
            this.tpDeviantsart.TabIndex = 11;
            this.tpDeviantsart.Text = "Deviantsart";
            this.tpDeviantsart.UseVisualStyleBackColor = true;
            // 
            // tpGo4up
            // 
            this.tpGo4up.Location = new System.Drawing.Point(4, 25);
            this.tpGo4up.Name = "tpGo4up";
            this.tpGo4up.Padding = new System.Windows.Forms.Padding(3);
            this.tpGo4up.Size = new System.Drawing.Size(949, 223);
            this.tpGo4up.TabIndex = 0;
            this.tpGo4up.Text = "Go4up";
            this.tpGo4up.UseVisualStyleBackColor = true;
            // 
            // tpPicload
            // 
            this.tpPicload.Location = new System.Drawing.Point(4, 25);
            this.tpPicload.Name = "tpPicload";
            this.tpPicload.Padding = new System.Windows.Forms.Padding(3);
            this.tpPicload.Size = new System.Drawing.Size(949, 223);
            this.tpPicload.TabIndex = 12;
            this.tpPicload.Text = "Picload";
            this.tpPicload.UseVisualStyleBackColor = true;
            // 
            // bMinimize
            // 
            this.bMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinimize.Location = new System.Drawing.Point(702, 308);
            this.bMinimize.Name = "bMinimize";
            this.bMinimize.Size = new System.Drawing.Size(75, 30);
            this.bMinimize.TabIndex = 2;
            this.bMinimize.Text = "Minimize";
            this.bMinimize.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(868, 350);
            this.ControlBox = false;
            this.Controls.Add(this.pbAction);
            this.Controls.Add(this.bMinimize);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bGo);
            this.Controls.Add(this.mtc);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mtc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private string idShorte = string.Empty;
        private string idBinbox = string.Empty;
        private string idRelink = string.Empty;
        private string idNCrypt = string.Empty;
        private string idImgur = string.Empty;
        private string urlMCrypter = string.Empty;
        private string idPicload = string.Empty;
        private string hashPicload = string.Empty;

        private void loadApis()
        {
            try
            {
                Dictionary<string, string> dictApis = new Dictionary<string, string>();
                using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "uploaderNet.apis")))
                    while (sr.Peek() >= 0)
                    {
                        string[] lineCheck = sr.ReadLine().Split("=".ToCharArray(), StringSplitOptions.None);
                        if (lineCheck.Length > 1)
                            if (!string.IsNullOrEmpty(lineCheck[0]))
                                if (!dictApis.Keys.Contains(lineCheck[0]))
                                    dictApis.Add(lineCheck[0], lineCheck[1]);
                    }
                idNCrypt = dictApis["idNCrypt"];
                idShorte = dictApis["idShorte"];
                idBinbox = dictApis["idBinbox"];
                idRelink = dictApis["idRelink"];
                idPicload = dictApis["idPicload"];
                hashPicload = dictApis["hashPicload"];
                idImgur = dictApis["idImgur"];
                urlMCrypter = dictApis["urlMCrypter"];
            }
            catch (Exception)
            {
                idShorte = "eeeeeeeee";
                idRelink = "2222222222";
                idBinbox = "idBinbox";
                idNCrypt = "rrrrrrrrrrrr";
                idImgur = "eeeeeeeee";
                idPicload = "idPicload";
                hashPicload = "dddddddddddd";
                urlMCrypter = @"http://encrypterme.ga/api";
                string voidMsg = string.Empty;
                new util().modernInputBox(this, "ID's not found" + Environment.NewLine + "Demo mode", "", SystemIcons.Error, out voidMsg, true);
                //throw new ArgumentException(@"""uploaderNet.apis"" not found. You need to create accounts and populate ID's");
            }
            try
            {
                string dirLog = Path.Combine(Application.StartupPath, "LOG");
                if (Directory.Exists(dirLog))
                    Directory.CreateDirectory(dirLog);
            }
            catch (Exception)
            {
                throw new ArgumentException("Can't create LOG folder!!");
            }
        }

        public frmMain()
        {
            loadApis();
            InitializeComponent();
            foreach (Control ctrl in this.Controls)
                ctrl.BackColor = this.BackColor;
            this.mtc.TabPages.Remove(this.tpCNL2);
            this.mtc.TabPages.Remove(this.tpDlc);
            this.MouseDown += (sender, e) =>
            {
                const int HT_CAPTION = 0x2;
                const int WM_NCLBUTTONDOWN = 0xA1;

                if (e.Button == MouseButtons.Left)
                {
                    util.ReleaseCapture();
                    util.SendMessage(new HandleRef(null, ((Control)sender).Handle), (IntPtr)WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
                }
            };
            this.Paint += (sender, e) =>
            {
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(255, 0, 174, 219)))
                    e.Graphics.FillRectangle(sb, new Rectangle(0, 0, ((Control)sender).Width, 5));
            };
            tmr.Tag = string.Empty;
            this.bGo.Click += doAction;
            this.bMinimize.Click += bMinimize_Click;
            this.bClose.Click += (sender, e) => { this.Close(); };
            this.KeyPreview = true;
            this.KeyPress += frmMain_KeyPress;
        }

        void bMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private Dictionary<string, string> extractDictFromFiles(string[] fileNames)
        {
            Dictionary<string, string> dictTitlesLinks = new Dictionary<string, string>();
            foreach (string fuLog in fileNames)
                using (StreamReader sr = new StreamReader(fuLog))
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;
                        string[] arrDict = line.Split("|".ToCharArray(), StringSplitOptions.None);
                        string voidMsg = string.Empty;
                        if (arrDict.Length < 4)
                            if (DialogResult.Cancel == new util().modernInputBox(this, "El formato del archivo\r\n" + fuLog + "\r\nno parece correcto. ¿Continuar?", voidMsg, SystemIcons.Question, out voidMsg))
                                return dictTitlesLinks;

                        string titleLinks = arrDict[3].Split(".".ToCharArray(), StringSplitOptions.None)[0];
                        if (string.IsNullOrEmpty(titleLinks))
                            titleLinks = arrDict[4].Split(".".ToCharArray(), StringSplitOptions.None)[0];
                        if (string.IsNullOrEmpty(titleLinks))
                            titleLinks = arrDict[3].Split(".".ToCharArray(), StringSplitOptions.None)[0];
                        if (string.IsNullOrEmpty(titleLinks))
                            if (DialogResult.Cancel == new util().modernInputBox(this, "introduce el título para relink\r\nel episodio son los dígitos 3º y 4º", titleLinks, SystemIcons.Information, out titleLinks))
                                return dictTitlesLinks;
                        if (string.IsNullOrEmpty(titleLinks))
                            return dictTitlesLinks;

                        if (!dictTitlesLinks.Keys.Contains(titleLinks))
                            dictTitlesLinks.Add(titleLinks, arrDict[2].Replace("http://go4up.com/dl/", ""));
                        else
                            dictTitlesLinks[titleLinks] += "," + arrDict[2].Replace("http://go4up.com/dl/", "");
                    }

            return dictTitlesLinks;
        }

        private string doMCrypter()
        {
            string fMCrypter = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            string sExt = ".txt";
            ofd.Title = "Selecciona los archivos que contienen links para " + urlMCrypter;
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Text files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "probandoMCrypter.txt";
            ofd.Multiselect = true;
            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fMCrypter;

            string linksHosts = string.Empty;

            fMCrypter = Path.Combine(Application.StartupPath, "LOG", ofd.SafeFileName + ".MCrypter.txt");

            foreach (string fName in ofd.FileNames)
            {
                string lBinbox = string.Empty;
                string[] arrAllLines = File.ReadAllLines(fName, Encoding.Default);

                string[] arrAllLinesMega = arrAllLines.OfType<System.String>().Where(x => (x.ToLower().Contains("mega.")) || (x.ToLower().Contains("encrypterme.ga"))).ToArray();
                if (arrAllLinesMega.Length == 0)
                    continue;

                string sMCrypter = string.Join(Environment.NewLine, new mcrypter().getCryptLinks(urlMCrypter, arrAllLinesMega));
                if (string.IsNullOrEmpty(sMCrypter))
                    fMCrypter = "Void";
                else
                {
                    string voidMsgBox = Path.GetFileNameWithoutExtension(fName);
                    if (DialogResult.OK == new util().modernInputBox(this, "Introduce el título para subir a Binbox los enlaces " + Environment.NewLine + sMCrypter, voidMsgBox, SystemIcons.Question, out voidMsgBox))
                        lBinbox = new binbox().binboxLink(idBinbox, voidMsgBox, sMCrypter);
                    fMCrypter = fName;
                    sMCrypter = string.Join(Environment.NewLine, arrAllLines) + Environment.NewLine + sMCrypter;
                    if(!string.IsNullOrEmpty(lBinbox))
                        using (StreamWriter sw = File.CreateText(fMCrypter))
                            sw.WriteLine(lBinbox + Environment.NewLine + sMCrypter);
                }
            }
            return fMCrypter;
        }

        private string doImgur()
        {
            string fImgur = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            string sExt = ".jpg";
            ofd.Title = "Selecciona los archivos que contienen links para subir a Go4Up";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Image files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "*.jpg";
            ofd.Multiselect = true;
            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fImgur;
            fImgur = Path.Combine(Application.StartupPath, "LOG", "fImgur" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            string voidMsgBox = string.Empty;
            int iCount = 0;
            using (StreamWriter sw = File.CreateText(fImgur))
                foreach (string fImgName in ofd.FileNames)
                {
                    string up = new imgur().imgurLink(idImgur, fImgName);
                    if (!string.IsNullOrEmpty(up))
                    {
                        string [] sUp = up.Split("#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        sw.WriteLine("[IMG]" + sUp[0] + "[/IMG]");
                        sw.WriteLine(Path.GetFileNameWithoutExtension(fImgName));
                        sw.WriteLine(sUp[1]);
                        if (false)//true pregunta/ false contínuo
                            if (++iCount < ofd.FileNames.Length)
                                if (DialogResult.Cancel == new util().modernInputBox(this, "Se escribió la información de " + fImgName + "\r\nSi no hay conexiones u subidas en curso\r\npodrías intentar una reconexión. ¿Continuar?", voidMsgBox, SystemIcons.Question, out voidMsgBox, true))
                                    break;
                    }
                }
            return fImgur;
        }

        private string doDeviantsart()
        {
            string fImgur = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            string sExt = ".jpg";
            ofd.Title = "Selecciona los archivos que contienen links para subir a Go4Up";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Image files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "*.jpg";
            ofd.Multiselect = true;
            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fImgur;
            fImgur = Path.Combine(Application.StartupPath, "LOG", "fImgur" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            string voidMsgBox = string.Empty;
            int iCount = 0;
            using (StreamWriter sw = File.CreateText(fImgur))
                foreach (string fImgName in ofd.FileNames)
                {
                    string up = new deviantsart().deviantsartLink(fImgName);
                    if (!string.IsNullOrEmpty(up))
                    {
                        sw.WriteLine("[IMG]" + up + "[/IMG]");
                        sw.WriteLine(Path.GetFileNameWithoutExtension(fImgName));
                        if (false)//true pregunta/ false contínuo
                            if (++iCount < ofd.FileNames.Length)
                                if (DialogResult.Cancel == new util().modernInputBox(this, "Se escribió la información de " + fImgName + "\r\nSi no hay conexiones u subidas en curso\r\npodrías intentar una reconexión. ¿Continuar?", voidMsgBox, SystemIcons.Question, out voidMsgBox, true))
                                    break;
                    }
                }
            return fImgur;
        }

        private string doPicload()
        {
            string fPicload = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            string sExt = ".jpg,*.png";
            ofd.Title = "Selecciona los archivos para subir a Picload";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Image files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "*.*";
            ofd.Multiselect = true;
            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fPicload;
            fPicload = Path.Combine(Application.StartupPath, "LOG", "fPicload" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            string voidMsgBox = string.Empty;
            int iCount = 0;
            using (StreamWriter sw = File.CreateText(fPicload))
                foreach (string fImgName in ofd.FileNames)
                {
                    string up = string.Empty;
                    using (PicloadNet.picload pl = new picload())
                        up = pl.picloadLink(idPicload, hashPicload, fImgName);
                    if (!string.IsNullOrEmpty(up))
                    {
                        sw.WriteLine("[IMG]" + up + "[/IMG]");
                        sw.WriteLine(Path.GetFileNameWithoutExtension(fImgName));
                        if (false)//true pregunta/ false contínuo
                            if (++iCount < ofd.FileNames.Length)
                                if (DialogResult.Cancel == new util().modernInputBox(this, "Se escribió la información de " + fImgName + "\r\nSi no hay conexiones u subidas en curso\r\npodrías intentar una reconexión. ¿Continuar?", voidMsgBox, SystemIcons.Question, out voidMsgBox, true))
                                    break;
                    }
                }
            return fPicload;
        }

        private string doBinbox()
        {
            string fBinbox = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();

            string sExt = ".txt";
            ofd.Title = "Selecciona los archivos que contienen links para subir a Binbox";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Text files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "*.*";
            ofd.Multiselect = true;
            ofd.FileName = "*.*";

            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fBinbox;
            string linksHosts = string.Empty;
            //Dictionary<string, string> dictTitlesLinks = extractDictFromFiles(ofd.FileNames);
            linksHosts = File.ReadAllText(ofd.FileName);
            string voidMsgBox = Path.GetFileNameWithoutExtension(ofd.FileName);
            if (DialogResult.Cancel == new util().modernInputBox(this, "Introduce el título para subir a Binbox los enlaces " + Environment.NewLine + linksHosts, voidMsgBox, SystemIcons.Question, out voidMsgBox))
                return "void";
            fBinbox = Path.Combine(Application.StartupPath, "LOG", voidMsgBox + ".binbox.txt");
            using (StreamWriter sw = File.CreateText(fBinbox))
                sw.WriteLine(new binbox().binboxLink(idBinbox, voidMsgBox, linksHosts));
            return fBinbox;
        }

        private string doNCrypt()
        {
            string fNCrypt = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();

            string sExt = ".txt";
            ofd.Title = "Selecciona los archivos que contienen links para subir a nCrypt";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Text files" + "( *" + sExt + ")| *" + sExt;
            ofd.DefaultExt = sExt;
            ofd.CheckFileExists = true;
            ofd.FileName = "*.*";
            ofd.Multiselect = true;

            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return fNCrypt;
            string linksHosts = string.Empty;
            //Dictionary<string, string> dictTitlesLinks = extractDictFromFiles(ofd.FileNames);
            linksHosts = File.ReadAllText(ofd.FileName);
            string voidMsgBox = Path.GetFileNameWithoutExtension(ofd.FileName);
            //if (DialogResult.Cancel == new util().modernInputBox(this, "Introduce el título para subir a nCrypt los enlaces " + Environment.NewLine + linksHosts, voidMsgBox, SystemIcons.Question, out voidMsgBox))                return "void";
            fNCrypt = Path.Combine(Application.StartupPath, "LOG", voidMsgBox + ".ncrypt.txt");
            string fNCryptAll = Path.Combine(Application.StartupPath, "LOG", voidMsgBox + ".Allncrypt.txt");
            string[] arrLink = linksHosts.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            using (StreamWriter swAll = File.CreateText(fNCryptAll))
            using (StreamWriter sw = File.CreateText(fNCrypt))
                foreach (string singleLink in arrLink)
                {
                    string sOutNCrypt = new ncrypt().ncryptLink(idNCrypt, "link " + (++i).ToString("00"), singleLink);
                    if (string.IsNullOrEmpty(sOutNCrypt))
                    {
                        Thread.Sleep(40000);
                        sOutNCrypt = new ncrypt().ncryptLink(idNCrypt, "link " + i.ToString("00"), singleLink);
                        if (string.IsNullOrEmpty(sOutNCrypt))
                            Debugger.Break();

                    }
                    swAll.WriteLine(sOutNCrypt);
                    int iPos = sOutNCrypt.LastIndexOf("http://");
                    if (iPos > 0)
                        sw.WriteLine(sOutNCrypt.Remove(iPos));
                    //Debugger.Break();
                }
            return fNCrypt;
        }

        private string doGo4up()
        {
            string sGo4up = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Selecciona los archivos que contienen links de Go4up";
            ofd.InitialDirectory = Path.GetDirectoryName(@"C:\Program Files\FileUploader\");
            ofd.Filter = "Text files (*.log; *.txt)|*.log;*.txt|All files (*.*)|*.*";
            ofd.DefaultExt = "*.*";
            ofd.FilterIndex = 1;
            ofd.CheckFileExists = true;
            ofd.FileName = "FileUploader.log";
            ofd.Multiselect = true;
            DialogResult ofdResult = DialogResult.Cancel;
            Invoke((Action)(() =>
            {
                ofdResult = ofd.ShowDialog((IWin32Window)this);
            }));
            if (ofdResult == DialogResult.Cancel)
                return sGo4up;
            string linksHosts = string.Empty;
            Dictionary<string, string> dictTitlesLinks = extractDictFromFiles(ofd.FileNames);
            ManualResetEvent syncAll = new ManualResetEvent(false);
            int iFileSync = 0;
            foreach (KeyValuePair<string, string> kvPair in dictTitlesLinks)
            {
                int iLinkSync = 0;
                string[] arrLinks = kvPair.Value.Split(",".ToCharArray(), StringSplitOptions.None);
                ManualResetEvent syncTitle = new ManualResetEvent(false);
                foreach (string idGo4up in arrLinks)
                {
                    go4up cGo4up = new go4up("http://go4up.com/download/gethosts/" + idGo4up + "/n");
                    {
                        cGo4up.FinishWebReq += (oSender, oEvents) =>
                        {
                            foreach (string l in ((string[])oEvents.LinksHosts))
                                linksHosts += l + Environment.NewLine;
                            if (arrLinks.Length == ++iLinkSync)
                            {
                                sGo4up = Path.Combine(Path.GetDirectoryName(ofd.FileName), kvPair.Key + ".txt");
                                //getShorteRelink(linksHosts, kvPair.Key, ep, sGo4up);
                                linksHosts = string.Empty;
                                syncTitle.Set();
                                if (dictTitlesLinks.Count == ++iFileSync)
                                    syncAll.Set();
                            }
                        };
                        cGo4up.getLinksHosts();
                    }
                }
                syncTitle.WaitOne();
            }
            syncAll.WaitOne();
            return sGo4up;
        }

        private void initTmr()
        {
            tmr.Interval = 450;
            tmr.Tick += new EventHandler(increasePb);
            tmr.Start();
        }

        private void increasePb(object sender, EventArgs e)
        {
            pbAction.Increment(4);
            if (pbAction.Value == pbAction.Maximum)
                pbAction.Value = 0;
        }

        private void actionDone(string linkText)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate() { actionDone(linkText); });
            else
            {
                this.tmr.Stop();
                this.pbAction.Value = 0;
                tmr.Tag = string.Empty;
                if (string.IsNullOrEmpty(linkText))
                    return;
                TextBox tb = new TextBox() { Text = linkText, Font = new Font(this.Font.Name, this.Font.SizeInPoints + 2), Bounds = new Rectangle(10, 10, 450, 20), Visible = true };
                this.mtc.SelectedTab.Controls.Add(tb);
                tb.SelectAll();
                tb.Focus();
            }
        }

        private void doAction(object sender, EventArgs e)
        {
            string tTmp = "prueba";
            if (tmr.Tag.ToString().Contains("w"))
                return;
            foreach (Control ctrl in this.mtc.SelectedTab.Controls)
                if (ctrl.Name == string.Empty)
                    this.mtc.SelectedTab.Controls.Remove(ctrl);
            initTmr();
            tmr.Tag = "w";
            Button b = (Button)sender;
            string selTab = this.mtc.SelectedTab.Name;
            Dictionary<string, Action> doActions = new Dictionary<string, Action>()
            {
                { "tpGo4up", () => tTmp = doGo4up() },
                { "tpMCrypter", () => tTmp = doMCrypter() },
                { "tpNCrypt", () => tTmp = doNCrypt() },
                { "tpBinbox", () => tTmp = doBinbox() },
                { "tpImgur", () => tTmp = doImgur() },
                { "tpDeviantsart", () => tTmp = doDeviantsart() },
                { "tpPicload", () => tTmp = doPicload() }
            };
            ThreadPool.QueueUserWorkItem(_ =>
            {
                doActions[selTab]();
                actionDone(tTmp);
            });
        }

    }
}