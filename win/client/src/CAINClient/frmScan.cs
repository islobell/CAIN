using System; 
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms; 
using System.Diagnostics;
using CAINClient.Properties;
using System.Drawing;

namespace CAINClient
{
    /// <summary>
    ///    Diálogo para realizar la catalogación de las canciones.
    /// </summary>
    public partial class frmScan : Form
    {
        SingletonApp theApp = SingletonApp.Instance;
        
        /// <summary>
        ///    Constructor.
        /// </summary>     
        public frmScan()
        {
            InitializeComponent();

            /* En el control del log no podrán seleccionarse elementos  */
            
            this.lbxLog.SelectionMode = SelectionMode.None;
            
            /* Creamos el hilo en segundo plano y lo configuramos */

            this.BKWorker.RunWorkerAsync();

            /* Actualizamos el estado de los botones */

            this.btnStart.Enabled = false;
            this.btnCancel.Text = "Cancelar";
            //this.btnCancel.Enabled = true;
        }

        #region Manejadores de eventos

        /// <summary>
        ///    Manejador el evento 'DoWork' del hilo.
        /// </summary>        
        private void BKWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            /* Si vamos a escanear todo otra vez, borramos la información de la base de datos y la carpeta con las canciones catalogadas */

            if (theApp.Settings.Reset)
            {
                if (theApp.DB.DeleteAll())
                {
                    if (Directory.Exists(theApp.Settings.PathDst))
                        Directory.Delete(theApp.Settings.PathDst, true);
                }
            }

            /* Creamos el log */

            CAIN.Logger log = new CAIN.Logger(this.lbxLog);

            /* Calculamos el número de archivos a escanear */

            int fileCount = 0;
            foreach (string path in theApp.Settings.FolderPaths)
            {
                if (!Directory.Exists(path)) continue;
                fileCount += new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0).Count();
            }

            /* Inicializamos las variables que vamos a usar */

            List<CAIN.Album> storedAlbums = new List<CAIN.Album>();
            int cataloguedFiles = 0;
            int notCataloguedFiles = 0;
            int alreadyCataloguedFiles = 0;
            int errorFiles = 0;
            int totalFiles = 0;

            log.WriteLine(String.Empty);
            log.WriteLine(" **********************************************************");
            log.WriteLine(" **       CAIN: Catalogador de información musical       **");
            log.WriteLine(" ** ---------------------------------------------------- **");
            log.WriteLine(String.Format(" **                {0}                **", DateTime.Now.ToString(@"dd MMM yyyy - HH\:mm\:ss")));
            log.WriteLine(" **********************************************************");
            log.WriteLine(String.Empty);

            foreach (string path in theApp.Settings.FolderPaths)
            {     
                if (!Directory.Exists(path))
                {
                    log.WriteLine(" * La ruta '" + path + "' no existe.");
                    log.WriteLine(String.Empty);
                }

                log.WriteLine(" **********************************************************\r\n");
                log.WriteLine(" Carpeta: " + path + "\r\n");
                log.WriteLine(" **********************************************************\r\n");
                log.WriteLine(String.Empty);
                
                /* Cargamos el archivo IDX */

                string idxFile = path + @"\files.idx";  
#if DEBUG
                //SOLO PRUEBAS !!!
                File.Delete(idxFile);
#endif
                /* Si vamos a escanear todo otra vez, borramos el archivo IDX */   

                if (theApp.Settings.Reset)
                    File.Delete(idxFile);

                List<string> hashes = CAIN.IDXFile.Load(idxFile);

                /* Obtenemos la lista de archivos que hay en la carpeta (menos los archivos ocultos) */

                //List<FileInfo> files = new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0).ToList();
                IEnumerable<FileInfo> files = new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0);

                /* Recorremos la lista de archivos */

