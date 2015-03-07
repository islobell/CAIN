using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace CAIN
{
    /// <summary>
    /// Clase para obtener la información del álbum más idoneo usando el título del álbum que aparece en los tags del archivo.
    /// </summary>
    public static class MetadataResolver1
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

            /**/

            int tolerance = 0;
            bool found = false;
            bool iterate = false;
            List<AcoustIDInfo> infos = new List<AcoustIDInfo>();

            do
            {
                found = false;
                infos.Clear();

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

                            /* Comparamos los títulos de los álbumes, si no coinciden no continuamos */

                            float reliability;
                            found = MetadataComparer.CompareAlbums(tag.Album, releaseGroup.Title, tolerance / 10.0f, out reliability);

                            if (!found)
                                continue;

                            /* Si coincide, lo guardamos en la lista de álbumes */

                            infos.Add(new AcoustIDInfo(String.Format("{0};{1};{2}", i, j, k), releaseGroup.Id, reliability));
                        }
                    }
                }
                                              
                Console.WriteLine(String.Empty);

                int top = Console.CursorTop -1;

                Console.WriteLine(String.Format(" Tolerancia [0-8]: {0}", tolerance));

                /* Si hemos obtenido resultados, los filtramos y los mostramos */
                          
                if (infos.Count > 0)
                {
                    DateTime dt1 = DateTime.Now;

                    /*  */

                    List<Metadata> metadatas = new List<Metadata>();
                    MetadataResolver1.ResolveAlbum(results, infos, storedAlbums, ref metadatas);

                    TimeSpan elapsed = DateTime.Now - dt1;

                    MetadataResolver1.PrintResults(metadatas);

                    /* Si hay metadatos, cogemos el álbum del primer elemento */

                    if (metadatas.Count > 0)
                        album = metadatas[0].Album;

                    Console.WriteLine(String.Format(" Tiempo transcurrido: {0} m {1} s {2} ms", elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds));
                } 
                else
                {
                    Console.WriteLine(" ··························································");
                    Console.WriteLine(" ················· ARCHIVO NO CATALOGADO ··················");
                    Console.WriteLine(" ··························································");
                }   
                
                /* Preguntamos al usuario si repetimos la búsqueda aumentando la tolerancia */

                if (tolerance < 8)
                {
                    Console.WriteLine(String.Empty);
                    Console.Write(" ¿Repetir búsqueda con más tolerancia? (Y/N) ");

                    ConsoleKey[] keys = keys = new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N };
                    iterate = Utils.ReadConsoleKey(true, keys).Key == ConsoleKey.Y;

                    if (iterate)
                    {
                        tolerance += 2;
                        if (tolerance > 8)
                            iterate = false;
                    }

                    //Utils.DeleteConsoleLines(iterate ? 6 : 2); 
                    int linesToDelete = Console.CursorTop - top + 1;
                    Utils.DeleteConsoleLines(iterate ? linesToDelete : 2);
                }
                else
                    iterate = false;
            }
            while (iterate);
        }

        private static void ResolveAlbum(List<AcoustID.Web.LookupResult> results, List<AcoustIDInfo> infos, List<Album> storedAlbums, ref List<Metadata> metadatas)
        {
            int i, j, k;
            Metadata data = null;

            if (infos.Count == 0)
                return;

            /* Si sólo hay un álbum, no tenemos que procesar nada */

            if (infos.Count == 1)
            {
                Utils.GetIndexes(infos.First().Indexes, out i, out j, out k);

                AcoustID.Web.ReleaseGroup releaseGroup = results[i].Recordings[j].ReleaseGroups[k];

                /*  */

                data = new Metadata();

                data.ResultIndex = i;
                data.RecordingIndex = j;
                data.ReleaseGroupIndex = k;
                data.Score = Convert.ToSingle(results[i].Score);
                data.Title = results[i].Recordings[j].Title;
                foreach (AcoustID.Web.Artist artist in results[i].Recordings[j].Artists)
                    data.Artists.Add(new Artist(artist.Id, artist.Name));
                data.Album = new Album(releaseGroup.Id, releaseGroup.Title);

                /* Si existe el album en la lista de álbumes ya catalogados, lo copiamos; si no, lo */
                 
                int count = storedAlbums.Count(item => item.Id == releaseGroup.Id);
                Debug.Assert(count < 2);   

                if (count == 1)//found)
                {
                    Album album = storedAlbums.First(item => item.Id == releaseGroup.Id); 
                    data.Album.Date = album.Date;
                }
                else
                {
                    MusicBrainz.Data.Release release = Utils.GetMusicBrainzDataById(data.Album.Id);

                    if (release != null && release.Data.Count > 0)
                        data.Album.Date = release.Data[0].Date;
                }   

                data.Reliability = infos[0].Reliability;
                data.Probability = 1.0f;
                data.Selected = true;

                metadatas.Add(data);

                return;
            }
            
            /* Agrupamos los álbumes por su Id */

            var items = infos.GroupBy(item => item.ReleaseGroupId).ToList();

            /*
             * Calculamos la probabilidad de cada grupo 
             * Para ello, creamos una lista donde cada elemento representa un grupo de álbumes
             * OJO, para luego poder buscar el álbum que toca, esta lista tiene que tener como 
             * clave la misma que tiene la lista de álbumes
             */

            List<KeyValuePair<string, float>> ratios = new List<KeyValuePair<string, float>>();

            foreach (var item in items)
            {
                /* Calculamos la probabilidad de cada grupo */

                float a = item.Count();
                int b = infos.Count();

                ratios.Add(new KeyValuePair<string, float>(item.First().Indexes, a / b));
            }

            /* Recorremos la lista de probabilidades */

            foreach (var ratio in ratios)
            {
                Utils.GetIndexes(ratio.Key, out i, out j, out k);

                AcoustID.Web.ReleaseGroup releaseGroup = results[i].Recordings[j].ReleaseGroups[k];

                /* Obtenemos, de la lista de álbumes, el álbum que tiene la misma clave que el elemento actual */

                AcoustIDInfo info = infos.FirstOrDefault(item => item.Indexes == ratio.Key);

                data = new Metadata();

                data.ResultIndex = i;
                data.RecordingIndex = j;
                data.ReleaseGroupIndex = k;
                data.Score = Convert.ToSingle(results[i].Score);
                data.Title = results[i].Recordings[j].Title;
                foreach (AcoustID.Web.Artist artist in results[i].Recordings[j].Artists)
                    data.Artists.Add(new Artist(artist.Id, artist.Name));
                data.Album = new Album(releaseGroup.Id, releaseGroup.Title);
                data.Reliability = info.Reliability;
                data.Probability = ratio.Value;

                metadatas.Add(data);
            }

            /* Seleccionamos los álbumes que tienen más posibilidades de ser correctos */

            float reliability = 0.0f;
            float probability = 0.0f;

            foreach (Metadata metadata in metadatas)
            {
                if (metadata.Reliability > reliability ||
                    Utils.FloatEquals(metadata.Reliability, reliability) && metadata.Probability > probability ||
                    Utils.FloatEquals(metadata.Reliability, reliability) && Utils.FloatEquals(metadata.Probability, probability))
                {
                    metadata.Selected = true;
                    reliability = metadata.Reliability;
                    probability = metadata.Probability;
                }
            }
                     
            /* Obtenemos el año desde MusicBrainz de los álbumes seleccionados */

            foreach (Metadata metadata in metadatas)
            {
                /**/

                if (!metadata.Selected) 
                    continue;

                /*  */

                int count = storedAlbums.Count(item => item.Id == metadata.Album.Id);
                Debug.Assert(count < 2);

                if (count == 1)//found)
                {
                    Album album = storedAlbums.First(item => item.Id == metadata.Album.Id);
                    metadata.Album.Date = album.Date;
                }
                else
                {
                    MusicBrainz.Data.Release release = Utils.GetMusicBrainzDataById(metadata.Album.Id);

                    if (release != null && release.Data.Count > 0)
                    {
                        if (String.IsNullOrEmpty(release.Data[0].Date))
                            metadata.Selected = false;
                        else
                            metadata.Album.Date = release.Data[0].Date;
                    }
                }
            }

            /* Ordenamos los metadatos por fiabilidad y probabilidad (de mayor a menor) */

            metadatas = metadatas.OrderByDescending(item => item.Reliability).ThenByDescending(item => item.Probability).OrderByDescending(item => item.Selected).ToList(); 
        }

        private static void PrintResults(List<Metadata> metadatas)
        {
            if (metadatas.Count == 0)
                return;

            StringBuilder str = new StringBuilder();
            str.AppendLine(" ==========================================================");
            str.AppendLine(" ==                  ARCHIVO CATALOGADO                  ==");
            str.AppendLine(" ==========================================================");  
            for (int i = 0; i < metadatas.Count; i++)
            {
                Metadata metadata = metadatas[i];

                str.AppendLine(String.Format(" Result Index      : {0,3:d} - Título    : [ '{1}' - Score: {2:0.000000} ]", metadata.ResultIndex, metadata.Title, metadata.Score));
                str.AppendLine(String.Format(" Recording Index   : {0,3:d} - Álbum     : [ {1} - '{2}' - Fiabilidad: {3,7:0.00%} - Probabilidad: {4,7:0.00%} ] {5}", metadata.RecordingIndex, metadata.Album.Id, metadata.Album.Title, metadata.Reliability, metadata.Probability, metadata.Selected ? String.Format("({0})", i + 1)/*"*"*/ : String.Empty));// pos[i] != -1 ? "*" : String.Empty));
                str.AppendLine(String.Format(" ReleaseGroup Index: {0,3:d} - Artista(s): [ '{1}' ]", metadata.ReleaseGroupIndex, String.Join("', '", metadata.Artists)));
                str.AppendLine(" ----------------------------------------------------------");
            }

            if (metadatas.Count(item => item.Selected) == 0)
            {
                //str.AppendLine(" ----------------------------------------------------------");
                str.AppendLine(" **          NO HAY INFORMACIÓN DE MUSICBRAINZ           **");
                str.AppendLine(" ----------------------------------------------------------");
                Console.Write(str.ToString());
                return;
            }

            str.AppendLine(" --         INFORMACIÓN DEL ÁLBUM (MUSICBRAINZ)          --");
            str.AppendLine(" ----------------------------------------------------------");

            for (int i = 0; i < metadatas.Count; i++)
            {
                Metadata metadata = metadatas[i]; 
 
                if (!metadata.Selected) continue;

                str.AppendLine(String.Format(" ({0}) Año: '{1}' ( {2} )", i + 1, String.IsNullOrEmpty(metadata.Album.Date) ? "????" : metadata.Album.Date, Utils.GetDateTime(metadata.Album.Date).ToString(@"dd/MM/yyyy")));
                str.AppendLine(" ----------------------------------------------------------");
            } 
   
            Console.Write(str.ToString());
        }     
    }
}

       