using System;
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

            if (!System.IO.File.Exists(file)) return hashes;

            System.IO.StreamReader reader = new System.IO.StreamReader(file);

            while (!reader.EndOfStream)
                hashes.Add(reader.ReadLine());

            reader.Close();

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
        ///    True, si la operación ha finalizado con éxito. False, si no.
        /// </returns>
        public static bool Save(string file, List<string> hashes)
        {
            if (hashes.Count == 0) return false;

            /* Si el archivo IDX existe, lo sobreescribimos; si no existe, lo creamos */

            System.IO.StreamWriter writer = new System.IO.StreamWriter(file);

            foreach (string hash in hashes)
                writer.WriteLine(hash);

            writer.Close();

            /* Añadimos el atributo 'oculto' al archivo IDX */

            System.IO.File.SetAttributes(file, System.IO.File.GetAttributes(file) | System.IO.FileAttributes.Hidden);

            return true;
        }
    }
}
