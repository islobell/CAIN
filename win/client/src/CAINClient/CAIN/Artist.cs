using System;
using System.Linq;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para el manejo de la información relacionada con los artistas/grupos.
    /// </summary>
    public class Artist
    {
        public long ID { get; set; }           ///< Identificador del artista/grupo en la base de datos
        public string MBID { get; set; }        ///< MusicBrainz Identifier (MBID) del artista/grupo
        public string Name { get; set; }         ///< Nombre del artista/grupo

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Artist()
        {
            this.ID = 0;
            this.MBID = String.Empty;
            this.Name = String.Empty;
        }

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Artist(string Name)
        {
            this.ID = 0;
            this.MBID = String.Empty;
            this.Name = Name;
        }

        /// <summary>
        ///    Constructor.
        /// </summary>
        public Artist(string MBID, string Name)
        {
            this.ID = 0;
            this.MBID = MBID;
            this.Name = Name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool IsValid()
        {
            bool bValid = true;

            //bValid &= !String.IsNullOrEmpty(this.MBID);
            bValid &= !String.IsNullOrEmpty(this.Name);
            
            return bValid;
        }

        public static bool Equals(Artist artist1, Artist artist2)
        {
            bool bEqual = true;

            bEqual &= String.Equals(artist1.Name, artist2.Name, StringComparison.OrdinalIgnoreCase);  

            return bEqual;
        }

        public static bool Equals(List<Artist> artists1, List<Artist> artists2)
        {
            if (artists1.Count != artists2.Count)
                return false;

            if (artists1.Count == 0 && artists2.Count == 0)
                return true;

            bool bEqual = false;

            for (int i = 0; i < artists1.Count && !bEqual; i++)
                bEqual = Artist.Equals(artists1[i], artists2[i]);  

            return bEqual;
        }

        public static bool Equals(string artists1, string artists2)
        {
            List<Artist> artist1 = Artist.GetArtistsFromString(artists1);
            List<Artist> artist2 = Artist.GetArtistsFromString(artists2);  
            return Artist.Equals(artist1, artist2);
        }

        public static List<Artist> GetArtistsFromString(string artists)
        {
            List<Artist> artist = new List<Artist>();
            
            List<string> items = artists.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            items = items.Where(item => !String.IsNullOrEmpty(item.Trim())).ToList();

            if (items.Count == 0)
                return artist;

            items.ForEach(item => artist.Add(new CAIN.Artist(item.Trim())));

            return artist;
        }
    }
}
