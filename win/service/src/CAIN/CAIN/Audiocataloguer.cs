using System;
using System.IO;
using System.Linq;
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
            /* Leemos el fichero de configuración */

            string confFile = AppDomain.CurrentDomain.BaseDirectory + @"\CAIN.conf";

            Settings settings = Settings.Load(confFile);

            /* Si ha habido algun problema al cargar el archivo de configuración, no continuamos */

            if (!settings.IsValid())
            {
                Console.WriteLine("**************************************************************************");
                Console.WriteLine("* El archivo de configuración no existe o no se ha cargado correctamente *");
                Console.WriteLine("**************************************************************************");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                return;
            }

            /* Identificador de usuario que se utilizará en todas las peticiones al servicio web de AcoustID */

            AcoustID.Configuration.ApiKey = settings.AcoustIDApiKey;

            /* Licencia freeware para usar la librería Bass.NET sin que salga la molesta pantalla de bienvenida (hay que registrarse antes de usar la librería) */

            Un4seen.Bass.BassNet.Registration(settings.BassNetEmail, settings.BassNetApiKey);

            /* Recorremos la lista de carpetas */

            AudioFile f = null;

            /* Recorremos la lista de carpetas */

            foreach (string path in settings.FolderPaths)
            {
                /* Comprobamos que la carpeta existe; si no es así, no continuamos */

                if (!Directory.Exists(path))
                {
                    Console.WriteLine(" * Folder '" + path + "' not exists!");
                    continue;
                }

                Console.WriteLine("**********************************************************");
                Console.WriteLine("Folder: " + path);
                Console.WriteLine("**********************************************************");

                /* Cargamos el archivo IDX */

                string idxFile = path + @"\files.idx";

                List<string> hashes = IDXFile.Load(idxFile);

                /* Obtenemos la lista de archivos que hay en la carpeta (menos los archivos ocultos) */

                List<FileInfo> files = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).Where(p => (p.Attributes & FileAttributes.Hidden) == 0).ToList();
                
                /* Si no hay archivos en la carpeta o si el número de archivos no se corresponde con el número de códigos MD5, 
                    vaciamos la lista de hashes y borramos el archivo IDX (si existe) */

                if (files.Count == 0 || files.Count != hashes.Count)
                {
                    hashes.Clear();
                    File.Delete(idxFile);
                }

                /* Recorremos la lista de archivos */

                foreach (FileInfo file in files)
                {
                    try
                    {
                        Console.WriteLine(file.Name);

                        /* Leemos el archivo */

                        f = new AudioFile(file.FullName);

                        /* Comprobamos si se ha producido algún error; si es así, no continuamos */

                        if (f.IsNull()) throw new Exception(f.Error);

                        /* Calculamos el código MD5 del archivo */

                        string hash = f.CalculateMD5Hash();

                        Console.WriteLine("MD5: " + hash);

                        /* Comprobamos si existe el código MD5 en la lista de códigos MD5; si es así, no continuamos */

                        if (hashes.Contains(hash)) throw new Exception("File already was scanned.");

                        /* Añadimos el código MD5 del archivo a la lista de códigos MD5 */

                        hashes.Add(hash);

                        /* Calculamos la huella digital del fichero */

                        string fingerprint = f.CalculateFingerprint();

                        //Console.WriteLine("Fingerprint: " + fingerprint);

                        /* Obtenemos el identificador (MBID) del fichero */

                        string id = f.GetTrackIdFromFingerprint(fingerprint);

                        Console.WriteLine("MBID: " + id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" * " + ex.Message);
                    }
                    finally
                    {
                        Console.WriteLine("----------------------------------------------------------");

                        /* Liberamos la memoria usada por el fichero */

                        f.Dispose();
                    }
                }

                /* Si no hay más archivos en la carpeta, actualizamos el archivo IDX */

                IDXFile.Save(idxFile, hashes);
            }

            Console.WriteLine("Work done. Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
