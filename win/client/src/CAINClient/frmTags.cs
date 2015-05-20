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
    ///    Diálogo para seleccionar un nombre de etiqueta de la lista de nombres de etiqueta que hay en la base de datos.
    /// </summary>     
    public partial class frmTags : Form
    {
        /// <summary>
        ///    Constructor.
        /// </summary>     
        /// <param name="availableTags">
        ///    La lista de nombre de etiqueta que hay en la base de datos.
        /// </param>   
        public frmTags(List<string> availableTags)
        {
            InitializeComponent();

            /* Cargamos los nombres de etiqueta en el objeto ListView */

            foreach (string tag in availableTags)
                this.lbxTags.Items.Add(tag);
        }

        #region Manejadores de eventos

        private void timer_Tick(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.lbxTags.SelectedItem != null;
        }

        private void lbxTags_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;  
            this.Close();
        }

        /// <summary>
        ///    Método que permite obtener el elemento seleccionado del objeto ListView.
        /// </summary>     
        public string GetName()
        {
            return this.lbxTags.SelectedItem.ToString();
        }

        #endregion
    }
}
