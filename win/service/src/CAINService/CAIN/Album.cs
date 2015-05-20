using System;
using System.Drawing;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo de la información relacionada con los álbumes.
    /// </summary>
    public class Album
    {
        public long ID { get; set; }                ///< Identificador del álbum en la base de datos
        public string MBID { get; set; }            ///< MusicBrainz Identifier (MBID) del álbum
        public string Title { get; set; }           ///< título del álbum
        public int Year { get; set; }               ///< Año del álbum
        public Image Cover { get; set; }            ///< Carátula del álbum

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Album()
        {
            this.ID = 0;
            this.MBID = String.Empty;
            this.Title = String.Empty;
            this.Year = 0;
            this.Cover = null;
        }

        /// <summary>
        ///    Comprueba si la información que contiene es válida.
        /// </summary>
        /// <returns>
        ///    True, si la información es válida. False, sino.
        /// </returns>
        public bool IsValid()
        {
            bool bValid = true;

            //bValid &= !String.IsNullOrEmpty(this.MBID);
            bValid &= !String.IsNullOrEmpty(this.Title);
            bValid &= this.Year > 0;

            return bValid;
        }

        /// <summary>
        ///    Compara dos objetos de tipo álbum.
        /// </summary>
        /// <returns>
        ///    True, si son iguales. False, sino.
        /// </returns>
        public static bool Equals(Album album1, Album album2)
        {
            bool bEqual = true;

            bEqual &= String.Equals(album1.Title, album2.Title);
            bEqual &= album1.Year == album2.Year;
            //bEqual &= CAIN.Utils.ImageEquals(album1.Cover, album2.Cover);

            return bEqual;
        }
    }
}
