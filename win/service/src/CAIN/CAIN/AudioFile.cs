using System;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo de los ficheros de audio.
    /// </summary>
    public class AudioFile
    {
        /// <summary>
        ///    Decodificador de archivos de audio (usa Bass.Net). 
        /// </summary>        
        private static BassDecoder Decoder = new BassDecoder();

        /// <summary>
        ///    Calculador de huellas digitales de AcoustID. 
        /// </summary>        
        private static AcoustID.ChromaContext Fingerprinter = new AcoustID.ChromaContext();

        /// <summary>
        ///    Calculador de códigos MD5. 
        /// </summary>        
        private static System.Security.Cryptography.MD5 MD5 = System.Security.Cryptography.MD5.Create();

        /// <summary>
        ///    Realizador de búsquedas en la base de datos de AcoustID. 
        /// </summary>
        private static AcoustID.Web.LookupService LookupService = new AcoustID.Web.LookupService();

        /// <summary>
        ///    Manejador de archivos de audio.
        /// </summary>
        private TagLib.File TagLibFile;

        /// <summary>
        ///    Mensaje de error (si se produce alguno).
		/// </summary>
        public string Error { get; private set; }

        /// <summary>
        ///    Constructor.
        /// </summary>
        /// <remarks>
        ///    Crea un objeto <see cref="TagLibFile" /> para uso interno. Si se produce 
        ///    una excepción, se almacena su mensaje en el objeto <see cref="Error" /> y 
        ///    se pone a null el objeto <see cref="TagLibFile" />.
        /// </remarks>
        public AudioFile(string file)
        {
            try
            {
                this.TagLibFile = TagLib.File.Create(file);

                /* Comprobamos si el archivo es de audio */

                if (!this.IsAudio())
                {
                    this.Error = "Not audio file.";
                    this.TagLibFile = null;
                }
            }
            catch (TagLib.UnsupportedFormatException ex)
            {
                this.Error = "Unsupported format file.";
                this.TagLibFile = null;
            }
            catch (TagLib.CorruptFileException ex)
            {
                this.Error = "Corrupt file.";
                this.TagLibFile = null;
            }
        }

        /// <summary>
        ///    Destructor.
        /// </summary>
        ~AudioFile()
        {
            if (this.TagLibFile != null) this.TagLibFile.Dispose();
        }

        /// <summary>
        ///    Libera la memoria usada por el archivo.
        /// </summary>
        public void Dispose()
        {
            if (this.TagLibFile != null) this.TagLibFile.Dispose();
        }

        /// <summary>
        ///    Comprueba si el fichero es no válido (o nulo).
        /// </summary>
        /// <returns>
        ///    True, si el fichero existe. False, si no.
        /// </returns>
        public bool IsNull()
        {
            return this.TagLibFile == null ? true : false;
        }

        /// <summary>
        ///    Comprueba si es un fichero de audio.
        /// </summary>
        /// <returns>
        ///    True, si es un fichero de audio. False, si no.
        /// </returns>
        private bool IsAudio()
        {
            List<TagLib.ICodec> codecs = new List<TagLib.ICodec>(this.TagLibFile.Properties.Codecs);

            if (codecs.Count == 1 &&
                (codecs[0].MediaTypes & TagLib.MediaTypes.Audio) != TagLib.MediaTypes.None && 
                codecs[0].Duration.TotalSeconds > 0)
                return true;
            else
                return false;            
        }

        /// <summary>
        ///    Calcula el código MD5 de un archivo binario.
        /// </summary>
        /// <returns>
        ///    El código MD5.
        /// </returns>
        public string CalculateMD5Hash()
        {
            using (System.IO.FileStream stream = System.IO.File.OpenRead(this.TagLibFile.Name))
            {
                byte[] hash = AudioFile.MD5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
            }
        }

        /// <summary>
        ///    Calcula la huella digital acústica del contenido musical del archivo.
        /// </summary>
        /// <returns>
        ///    La huella digital, si se ha podido calcular. Una cadena vacía, si no.
        /// </returns>
        public string CalculateFingerprint()
        {
            AudioFile.Decoder.Load(this.TagLibFile.Name);

            if (!AudioFile.Decoder.Ready) return String.Empty;

            AudioFile.Fingerprinter.Start(AudioFile.Decoder.SampleRate, AudioFile.Decoder.Channels);
            AudioFile.Decoder.Decode(AudioFile.Fingerprinter.Consumer, 120);
            AudioFile.Fingerprinter.Finish();

            return AudioFile.Fingerprinter.GetFingerprint();
        }

        /// <summary>
        ///    Calcula el identificador (MBID) del contenido musical del archivo en base a su huella digital.
        /// </summary>
        /// <param name="fingerprint">
        ///    La huella digital del archivo.
        /// </param>
        /// <returns>
        ///    El identificador, si se ha podido obtener. Una cadena vacía, si no.
        /// </returns>
        public string GetTrackIdFromFingerprint(string fingerprint)
        {
            /* ¡OJO! La duración del audio la necesitamos en segundos */

            int duration = (int) this.TagLibFile.Properties.Duration.TotalSeconds;
            List<AcoustID.Web.LookupResult> results = AudioFile.LookupService.Get(fingerprint, duration);

            if (results.Count == 0) return String.Empty;

            /* Cogemos el primer registro, pues tiene el "score" más alto (o sea, que es el más fiable) */

            return results[0].Id;
        }
    }
}
