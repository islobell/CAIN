using System;

namespace CAIN
{
    /// <summary>
    ///    Clase para almacenar información obtenida de la base de datos de AcoustID.
    /// </summary>
    public class AcoustIDInfo
    {
        public string Indexes { get; private set;  }              ///< Posiciones en las distintas listas (results, recordings y release groups) para poder localizar la información
        public string ReleaseGroupId { get; private set; }        ///< MusicBrainz Identifier (MBID) del álbum
        public float Reliability { get; private set; }            ///< Fiabilidad de la información 

        /// <summary>
        ///    Constructor.
        /// </summary>
        public AcoustIDInfo()
        {
            this.Indexes = String.Empty;
            this.ReleaseGroupId = String.Empty;
            this.Reliability = 0.0f;
        }

        /// <summary>
        ///    Constructor.
        /// </summary>   
        /// <param name="indexes">
        ///    Posiciones en las distintas listas (results, recordings y release groups).
        /// </param>    
        /// <param name="releaseGroupId">
        ///    MusicBrainz Identifier (MBID) del álbum.
        /// </param>   
        /// <param name="reliability">
        ///    Fiabilidad de la información.
        /// </param>
        public AcoustIDInfo(string indexes, string releaseGroupId, float reliability = 0.0f)
        {
            this.Indexes = indexes;
            this.ReleaseGroupId = releaseGroupId;
            this.Reliability = reliability;
        }
    }
}
