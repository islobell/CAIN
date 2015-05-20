using System;
using System.IO;

namespace CAIN
{
    /// <summary>
    ///    Clase para el manejo del registro del proceso de catalogación.
    /// </summary>
    public class Logger : IDisposable
    {
        /* Atributos para saber si */

        private StreamWriter writer = null;

        /// <summary>
        ///    Constructor.
        /// </summary>    
        /// <param name="file">
        ///    El nombre del archivo donde se escribirá la información del registro.
        /// </param>       
        public Logger(string file)
        {
            this.writer = new StreamWriter(file);
        }

        public void Dispose()
        {
            if (this.writer != null)
                this.writer.Close();
        }

        ~Logger()
        {
            Dispose();
        }

        /// <summary>
        ///    Método que permite escribir una cadena de texto.
        /// </summary>    
        /// <param name="str">
        ///    La cadena de texto.
        /// </param>       
        public void Write(string str)
        {
            this.writer.Write(str);
        }

        /// <summary>
        ///    Método que permite escribir una línea de texto.
        /// </summary>    
        /// <param name="str">
        ///    La cadena de texto.
        /// </param>       
        public void WriteLine(string str)
        {
            this.writer.WriteLine(str);
        }
    }
}
