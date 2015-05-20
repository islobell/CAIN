using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAINClient
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            this.lblAppName.Text = String.Format("{0} v{1}", assembly.GetName().Name, assembly.GetName().Version.ToString());//2));

            this.lblWeb.Text = "GitHub: https://github.com/islobell/CAIN";
            this.lblWeb.Links.Add(8, -1, "https://github.com/islobell/CAIN");
            this.lblWeb.LinkClicked += OnLinkClicked;

            this.lblCopyright.Text = String.Format("Copyright \u00a9 Israel López Bellver, {0}", DateTime.Now.Year);
        }        

        #region Manejadores de eventos
        
        private void OnLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        #endregion

    }
}
