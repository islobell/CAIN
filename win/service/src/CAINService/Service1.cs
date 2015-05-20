using System;  
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CAINService
{
    public partial class Service1 : ServiceBase
    {
        string SettingsFilename; 
        private CAIN.Settings Settings; 
        private BackgroundWorker BKWorker; 
        CAIN.DBConnection Connection;
        private CAIN.DB DB;  
        private CAIN.Logger Log;

        public Service1()
        {
            InitializeComponent();

            /* Configuramos el servicio */

            this.AutoLog = false;
            this.CanStop = true;
            this.CanPauseAndContinue = false;

            /* Creamos el hilo de trabajo en segundo plano */
            
            this.BKWorker = new BackgroundWorker();
            this.BKWorker.DoWork += OnDoWork;
            this.BKWorker.WorkerSupportsCancellation = true; 
        }

        protected override void OnStart(string[] args)
        {
            /* Creamos el archivo de registro */

            string logFilename = String.Format(@"{0}\{1}_start.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString(@"dd-MM-yyyy_HH\.mm\.ss"));

            this.Log = new CAIN.Logger(logFilename);

            this.Log.WriteLine(String.Format(" {0} - El servicio ha sido iniciado por el usuario.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss"))); 

            /*Cargamos el archivo de configuración */

            this.SettingsFilename = AppDomain.CurrentDomain.BaseDirectory + @"\CAIN.conf";

            /*Comprobamos si existe el archivo de configuración */

            if (!File.Exists(this.SettingsFilename))
            {
                this.Log.WriteLine(String.Format(" {0} - El archivo de configuración no existe.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
                return;
            }

            this.Log.WriteLine(String.Format(" {0} - Cargando el archivo de configuración...", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));

            /*Cargamos el archivo de configuración */
            
            this.Settings = CAIN.Settings.Load(this.SettingsFilename);

//#if DEBUG
            this.Log.WriteLine(String.Empty);
            this.Log.WriteLine(" **********************************************************");
            this.Log.WriteLine(" **             PARÁMETROS DE CONFIGURACIÓN              **");
            this.Log.WriteLine(" **********************************************************");
            this.Log.WriteLine(this.Settings.ToString());
            this.Log.WriteLine(" **********************************************************");
            this.Log.WriteLine(String.Empty);
//#endif
            /* Si la configuración no es válida, no continuamos */

            if (!this.Settings.IsValid())
            {
                this.Log.WriteLine(String.Format(" {0} - La información del archivo de configuración no es válida.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
                return;
            }

            this.Log.WriteLine(String.Format(" {0} - El archivo de configuración se cargo correctamente.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));

            /* Abrimos la conexión con la base de datos */           

            this.Log.WriteLine(String.Format(" {0} - Estableciendo la conexión con la base de datos...", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));

            string str = "server=127.0.0.1; database=musicdb; uid=" + this.Settings.BDUser + "; pwd=" + this.Settings.DBPassword + ";";

            this.Connection = new CAIN.DBConnection(str);

            /* Si no se ha abierto la conexión con la base de datos, no continuamos */

            if (!this.Connection.IsOpen())
            {
                this.Log.WriteLine(String.Format(" {0} - {1}.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss"), this.Connection.Error));
                return;
            }

            /* Pasamos la conexión al objeto que trabajará con la base de datos */

            this.DB = new CAIN.DB(this.Connection);

            this.Log.WriteLine(String.Format(" {0} - La conexión a la base de datos se ha establecido correctamente.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss"))); 

            /* Asignamos la clave a la API de AcoustID para poder realizar peticiones a su servicio web */

            AcoustID.Configuration.ApiKey = this.Settings.AcoustIDApiKey;    

            this.Log.WriteLine(String.Format(" {0} - El servicio se ha iniciado correctamente.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));             
            this.Log.Dispose();

            Thread.Sleep(500);

            /* Iniciamos el proceso de catalogación */

            this.BKWorker.RunWorkerAsync();  
        }

        protected override void OnStop()
        {
            this.BKWorker.CancelAsync();

            /* Esperamos a que termine el hilo */

            while (this.BKWorker.IsBusy) 
                Thread.Sleep(100);
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.BKWorker.CancellationPending)
            {
                /* Creamos el archivo de registro */

                string logFilename = String.Format(@"{0}\{1}.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString(@"dd-MM-yyyy_HH\.mm\.ss"));

                this.Log = new CAIN.Logger(logFilename);
            
                /* Si vamos a escanear todo otra vez, borramos la información de la base de datos */

                if (this.Settings.Reset)
                {
                    this.DB.DeleteAll();
                    if (Directory.Exists(this.Settings.PathDst))
                        Directory.Delete(this.Settings.PathDst, true);
                }

                /* Calculamos el número de archivos a escanear */

                int fileCount = 0;
                foreach (string path in this.Settings.FolderPaths)
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

                this.Log.WriteLine(String.Empty);
                this.Log.WriteLine(" **********************************************************");
                this.Log.WriteLine(" **       CAIN: Catalogador de información musical       **");
                this.Log.WriteLine(" ** ---------------------------------------------------- **");
                this.Log.WriteLine(String.Format(" **                {0}                **", DateTime.Now.ToString(@"dd MMM yyyy - HH\:mm\:ss")));
                this.Log.WriteLine(" **********************************************************");
                this.Log.WriteLine(String.Empty);

                foreach (string path in this.Settings.FolderPaths)
                {
                    if (!Directory.Exists(path))
                    {
                        this.Log.WriteLine(" * La ruta '" + path + "' no existe.");
                        this.Log.WriteLine(String.Empty);
                    }

                    this.Log.WriteLine(" **********************************************************");
                    this.Log.WriteLine(" Carpeta: " + path);
                    this.Log.WriteLine(" **********************************************************");
                    this.Log.WriteLine(String.Empty);

                    /* Cargamos el archivo IDX */

                    string idxFile = path + @"\files.idx";

                    /* Si vamos a escanear todo otra vez, borramos el archivo IDX */

                    if (this.Settings.Reset)
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
                            this.Log.WriteLine(String.Format(" {0}", file.Name));//.FullName));

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

                                /* Lo añadimos al archivo IDX */

                                CAIN.IDXFile.Add(idxFile, hash);

                                /* Si el archivo ya ha sido escaneado, no continuamos */

                                if (hashes.Contains(hash))
                                {
                                    alreadyCataloguedFiles++;
                                    this.Log.WriteLine(" * El archivo ya fue escaneado.");
                                    continue;
                                }

                                /* Obtenemos los metadatos de AcoustID */

                                CAIN.Song.GetMetadataFromInternet(f, (int)this.Settings.Mode, storedAlbums, out entity, this.Log);
                            }

                            /* Si la entidad ya existe en la base de datos, no continuamos */

                            if (this.DB.Exists(entity))
                            {
                                alreadyCataloguedFiles++;
                                this.Log.WriteLine(" * El archivo ya existe en la base de datos.");
                                continue;
                            }

                            if (!String.IsNullOrEmpty(entity.Album.MBID) && storedAlbums.Count(item => item.MBID == entity.Album.MBID) == 0)
                                storedAlbums.Add(entity.Album);

                            /* Establecemos la carpeta donde se guardará el archivo */

                            entity.Track.Path = CAIN.Utils.GetFullFileName(this.Settings.PathDst, file.Name, entity);

                            /* Guardamos la entidad en la base de datos */

                            if (this.DB.Insert(entity))
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
                            this.Log.WriteLine(" * " + ex.Message);
                        }
                        finally
                        {
                            /* Liberamos la memoria usada por el fichero */

                            totalFiles++;
                            //Debug.Assert(totalFiles == cataloguedFiles + notCataloguedFiles + alreadyCataloguedFiles + errorFiles);

                            this.Log.WriteLine(String.Empty);
                            this.Log.WriteLine(" -------------------------===$$===-------------------------");
                            this.Log.WriteLine(String.Empty);
                        }

                        /* Si el usuario a cancelado el proceso, no continuamos */

                        if (this.BKWorker.CancellationPending)
                            break;
                    }

                    /* Si no hay más archivos en la carpeta, actualizamos el archivo IDX */

                    //CAIN.IDXFile.Save(idxFile, hashes);

                    /* Si el usuario a cancelado el proceso, no continuamos */

                    if (this.BKWorker.CancellationPending)
                    {
                        e.Cancel = true; 
                        this.Log.WriteLine(String.Empty);
                        this.Log.WriteLine(String.Format(" {0} - El proceso de catalogación ha sido cancelado por el usuario.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
                        break;
                    }
                }

                /* Si terminamos el proceso de catalogación, guardamos las estadísticas */

                this.Log.WriteLine(" ----------------------------------------------------------");
                this.Log.WriteLine(" --              RESUMEN DE LA CATALOGACIÓN              --");
                this.Log.WriteLine(" ----------------------------------------------------------");
                this.Log.WriteLine(String.Format("         Archivos catalogados   : {0,5:d} - {1,7:0.00%}", cataloguedFiles, (float)cataloguedFiles / totalFiles));
                this.Log.WriteLine(String.Format("         Archivos no catalogados: {0,5:d} - {1,7:0.00%}", notCataloguedFiles, (float)notCataloguedFiles / totalFiles));
                this.Log.WriteLine(String.Format("         Archivos ya catalogados: {0,5:d} - {1,7:0.00%}", alreadyCataloguedFiles, (float)alreadyCataloguedFiles / totalFiles));
                this.Log.WriteLine(String.Format("         Archivos erróneos      : {0,5:d} - {1,7:0.00%}", errorFiles, (float)errorFiles / totalFiles));
                this.Log.WriteLine(" ----------------------------------------------------------");
                this.Log.WriteLine(String.Format("         TOTAL                  : {0,5:d} - {1,7:0.00%}", totalFiles, 1.0f));
                this.Log.WriteLine(" ----------------------------------------------------------");
                this.Log.WriteLine(String.Empty);

                if (!this.BKWorker.CancellationPending)
                    this.Log.WriteLine(String.Format(" {0} - El proceso de catalogación terminó con éxito.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
            
                /* Si el proceso de catalogación fue reseteado, lo cambiamos y guardamos los cambios en el archivo de configuración */

                if (this.Settings.Reset)
                {
                    this.Settings.Reset = false;
                    this.Settings.Save(this.SettingsFilename);
                }

                /* Esperamos al próximo ciclo */
                                                  
                long intervalInMinutes = this.Settings.Interval / (60 * 1000);
                this.Log.WriteLine(String.Format(" {0} - Esperando para iniciar el proceso de catalogación de nuevo (tiempo de espera: {1} minuto(s)).", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss"), intervalInMinutes));
                
                DateTime when = DateTime.Now;
                TimeSpan elapsed = new TimeSpan();      

                while (elapsed.TotalMinutes < intervalInMinutes)
                {
                    System.Threading.Thread.Sleep(1000);
                    elapsed = DateTime.Now - when;

                    if (this.BKWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        this.Log.WriteLine(String.Format(" {0} - El proceso de catalogación ha sido cancelado por el usuario.", DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss")));
                        break;
                    }
                }

                this.Log.Dispose();
            }
        } 
    }
}
