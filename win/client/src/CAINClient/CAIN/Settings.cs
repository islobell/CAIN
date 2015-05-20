using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo del archivo de configuración.
    /// </summary>
    public class Settings
    {
        public enum ModeTypes { None = -1, SearchByTagAlBum, SearchByOriginalAlbum };

        /// <summary>
        ///    Clave de registro para usar la API de AcoustID.
        /// </summary>
        public string AcoustIDApiKey { get; set; }

        /// <summary>
        ///    Email de registro para usar la API de Bass.Net.
        /// </summary>
        //public string BassNetEmail { get; set; }

        /// <summary>
        ///    Clave de registro para usar la API de Bass.Net.
        /// </summary>
        //public string BassNetApiKey { get; set; }

        /// <summary>
        ///    Nombre de usuario para acceder a la base dse datos.
        /// </summary>
        public string BDUser { get; set; }

        /// <summary>
        ///    Contraseña para acceder a la base dse datos.
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        ///    Carpeta donde se guardarán los archivos de audio escaneados.
        /// </summary>
        public string PathDst { get; set; }   

        /// <summary>
        ///    Lapso de tiempo mínimo (en segundos) que debe pasar entre 2 procesos de catalogación.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        ///    Lapso de tiempo mínimo (en segundos) que debe pasar entre 2 procesos de catalogación.
        /// </summary>
        public ModeTypes Mode { get; set; }  

        /// <summary>
        ///    Indica si hay que volver a catalogar todas las canciones.
        /// </summary>
        public bool Reset { get; set; }

        /// <summary>
        ///    Ruta de las carpetas a escanear.
        /// </summary>
        public List<string> FolderPaths { get; set; }

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Settings()
        {
            this.AcoustIDApiKey = String.Empty;
            this.BDUser = String.Empty;
            this.DBPassword = String.Empty;
            this.PathDst = String.Empty;
            this.Interval = 0;
            this.Mode = ModeTypes.None;          
            this.Reset = false;
            this.FolderPaths = new List<string>();
        }
        
        /// <summary>
        ///    Comprueba si la instancia del objeto 'Settings' es válida.
        /// </summary>
        /// <returns>
        ///    True, si es válido. False, sino.
        /// </returns>
        public bool IsValid()
        {
            if (String.IsNullOrEmpty(this.AcoustIDApiKey) ||
                String.IsNullOrEmpty(this.BDUser) ||
                String.IsNullOrEmpty(this.DBPassword) ||
                String.IsNullOrEmpty(this.PathDst) ||
                this.Interval == 0 ||
                this.Mode == ModeTypes.None ||
                this.FolderPaths.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        ///    Comprueba si la instancia del objeto 'Settings' es válido.
        /// </summary>
        /// <returns>
        ///    True, si es válido. False, sino.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format(" AcoustIDApiKey = {0}", this.AcoustIDApiKey));
            sb.AppendLine(String.Format(" BDUser = {0}", this.BDUser));
            sb.AppendLine(String.Format(" DBPassword = {0}", this.DBPassword));
            sb.AppendLine(String.Format(" PathDst = {0}", this.PathDst));
            sb.AppendLine(String.Format(" Interval = {0}", this.Interval));
            sb.AppendLine(String.Format(" Mode = {0}", this.Mode));
            sb.AppendLine(String.Format(" Reset = {0}", this.Reset));
            sb.AppendLine(String.Format(" FolderPaths = {0}", this.FolderPaths.Count));
            for (int i = 0; i < this.FolderPaths.Count; i++)
                sb.AppendLine(String.Format(" Path #{0} = {1}", i + 1, this.FolderPaths[i]));
            return sb.ToString();
        }
        
        /// <summary>
        ///    Crea una instancia del objeto 'Settings' a partir del fichero de configuración.
        /// </summary>
        /// <param name="file">
        ///    La ruta del archivo de configuración.
        /// </param>
        /// <returns>
        ///    Una instancia del objeto 'Settings'.
        /// </returns>
        public static Settings Load(string file)
        {
            Settings settings = new Settings();

            if (!File.Exists(file)) return settings;

            using (StreamReader reader = File.OpenText(file))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                settings = (Settings)serializer.Deserialize(reader, typeof(Settings));
            }

            return settings;
        }

        /// <summary>
        ///    Guarda el objeto 'Settings' en el fichero de configuración.
        /// </summary>
        /// <param name="file">
        ///    La ruta del archivo de configuración.
        /// </param>
        public void Save(string file)
        {                                            
            using (StreamWriter writer = File.CreateText(file))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(writer, this);
            }
        }
    }
}
