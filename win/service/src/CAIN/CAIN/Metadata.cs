using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIN
{
    public class Artist
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public Artist(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public override string ToString()
        {
            return this.Name;
        } 
    }

    public class Album
    {
        public string Id;
        public string Title;
        public string Date;

        public Album()
        {
            this.Id = String.Empty;
            this.Title = String.Empty;
            this.Date = String.Empty;
        }
        
        public Album(string Id, string Title)
        {
            this.Id = Id;
            this.Title = Title;
            this.Date = String.Empty;
        }

        public Album(string Id, string Title, string Date)
        {
            this.Id = Id;
            this.Title = Title;
            this.Date = Date;
            //this.Date = Date;
        }

        public Album(Album album)
        {
            this.Id = album.Id;
            this.Title = album.Title;
            this.Date = album.Date;
        }

        public void Copy(Album album)
        {
            this.Id = album.Id;
            this.Title = album.Title;
            this.Date = album.Date;
        }

        public bool IsEmpty()
        {
            if (String.IsNullOrEmpty(this.Id) ||
                String.IsNullOrEmpty(this.Title) ||
                String.IsNullOrEmpty(this.Date))
                return true;
            else
                return false;
        }
    }

    public class Metadata
    {
        public int ResultIndex;
        public int RecordingIndex;
        public int ReleaseGroupIndex;
        public float Score;
        public string Title;
        public List<Artist> Artists;
        public Album Album;
        public float Reliability;
        public float Probability;
        public bool Selected;

        public Metadata()
        {
            this.ResultIndex = -1;
            this.RecordingIndex = -1;
            this.ReleaseGroupIndex = -1;
            this.Score = 0.0f;
            this.Title = String.Empty;
            this.Artists = new List<Artist>();
            this.Album = new Album();
            this.Reliability = 0.0f;
            this.Probability = 0.0f;
            this.Selected = false;
        }

        public void Copy(Metadata data)
        {
            this.ResultIndex = data.ResultIndex;
            this.RecordingIndex = data.RecordingIndex;
            this.ReleaseGroupIndex = data.ReleaseGroupIndex;
            this.Score = data.Score;
            this.Title = data.Title;
            this.Artists = data.Artists;
            this.Album = data.Album;
            this.Reliability = data.Reliability;
            this.Probability = data.Probability;
            this.Selected = data.Selected;
        }
    }

    public class AcoustIDInfo
    {
        public string Indexes;
        public string ReleaseGroupId;
        public float Reliability;

        public AcoustIDInfo()
        {
            this.Indexes = String.Empty;
            this.ReleaseGroupId = String.Empty; 
            this.Reliability = 0.0f;
        }

        public AcoustIDInfo(string indexes, string releaseGroupId, float reliability)
        {
            this.Indexes = indexes;
            this.ReleaseGroupId = releaseGroupId;
            this.Reliability = reliability;
        } 
    } 
}
