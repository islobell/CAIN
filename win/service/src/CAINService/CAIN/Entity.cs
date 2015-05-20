using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIN
{
    public class Entity
    {       
        //public string PathSrc;

        public Track Track;
        public Album Album;
        public List<Artist> Artists;
        public List<Tag> Tags;

        public Entity()
        {
            /*this.Track = new Track();
            this.Album = new Album();*/
            this.Artists = new List<Artist>();
            this.Tags = new List<Tag>();
        }

        public bool GetArtistsFromString(string artists)
        {
            this.Artists = Artist.GetArtistsFromString(artists);
            return this.Artists.Count > 0;
        }

        public void AddTag(Tag tag)
        {
            this.Tags.Add(tag);
        }

        public void EditTag(Tag tag)
        {
            int pos = this.Tags.FindIndex(x => x.ID == tag.ID);
            if (pos != -1)
                this.Tags[pos] = tag;
        }

        public void RemoveTag(Tag tag)
        {
            int pos = this.Tags.FindIndex(x => x.ID == tag.ID);
            if (pos != -1)
                this.Tags.RemoveAt(pos);
        }

        public void RemoveTags()
        {
            this.Tags.Clear();
        }

        public bool HasTag(Tag tag)
        {
            return this.Tags.Exists(item => Tag.Equals(item, tag));
        }

        public bool IsValid()
        {
            bool bValid = true;

            bValid &= !this.Track.IsValid();
            bValid &= !this.Album.IsValid();
            bValid &= !this.Artists.TrueForAll(x => x.IsValid());

            return bValid;
        }

        public static bool Equals(Entity entity1, Entity entity2)
        {
            bool bEqual = true;

            bEqual &= Track.Equals(entity1.Track, entity2.Track);
            bEqual &= Album.Equals(entity1.Album, entity2.Album);
            bEqual &= Artist.Equals(entity1.Artists, entity2.Artists);
            bEqual &= Tag.Equals(entity1.Tags, entity2.Tags);

            return bEqual;
        }
    }
}
