using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace CAIN
{                    
    /// <summary>
    /// Clase para obtener los metadatos de un archivo de audio.
    /// </summary>
    public static class MetadataResolver
    {
        /// <summary>
        ///    Calcula el identificador (MBID) del contenido musical del archivo en base a su huella digital.
        /// </summary>
        /// <param name="option">
        ///    La opción elegida: 0 - obtener la información del álbum a partir de los metadatos ; 1 - obtener la información del álbum original.
        /// </param> 
        /// <param name="results">
        ///    Metadatos extraidos del archivo de audio.
        /// </param>     
        /// <param name="storedAlbums">
        ///    Lista de álbumes que ya han sido catalogados (sólo opción 0).
        /// </param>
        /// <param name="album">
        ///    Información del nuevo álbum catalogado; null, si ya existe (sólo opción 0).
        /// </param>
        public static void Resolve(int option, List<AcoustID.Web.LookupResult> results, TagLib.Tag tag, List<Album> storedAlbums, out Album album)
        {
            album = null;

            switch (option)
            {
                case 0:
                    MetadataResolver1.Resolve(results, tag, storedAlbums, out album);
                    break;
                case 1:
                    MetadataResolver2.Resolve(results, tag, storedAlbums, out album);
                    break;
                default:
                    Debug.Assert(true);
                    break;
            }
        }
    }
}
