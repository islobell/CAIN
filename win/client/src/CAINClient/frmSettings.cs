#define OPT1
//#define OPT2  
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

namespace CAINClient
{
    /// <summary>
    ///    Diálogo para cambiar la configuración del servicio de Windows.
    /// </summary>     
    public partial class frmSettings : Form
    {        
        SingletonApp theApp = SingletonApp.Instance;

        const int MAX_FOLDER_PATHS = 50;      ///< Constante con el número máximo de carpetas que se pueden seleccionar 
        private static string DefaultPath = AppDomain.CurrentDomain.BaseDirectory;

        public frmSettings()
        {
            InitializeComponent();

            /* Inicializamos los controles */

            this.lbxFolderPaths.Items.AddRange(theApp.Settings.FolderPaths.ToArray());
            this.optSearchByTag.Checked = theApp.Settings.Mode == CAIN.Settings.ModeTypes.SearchByTagAlBum;
            this.optSearchOriginal.Checked = theApp.Settings.Mode == CAIN.Settings.ModeTypes.SearchByOriginalAlbum;
            this.sbxElapsedTime.Value = theApp.Settings.Interval / (1000 * 60); //son minutos
            this.lblPathDst.Text = theApp.Settings.PathDst;

            this.ActiveControl = this.btnOK;
        }

        #region Manejadores de eventos

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.btnAddFolderPath.Enabled = this.lbxFolderPaths.Items.Count < MAX_FOLDER_PATHS;
            this.btnDeleteFolderPath.Enabled = this.lbxFolderPaths.SelectedIndex != -1;
            this.btnGoUpFolderPath.Enabled = this.lbxFolderPaths.SelectedIndex != -1 && this.lbxFolderPaths.SelectedIndex > 0;
            this.btnGoDownFolderPath.Enabled = this.lbxFolderPaths.SelectedIndex != -1 && this.lbxFolderPaths.SelectedIndex < this.lbxFolderPaths.Items.Count-1; 
        } 

        private void btnAddFolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();                    
            dlg.Description = "Seleccione la carpeta donde desea buscar canciones: ";
            dlg.SelectedPath = frmSettings.DefaultPath; 
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                frmSettings.DefaultPath = dlg.SelectedPath;
                this.lbxFolderPaths.Items.Add(dlg.SelectedPath);
            }
        }

        private void btnDeleteFolderPath_Click(object sender, EventArgs e)
        {
            this.lbxFolderPaths.Items.RemoveAt(this.lbxFolderPaths.SelectedIndex);
        }

        private void btnGoUpFolderPath_Click(object sender, EventArgs e)
        {
            int newIndex = this.lbxFolderPaths.SelectedIndex - 1;

            object selected = this.lbxFolderPaths.SelectedItem;

            this.lbxFolderPaths.Items.Remove(selected);
            this.lbxFolderPaths.Items.Insert(newIndex, selected);
            this.lbxFolderPaths.SetSelected(newIndex, true);
        }

        private void btnGoDownFolderPath_Click(object sender, EventArgs e)
        {  
            int newIndex = this.lbxFolderPaths.SelectedIndex + 1;

            object selected = this.lbxFolderPaths.SelectedItem;

            this.lbxFolderPaths.Items.Remove(selected);
            this.lbxFolderPaths.Items.Insert(newIndex, selected);
            this.lbxFolderPaths.SetSelected(newIndex, true);
        }

        private void btnPathDst_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = this.lblPathDst.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.lblPathDst.Text = dlg.SelectedPath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /* Comprobamos si ha cambiado el tipo de catalogación, si es así avisamos al usuario de que se borrara */
            
            CAIN.Settings.ModeTypes mode = this.optSearchByTag.Checked ? CAIN.Settings.ModeTypes.SearchByTagAlBum : CAIN.Settings.ModeTypes.SearchByOriginalAlbum;

            if (theApp.Settings.Mode != mode)
            {
                if (MessageBox.Show("¡ATENCIÓN! El modo de catalogación ha cambiado.\r\nSi continúa, la próxima vez que se reinice el servicio se volverán a catalogar TODAS las canciones de las carpetas seleccionadas.\r\n¿Desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.DialogResult = DialogResult.None;
                    return;    
                }
            }

            /* Guardamos la configuración */

            theApp.Settings.FolderPaths = this.lbxFolderPaths.Items.Cast<String>().ToList(); 
            theApp.Settings.Reset = theApp.Settings.Mode != mode;
            theApp.Settings.Mode = mode;
            theApp.Settings.Interval = (int)this.sbxElapsedTime.Value * 1000 * 60;   //son minutos
            theApp.Settings.PathDst = this.lblPathDst.Text;

            theApp.SaveSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        
        #endregion
    }
}
