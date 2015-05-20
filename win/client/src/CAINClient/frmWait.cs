using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAINClient
{
    /// <summary>
    ///    Diálogo de espera que permite ejecutar un hilo en segundo plano.
    /// </summary>
    public partial class frmWait : Form
    {

        public BackgroundWorker BKWorker = null;

        /// <summary>
        ///    Constructor.
        /// </summary>     
        /// <param name="message">
        ///    La cadena de texto a mostrar.
        /// </param>     
        /// <param name="showCancelButton">
        ///    La cadena de texto a mostrar.
        /// </param>    
        public frmWait(string message, bool showCancelButton = false)
        {                                          
            InitializeComponent();

            this.lblText.Text = message;
            //this.btnCancel.Visible = showCancelButton;

            this.BKWorker = new BackgroundWorker();
        }

        #region Manejadores de eventos

        private void frmWait_Resize(object sender, EventArgs e)
        {
            /*if (this.btnCancel.Visible)
            {
                this.Height = 140;
                this.Top = (Application.OpenForms["frmMain"].Top + Application.OpenForms["frmMain"].Height / 2) - this.Height / 2;
            }*/
        }
        
        private void frmWait_Load(object sender, EventArgs e)
        { 
            /*this.btnCancel.Visible = true;
            this.Height = 140;
            this.Top = (Application.OpenForms["frmMain"].Top + Application.OpenForms["frmMain"].Height / 2) - this.Height / 2; */
            
            this.BKWorker.RunWorkerAsync(this.Handle);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.BKWorker.CancelAsync();
        }

        #endregion
    }
}
