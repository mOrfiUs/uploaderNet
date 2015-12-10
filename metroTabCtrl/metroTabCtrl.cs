using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace metroTabCtrl
{
    public class metroTabCtrl : TabControl
    {
        public metroTabCtrl(): base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.ResizeRedraw = true;
        }
        
        protected void paintTransBG(Graphics g, Rectangle r)
        {
            if ((this.Parent != null))
            {
                r.Offset(this.Location);
                GraphicsState gs = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    g.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                    PaintEventArgs e = new PaintEventArgs(g, r);
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                }
                finally
                {
                    g.Restore(gs);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.paintTransBG(e.Graphics, this.ClientRectangle);
            foreach (TabPage tbp in this.TabPages)
            {
                this.PaintTabUnderLine(e.Graphics, tbp);
                this.PaintTabText(e.Graphics, tbp);
            }
            this.PaintBorder(e);
        }

        private void PaintTabUnderLine(Graphics graph, TabPage tbp)
        {
            Rectangle r = this.GetTabRect(this.TabPages.IndexOf(tbp));
            if (tbp == this.SelectedTab)
            {
                r.Offset(1, r.Height + 3);
                r.Inflate(-2, 5);
                graph.FillRectangle(new SolidBrush(c), r);
            }
            else
            {
                r.Offset(-3, r.Height + 4);
                r.Inflate(2, 2);
                graph.FillRectangle(new SolidBrush(SystemColors.ControlText), r);
            }
        }

        private void PaintTabText(Graphics g, TabPage tbp)
        {
            Rectangle r = this.GetTabRect(this.TabPages.IndexOf(tbp));
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            Brush b = tbp.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
            float fs = this.Font.Size;
            if (fs > 9)
                fs = fs - 1F;
            using (Font f = new Font(this.Font.Name, fs, tbp == this.SelectedTab ? FontStyle.Bold : FontStyle.Regular))
                g.DrawString(tbp.Text, f, b, r, sf);
        }

        private void PaintBorder(PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                Rectangle r = this.TabPages[0].Bounds;
                r.Inflate(1, 1);
                ControlPaint.DrawBorder(e.Graphics, r, c, ButtonBorderStyle.Inset);
                for (int i = 0; i < 3; i++)
                {
                    r.Offset(1, 1);
                    ControlPaint.DrawBorder(e.Graphics, r, c, ButtonBorderStyle.Inset);
                }
            }
        }

        private Color c = Color.FromArgb(255, 0, 174, 219);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            const int WM_SETFONT = 0x30;
            const int WM_FONTCHANGE = 0x1d;
            base.OnFontChanged(e);
            IntPtr hFont = this.Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
            this.ItemSize = new Size(0, this.Font.Height + 3);
        }
    }
}
