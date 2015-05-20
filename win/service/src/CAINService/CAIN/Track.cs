using System;
using System.Collections.Generic;

namespace CAIN
{
    public class Track
    {
        public enum StatusTypes { 
            None,           /* El estado no ha sido establecido todavía */
            NoResults,      /* No hay resultados en AcoustID o en MusicBrainz */
            Cataloged,      /* Hay resultados y la información que se buscaba se ha encontrado  */
            NotCataloged    /* Hay resultados, pero la información que se buscaba no se ha encontrado */
        };

        public long ID { get; set; }
        public string MBID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }   /* en segundos */
        public string Path { get; set; }
        public StatusTypes Status { get; set; }
        public int Reliability { get; set; }

        public Track()
        {
            this.ID = 0;
            this.MBID = String.Empty;
            this.Title = String.Empty;
            this.Duration = 0;
            this.Path = String.Empty;
            this.Status = StatusTypes.None; 
            this.Reliability = 0;
        }

        public bool IsValid()
        {
            bool bValid = true;

            //bValid &= !String.IsNullOrEmpty(this.MBID);
            bValid &= !String.IsNullOrEmpty(this.Title);
            bValid &= this.Duration > 0;
            bValid &= !String.IsNullOrEmpty(this.Path);
            bValid &= this.Status != StatusTypes.None;

            return bValid;
        }

        public static bool Equals(Track track1, Track track2)
        {
            bool bEqual = true;

            bEqual &= String.Equals(track1.Title, track2.Title);
            bEqual &= track1.Duration == track2.Duration;
            //bEqual &= String.Equals(track1.Path, track2.Path);
            //bEqual &= track1.Reliability == track2.Reliability;
            //bEqual &= track1.Status == track2.Status;   

            return bEqual;
        }
    }
}
