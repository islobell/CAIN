using System;
using System.IO;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo de los archivos IDX.
    /// </summary>
    public static class IDXFile
    {
        /// <summary>
        ///    Carga el archivo IDX y vuelca su contenido en una lista de códigos MD5.
        /// </summary>
        /// <param name="file">
        ///    La ruta del archivo IDX.
        /// </param>
        /// <returns>
        ///    Una lista de códigos MD5.
        /// </returns>
        public static List<string> Load(string file)
        {
            List<string> hashes = new List<string>();

            if (!File.Exists(file)) return hashes;

            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                    hashes.Add(reader.ReadLine());
            }

            return hashes;
        }

        /// <summary>
        ///    Guarda la lista de códigos MD5 en el archivo IDX.
        /// </summary>
        /// <param name="file">
        ///    La ruta del archivo IDX.
        /// </param>
        /// <param name="hashes">
        ///    La lista de códigos MD5.
        /// </param>
        /// <returns>
        ///    True, si la operación ha finalizado con éxito. False, sino.
        /// </returns>
        public static void Add(string file, string hash)
        {
            /* Si el archivo IDX existe, lo sobreescribimos; sino existe, lo creamos */
            
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine(hash);
            }

            /* Añadimos el atributo 'oculto' al archivo IDX */

            if ((File.GetAttributes(file) & FileAttributes.Hidden) != FileAttributes.Hidden)
                File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);
        }

        /// <summary>
        ///    Guarda la lista de códigos MD5 en el archivo IDX.
        /// </summary>
        /// <param name="file">
        ///    La ruta del archivo IDX.
        /// </param>
        /// <param name="hashes">
        ///    La lista de códigos MD5.
        /// </param>
        /// <returns>
        ///    True, si la operación ha finalizado con éxito. False, sino.
        /// </returns>
        public static bool Save(string file, List<string> hashes)
        {
            if (hashes.Count == 0) return false;

            /* Si el archivo IDX existe, lo sobreescribimos; sino existe, lo creamos */

            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (string hash in hashes)
                    writer.WriteLine(hash);
            }

            /* Añadimos el atributo 'oculto' al archivo IDX */

            File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);

            return true;
        }
    }
}
