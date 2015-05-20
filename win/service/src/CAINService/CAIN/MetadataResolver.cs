using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace CAIN
{                    
    /// <summary>
    /// Clase para catalogar una canción.
    /// </summary>
    public static class MetadataResolver
    {        
        /// <summary>
        ///    Método para catalogar la información relacionada con la canción a partir de una lista de resultados obtenidos desde AcoustID.
        /// </summary>
        /// <param name="option">
        ///    La opción elegida: 0 - obtener la información del álbum a partir de los metadatos ; 1 - obtener la información del álbum original.
        /// </param> 
        /// <param name="results">
        ///    Lista de resultados obtenidos desde AcoustID.
        /// </param> 
        /// <param name="file">
        ///    Objeto de tipo <see cref="TagLib.File" />.
        /// </param>     
        /// <param name="storedAlbums">
        ///    Lista de álbumes que ya han sido catalogados.
        /// </param>
        /// <param name="entity">
        ///    Objeto de tipo <see cref="Entity" /> que contendrá los información que más se ajuste a los metadatos de la canción.
        /// </param> 
        /// <param name="log">
        ///    Objeto de tipo <see cref="Logger" /> que registrá los diferentes pasos del proceso de catalogación.
        /// </param>
        public static void Resolve(int option, List<AcoustID.Web.LookupResult> results, TagLib.File file, List<Album> storedAlbums, out Entity entity, Logger log = null)
        {
            entity = null;

            if (log != null)
            {
                log.WriteLine(" ----------------------------------------------------------");
                log.WriteLine(" --                         TAGS                         --");
                log.WriteLine(" ----------------------------------------------------------");
                log.WriteLine(String.Format(" Título    : '{0}'", file.Tag.Title));
                log.WriteLine(String.Format(" Álbum     : '{0}'", file.Tag.Album));
                log.WriteLine(String.Format(" Artista(s): [ '{0}' ]", String.Join("', '", file.Tag.Performers)));
                log.WriteLine(" ----------------------------------------------------------");
                //log.Write(str.ToString());                                                        
            }

            switch (option)
            {
                case 0:
                    MetadataResolver1.Resolve(results, file, out entity, storedAlbums, log);
                    break;
                case 1:
                    MetadataResolver2.Resolve(results, file, out entity, storedAlbums, log);
                    break;
                default:
                    Debug.Assert(true);
                    break;
            }
        }

        /// <summary>
        ///    Método para obtener una lista de entidades que tienen relación con la información de una entidad.
        /// </summary>
        /// <param name="option">
        ///    La opción elegida: 0 - obtener la información del álbum a partir de los metadatos ; 1 - obtener la información del álbum original.
        /// </param>  
        /// <param name="ignoreAlbum">
        ///    Indica si vamos a tener en cuenta la información del álbum en el proceso de catalogación.
        /// </param> 
        /// <param name="ignoreTitle">                   
        ///    Indica si vamos a tener en cuenta la información del título en el proceso de catalogación.
        /// </param> 
        /// <param name="ignoreArtists">                 
        ///    Indica si vamos a tener en cuenta la información de los artistas en el proceso de catalogación.
        /// </param>     
        /// <param name="storedAlbums">
        ///    Lista de álbumes que ya han sido catalogados.
        /// </param>
        /// <returns>
        ///    Lista de objetos de tipo <see cref="Entity" />.
        /// </returns>
        public static List<Entity> GetEntities(int option, Entity entity, int tolerance = 3, bool ignoreTitle = false, bool ignoreArtists = false, bool ignoreAlbum = false)//, bool deleteNoDuration = false, bool deleteNoYear = false)
        {
            List<Entity> entities = new List<Entity>();

            switch (option)
            {
                case 0:
                    entities = MetadataResolver1.GetEntities(entity, tolerance, ignoreTitle, ignoreArtists, ignoreAlbum);//, deleteNoDuration, deleteNoYear);
                    break;
                case 1:
                    entities = MetadataResolver2.GetEntities(entity, tolerance, ignoreTitle, ignoreArtists, ignoreAlbum);
                    break;
                default:
                    Debug.Assert(true);
                    break;
            }

            return entities;
        }
    }
}
