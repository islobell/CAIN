using System;
using System.IO;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo de los ficheros de audio.
    /// </summary>
    public class Song
    {  
        /// <summary>
        ///    Constructor.
        /// </summary>      
        /// <param name="file">
        ///    El ruta del archivo de audio.
        /// </param>    
        /// <param name="error">
        ///    Mensaje de error que se devuelve si se produce una excepción.
        /// </param>  
        /// <returns>
        ///    Un objeto de tipo <see cref="TagLib.File" />, si no se produce una exceptión. Null, sino.
        /// </returns>
        public static TagLib.File Create(string file, out string error)
        {
            error = String.Empty;

            try
            {
                return TagLib.File.Create(file);
            }
            catch (TagLib.UnsupportedFormatException ex)
            {
                error = "El formato del archivo no está soportado.";
                return null;
            }
            catch (TagLib.CorruptFileException ex)
            {
                error = "El fichero está dañado.";  
                return null;
            }
        }

        /// <summary>
        ///    Comprueba si es un fichero de audio.
        /// </summary>   
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param> 
        /// <returns>
        ///    True, si es un fichero de audio. False, sino.
        /// </returns>
        public static bool IsAudio(TagLib.File file)
        {
            List<TagLib.ICodec> codecs = new List<TagLib.ICodec>(file.Properties.Codecs);

            return (codecs.Count == 1 &&
                    (codecs[0].MediaTypes & TagLib.MediaTypes.Audio) != TagLib.MediaTypes.None &&
                     codecs[0].Duration.TotalSeconds > 0);  
        }

        /// <summary>
        ///    Comprueba si el fichero de audio tiene tags.
        /// </summary>   
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param> 
        /// <returns>
        ///    True, si tiene. False, sino.
        /// </returns>
        public static bool HasTags(TagLib.File file)
        {
            if (file.Tag.IsEmpty)
                return false;

            bool bHasTags = true;

            bHasTags &= !String.IsNullOrEmpty(file.Tag.Title);
            bHasTags &= file.Properties.Duration.TotalSeconds > 0;
            bHasTags &= !String.IsNullOrEmpty(file.Tag.Album);
            bHasTags &= file.Tag.Year > 0;
            bHasTags &= file.Tag.Artists.Length > 0;

            return bHasTags;
            //return !file.Tag.IsEmpty;
        }

        /// <summary>
        ///    Calcula el código MD5 de un archivo binario.
        /// </summary>    
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param> 
        /// <returns>
        ///    El código MD5.
        /// </returns>
        public static string CalculateMD5Hash(TagLib.File file)
        {
            using (FileStream stream = File.OpenRead(file.Name))
            {
                System.Security.Cryptography.MD5 MD5 = System.Security.Cryptography.MD5.Create(); 
                byte[] hash = MD5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
            }             
        }

        /// <summary>
        ///    Calcula la huella digital acústica del contenido musical del archivo.
        /// </summary>   
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param> 
        /// <returns>
        ///    La huella digital, si se ha podido calcular. Una cadena vacía, sino.
        /// </returns>
        public static string CalculateFingerprint(TagLib.File file)
        {
            NAudioDecoder decoder = new NAudioDecoder();
            decoder.Load(file.Name);

            if (!decoder.Ready)
                return String.Empty;
            
            AcoustID.ChromaContext fingerprinter = new AcoustID.ChromaContext();
            fingerprinter.Start(decoder.SampleRate, decoder.Channels);
            decoder.Decode(fingerprinter.Consumer, 120);
            fingerprinter.Finish();
            decoder.Dispose();

            return fingerprinter.GetFingerprint();
        }

        /// <summary>
        ///    Calcula la huella digital acústica del contenido musical del archivo.
        /// </summary>      
        /// <param name="file">
        ///    El ruta del archivo de audio.
        /// </param> 
        /// <returns>
        ///    La huella digital, si se ha podido calcular. Una cadena vacía, sino.
        /// </returns>
        public static string CalculateFingerprint(string file)
        {
            NAudioDecoder decoder = new NAudioDecoder();
            decoder.Load(file);

            if (!decoder.Ready)
                return String.Empty;

            AcoustID.ChromaContext fingerprinter = new AcoustID.ChromaContext();
            fingerprinter.Start(decoder.SampleRate, decoder.Channels);
            decoder.Decode(fingerprinter.Consumer, 120);
            fingerprinter.Finish();
            decoder.Dispose();

            return fingerprinter.GetFingerprint();
        }
            
        /// <summary>
        ///    Obtiene una entidad buscando su información en la base de datos de AcoustID.
        /// </summary>    
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param>      
        /// <param name="option">
        ///    Tipo de búsqueda (por metadatos o por más antiguo).
        /// </param>     
        /// <param name="storedAlbums">
        ///    Lista de álbumes previamente almacenados.
        /// </param>     
        /// <param name="entity">
        ///    Objeto de tipo <see cref="Entity" /> que será devuelto si la catalogación ha sido exitosa. Null, sino.
        /// </param>     
        /// <param name="log">
        ///    Objeto de tipo <see cref="Logger" />, si va a registrar el proceso de catalogación.
        /// </param> 
        /// <returns>
        ///    La huella digital, si se ha podido calcular. Una cadena vacía, sino.
        /// </returns>
        public static void GetMetadataFromInternet(TagLib.File file, int option, List<Album> storedAlbums, out Entity entity, Logger log = null)
        {
            entity = null;

            string[] metadata = { "recordings", "releasegroups", "compress" };

            /* ¡OJO! La duración del audio la necesitamos en segundos */

            int duration = (int)file.Properties.Duration.TotalSeconds;

            /* Obtenemos una lista de resultados posibles relacionados con la huella digital */

            string fp = Song.CalculateFingerprint(file);
                
            AcoustID.Web.LookupService LookupService = new AcoustID.Web.LookupService();
            List<AcoustID.Web.LookupResult> results = LookupService.Get(fp, duration, metadata);

            /* Obtenemos los metadatos que mejor se ajustan a la opción seleccionada  */

            MetadataResolver.Resolve(option, results, file, storedAlbums, out entity, log);                  
        }

        /// <summary>
        ///    Guarda la información de una entidad como metadatos de un archivo de audio.
        /// </summary>    
        /// <param name="file">             
        ///    Un objeto de tipo <see cref="TagLib.File" />.
        /// </param>      
        /// <param name="option">
        ///    Tipo de búsqueda (por metadatos o por más antiguo).
        /// </param>     
        /// <param name="storedAlbums">
        ///    Lista de álbumes previamente almacenados.
        /// </param>     
        /// <param name="entity">
        ///    Objeto de tipo <see cref="Entity" /> que será devuelto si la catalogación ha sido exitosa. Null, sino.
        /// </param>     
        /// <param name="log">
        ///    Objeto de tipo <see cref="Logger" />, si va a registrar el proceso de catalogación.
        /// </param>
        public static void SaveMetadata(Entity entity)
        {
            TagLib.File f = TagLib.File.Create(entity.Track.Path);

            f.Tag.Clear();

            f.Tag.Title = entity.Track.Title;
            f.Tag.Album = entity.Album.Title;
            f.Tag.Year = (uint)entity.Album.Year;
            if (entity.Tags.Exists(item => item.Name == "Género"))
            {
                string[] genres = new string[] { entity.Tags.Find(item => item.Name == "Género").Value };
                f.Tag.Genres = new string[] { };
                f.Tag.Genres = genres;
            }
            if (entity.Artists.Count > 0)
            {
                string[] artist = new string[entity.Artists.Count];
                for (int i = 0; i < entity.Artists.Count; i++)
                    artist[i] = entity.Artists[i].Name;
                f.Tag.Performers = new string[] { };
                f.Tag.Performers = artist;
            }
            if (entity.Album.Cover != null)
            {
                TagLib.ByteVector buffer = new TagLib.ByteVector(Utils.ImageToByteArray(entity.Album.Cover));
                List<TagLib.Picture> pictures = new List<TagLib.Picture>();
                pictures.Add(new TagLib.Picture(buffer));
                f.Tag.Pictures = pictures.ToArray();
            }

            f.Save();
        }
    }
}
