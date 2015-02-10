using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {         
            /* Leemos el fichero de configuración */

            string confFile = AppDomain.CurrentDomain.BaseDirectory + @"\CAIN.conf";

            CAIN.Settings settings = CAIN.Settings.Load(confFile);

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

            /* Licencia freeware para poder usar la librería Bass.NET sin que salga la molesta pantalla de bienvenida (hay que registrarse antes de usar la librería) */

            Un4seen.Bass.BassNet.Registration(settings.BassNetEmail, settings.BassNetApiKey);

            /* Recorremos la lista de carpetas */

            CAIN.AudioFile f = null;

            /* Recorremos la lista de carpetas */

            foreach (string path in settings.FolderPaths)
            {
                Console.WriteLine("**********************************************************");
                Console.WriteLine("Folder: " + path);
                Console.WriteLine("**********************************************************");

                /* Cargamos el archivo IDX */

                string idxFile = path + @"\files.idx";

                //Si existe el archivo IDX, lo borramos (sólo para pruebas)
                System.IO.File.Delete(idxFile);

                List<string> hashes = CAIN.IDXFile.Load(idxFile);

                /* Obtenemos la lista de archivos que hay en la carpeta */

                List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(path, "*.*", System.IO.SearchOption.AllDirectories));

                /* Si no hay archivos en la carpeta o si el número de archivos no se corresponde con el número de códigos MD5, 
                   vaciamos la lista de hashes y borramos el archivo IDX (si existe) */

                if (files.Count == 0 || files.Count != hashes.Count)                 
                {
                    hashes.Clear();
                    System.IO.File.Delete(idxFile);
                }

                /* Recorremos la lista de archivos */

                foreach (string file in files)
                {                    
                    try 
                    {
                        Console.WriteLine(System.IO.Path.GetFileName(file));

                        /* Leemos el archivo */

                        f = new CAIN.AudioFile(file);

                        /* Comprobamos si se ha producido algún error; si es así, no continuamos */
                        
                        if (f.IsNull()) throw new System.Exception(f.Error);

                        /* Calculamos el código MD5 del archivo */

                        string hash = f.CalculateMD5Hash();

                        Console.WriteLine("MD5: " + hash);

                        /* Comprobamos si existe el código MD5 en la lista de códigos MD5; si es así, no continuamos */

                        if (hashes.Contains(hash)) throw new System.Exception("File already was scanned.");

                        /* Añadimos el código MD5 del archivo a la lista de códigos MD5 */

                        hashes.Add(hash);

                        /* Calculamos la huella digital del fichero */

                        string fingerprint = f.CalculateFingerprint();

                        //Console.WriteLine("Fingerprint: " + fingerprint);

                        /* Obtenemos el identificador (MBID) del fichero */

                        string id = f.GetTrackIdFromFingerprint(fingerprint);

                        Console.WriteLine("AcoustID: " + id);
                    }
                    catch (System.Exception ex)
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

                CAIN.IDXFile.Save(idxFile, hashes);
	        }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
