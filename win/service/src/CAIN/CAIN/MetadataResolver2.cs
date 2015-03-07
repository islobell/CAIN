//#define TEST1
//#define TEST2
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para obtener la información del álbum "original" (o más antiguo).
    /// </summary>
    public static class MetadataResolver2
    {
        public static void Resolve(List<AcoustID.Web.LookupResult> results, TagLib.Tag tag, List<Album> storedAlbums, out Album album)
        {     
            album = null;

            /**/

            StringBuilder str = new StringBuilder(); 
            str.AppendLine(" ----------------------------------------------------------");
            str.AppendLine(" --                         TAGS                         --");
            str.AppendLine(" ----------------------------------------------------------");
            str.AppendLine(String.Format(" Título    : '{0}'", tag.Title));
            str.AppendLine(String.Format(" Álbum     : '{0}'", tag.Album));
            str.AppendLine(String.Format(" Artista(s): [ '{0}' ]", String.Join("', '", tag.Performers)));
            str.AppendLine(" ----------------------------------------------------------");
            Console.Write(str.ToString());

            bool found = false;
            List<string> releaseGroupIds = new List<string>();

            /* Recorremos la lista de resultados de AcoustID */

            for (int i = 0; i < results.Count; i++)
            {
                AcoustID.Web.LookupResult result = results[i];

                for (int j = 0; j < result.Recordings.Count; j++)
                {
                    AcoustID.Web.Recording recording = result.Recordings[j];

                    if (recording.Artists.Count == 0 ||
                        recording.ReleaseGroups.Count == 0)
                        continue;

                    List<string> artists = new List<string>();
                    foreach (AcoustID.Web.Artist artist in recording.Artists)
                        artists.Add(artist.Name);

                    /* Comparamos los artistas, si no coinciden no continuamos */

                    found = MetadataComparer.CompareArtists(tag.Performers.ToList(), artists);

                    if (!found)
                        continue;

                    /* Comparamos los títulos de las canciones, si no coinciden no continuamos */

                    found = MetadataComparer.CompareTitles(tag.Title, recording.Title);

                    if (!found)
                        continue;

                    for (int k = 0; k < recording.ReleaseGroups.Count; k++)
                    {
                        AcoustID.Web.ReleaseGroup releaseGroup = recording.ReleaseGroups[k];
                        
                        /* Cogemos todos los álbumes */

                        releaseGroupIds.Add(releaseGroup.Id);
                    }
                }
            }

            /* Si hemos obtenido resultados, los filtramos y los mostramos */         

            //if (releaseGroupIds.Count > 0)
            {
                DateTime dt1 = DateTime.Now;

                MusicBrainz.Data.Release release;
                MetadataResolver2.ResolveAlbum(results, releaseGroupIds, out release);

                /**/

                TimeSpan elapsed = DateTime.Now - dt1;   
            
                MetadataResolver2.PrintResults(release);

                if (release != null && release.Data.Count > 0)
                    album = new Album(release.Data[0].Releasegroup.Id, release.Data[0].Title, release.Data[0].Date);

                if (release != null && release.Data.Count > 0)
                  Console.WriteLine(String.Format(" Tiempo transcurrido: {0} m {1} s {2} ms", elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds));
            }

        }

        public static void ResolveAlbum(List<AcoustID.Web.LookupResult> results, List<string> releaseGroupIds, out MusicBrainz.Data.Release release)
        {
            release = null;

            if (releaseGroupIds.Count == 0) 
                return;

            /* Quitamos los elementos repetidos */

            releaseGroupIds = releaseGroupIds.Distinct().ToList();

            /* Obtenemos los álbumes ordenados por fecha de más antiguo a más actual */

            release = Utils.GetMusicBrainzDataByIdList(releaseGroupIds);
        }

        private static void PrintResults(MusicBrainz.Data.Release release)
        {
            if (release == null || release.Data.Count == 0)
            {
                Console.WriteLine(" ----------------------------------------------------------");
                Console.WriteLine(" **          NO HAY INFORMACIÓN DE MUSICBRAINZ           **");
                Console.WriteLine(" ----------------------------------------------------------"); 
                return;
            }

            Console.WriteLine(String.Empty);  
            Console.Write(String.Format(" Resultados a mostrar [1-{0}]: ", release.Data.Count >= 9 ? "9" : release.Data.Count.ToString()));
            List<ConsoleKey> keys = new List<ConsoleKey>();
            if (release.Data.Count > 0)
                keys.Add(ConsoleKey.D1);
            if (release.Data.Count > 1)
                keys.Add(ConsoleKey.D2);
            if (release.Data.Count > 2)
                keys.Add(ConsoleKey.D3);
            if (release.Data.Count > 3)
                keys.Add(ConsoleKey.D4);
            if (release.Data.Count > 4)
                keys.Add(ConsoleKey.D5);
            if (release.Data.Count > 5)
                keys.Add(ConsoleKey.D6);
            if (release.Data.Count > 6)
                keys.Add(ConsoleKey.D7);
            if (release.Data.Count > 7)
                keys.Add(ConsoleKey.D8);
            if (release.Data.Count > 8)
                keys.Add(ConsoleKey.D9);
            ConsoleKeyInfo key = Utils.ReadConsoleKey(true, keys.ToArray());
            int items = Int32.Parse(key.KeyChar.ToString());

            StringBuilder str = new StringBuilder();
            str.AppendLine(String.Empty);
            str.AppendLine(" ----------------------------------------------------------");
            str.AppendLine(" --         INFORMACIÓN DEL ÁLBUM (MUSICBRAINZ)          --");
            str.AppendLine(" ----------------------------------------------------------");
            for (int i = 0; i < items && i < release.Data.Count; i++)
            {
                MusicBrainz.Data.ReleaseData data = release.Data[i];

                str.AppendLine(String.Format(" Id    : {0}", data.Releasegroup.Id));
                str.AppendLine(String.Format(" Título: '{0}'", data.Title));
                if (String.IsNullOrEmpty(data.Date))
                    str.AppendLine(" Año   : ????");
                else
                    str.AppendLine(String.Format(" Año   : '{0}' ( {1} )", data.Date, Utils.GetDateTime(data.Date).ToString(@"dd/MM/yyyy")));
                str.AppendLine(" ----------------------------------------------------------");
            }

            Console.Write(str.ToString());
        }
    }
}
