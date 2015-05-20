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
    ///    Diálogo que permite la selección de columnas a mostrar en la vista del panel derecho.
    /// </summary>
    public partial class frmColumns : Form
    {
        /// <summary>
        ///    Constructor.
        /// </summary>  
        /// <param name="availableColumns">
        ///    La lista de columnas disponibles.
        /// </param>    
        /// <param name="displayedColumns">
        ///    La lista de columnas que se están mostrando.
        /// </param>  
        public frmColumns(List<string> availableColumns, List<string> displayedColumns)
        {
            InitializeComponent();

            /* La primera columna de la lista de columnas disponibles no será seleccionable (ya que siempre se mostrará en la vista), 
             * por lo que la quitamos de la lista de columnas disponibles */

            availableColumns = availableColumns.Skip(1).ToList();

            foreach (string column in availableColumns)
                this.lbxColumns.Items.Add(column, displayedColumns.Exists(item => item == column));
        }

        /// <summary>
        ///    Método que permite obtener la lista de columnas a mostrar en la vista del panel derecho.
        /// </summary>  
        /// <returns>
        ///    La lista de columnas a mostrar en la vista del panel derecho.
        /// </returns>  
        public List<string> GetDisplayedColumns()
        {
            List<string> displayedColumns = this.lbxColumns.CheckedItems.OfType<string>().ToList();
            
            /* Añadimos la columna 'Título' a la lista de columnas a mostrar */

            displayedColumns.Insert(0, "Título");

            return displayedColumns;
        }

        #region Manejadores de eventos

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lbxColumns.Items.Count; i++)
                this.lbxColumns.SetItemChecked(i, true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lbxColumns.Items.Count; i++)
                this.lbxColumns.SetItemChecked(i, false);
        }

        #endregion
    }
}
