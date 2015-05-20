using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using MySql.Data.MySqlClient;

namespace CAINClient
{
    /// <summary>
    ///    Clase global que alamcena objetos y métodos que se pueden usar en distintas partes de la aplicación.
    /// </summary>
    public sealed class SingletonApp
    {          
        private string SettingsFilename;                   ///< Nombre el archivo de configuración
        private CAIN.DBConnection DBConnection;                 ///< Objeto de tipo <see cref="CAIN.DBConnection" />
        public CAIN.Settings Settings { get; private set; }     ///< Objeto de tipo <see cref="Settings" />
        public CAIN.DB DB { get; private set; }     ///< Objeto de tipo <see cref="DB" />
                                                              
        /// <summary>
        ///    Métod que permite obtener la conexión a la base de datos.
        /// </summary> 
        /// <returns>
        ///    La conexión a la base de datos.
        /// </returns>
        public MySqlConnection Connection { get { return this.DBConnection.Connection; } } 

        private static readonly SingletonApp instance = new SingletonApp();

        /// <summary>
        ///    Métod que permite obtener la instacia del objeto <see cref="SingletonApp" />.
        /// </summary> 
        /// <returns>
        ///    La conexión a la base de datos.
        /// </returns>
        public static SingletonApp Instance { get { return instance; } }

        /// <summary>
        ///    Constructor.
        /// </summary>
        private SingletonApp()
        {
            /* Cargamos la información del archivo de configuración */

            this.SettingsFilename = AppDomain.CurrentDomain.BaseDirectory + @"\CAIN.conf";
                                        
            if (!LoadSettings())                   
                return;

            /* Abrimos la conexión de la base de datos */

            string connection = "server=127.0.0.1; database=musicdb; uid=" + this.Settings.BDUser + "; pwd=" + this.Settings.DBPassword + ";";

            /* El constructor abre la conexión con la base de datos */

            this.DBConnection = new CAIN.DBConnection(connection);

            if (!this.DBConnection.IsOpen())
            {
                MessageBox.Show(this.DBConnection.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /* Pasamos la conexión a todas las clases que la necesitan */

            this.DB = new CAIN.DB(this.DBConnection);
        }

        /// <summary>
        ///    Método que permite cargar la información del archivo de configuración.
        /// </summary> 
        public bool LoadSettings()
        {
            /* Comprobamos si existe el archivo de configuración */

            if (!File.Exists(this.SettingsFilename))
            {
                MessageBox.Show("El archivo de configuración no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            /* Cargamos el archivo de configuración */

            this.Settings = CAIN.Settings.Load(this.SettingsFilename);

            /* Comprobamos que la información del archivo de configuración es válida */
            
            if (!this.Settings.IsValid())
            {
                MessageBox.Show("La información del archivo de configuración no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        
        /// <summary>
        ///    Método que permite cargar la información del archivo de configuración.
        /// </summary> 
        public void SaveSettings()
        {
            this.Settings.Save(AppDomain.CurrentDomain.BaseDirectory + "CAIN.conf");
        }
    }
}
