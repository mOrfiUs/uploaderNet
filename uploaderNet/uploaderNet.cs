using System;
using System.Windows.Forms;

namespace uploaderNet
{
    class uploaderNet
    {
        [STAThread]
        private static void Main(string[] args)
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
        }
    }
}
