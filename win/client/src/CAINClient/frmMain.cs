using System;    
using System.IO;
using System.Linq;
using System.Text;        
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CAINClient
{
    /// <summary>
    ///    Diálogo principal de la aplicación (I).
    /// </summary>
    public partial class frmMain : Form
    {
        SingletonApp theApp = SingletonApp.Instance;

        private List<CAIN.Entity> Entities = new List<CAIN.Entity>();            ///< Lista de las entidades que hay en la base de datos   
        private List<CAIN.Entity> DisplayedEntities = new List<CAIN.Entity>();   ///< Lista de las entidades que se muestran     
        private List<CAIN.Entity> SelectedEntities = new List<CAIN.Entity>();    ///< Lista de las entidades seleccionadas por el usuario 
        private List<int> SelectedIndexes = new List<int>();                     ///< Lista de las posiciones que ocupan las entidades seleccionadas por el usuario en la lista de entidades que se muestran
        private List<string> DisplayedColumns = new List<string>();              ///< Lista de las columnas que se muestran en la vista del panel derecho
        private EditionPanelValues InitialValues = new EditionPanelValues();     ///< Objeto que guarda el estado inicial de los controles del panel izquierdo
        private EditionPanelValues CurrentValues = new EditionPanelValues();     ///< Objeto que guarda el estado actual de los controles del panel izquierdo
        private bool ShowOnlyPending;                                            ///< Indica si sólo se mostrarán las entidades con baja fiabilidad
        private bool SavingChanges;                                              ///< Indica si se están guardando los cambios         
        private bool AdminMode;                                                  ///< Indica si estamos en modo administrador

        #region Metodos externos

        static uint WM_CLOSE = 0x10;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        /// <summary>
        ///    Constructor.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            /* Comprobamos si se estamos en modo administrador */

            string[] commandLinesParams = Environment.GetCommandLineArgs();

            if (commandLinesParams.Length > 1)
                this.AdminMode = String.Equals(commandLinesParams[1], "admin");

            /* Si estamos en modo administrador, hacemos visibles los comando de escaneo de carpetas */
#if !DEBUG
            this.menuFileScan.Visible = this.AdminMode;
            this.toolFileScan.Visible = this.AdminMode;
#endif                                                                     
            /* Establecemos el texto que aparecerá en la cabecera de la aplicación */

            System.Reflection.AssemblyName assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            this.Text = String.Format("{0} v{1}", assembly.Name, assembly.Version.ToString(2));

            /* Identificador de usuario que se utilizará en todas las peticiones al servicio web de AcoustID */

            AcoustID.Configuration.ApiKey = theApp.Settings.AcoustIDApiKey;

            /* Establecemos las columnas a la vista del panel derecho */

            this.DisplayedColumns = new List<string>() { "Título", "Duración", "Álbum", "Año", "Artista(s)", "Estado" };
            DataGridViewCustomizer.SetColumns(this.dgvView, this.DisplayedColumns);

            /* Llenamos el combobox de la barra de herramientas con las columnas a mostrar */

            this.toolColumns.ComboBox.DataSource = this.DisplayedColumns;

            /* Llenamos el combobox de la barra de herramientas con las operaciones de filtrado disponibles */

            if (this.toolOperations.Items.Count == 0)
            {
                this.toolOperations.ComboBox.DataSource = new List<string>() {
                    "Empieza por", "No empieza por", "Termina por", "No termina por", 
                    "Contiene", "No contiene", "Es igual a", "No es igual a"
                };
            }

            this.SavingChanges = false;

            //this.ActiveControl = this.dgvView;

            /* Actualizamos la información de la vista */

            //this.UpdateView();               
        }

        #region Manejadores de eventos

        private void timer_Tick(object sender, EventArgs e)
        {
            /* Comprobamos el estado del servicio... */

            bool bInstalled = SCManager.ServiceIsInstalled("CAINSvc");
            this.menuServiceInstall.Enabled = !bInstalled;
            //this.menuServiceUninstall.Enabled = bInstalled;
            if (bInstalled)
            {
                ServiceState status = SCManager.GetServiceStatus("CAINSvc");
                this.menuServiceUninstall.Enabled = status == ServiceState.Stopped;
                this.menuServiceStart.Enabled = status == ServiceState.Stopped;
                this.menuServiceStop.Enabled = status == ServiceState.Running;
                //this.menuFileScan.Enabled = status == ServiceState.Stopped;
            }
            else
            {
                this.menuServiceUninstall.Enabled = false;
                this.menuServiceStart.Enabled = false;
                this.menuServiceStop.Enabled = false;
            }

            /* Si hemos seleccionado sólo una entidad y su estado es distinto a 'Sin resultados'... */

            if (this.SelectedIndexes.Count == 1 &&
                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Status != CAIN.Track.StatusTypes.NoResults)
            {
                this.menuFileFind.Enabled = true;
                this.toolFileFind.Enabled = true;
            }
            else
            {
                this.menuFileFind.Enabled = false;
                this.toolFileFind.Enabled = false;
            }

            /* Si hemos seleccionado al menos una entidad y su estado es distinto a 'Sin resultados'... */

            if (this.SelectedIndexes.Count > 0 &&
                this.SelectedEntities.TrueForAll(item => item.Track.Reliability <= 80 && item.Album.Year > 0))
            {
                this.menuFileConfirm.Enabled = true;
                this.toolFileConfirm.Enabled = true;
            }
            else
            {
                this.menuFileConfirm.Enabled = false;
                this.toolFileConfirm.Enabled = false;
            }

            /* Si hay al menos una entidad... */

            if (this.Entities.Count > 0)
            {
                this.menuFileColumns.Enabled = true;
                this.toolFileColumns.Enabled = true;
                this.menuFilePending.Enabled = this.Entities.Where(item => item.Track.Reliability <= 80).Count() > 0;
                this.toolFilePending.Enabled = this.Entities.Where(item => item.Track.Reliability <= 80).Count() > 0;
            }
            else
            {
                this.menuFileColumns.Enabled = false;
                this.toolFileColumns.Enabled = false;
                this.menuFilePending.Enabled = false;
                this.toolFilePending.Enabled = false;
            }

            /* Si hay entidades seleccionadas... */

            if (this.SelectedIndexes.Count > 0)
            {
                this.menuFilePlaylist.Enabled = true;
                this.toolFilePlaylist.Enabled = true;

                this.txtTitle.Enabled = true;
                this.txtDuration.Enabled = true;
                this.txtAlbum.Enabled = true;
                this.btnAlbum.Enabled = true;
                this.txtYear.Enabled = true;
                this.pbxCover.Enabled = true;
                this.pbxCover.BackColor = SystemColors.Window;
                this.txtArtists.Enabled = true;
                this.ltvTags.Enabled = true;
                if (this.ltvTags.Columns.Count == 0)
                {
                    this.ltvTags.Columns.Add("Nombre", (this.ltvTags.Width / 2) - 1);
                    this.ltvTags.Columns.Add("Valor", (this.ltvTags.Width / 2) - 1);
                }
                this.btnNewTag.Enabled = true;
            }
            else
            {
                this.menuFilePlaylist.Enabled = false;
                this.toolFilePlaylist.Enabled = false;

                this.txtTitle.Enabled = false;
                this.txtDuration.Enabled = false;
                this.txtAlbum.Enabled = false;
                this.btnAlbum.Enabled = false;
                this.txtYear.Enabled = false;
                this.pbxCover.Enabled = false;
                this.pbxCover.BackColor = SystemColors.ButtonFace;
                this.txtArtists.Enabled = false;
                this.ltvTags.Enabled = false;
                this.ltvTags.Columns.Clear();
                this.btnNewTag.Enabled = false;
            }

            /* Si hay etiquetas seleccionadas... */

            if (this.ltvTags.SelectedIndices.Count > 0)
            {
                this.btnEditTag.Enabled = true;
                this.btnDeleteTag.Enabled = true;
            }
            else
            {
                this.btnEditTag.Enabled = false;
                this.btnDeleteTag.Enabled = false;
            }

            /* Si hay cambios en el contenido de los controles del panel izquierdo... */

            if (this.HasLeftControlsChanges())
            {
                this.menuFileSave.Enabled = true;
                this.toolFileSave.Enabled = true;
            }
            else
            {
                this.menuFileSave.Enabled = false;
                this.toolFileSave.Enabled = false;
            }
        }

        #region Menú 'Archivo'

        private void menuFileScan_Click(object sender, EventArgs e)
        {
            /* Preguntamos al usuario asi quiere continuar */

            if (MessageBox.Show(this, "A continuación se iniciará el escaneo de las carpetas seleccionadas. Este procesó ELIMINARÁ toda la información que hubiera en la base de datos.\r\n¿Desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            /* Borramos la información de la base de datos y la carpeta de música */

            string PathScript = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\..\..\db\script.sql";

            if (Directory.Exists(theApp.Settings.PathDst))
                Directory.Delete(theApp.Settings.PathDst, true);

            MySqlScript script = new MySqlScript(theApp.Connection, File.ReadAllText(PathScript));
            script.Execute();

            /* Si no hay carpetas seleccionadas, se mostrará un aviso */

            if (theApp.Settings.FolderPaths.Count == 0)
            {
                MessageBox.Show(this, "No se han establecido carpetas donde realizar el escaneo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* Abrimos el diálogo que realizará el proceso de escaneo */

            frmScan frm = new frmScan();
            frm.ShowDialog();

            /* Actualizamos la vista */

            this.UpdateView();
        }

        private void menuFileUpdate_Click(object sender, EventArgs e)
        {
            /* Actualizamos la vista */

            this.UpdateView();
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            /* Indicamos que vamos a guardar los cambios */

            this.SavingChanges = true;

            int count = this.SelectedEntities.Count;

            /* Comprobamos que los valores a guardar son válidos */

            /*string message;
            if (!this.CurrentValues.IsValid(count, out message))
            {
                message = message.Insert(0, "Se ha producido los siguientes errores:\r\n");
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            /* Guardamos los cambios en las entidades */

            if ((count == 1 && !String.Equals(this.InitialValues.Title, this.CurrentValues.Title)) ||
                (count > 1 && !String.IsNullOrEmpty(this.CurrentValues.Title) && !String.Equals(this.InitialValues.Title, this.CurrentValues.Title)))
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.Track.Title = this.CurrentValues.Title;

            if ((count == 1 && !String.Equals(this.InitialValues.Duration, this.CurrentValues.Duration)) ||
                (count > 1 && !String.IsNullOrEmpty(this.CurrentValues.Duration) && !String.Equals(this.InitialValues.Duration, this.CurrentValues.Duration)))
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.Track.Duration = Convert.ToInt32(this.CurrentValues.Duration);

            if ((count == 1 && !String.Equals(this.InitialValues.Album, this.CurrentValues.Album)) ||
                (count > 1 && !String.IsNullOrEmpty(this.CurrentValues.Album) && !String.Equals(this.InitialValues.Album, this.CurrentValues.Album)))
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.Album.Title = this.CurrentValues.Album;

            if ((count == 1 && !String.Equals(this.InitialValues.Year, this.CurrentValues.Year)) ||
                (count > 1 && !String.IsNullOrEmpty(this.CurrentValues.Year) && !String.Equals(this.InitialValues.Year, this.CurrentValues.Year)))
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.Album.Year = Convert.ToInt32(this.CurrentValues.Year);

            if ((count == 1 && !CAIN.Utils.ImageEquals(this.InitialValues.Cover, this.CurrentValues.Cover)) ||
                (count > 1 && this.CurrentValues.Cover != null && !CAIN.Utils.ImageEquals(this.InitialValues.Cover, this.CurrentValues.Cover)))
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.Album.Cover = this.CurrentValues.Cover;

            if ((count == 1 && !String.Equals(this.InitialValues.Artists, this.CurrentValues.Artists)) ||
                (count > 1 && !String.IsNullOrEmpty(this.CurrentValues.Artists) && !String.Equals(this.InitialValues.Artists, this.CurrentValues.Artists)))
            {
                foreach (CAIN.Entity entity in this.SelectedEntities)
                    entity.GetArtistsFromString(this.CurrentValues.Artists);
            }

            if ((count == 1 && !CAIN.Tag.Equals(this.InitialValues.Tags, this.CurrentValues.Tags)) ||
                (count > 1 && this.CurrentValues.Tags.Count > 0 && !CAIN.Tag.Equals(this.InitialValues.Tags, this.CurrentValues.Tags)))
            {
                foreach (CAIN.Entity entity in this.SelectedEntities)
                {
                    if (count > 1)
                    {
                        foreach (CAIN.Tag tag in this.CurrentValues.Tags)
                        {
                            if (!entity.HasTag(tag))
                                entity.AddTag(tag);
                        }
                    }
                    else
                        entity.Tags = this.CurrentValues.Tags;
                }
            }

            /* Guardamos la entidades en la base de datos */

            if (theApp.DB.Edit(this.SelectedEntities))
            {
                /* Actualizamos la vista con los cambios */

                this.UpdateView();
            }

            /* Indicamos que hemos terminado de guardar los cambios */

            this.SavingChanges = false;
        }

        private void menuFileColumns_Click(object sender, EventArgs e)
        {
            /* Obtenemos las colunas disponibles (fijas + etiquetas) y las columnas que se están mostrando */

            List<string> availableColumns = this.GetAvailableColumns();
            List<string> displayedColumns = DataGridViewCustomizer.GetColumns(this.dgvView);

            /* Obtenemos las colunas disponibles (fijas + etiquetas) y las columnas que se están mostrando */

            frmColumns dlg = new frmColumns(availableColumns, displayedColumns);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                /* Obtenemos las columnas a mostrar */

                this.DisplayedColumns = dlg.GetDisplayedColumns();

                /* Establecemos las columnas a la vista del panel derecho */

                DataGridViewCustomizer.SetColumns(this.dgvView, this.DisplayedColumns);

                /* Llenamos el combobox de la barra de herramientas con las columnas a mostrar */

                this.toolColumns.ComboBox.DataSource = this.DisplayedColumns;

                /* Actualizamos la vista con los cambios */

                this.UpdateView();
            }
        }

        private void menuFileFind_Click(object sender, EventArgs e)
        {
            frmFind dlg = new frmFind(this.DisplayedEntities[this.SelectedIndexes[0]]);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CAIN.Entity entity = dlg.GetEntity();

                /* Si la información de la entidad seleccionada y la entidad a modificar es la misma, no continuamos */

                if (CAIN.Entity.Equals(this.DisplayedEntities[this.SelectedIndexes[0]], entity))
                    return;

                this.DisplayedEntities[this.SelectedIndexes[0]].Track.MBID = entity.Track.MBID;
                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Title = entity.Track.Title;
                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Duration = entity.Track.Duration;
                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Reliability = 100;// entity.Track.Reliability;
                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Status = CAIN.Track.StatusTypes.Cataloged;
                this.DisplayedEntities[this.SelectedIndexes[0]].Album.MBID = entity.Album.MBID;
                this.DisplayedEntities[this.SelectedIndexes[0]].Album.Title = entity.Album.Title;
                this.DisplayedEntities[this.SelectedIndexes[0]].Album.Year = entity.Album.Year;
                if (entity.Album.Cover != null)
                    this.DisplayedEntities[this.SelectedIndexes[0]].Album.Cover = entity.Album.Cover;
                this.DisplayedEntities[this.SelectedIndexes[0]].Artists = entity.Artists;

                /*int index = this.SelectedIndexes[0];

                DataGridViewCustomizer.UpdateRow(this.dgvView, index, this.DisplayedEntities[this.SelectedIndexes[0]]); 
                
                this.dgvView.ClearSelection();
                this.dgvView.Rows[index].Selected = true;*/

                /* Calculamos la nueva ubicación del archivo */

                string filename = Path.GetFileName(this.DisplayedEntities[this.SelectedIndexes[0]].Track.Path);

                string oldLocation = this.DisplayedEntities[this.SelectedIndexes[0]].Track.Path;
                string newLocation = CAIN.Utils.GetFullFileName(theApp.Settings.PathDst, filename, entity);

                this.DisplayedEntities[this.SelectedIndexes[0]].Track.Path = newLocation;

                /* Guardamos los cambios en la base de datos */

                if (theApp.DB.Edit(this.DisplayedEntities[this.SelectedIndexes[0]]))
                {
                    /* Si la ubicación ha cambiado, movemos el archivo (y borramos la carpeta si está vacía) */

                    if (newLocation != oldLocation)
                    {
                        CAIN.Utils.MoveFile(oldLocation, newLocation);
                        CAIN.Utils.RemoveEmptyDirs(theApp.Settings.PathDst);

                        /* Modificamos los metadatos del archivo (por si hubiera cambios) */

                        CAIN.Song.SaveMetadata(this.DisplayedEntities[this.SelectedIndexes[0]]);
                    }
                }

                /* Actualizamos la vista con los cambios */

                this.UpdateView();
            }
        }

        private void menuFilePending_Click(object sender, EventArgs e)
        {
            this.ShowOnlyPending = !this.toolFilePending.Checked;

            this.menuFilePending.Checked = !this.menuFilePending.Checked;
            this.toolFilePending.Checked = !this.toolFilePending.Checked;

            this.toolPattern.Text = String.Empty;
            this.FilterEntities();
        }

        private void menuFileConfirm_Click(object sender, EventArgs e)
        {
            /* Marcamos como catalogados las entidades seleccionadas */

            foreach (int index in this.SelectedIndexes)
            {
                this.DisplayedEntities[index].Track.Reliability = 100;
                this.DisplayedEntities[index].Track.Status = CAIN.Track.StatusTypes.Cataloged;
            }

            /* Actualizamos la vista con los cambios */

            if (theApp.DB.Edit(this.SelectedEntities))
            {
                /* Actualizamos la vista con los cambios */

                this.UpdateView();
            }
        }

        private void menuFilePlaylist_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dlg.Filter = "Lista de reproducción (*.M3U)|*.m3u|Lista de reproducción (*.M3U8)|*.m3u8|Lista de reproducción (*.PLS)|*.pls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(dlg.FileName))
                {
                    case ".m3u":
                        CAIN.Playlist.Save(dlg.FileName, this.SelectedEntities, CAIN.Playlist.FormatTypes.M3U);
                        break;
                    case ".m3u8":
                        CAIN.Playlist.Save(dlg.FileName, this.SelectedEntities, CAIN.Playlist.FormatTypes.M3U8);
                        break;
                    case ".pls":
                        CAIN.Playlist.Save(dlg.FileName, this.SelectedEntities, CAIN.Playlist.FormatTypes.PLS);
                        break;
                }

                MessageBox.Show(this, "La lista de reproducción se creó con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Menú 'Servicio'

        private void menuServiceInstall_Click(object sender, EventArgs e)
        {
            try
            {
                SCManager.Install("CAINSvc", "CAIN Service", AppDomain.CurrentDomain.BaseDirectory + "CAINService.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuServiceUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                SCManager.Uninstall("CAINSvc");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuServiceStart_Click(object sender, EventArgs e)
        {
            frmWait dlg = new frmWait("Iniciando el servicio. Espere, por favor...", true);
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnStartService;
            dlg.BKWorker.RunWorkerCompleted += OnStartServiceCompleted;
            dlg.ShowDialog();
        }

        private void OnStartServiceCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                MessageBox.Show(e.Result.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OnStartService(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int times = 0;
            int timeCount = 10;
            bool ok = false;
            string message = String.Empty;

            do
            {
                try
                {
                    SCManager.StartService("CAINSvc");
                    ok = true;
                }
                catch (Exception ex)
                {
                    if (times < timeCount)
                        Thread.Sleep(500);
                    else
                        message = ex.Message;
                }
                finally
                {
                    times++;
                }
            }
            while (!ok && times <= timeCount);

            if (!ok)
                e.Result = message;

            SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void menuServiceStop_Click(object sender, EventArgs e)
        {
            frmWait dlg = new frmWait("Parando el servicio. Espere, por favor...", true);
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnStopService;
            dlg.BKWorker.RunWorkerCompleted += OnStopServiceCompleted;
            dlg.ShowDialog();
        }

        private void OnStopServiceCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                MessageBox.Show((string)e.Result, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OnStopService(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int times = 0;
            int timeCount = 10;
            bool ok = false;
            string message = String.Empty;

            do
            {
                try
                {
                    SCManager.StopService("CAINSvc");
                    ok = true;
                }
                catch (Exception ex)
                {
                    if (times < timeCount)
                        Thread.Sleep(500);
                    else
                        message = ex.Message;
                }
                finally
                {
                    times++;
                }
            }
            while (!ok && times <= timeCount);

            if (!ok)
                e.Result = message;

            SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

        }

        private void menuServiceSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();

            /* Cargamos la información del archivo de configuración (por si hubiera cambios) */

            theApp.LoadSettings();
        }

        #endregion

        #region Menú 'Ayuda'

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        #endregion

        private void dgvView_SelectionChanged(object sender, EventArgs e)
        {
            /* Vaciamos las listas que gestionan las entidades seleccionadas */

            this.SelectedIndexes.Clear();
            this.SelectedEntities.Clear();

            /* Si no hay elementos seleccionados, vaciamos los controles del panel izquierdo y no continuamos */

            if (this.dgvView.SelectedRows.Count == 0)
            {
                this.ClearLeftPanelControls();
                return;
            }

            /* Llenamos las listas de posiciones y entidades seleccionadas (y las ordenamos) */

            List<DataGridViewRow> selectedRows = this.dgvView.SelectedRows.Cast<DataGridViewRow>().ToList();
            selectedRows = selectedRows.Where(item => item.Selected).OrderBy(item => item.Index).ToList();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                this.SelectedIndexes.Add(selectedRows[i].Index);
                this.SelectedEntities.Add(this.DisplayedEntities[selectedRows[i].Index]);
            }

            /* Llenamos los controles del panel izquierdo con la información de las entidades seleccionadas */

            this.FillLeftPanelControls();

            /* Actualizamos la información de la barra de estado */

            this.statusBar.Items["statusSelected"].Text = this.dgvView.SelectedRows.Count.ToString();
        }

        private void dgvView_MouseMove(object sender, MouseEventArgs e)
        {
            this.dgvView.Cursor = Cursors.Default;
        }

        private void dgvView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* Si el cursor está en la cabecera de la vista, lo cambiamos para indicar que se puede realizar una acción */

            this.dgvView.Cursor = e.RowIndex == -1 ? Cursors.Hand : Cursors.Default;
        }

        private void dgvView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.Button == MouseButtons.Right)
            {
                if (!this.dgvView.Rows[e.RowIndex].Selected)
                {
                    this.dgvView.ClearSelection();
                    this.dgvView.Rows[e.RowIndex].Selected = true;
                }

                this.contextMenu.Items["menuFind"].Enabled = this.SelectedEntities.Count == 1;
                this.contextMenu.Items["menuStatus"].Enabled = this.SelectedEntities.TrueForAll(item => item.Track.Reliability <= 80 && item.Album.Year > 0);
                this.contextMenu.Show(MousePosition);
            }
        }

        private void dgvView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* Si pulsados el botón izquierdo del ratón cuando el cursor está en la cabecera de la vista, 
             * ordenamos de forma ascendente/descendente las entidades en base a la columna donde se pulsó */

            if (e.Button == MouseButtons.Left)
            {
                DataGridViewCustomizer.SortByColumn(this.dgvView, e.ColumnIndex, ref this.DisplayedEntities);
                DataGridViewCustomizer.Fill(this.dgvView, this.DisplayedEntities);
                //this.dgvView.ClearSelection();
            }
        }

        private void toolColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DisplayedEntities.Count == 0 || String.IsNullOrEmpty(this.toolPattern.Text))
                return;

            this.toolPattern.Text = String.Empty;
            this.FilterEntities();
        }

        private void toolOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DisplayedEntities.Count == 0 || String.IsNullOrEmpty(this.toolPattern.Text))
                return;

            //this.toolPattern.Text = String.Empty;
            this.FilterEntities();
        }

        private void toolPattern_TextChanged(object sender, EventArgs e)
        {
            this.FilterEntities();
        }

        private void pbxCover_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Archivos de imagen (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(dlg.FileName);
                this.pbxCover.Image = img;
            }
        }

        private void btnAlbum_Click(object sender, EventArgs e)
        {
            frmAlbum dlg = new frmAlbum(this.Entities);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CAIN.Album album = dlg.GetAlbum();

                this.txtAlbum.Text = album.Title;
                this.txtYear.Text = album.Year.ToString();
                this.pbxCover.Image = album.Cover;
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            int count = this.SelectedEntities.Count;

            this.errorProvider1.SetError(this.txtTitle, String.Empty);
            if (count == 1 && String.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                this.errorProvider1.SetError(this.txtTitle, "El título no puede estar vacío.");
        }

        private void txtAlbum_TextChanged(object sender, EventArgs e)
        {
            int count = this.SelectedEntities.Count;

            this.errorProvider2.SetError(this.txtAlbum, String.Empty);
            if (count == 1 && String.IsNullOrEmpty(this.txtAlbum.Text.Trim()))
                this.errorProvider2.SetError(this.txtAlbum, "El álbum no puede estar vacío.");
        }

        private void txtDuration_TextChanged(object sender, EventArgs e)
        {
            int count = this.SelectedEntities.Count;

            this.errorProvider3.SetError(this.txtDuration, String.Empty);
            if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.txtDuration.Text.Trim())))
            {
                int duration = CAIN.Utils.GetSeconds(this.txtDuration.Text.Trim());
                if (duration == 0)
                    this.errorProvider3.SetError(this.txtDuration, "La duración tiene que tener el formato '[m]m:ss'.");
            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            int count = this.SelectedEntities.Count;

            this.errorProvider4.SetError(this.txtYear, String.Empty);
            if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.txtYear.Text.Trim())))
            {
                int year;// = 0;               
                int currentYear = DateTime.Now.Year;
                Int32.TryParse(this.txtYear.Text.Trim(), out year);
                if (count == 1 && (year < 1799 || year > currentYear))
                    this.errorProvider4.SetError(this.txtYear, String.Format("El año tiene que ser un valor comprendido entre 1799 y {0}.", currentYear));
            }
        }

        private void txtArtists_TextChanged(object sender, EventArgs e)
        {
            int count = this.SelectedEntities.Count;

            this.errorProvider5.SetError(this.txtArtists, String.Empty);
            if (count == 1 || (count > 1 && !String.IsNullOrEmpty(this.txtArtists.Text.Trim())))
            {
                List<string> artists = this.txtArtists.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                artists = artists.Where(item => !String.IsNullOrEmpty(item.Trim())).ToList();
                if (artists.Count == 0)
                    this.errorProvider5.SetError(this.txtArtists, "Los artistas no pueden estar vacíos");
            }
        }

        private void btnNewTag_Click(object sender, EventArgs e)
        {
            frmTag dlg = new frmTag(this.ltvTags.Items.Cast<ListViewItem>().ToList());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                List<string> items = new List<string>() { dlg.TagName, dlg.TagValue };
                this.ltvTags.Items.Add(new ListViewItem(items.ToArray()));
            }
        }

        private void btnEditTag_Click(object sender, EventArgs e)
        {
            int index = this.ltvTags.SelectedIndices[0];

            frmTag dlg = new frmTag(this.ltvTags.Items.Cast<ListViewItem>().ToList(), index);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.ltvTags.Items[index].SubItems[0].Text = dlg.TagName;
                this.ltvTags.Items[index].SubItems[1].Text = dlg.TagValue;
            }
        }

        private void btnDeleteTag_Click(object sender, EventArgs e)
        {
            this.ltvTags.Items.RemoveAt(this.ltvTags.SelectedIndices[0]);
        }

        private void ltvTags_DoubleClick(object sender, EventArgs e)
        {
            if (this.ltvTags.SelectedItems.Count > 0)
                btnEditTag_Click(this.ltvTags, null);
        }

        #endregion

        #region Métodos generales

        /// <summary>
        ///    Método que actualiza la tabla de entidades cargando la información desde la base de datos.
        /// </summary>        
        private void UpdateView()
        {
            /* Nos guardamos lal ista de entidades seleccionadas */

            List<int> selectedIndexes = new List<int>(this.SelectedIndexes);

            /* Quitamos la selección antes de lanzar el thead, sino salta una excepción */

            this.dgvView.ClearSelection();

            /* Mostramos un diálogo de espera que a su vez lanza un hilo que obtiene las entidades que hay en la base de datos */

            frmWait dlg = new frmWait("Actualizando el contenido de la vista. Espere, por favor...");
            dlg.BKWorker = new BackgroundWorker();
            dlg.BKWorker.DoWork += OnUpdateView;
            dlg.ShowDialog();

            /* Actualizamos el contenido de la vista del panel derecho */

            this.FilterEntities();

            /* Si había filas seleccionadas, las seleccionamos y hacemos scroll para que sean visibles */

            if (this.Entities.Count > 0 && selectedIndexes.Count > 0)
            {
                this.dgvView.ClearSelection();
                for (int i = 0; i < selectedIndexes.Count; i++)
                    this.dgvView.Rows[selectedIndexes[i]].Selected = true;

                this.dgvView.FirstDisplayedScrollingRowIndex = this.dgvView.SelectedRows[0].Index;
            }
        }

        /// <summary>
        ///    Método que actualiza el contenido de la vista del panel derecho, filtrando las entidades a mostrar si procede.
        /// </summary> 
        private void FilterEntities()
        {
            /* Si se ha establecido un patrón de filtrado, se filtran las entidades a mostrar; sino hay patrón de filtrado, se mostrarán todas las entidades */

            /*if (String.IsNullOrEmpty(this.toolPattern.Text))// && this.toolColumns.Items.Count == 0 && this.toolOperations.Items.Count == 0)
                this.DisplayedEntities = this.Entities;
            else*/
            this.DisplayedEntities = EntityFilter.Filter(this.dgvView, this.toolColumns.SelectedIndex, this.Entities, this.toolOperations.SelectedItem.ToString(), this.toolPattern.Text);

            /* Si sólo vamos a mostrar las entidades con una fiabilidad menor, filtramos las entidades a mostrar */

            if (this.ShowOnlyPending)
                this.DisplayedEntities = this.DisplayedEntities.Where(item => item.Track.Reliability <= 80).ToList();

            /* Llenamos la vista del panel derecho con las entidades a mostrar */

            DataGridViewCustomizer.Fill(this.dgvView, this.DisplayedEntities);
            //this.dgvView.ClearSelection();

            /* Actualizamos la barra de estado */

            this.statusBar.Items["statusNoCataloged"].Text = this.DisplayedEntities.Count(item => item.Track.Status != CAIN.Track.StatusTypes.Cataloged).ToString();
            this.statusBar.Items["statusCataloged"].Text = this.DisplayedEntities.Count(item => item.Track.Status == CAIN.Track.StatusTypes.Cataloged).ToString();
            this.statusBar.Items["statusDisplayed"].Text = this.DisplayedEntities.Count.ToString(); 
            this.statusBar.Items["statusTotal"].Text = this.Entities.Count.ToString();
        }

        /// <summary>
        ///    Callback que usa el hilo del diálogo de espera y que se encarga de cargar la información almacenada en la base de datos.
        /// </summary>        
        private void OnUpdateView(object sender, DoWorkEventArgs e)
        {
            /* Volcamos la información de la base de datos en la lista de entidades */

            this.Entities = theApp.DB.Load();

            /* Enviamos un mensaje para que se cierre el diálogo de espera */

            SendMessage((IntPtr)e.Argument, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        ///    Método que devuelve una lista de las columnas disponibles.
        /// </summary> 
        /// <returns>
        ///    La lista de columnas disponibles.
        /// </returns>       
        private List<string> GetAvailableColumns()
        {
            /* Primero las columnas fijas */

            List<string> columns = new List<string>() { "Título", "Duración", "Álbum", "Año", "Artista(s)", "Estado" };

            /* Segundo las columnas relacionadas con las etiquetas */

            List<string> tagNames = new List<string>();
            this.Entities.ForEach(item => tagNames.AddRange(item.Tags.Select(y => y.Name).ToList()));
            tagNames = tagNames.Distinct().ToList();

            columns.AddRange(tagNames);

            return columns;
        }

        /// <summary>
        ///    Borra el contenido de los controles del panel izquierdo.
        /// </summary>           
        public void ClearLeftPanelControls()
        {
            this.txtTitle.Text = String.Empty;
            this.txtDuration.Text = String.Empty;
            this.txtAlbum.Text = String.Empty;
            this.txtYear.Text = String.Empty;
            this.txtArtists.Text = String.Empty;
            this.pbxCover.Image = null;
            this.ltvTags.Items.Clear();

            this.InitialValues = new EditionPanelValues();
        }

        /// <summary>
        ///    LLena los controles del panel izquierdo con el contenido de los elementos seleccionados en la vista del panel derecho.
        /// </summary>           
        public void FillLeftPanelControls()
        {
            this.InitialValues.Title = GetTitleText();
            //string duration = GetDurationText();
            this.InitialValues.Duration = GetDurationText();
            this.InitialValues.Album = GetAlbumText();
            this.InitialValues.Year = GetYearText();
            this.InitialValues.Artists = GetArtistsText();
            this.InitialValues.Cover = GetCover();
            this.InitialValues.Tags = GetTags();

            this.txtTitle.Text = this.InitialValues.Title;
            this.txtDuration.Text = CAIN.Utils.FormatSeconds(this.InitialValues.Duration);
            this.txtAlbum.Text = this.InitialValues.Album;
            this.txtYear.Text = this.InitialValues.Year;
            this.txtArtists.Text = this.InitialValues.Artists;
            this.pbxCover.Image = this.InitialValues.Cover;
            this.ltvTags.Items.Clear();
            foreach (CAIN.Tag tag in this.InitialValues.Tags)
            {
                List<string> item = new List<string>() { tag.Name, tag.Value };
                this.ltvTags.Items.Add(new ListViewItem(item.ToArray()));
            }
        }

        /// <summary>
        ///    Obtiene la cadena de texto que se mostrará comparando el título de la canciones de todos los elementos seleccionados en la vista del panel derecho.
        /// </summary> 
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetTitleText()
        {
            List<string> elements = new List<string>(this.SelectedEntities.Select(item => item.Track.Title).ToList());
            return GetText(elements);
        }

        /// <summary>
        ///    Obtiene la cadena de texto que se mostrará comparando la duración de la canción de todos los elementos seleccionados en la vista del panel derecho.
        /// </summary> 
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetDurationText()
        {
            List<string> elements = new List<string>(this.SelectedEntities.Select(item => item.Track.Duration.ToString()).ToList());
            return GetText(elements);
        }

        /// <summary>
        ///    Obtiene la cadena de texto que se mostrará comparando el título del álbum de todos los elementos seleccionados en la vista del panel derecho.
        /// </summary> 
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetAlbumText()
        {
            List<string> elements = new List<string>(this.SelectedEntities.Select(item => item.Album.Title).ToList());
            return GetText(elements);
        }

        /// <summary>
        ///    Obtiene la cadena de texto que se mostrará comparando el año del álbum de todos los elementos seleccionados en la vista del panel derecho.
        /// </summary> 
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetYearText()
        {
            List<string> elements = new List<string>(this.SelectedEntities.Select(item => item.Album.Year.ToString()).ToList());
            return GetText(elements);
        }

        /// <summary>
        ///    Obtiene la cadena de texto que se mostrará comparando los artistas de todos los elementos seleccionados en la vista del panel derecho.
        /// </summary> 
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetArtistsText()
        {
            List<string> elements = new List<string>(this.SelectedEntities.Select(item => String.Join(", ", item.Artists.Select(y => y.Name))).ToList());
            return GetText(elements);
        }

        /// <summary>
        ///    Método que compara una lista de cadenas de texto y devuelve la cadena de texto coincidente o una cadena vacía si almenos una de las cadenas no coincide.
        /// </summary> 
        /// <param name="elements">
        ///    La lista de cadenas de texto a comparar.
        /// </param>
        /// <returns>
        ///    La cadena de texto, si sólo hay un elemento o coinciden todos. Una cadena vacía, sino.
        /// </returns>
        private string GetText(List<string> elements)
        {
            string text = elements[0];
            elements = elements.Skip(1).ToList();

            return elements.TrueForAll(item => String.Equals(text, item, StringComparison.OrdinalIgnoreCase)) ? text : String.Empty;
        }

        /// <summary>
        ///    Método que compara las carátulas de los álubmes de todos los elementos seleccionados en la vista del panel derecho y devuelve la carátula coincidente o un objeto nulo si almenos una de las carátulas no coincide.
        /// </summary>
        /// <returns>
        ///    La carátula, si sólo hay un elemento o coinciden todos. Un objeto nulo, sino.
        /// </returns>
        private Image GetCover()
        {
            List<Image> covers = this.SelectedEntities.Select(item => item.Album.Cover).ToList();

            Image cover = covers[0];
            covers = covers.Skip(1).ToList();

            return covers.TrueForAll(item => CAIN.Utils.ImageEquals(cover, item)) ? cover : null;
        }

        /// <summary>
        ///    Método que compara las listas de etiquetas de todos los elementos seleccionados en la vista del panel derecho y devuelve la lista de etiquetas coincidente o una lista vacía si almenos una de las carátulas no coincide.
        /// </summary>
        /// <returns>
        ///    La lista de etiquetas, si sólo hay un elemento o coinciden todos. Una lista vacía, sino.
        /// </returns>
        private List<CAIN.Tag> GetTags()
        {
            List<List<CAIN.Tag>> tags = this.SelectedEntities.Select(item => item.Tags).ToList();

            List<CAIN.Tag> tag = tags[0];
            tags = tags.Skip(1).ToList();

            return tags.TrueForAll(item => CAIN.Tag.Equals(tag, item)) ? tag : new List<CAIN.Tag>();
        }

        /// <summary>
        ///    Método que comprueba si se ha realizado cambios en el contenido de uno o varios controles del panel izquierdo.
        /// </summary> 
        /// <returns>
        ///    True, si se han producido cambios; False, sino.
        /// </returns>       
        private bool HasLeftControlsChanges()
        {
            /* Si estamos guardando cambios, no comprobaremos si hay cambios */

            if (this.SavingChanges)
                return true;

            /* Si hay errores de validación, no continuamos */

            if (!String.IsNullOrEmpty(this.errorProvider1.GetError(this.txtTitle)) ||
                !String.IsNullOrEmpty(this.errorProvider2.GetError(this.txtAlbum)) ||
                !String.IsNullOrEmpty(this.errorProvider3.GetError(this.txtDuration)) ||
                !String.IsNullOrEmpty(this.errorProvider4.GetError(this.txtYear)) ||
                !String.IsNullOrEmpty(this.errorProvider5.GetError(this.txtArtists)))
                return false;

            /* Obtenemos el contenido actual de los controles del panel izquierdo */

            this.CurrentValues = new EditionPanelValues();

            this.CurrentValues.Title = this.txtTitle.Text.Trim();
            int seconds = CAIN.Utils.GetSeconds(this.txtDuration.Text.Trim());
            this.CurrentValues.Duration = seconds > 0 ? seconds.ToString() : String.Empty;
            this.CurrentValues.Album = this.txtAlbum.Text.Trim();
            this.CurrentValues.Year = this.txtYear.Text.Trim();
            this.CurrentValues.Artists = this.txtArtists.Text.Trim();
            this.CurrentValues.Cover = this.pbxCover.Image;
            foreach (ListViewItem item in this.ltvTags.Items)
                this.CurrentValues.Tags.Add(new CAIN.Tag(item.SubItems[0].Text, item.SubItems[1].Text));

            /* Comparamos los estados inicial y actual para determinar si hay cambios */

            return !EditionPanelValues.Equals(this.InitialValues, this.CurrentValues);
        }

        #endregion
    }
}
