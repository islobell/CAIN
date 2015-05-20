using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class frmAlbum : Form
    {
        SingletonApp theApp = SingletonApp.Instance;

        private List<CAIN.Entity> Entities = new List<CAIN.Entity>();   ///< Lista de entidades que obtendremos como resultado de la búsqueda
        //private List<CAIN.Album> Albums = new List<CAIN.Album>();       ///< Lista de álbumes obtenidos de la base de datos
        private int Index;                                              ///< Posición de la entidad seleccionada en la lista de entidades

        /*#region Metodos externos

        static uint WM_CLOSE = 0x10;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        #endregion*/

        /// <summary>
        ///    Constructor.
        /// </summary>      
        public frmAlbum(List<CAIN.Entity> entities)
        {
            InitializeComponent();

            /* Nos guardamos las entidades que usaremos para realizar el filtrado */

            this.Entities = entities;

            /* Establecemos el estado inicial de las opciones */

            this.btnOK.Enabled = false;

            /* Establecemos las columnas a mostrar en la vista */

            List<string> columns = new List<string>() { "Título", "Año", "Artista(s)" };

            DataGridViewCustomizer.SetColumns(this.dgvView, columns);

            /* Mostramos un diálogo de espera que a su vez lanza un hilo que obtiene las entidades que coinciden con las opciones e búsqueda */

            /*frmWait dlg = new frmWait("Buscando álbumes. Espere, por favor...");
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnSearchAlbums;
            dlg.ShowDialog();*/

            var groups = entities.GroupBy(item => item.Album.Title);//.Select(item => item.First()).ToList(); 
            this.Entities = groups.Select(item => item.First()).ToList();//entities.GroupBy(item => item.Album.Title).Select(item => item.First()).ToList();
            List<CAIN.Album> albums = this.Entities.Select(item => item.Album).ToList();
            //List<string> artists =this.Entities.Select(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList();
                
            List<string> artists = new List<string>();

            foreach (var group in groups)
            {
                List<CAIN.Entity> ents = entities.Where(item => String.Equals(item.Album.Title, group.First().Album.Title)).ToList();
 
                List<string> names = new List<string>();
                ents.ForEach(item => names.AddRange(item.Artists.Select(y => y.Name)));

                artists.Add(names.Distinct().Count() > 1 ? String.Format("Varios artistas ({0}, ...)", names[0]) : names[0]);
            }

            Debug.Assert(albums.Count == artists.Count);            

            /* Actualizamos el contenido de la vista */

            DataGridViewCustomizer.Fill(this.dgvView, albums, artists);
            //this.dgvView.ClearSelection();
        }

        /// <summary>
        ///    Método que devuelve el álbum seleccionado.
        /// </summary> 
        /// <returns>
        ///    El álbum seleccionado.
        /// </returns>       
        public CAIN.Album GetAlbum()
        {
            return this.Entities[this.Index].Album;
        }

        /// <summary>
        ///    Callback que usa el hilo del diálogo de espera y que se encarga de buscar la información almacenada en la base de datos de MusicBrainz.
        /// </summary>        
        //private void OnSearchAlbums(object sender, DoWorkEventArgs e)
        //{
        //    /* Obtenemos los álbumes de la base de datos */

        //    this.Entities = this.Entities.GroupBy(item => item.Album.Title).Select(item => item.First()).ToList();
        //    List<CAIN.Album> albums = this.Entities.Select(item => item.Album).ToList();
        //    List<string> artists = this.Entities.Select(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList();

        //    /* Enviamos un mensaje para que se cierre el diálogo de espera */

        //    SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        //}

        #region Manejadores de eventos

        private void dgvView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvView.SelectedRows.Count == 0)
            {
                this.btnOK.Enabled = false;
                return;
            }

            this.Index = this.dgvView.SelectedRows[0].Index;
            this.btnOK.Enabled = true;
        }            

        private void dgvView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK.PerformClick();
        }

        #endregion
    }
}
