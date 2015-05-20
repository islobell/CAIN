using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIN
{
    class Playlist
    {
        public enum FormatTypes { M3U, M3U8, PLS };

        public static void Save(string filename, List<Entity> entities, FormatTypes type)
        {
            switch (type)
            {
                case FormatTypes.M3U:
                    SaveAsM3U(filename, entities);
                    break;
                case FormatTypes.M3U8:
                    SaveAsM3U8(filename, entities);
                    break;
                case FormatTypes.PLS:
                    SaveAsPLS(filename, entities);
                    break;
            }
        }

        public static void SaveAsM3U(string filename, List<Entity> entities)
        {
            using (StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.ASCII))
            {
                writer.WriteLine("#EXTM3U");
                foreach (Entity entity in entities)
                {
                    writer.WriteLine(String.Format("#EXTINF:{0},{1} - {2}", entity.Track.Duration, String.Join("/", entity.Artists), entity.Track.Title));
                    writer.WriteLine(entity.Track.Path);
                }
            }
        }

        public static void SaveAsM3U8(string filename, List<Entity> entities)
        {
            using (StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("#EXTM3U");
                foreach (Entity entity in entities)
                {
                    writer.WriteLine(String.Format("#EXTINF:{0},{1} - {2}", entity.Track.Duration, String.Join("/", entity.Artists), entity.Track.Title));
                    writer.WriteLine(entity.Track.Path);
                }
            }
        }

        public static void SaveAsPLS(string filename, List<Entity> entities)
        {
            using (StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.ASCII))
            {
                writer.WriteLine("[playlist]");                for (int i = 0; i < entities.Count; i++)
                {  
                    Entity entity = entities[i];
                    writer.WriteLine(String.Format("File{0}={1}", i+1, entity.Track.Path)); 
                    writer.WriteLine(String.Format("Title{0}={1} - {2}", i+1, String.Join("/", entity.Artists), entity.Track.Title)); 
                    writer.WriteLine(String.Format("Length{0}={1}", i+1, entity.Track.Duration));
                }                 writer.WriteLine(String.Format("NumberOfEntries={0}", entities.Count));                writer.WriteLine("Version=2");
            }
        }
    }
}
