using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAINClient
{    
    /// <summary>
    ///    Diálogo para realizar búsquedas relacionadas con una entidad.
    /// </summary>
    public partial class frmFind : Form
    {
        SingletonApp theApp = SingletonApp.Instance;

        private CAIN.Entity Entity;                                     ///< Entidad que usaremos para realizar la búsqueda 
        private List<CAIN.Entity> Entities = new List<CAIN.Entity>();   ///< Lista de entidades que obtendremos como resultado de la búsqueda  
        private int Index;                                              ///< Posición de elemento seleccionado en la vista
        private Image Cover;                                              ///< Imagen de la carátula

        #region Metodos externos

        static uint WM_CLOSE = 0x10;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        /// <summary>
        ///    Constructor.
        /// </summary>      
        public frmFind(CAIN.Entity entity)
        {
            InitializeComponent();

            /* Establecemos el estado inicial de las opciones */

            this.Cover = null;
            this.chkIgnoreAlbum.Checked = true;

            /* Establecemos las columnas a mostrar en la vista */
            
            List<string> columns = new List<string>() { "Título", "Duración", "Álbum", "Año", "Artista(s)" }; 

            DataGridViewCustomizer.SetColumns(this.dgvView, columns);

            /* Nos guardamos la entidad que usaremos para realizar la búsqueda */
            
            this.Entity = entity;
        }

        /// <summary>
        ///    Método que devuelve la entidad seleccionada.
        /// </summary> 
        /// <returns>
        ///    La entidad seleccionada.
        /// </returns>       
        public CAIN.Entity GetEntity()
        {
            CAIN.Entity entity = new CAIN.Entity();

            entity.Track = new CAIN.Track();
            entity.Track.Title = this.txtTitle.Text;
            entity.Track.Duration = CAIN.Utils.GetSeconds(this.txtDuration.Text);
            entity.Album = new CAIN.Album();
            entity.Album.Title = this.txtAlbum.Text;
            entity.Album.Year = Convert.ToInt32(this.txtYear.Text);
            entity.Album.Cover = this.Cover;
            entity.GetArtistsFromString(this.txtArtists.Text.Trim());
            
            return entity;// this.Entities[this.Index];
        }

        /// <summary>
        ///    Callback que usa el hilo del diálogo de espera y que se encarga de buscar la información almacenada en la base de datos de MusicBrainz.
        /// </summary>        
        private void OnSearchAlbums(object sender, DoWorkEventArgs e)
        {                   
            /* Leemos las opciones seleccionadas por el usuario */
 
            bool bIgnoreTitle = this.chkIgnoreTitle.Checked;
            bool bIgnoreArtists = this.chkIgnoreArtists.Checked;  
            bool bIgnoreAlbum = this.chkIgnoreAlbum.Checked;
            int nTolerance = Convert.ToInt32(this.sbxTolerance.Value);

            /* Obtenemos las entidades que coinciden con las opciones seleccionadas */

            this.Entities = CAIN.MetadataResolver.GetEntities((int)theApp.Settings.Mode, this.Entity, nTolerance, bIgnoreTitle, bIgnoreArtists, bIgnoreAlbum);//, true, true);

            /* Descartamos aquellas entidades que no tengan duración */

            /*this.Entities = this.Entities.Where(item => item.Track.Duration > 0 && item.Album.Year > 0).ToList();*/

            /* Enviamos un mensaje para que se cierre el diálogo de espera */

            SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        ///    Callback que usa el hilo del diálogo de espera y que se encarga de buscar la carátula en la base de datos de MusicBrainz.
        /// </summary>        
        private void OnSearchCover(object sender, DoWorkEventArgs e)
        {
            this.Cover = CAIN.Utils.GetCover(this.Entities[this.Index].Album.MBID);

            SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        #region Manejadores de eventos

        private void timer_Tick(object sender, EventArgs e)
        {
            this.sbxTolerance.Enabled = !this.chkIgnoreAlbum.Checked;
                
            if (this.dgvView.SelectedRows.Count > 0)
            {
                this.txtTitle.Enabled = true;
                this.txtDuration.Enabled = true;
                this.txtAlbum.Enabled = true;
                this.txtYear.Enabled = true;
                this.pbxCover.Enabled = true;
                this.pbxCover.BackColor = SystemColors.Window;
                this.txtArtists.Enabled = true;

                this.btnOK.Enabled = String.IsNullOrEmpty(this.errorProvider1.GetError(this.txtTitle)) &&
                                    String.IsNullOrEmpty(this.errorProvider2.GetError(this.txtAlbum)) &&
                                    String.IsNullOrEmpty(this.errorProvider3.GetError(this.txtDuration)) &&
                                    String.IsNullOrEmpty(this.errorProvider4.GetError(this.txtYear)) &&
                                    String.IsNullOrEmpty(this.errorProvider5.GetError(this.txtArtists));
            }
            else
            {                                           
                this.txtTitle.Enabled = false;
                this.txtDuration.Enabled = false;
                this.txtAlbum.Enabled = false;
                this.txtYear.Enabled = false;
                this.pbxCover.Enabled = false;
                this.pbxCover.BackColor = SystemColors.ButtonFace;
                this.txtArtists.Enabled = false;

                this.btnOK.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            /* Vaciamos la vista */

            this.dgvView.Rows.Clear();

            /* Mostramos un diálogo de espera que a su vez lanza un hilo que obtiene las entidades que coinciden con las opciones e búsqueda */

            frmWait dlg = new frmWait("Buscando álbumes. Espere, por favor...");
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnSearchAlbums;
            dlg.ShowDialog();

            if (this.Entities.Count == 0)
            {
                MessageBox.Show(this, "No se encontraron resultados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* Actualizamos el contenido de la vista */

            DataGridViewCustomizer.Fill(this.dgvView, this.Entities, false);
        }

        private void pbxCover_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Archivos de imagen (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.Cover = Image.FromFile(dlg.FileName);
                this.pbxCover.Image = this.Cover;
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            this.errorProvider1.SetError(this.txtTitle, String.Empty);
            if (String.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                this.errorProvider1.SetError(this.txtTitle, "El título no puede estar vacío");
        }

        private void txtAlbum_TextChanged(object sender, EventArgs e)
        {
            this.errorProvider2.SetError(this.txtAlbum, String.Empty); 
            if (String.IsNullOrEmpty(this.txtAlbum.Text.Trim()))
                this.errorProvider2.SetError(this.txtAlbum, "El álbum no puede estar vacío");
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {                                             
            this.errorProvider3.SetError(this.txtDuration, String.Empty);
            int duration = CAIN.Utils.GetSeconds(this.txtDuration.Text.Trim());
            if (duration == 0)
                this.errorProvider3.SetError(this.txtDuration, "La duración tiene que tener el formato '[m]m:ss'");
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {                  
            this.errorProvider4.SetError(this.txtYear, String.Empty);
                              
            int year;// = 0;               
            int currentYear = DateTime.Now.Year;
            Int32.TryParse(this.txtYear.Text.Trim(), out year);
            if (year < 1799 || year > currentYear)
                this.errorProvider4.SetError(this.txtYear, String.Format("El año tiene que ser un valor comprendido entre 1799 y {0}", currentYear));
        }

        private void txtArtists_TextChanged(object sender, EventArgs e)
        {                    
            this.errorProvider5.SetError(this.txtArtists, String.Empty);                           
            if (!String.IsNullOrEmpty(this.txtArtists.Text.Trim()))
            {
                List<string> artists = this.txtArtists.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                artists = artists.Where(item => !String.IsNullOrEmpty(item.Trim())).ToList();
                if (artists.Count == 0)
                    this.errorProvider5.SetError(this.txtArtists, "Los artistas no pueden estar vacíos");
                else
                    this.errorProvider5.Clear();
            }
        }
        
        private void chkIgnoreAlbum_CheckedChanged(object sender, EventArgs e)
        {     
            this.sbxTolerance.Enabled = !this.chkIgnoreAlbum.Checked;
        } 

        private void dgvView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvView.SelectedRows.Count == 0)
            {
                this.Index = -1;
                this.txtTitle.Text = String.Empty;
                this.txtDuration.Text = String.Empty;
                this.txtAlbum.Text = String.Empty;
                this.txtYear.Text = String.Empty;
                this.txtArtists.Text = String.Empty;
                this.pbxCover.Image = null;
                this.Cover = null;
                return;
            }

            this.Index = this.dgvView.SelectedRows[0].Index;

            this.txtTitle.Text = this.Entities[this.Index].Track.Title;
            this.txtDuration.Text = CAIN.Utils.FormatSeconds(this.Entities[this.Index].Track.Duration.ToString());
            this.txtAlbum.Text = this.Entities[this.Index].Album.Title;
            this.txtYear.Text = this.Entities[this.Index].Album.Year.ToString();
            this.txtArtists.Text = String.Join(", ", this.Entities[this.Index].Artists);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.chkSearchCover.Checked)
                return;

            /* Buscamos la carátula de la entidad seleccionada */

            frmWait dlg = new frmWait("Buscando carátula. Espere, por favor...");
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnSearchCover;
            dlg.ShowDialog();
        }           

        private void dgvView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }

        #endregion
    }
}
