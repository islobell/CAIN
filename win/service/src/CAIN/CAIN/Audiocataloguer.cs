using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para la catalogación de los archivos de audio.
    /// </summary>    
    class AudioCataloguer
    {
        /// <summary>
        ///    Realiza el proceso de catalogación.
        /// </summary>
        public void DoWork()
        {
            int w = Math.Max(150, Console.LargestWindowWidth - 20);
            int h = Math.Min(60, Console.LargestWindowHeight);
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, 1000);

            Console.WriteLine(String.Empty);
            Console.WriteLine(" **********************************************************");
            Console.WriteLine(" **       CAIN: Catalogador de información musical       **");
            Console.WriteLine(" ** ---------------------------------------------------- **");
            Console.WriteLine(String.Format(" **                {0}                **", DateTime.Now.ToString(@"dd MMM yyyy - HH\:mm\:ss")));
            Console.WriteLine(" **********************************************************");
            Console.WriteLine(String.Empty);

            Console.WriteLine(" Opciones de catalogación");
            Console.WriteLine(" ------------------------");
            Console.WriteLine(" 0 - Obtener el álbum que aparece en los metadatos.");
            Console.WriteLine(" 1 - Obtener el álbum original.");
            Console.WriteLine(String.Empty);
            Console.Write(" Elige una opción [0-1]: ");

            ConsoleKey[] keys = new ConsoleKey[] { ConsoleKey.D0, ConsoleKey.D1 };
            ConsoleKeyInfo key = Utils.ReadConsoleKey(true, keys);

            int option = Int32.Parse(key.KeyChar.ToString());

            Console.WriteLine(String.Empty);
            Console.WriteLine(String.Empty);
            Console.WriteLine(" **======================================================**");
            Console.WriteLine(String.Empty);

            //Console.SetCursorPosition(0, Console.CursorTop + 4);

            /* Leemos el fichero de configuración */

            string confFile = AppDomain.CurrentDomain.BaseDirectory + @"\CAIN.conf";

            Settings settings = Settings.Load(confFile);

            /* Si ha habido algun problema al cargar el archivo de configuración, no continuamos */

            if (!settings.IsValid())
            {
                Console.WriteLine(String.Empty);
                //Console.WriteLine(" *************************************************************************************************");
                Console.WriteLine(" ** Error al cargar el archivo de configuración (no existe o no tiene los parámetros esperados) **");
                //Console.WriteLine(" *************************************************************************************************");
                Console.Write(" Pulse una tecla para continuar... ");//Press any key to continue...");
                Console.ReadKey(true);
                return;
            }

            /* Identificador de usuario que se utilizará en todas las peticiones al servicio web de AcoustID */

            AcoustID.Configuration.ApiKey = settings.AcoustIDApiKey;

            /* Licencia freeware para usar la librería Bass.NET sin que salga la molesta pantalla de bienvenida (hay que registrarse antes de usar la librería) */

            Un4seen.Bass.BassNet.Registration(settings.BassNetEmail, settings.BassNetApiKey);

            /* Inicialización de contadores, banderas y demás variables */

            AudioFile f = null;
            List<Album> storedAlbums = new List<Album>();
            int cataloguedFiles = 0;
            int notCataloguedFiles = 0;
            int alreadyCataloguedFiles = 0;
            int errorFiles = 0;
            int totalFiles = 0;
            bool error = false;
            bool exit = false;

            /* Recorremos la lista de carpetas */

            foreach (string path in settings.FolderPaths)
            {
                /* Comprobamos que la carpeta existe; si no es así, no continuamos */

                if (!Directory.Exists(path))
                {
                    Console.WriteLine(" * La ruta '" + path + "' no existe.");
                    Console.WriteLine(String.Empty);
                    continue;
                }

                //Console.WriteLine(String.Empty);
                Console.WriteLine(" **********************************************************");
                Console.WriteLine(" Carpeta: " + path);
                Console.WriteLine(" **********************************************************");
                Console.WriteLine(String.Empty);

                /* Cargamos el archivo IDX */

                string idxFile = path + @"\files.idx";

                List<string> hashes = IDXFile.Load(idxFile);

                /* Obtenemos la lista de archivos que hay en la carpeta (menos los archivos ocultos) */

                //List<FileInfo> files = new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0).ToList();
                IEnumerable<FileInfo> files = new DirectoryInfo(path).EnumerateFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0);
                
                /* Si no hay archivos en la carpeta o si el número de archivos no se corresponde con el número de códigos MD5, 
                    vaciamos la lista de hashes y borramos el archivo IDX (si existe) */

                if (files.Count() == 0 || files.Count() != hashes.Count)
                {
                    hashes.Clear();
                    File.Delete(idxFile);
                }

                /* Recorremos la lista de archivos */

                foreach (FileInfo file in files)
                {
                    try
                    {
                        Console.WriteLine(String.Format(" {0}", file.Name));

                        /* Leemos el archivo */

                        f = new AudioFile(file.FullName);

                        /* Comprobamos si se ha producido algún error; si es así, no continuamos */

                        if (f.IsNull())
                        {
                            errorFiles++;
                            throw new Exception(f.Error);
                        }

                        /* Calculamos el código MD5 del archivo */

                        string hash = f.CalculateMD5Hash();

                        //Console.WriteLine(" MD5: " + hash);

                        /* Comprobamos si existe el código MD5 en la lista de códigos MD5; si es así, no continuamos */

                        if (hashes.Contains(hash))
                        {
                            alreadyCataloguedFiles++;
                            throw new Exception("El archivo ya fue escaneado.");//File already was scanned.");
                        }

                        /* Añadimos el código MD5 del archivo a la lista de códigos MD5 */

                        hashes.Add(hash);

                        /* Calculamos la huella digital del fichero */

                        f.CalculateFingerprint();

                        //Console.WriteLine("Fingerprint: " + f.Fingerprint);

                        /* Obtenemos el identificador (MBID) del fichero */

                        f.GetTrackIdFromFingerprint();

                        //Console.WriteLine(" Id : " + f.Id);

                        /* Obtenemos los metadatos de AcoustID */

                        Album album;
                        f.GetMetadataFromInternet(option, storedAlbums, out album);

                        /* si el album existe y no está en la lista de álbumes catalogados, lo añadimos a la lista */

                        if (album != null && storedAlbums.Count(item => item.Id == album.Id) == 0)
                            storedAlbums.Add(album);

                        /* actualizamos los contadores */

                        bool catalogued = album != null ? true : false;
                        cataloguedFiles += catalogued ? 1 : 0;
                        notCataloguedFiles += !catalogued ? 1 : 0;
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        Console.WriteLine(" * " + ex.Message);
#if DEBUG
                        // Get stack trace for the exception with source file information
                        var st = new StackTrace(ex, true);
                        // Get the top stack frame
                        var frame = st.GetFrame(0);
                        // Get the line number from the stack frame
                        var filename = frame.GetFileName();
                        // Get the line number from the stack frame
                        var method = frame.GetMethod();
                        // Get the line number from the stack frame
                        var line = frame.GetFileLineNumber();
#endif
                    }
                    finally
                    {
                        /* Liberamos la memoria usada por el fichero */

                        f.Dispose();

                        totalFiles++;

                        Console.WriteLine(String.Empty);
                        Console.WriteLine(" -------------------------===$$===-------------------------");
                        Console.WriteLine(String.Empty);

                        /* Si se ha producido un error, continuamos sin preguntar */

                        if (!error)
                        {
                            Console.Write(" Pulsa ENTER para continuar o ESC para cancelar... ");
                            keys = new ConsoleKey[] { ConsoleKey.Enter, ConsoleKey.Escape };
                            exit = Utils.ReadConsoleKey(false, keys).Key == ConsoleKey.Escape;

                            Utils.DeleteConsoleLines(1);
                        }

                        error = false;
                    }

                    /* Si el usuario a cancelado el proceso, no continuamos */

                    if (exit) 
                        break;
                }

                /* Si no hay más archivos en la carpeta, actualizamos el archivo IDX */

                IDXFile.Save(idxFile, hashes);

                /* Si el usuario a cancelado el proceso, no continuamos */

                if (exit) 
                    break;
            }

            /* Si terminamos el proceso de catalogación, mostramos las estadísticas */

            if (exit)
            {

                Debug.Assert(totalFiles == cataloguedFiles + notCataloguedFiles + alreadyCataloguedFiles + errorFiles);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" ----------------------------------------------------------");
                sb.AppendLine(" --              RESUMEN DE LA CATALOGACIÓN              --");
                sb.AppendLine(" ----------------------------------------------------------");
                sb.AppendLine(String.Format("         Archivos catalogados   : {0,5:d} - {1,7:0.00%}", cataloguedFiles, (float)cataloguedFiles / totalFiles));
                sb.AppendLine(String.Format("         Archivos no catalogados: {0,5:d} - {1,7:0.00%}", notCataloguedFiles, (float)notCataloguedFiles / totalFiles));
                sb.AppendLine(String.Format("         Archivos ya catalogados: {0,5:d} - {1,7:0.00%}", alreadyCataloguedFiles, (float)alreadyCataloguedFiles / totalFiles));
                sb.AppendLine(String.Format("         Archivos erróneos      : {0,5:d} - {1,7:0.00%}", errorFiles, (float)errorFiles / totalFiles));
                sb.AppendLine(" ----------------------------------------------------------");
                sb.AppendLine(String.Format("         TOTAL                  : {0,5:d} - {1,7:0.00%}", totalFiles, 1.0f));
                sb.AppendLine(" ----------------------------------------------------------");
                sb.AppendLine(String.Empty);

                Console.Write(sb.ToString());
            }

            Console.Write(" Pulse una tecla para salir... ");
            Console.ReadKey(true);
        }
    }
}
