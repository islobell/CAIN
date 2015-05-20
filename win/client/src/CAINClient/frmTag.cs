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
    ///    Diálogo para crear/editar una etiqueta.
    /// </summary>
    public partial class frmTag : Form
    {
        SingletonApp theApp = SingletonApp.Instance;  
                                                                            
        private List<ListViewItem> ListViewItems;        ///< Lista de elementos que hay en el objeto ListView de las etiquetas        
        private int ListViewSelectedIndex;               ///< Posición del elemento seleccionado en el objeto ListView de las etiquetas

        /* Atributos que serán consultados desde fuera del diálogo */

        public string TagName;                           ///< Nombre de la etiqueta
        public string TagValue;                          ///< Valor de la etiqueta

        /// <summary>
        ///    Constructor.
        /// </summary>     
        /// <param name="items">
        ///    La lista de elementos del objeto ListView que contiene las etiquetas.
        /// </param>    
        /// <param name="index">
        ///    La posición del elemento seleccionado en el objeto ListView. -1, sino hay ningún elemento seleccionado.
        /// </param>     
        public frmTag(List<ListViewItem> items, int index = -1)
        {
            InitializeComponent();

            /* Nos guardamos los parámetros para poder trabajar con ellos más adelante */

            this.ListViewItems = items;
            this.ListViewSelectedIndex = index;

            /* Obtenemos la lista de nombres de etiqueta que hay en la base de datos. Si la lista está vacía, desactivamos el botón */

            this.btnSelectTag.Enabled = theApp.DB.GetTagNames().Count > 0;

            /* Establecemos el nombre y el icono del diálogo, así como el contenido de los controles */

            this.Text = index != -1 ? "Editar etiqueta" : "Crear etiqueta";
            this.Icon = index != -1 ? CAINClient.Properties.Resources.tag_blue_edit : CAINClient.Properties.Resources.tag_blue_add;
            this.txtName.Text = index != -1 ? items[index].SubItems[0].Text : String.Empty;
            this.txtValue.Text = index != -1 ? items[index].SubItems[1].Text : String.Empty;

            this.ActiveControl = this.txtName;
        }

        #region Manejadores de eventos

        private void timer_Tick(object sender, EventArgs e)
        {
            this.btnOK.Enabled = !String.IsNullOrEmpty(this.txtName.Text.Trim()) && !String.IsNullOrEmpty(this.txtValue.Text.Trim());
        }

        private void btnSelectTag_Click(object sender, EventArgs e)
        {
            frmTags dlg = new frmTags(theApp.DB.GetTagNames());
            if (dlg.ShowDialog() == DialogResult.OK)
                this.txtName.Text = dlg.GetName();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool bOK = true;

            /* Si hay campos vacíos, no continuamos */

            if (String.IsNullOrEmpty(this.txtName.Text) ||
                String.IsNullOrEmpty(this.txtValue.Text))
            {
                MessageBox.Show("No puede haber campos vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bOK = false;
            }

            /* Comprobamos que el campo 'Nombre' no existe en la lista de elementos del objeto ListView */
            
            if (bOK)
            {
                for (int i = 0; i < this.ListViewItems.Count; i++)
                {
                    if (this.ListViewSelectedIndex == i) continue;

                    ListViewItem item = this.ListViewItems[i];

                    if (String.Equals(this.txtName.Text, item.SubItems[0].Text))
                    {
                        MessageBox.Show("La etiqueta que va a crear ya existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bOK = false;
                        break;
                    }
                } 
            } 

            /* Si no se cumplen las condiciones para cerrar el diálogo, no continuamos */
            
            if (!bOK)
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            this.TagName = this.txtName.Text;
            this.TagValue = this.txtValue.Text;
        }

        #endregion
    }
}