                foreach (FileInfo file in files)
                {
                    try
                    {
                        this.lblStatus.Invoke(new Action(() => this.lblStatus.Text = file.FullName));
                        log.WriteLine(String.Format(" {0}", file.Name));//.FullName));

                        /* Leemos el archivo */

                        CAIN.Entity entity;
                        string srcPath = file.FullName;

                        /* Comprobamos si se ha producido algún error; si es así, no continuamos */

                        string error;
                        using (TagLib.File f = CAIN.Song.Create(file.FullName, out error))
                        {
                            /* Si se ha producido algún problema al leer el archivo, no continuamos */

                            if (f == null)
                                throw new Exception(error);

                            /* Si no es un archivo de audio, no continuamos */

                            if (!CAIN.Song.IsAudio(f))
                                throw new Exception("El archivo no es de audio.");

                            /* Si el archivo no contiene metadatos, no continuamos */

                            if (!CAIN.Song.HasTags(f))
                                throw new Exception("El archivo no contiene metadatos.");

                            /* Calculamos el código MD5 del archivo */

                            string hash = CAIN.Song.CalculateMD5Hash(f);

                            /* Si el archivo ya ha sido escaneado, no continuamos */

                            if (hashes.Contains(hash))
                            {
                                alreadyCataloguedFiles++;
                                log.WriteLine(" * El archivo ya fue escaneado.");
                                continue;
                            }

                            /* Obtenemos los metadatos de la base de datos de AcoustID */

                            CAIN.Song.GetMetadataFromInternet(f, (int)theApp.Settings.Mode, storedAlbums, out entity, log);
                        }

                        /* Si la entidad ya existe en la base de datos, no continuamos */

                        if (theApp.DB.Exists(entity))
                        {
                            alreadyCataloguedFiles++;
                            log.WriteLine(" * El archivo ya existe en la base de datos.");
                            continue;
                        }

                        if (!String.IsNullOrEmpty(entity.Album.MBID) && storedAlbums.Count(item => item.MBID == entity.Album.MBID) == 0)
                            storedAlbums.Add(entity.Album);

                        /* Establecemos la carpeta donde se guardará el archivo */

                        entity.Track.Path = CAIN.Utils.GetFullFileName(theApp.Settings.PathDst, file.Name, entity);

                        /* Guardamos la entidad en la base de datos */

                        if (theApp.DB.Insert(entity))
                        {
                            /* Guardamos el archivo en su ubicación final */

                            CAIN.Utils.CopyFile(srcPath, entity.Track.Path);
                                    
                            /* Guardamos los metadatos en el archivo de audio */

                            CAIN.Song.SaveMetadata(entity);
                        }

                        /* Actualizamos los contadores */

                        bool catalogued = entity.Track.Status == CAIN.Track.StatusTypes.Cataloged;
                        cataloguedFiles += catalogued ? 1 : 0;
                        notCataloguedFiles += !catalogued ? 1 : 0;
                    }
                    catch (Exception ex)
                    {
                        errorFiles++;
                        log.WriteLine(" * " + ex.Message);
                    }
                    finally
                    {
                        /* Liberamos la memoria usada por el fichero */

                        totalFiles++;
                        Debug.Assert(totalFiles == cataloguedFiles + notCataloguedFiles + alreadyCataloguedFiles + errorFiles);

                        log.WriteLine(String.Empty);
                        log.WriteLine(" -------------------------===$$===-------------------------");
                        log.WriteLine(String.Empty);

                        /* Calculamos el progreso actual */

                        float progress = (float)totalFiles / fileCount * 100;
                        this.BKWorker.ReportProgress((int)progress);
                    }

                    /* Si el usuario a cancelado el proceso, no continuamos */

                    if (this.BKWorker.CancellationPending)
                        break;
                }

                /* Si no hay más archivos en la carpeta, actualizamos el archivo IDX */

                CAIN.IDXFile.Save(idxFile, hashes);

                /* Si el usuario a cancelado el proceso, no continuamos */

                if (this.BKWorker.CancellationPending)
                {
                    e.Cancel = true;
                    log.WriteLine(" *****        Proceso cancelado por el usuario        *****");
                    log.WriteLine(String.Empty);
                    break;
                }
            }

            /* Si terminamos el proceso de catalogación, mostramos las estadísticas */

            Debug.Assert(totalFiles == cataloguedFiles + notCataloguedFiles + alreadyCataloguedFiles + errorFiles);

            log.WriteLine(" ----------------------------------------------------------");
            log.WriteLine(" --              RESUMEN DE LA CATALOGACIÓN              --");
            log.WriteLine(" ----------------------------------------------------------");
            log.WriteLine(String.Format("         Archivos catalogados   : {0,5:d} - {1,7:0.00%}", cataloguedFiles, (float)cataloguedFiles / totalFiles));
            log.WriteLine(String.Format("         Archivos no catalogados: {0,5:d} - {1,7:0.00%}", notCataloguedFiles, (float)notCataloguedFiles / totalFiles));
            log.WriteLine(String.Format("         Archivos ya catalogados: {0,5:d} - {1,7:0.00%}", alreadyCataloguedFiles, (float)alreadyCataloguedFiles / totalFiles));
            log.WriteLine(String.Format("         Archivos erróneos      : {0,5:d} - {1,7:0.00%}", errorFiles, (float)errorFiles / totalFiles));
            log.WriteLine(" ----------------------------------------------------------");
            log.WriteLine(String.Format("         TOTAL                  : {0,5:d} - {1,7:0.00%}", totalFiles, 1.0f));
            log.WriteLine(" ----------------------------------------------------------");
            log.WriteLine(String.Empty);
        }

        private void BKWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pbrProgress.Value = e.ProgressPercentage;
        }

        private void BKWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /* Primero miramos si se ha producido una excepción */

            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK);
            }
            else
            {
                if (e.Cancelled)
                    this.lblStatus.Text = "La catalogación ha sido cancelada por el usuario.";
                else
                    this.lblStatus.Text = "La catalogación finalizó con éxito.";

                /* Si el proceso de catalogación fue reseteado, lo cambiamos y guardamos los cambios en el archivo de configuración */

                if (theApp.Settings.Reset)
                {
                    theApp.Settings.Reset = false;
                    theApp.SaveSettings();
                }
            }

            this.btnStart.Enabled = true;
            this.btnCancel.Text = "Cerrar";
            this.lbxLog.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /* Vaciamos el ListBox */

            this.lbxLog.Items.Clear();

            /* Iniciamos el hilo */

            this.BKWorker.RunWorkerAsync(); 
            
            /* Deshabilitamos los botones */

            this.btnStart.Enabled = false;
            this.btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {         
            /* Cancelamos el hilo */

            if (this.btnCancel.Text == "Cancelar")
                this.BKWorker.CancelAsync();
            else
                this.Close();
        }

        private void chkShowLog_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = this.chkShowLog.Checked ? 500 : 163; 
            this.Top = (Application.OpenForms["frmMain"].Top + Application.OpenForms["frmMain"].Height / 2) - this.Height / 2;
        }

        private void frmSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Si el hilo está ocupado, mostramos una advertencia y no cerramos el diálogo */

            if (this.BKWorker.IsBusy)
            {
                MessageBox.Show(this, "Un proceso en segundo plano está ejecutándose. Cancele el proceso antes de cerrar el diálogo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        #endregion
    }
}
