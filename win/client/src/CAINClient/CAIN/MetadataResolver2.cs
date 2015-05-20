//#define TEST1
//#define TEST2
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Drawing;

namespace CAIN
{
    /// <summary>
    /// Clase para obtener la información del álbum "original" (o más antiguo).
    /// </summary>
    public static class MetadataResolver2
    {
        public static void Resolve(List<AcoustID.Web.LookupResult> results, TagLib.File file, out Entity entityOut, List<Album> storedAlbums = null, Logger log = null)
        {
            entityOut = null;

            Entity entityIn = new Entity();
            entityIn.Track = new Track();
            entityIn.Track.Title = file.Tag.Title;
            entityIn.Track.Duration = (int)file.Properties.Duration.TotalSeconds;   
            entityIn.Track.Path = file.Name;

            entityIn.Album = new Album();
            entityIn.Album.Title = file.Tag.Album;
            entityIn.Album.Year = (int)file.Tag.Year;
            if (file.Tag.Pictures.Length > 0)
                entityIn.Album.Cover = Utils.ByteArrayToImage(file.Tag.Pictures[0].Data.Data);

            foreach (string artist in file.Tag.Performers)
                entityIn.Artists.Add(new Artist(artist));

            if (!String.IsNullOrEmpty(file.Tag.FirstGenre))
                entityIn.Tags.Add(new Tag("Género", file.Tag.FirstGenre));

            MetadataResolver2.Resolve(results, entityIn, out entityOut, false, false, false, true, true, storedAlbums, log);
        }
        
        public static List<Entity> Resolve(List<AcoustID.Web.LookupResult> results, 
                                    Entity entityIn, 
                                    out Entity entityOut,   
                                    bool ignoreTitle,
                                    bool ignoreArtists,  
                                    bool ignoreAlbum,
                                    bool searchCover = false,
                                    bool printResults = false,
                                    List<Album> storedAlbums = null, 
                                    Logger log = null)
        {
            entityOut = null;
            List<Entity> entities = new List<Entity>();

            /* Si no hay resultados, usamos la información que hay en los tags */

            if (results.Count == 0)
            {
                entityOut = entityIn;
                entityOut.Track.Status = Track.StatusTypes.NoResults;

                return entities;
            }
            
            bool found = false;
            List<AcoustIDInfo> infos = new List<AcoustIDInfo>();

           try
           {
               /* Recorremos la lista de resultados de AcoustID */

                for (int i = 0; i < results.Count; i++)
                {
                    AcoustID.Web.LookupResult result = results[i];

                    for (int j = 0; j < result.Recordings.Count; j++)
                    {
                        AcoustID.Web.Recording recording = result.Recordings[j];

                        if (!ignoreArtists)
                        {
                            if (recording.Artists.Count == 0 ||
                                recording.ReleaseGroups.Count == 0)
                                continue;

                            List<string> artists = new List<string>();
                            foreach (AcoustID.Web.Artist artist in recording.Artists)
                                artists.Add(artist.Name);

                            /* Comparamos los artistas, sino coinciden no continuamos */

                            found = MetadataComparer.CompareArtists(entityIn.Artists.Select(item => item.Name).ToList(), artists);

                            if (!found)
                                continue;
                        }

                        /* Comparamos los títulos de las canciones, sino coinciden no continuamos */

                        if (!ignoreTitle)
                        {
                            found = MetadataComparer.CompareTitles(entityIn.Track.Title, recording.Title);

                            if (!found)
                                continue;
                        }

                        for (int k = 0; k < recording.ReleaseGroups.Count; k++)
                        {
                            AcoustID.Web.ReleaseGroup releaseGroup = recording.ReleaseGroups[k];

                            /* Si coincide, lo guardamos en la lista de álbumes */

                            infos.Add(new AcoustIDInfo(String.Format("{0};{1};{2}", i, j, k), releaseGroup.Id, 0.0f));
                        }
                    }
                }

                /* Si hemos obtenido resultados, los filtramos y los mostramos */

                found = false;

                if (infos.Count > 0)
                {
                    //DateTime dt1 = DateTime.Now;

                    List<Metadata> metadatas = new List<Metadata>();
                    MetadataResolver2.ResolveAlbum(results, infos, ref metadatas, storedAlbums);
                    if (printResults) MetadataResolver2.PrintResults(metadatas, log);

                    /* Si hay metadatos, cogemos el álbum del primer elemento */

                    if (metadatas.Count > 0)
                    {
                        /* Como ya están ordenados, cogemos el primero de la lista */

                        entityOut = metadatas[0].Entity;
                        //entity.PathSrc = file.Name;    
                        entityOut.Track.Path = entityIn.Track.Path;
                        if (searchCover && entityOut.Album.Cover == null)
                            entityOut.Album.Cover = Utils.GetCover(entityOut.Album.MBID);
                        entityOut.Tags = entityIn.Tags;
                        entityOut.Track.Status = Track.StatusTypes.Cataloged;

                        entities = metadatas.Select(item => item.Entity).ToList();
                        found = true;
                    }

                    /*if (log != null)
                    {    
                        TimeSpan elapsed = DateTime.Now - dt1;
                        log.WriteLine(String.Format(" Tiempo transcurrido: {0} m {1} s {2} ms", elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds));
                    }*/
                }

                /* Si no hemos encontrado la información que buscábamos, usamos la información que hay en los tags */

                if (!found)
                {
                    entityOut = entityIn;
                    entityOut.Track.Status = Track.StatusTypes.NotCataloged;
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

           Debug.Assert(entityOut != null);
           return entities;
        }

        //private static void ResolveAlbum(List<AcoustID.Web.LookupResult> results, List<AcoustIDInfo> infos, List<Album> storedAlbums, ref List<Metadata> metadatas)
        private static void ResolveAlbum(List<AcoustID.Web.LookupResult> results, List<AcoustIDInfo> infos, ref List<Metadata> metadatas, List<Album> storedAlbums = null)
        {
            /* Quitamos los elementos repetidos */

            infos = infos.GroupBy(item => item.ReleaseGroupId).Select(item => item.First()).ToList();

            List<string> releaseGroupIds = new List<string>();

            foreach (AcoustIDInfo info in infos)
                releaseGroupIds.Add(info.ReleaseGroupId);                 

            /* Obtenemos los álbumes ordenados por fecha de más antiguo a más actual */

            MusicBrainz.Data.Release release = Utils.GetMusicBrainzDataByIdList(releaseGroupIds);

            if (release == null && release.Data.Count == 0)
            {
                metadatas = new List<Metadata>();
                return;
            }

            /* Seleccionamos sólo los que sean del mismo año que el primero */

            int year = Utils.GetDateTime(release.Data[0].Date).Year;
            release.Data = release.Data.Where(item => Utils.GetDateTime(item.Date).Year == year).ToList();

            /* Agrupamos por MBID */

            var groups = release.Data.GroupBy(item => item.Releasegroup.Id).ToList();

            foreach (var group in groups)
            {
                /* Calculamos la probabilidad de cada grupo */

                float a = group.Count();
                int b = release.Data.Count;

                AcoustIDInfo info = infos.First(item => item.ReleaseGroupId == group.First().Releasegroup.Id);

                Metadata data = MetadataResolver2.CreateMetadata(results, info, group.First().Date, a / b, true, storedAlbums);
                metadatas.Add(data);
            }
        }

        private static Metadata CreateMetadata(List<AcoustID.Web.LookupResult> results, AcoustIDInfo info, string date, float probability, bool selected, List<Album> storedAlbums = null)
        {
            int i, j, k;
            Utils.GetIndexes(info.Indexes, out i, out j, out k);

            AcoustID.Web.LookupResult result = results[i];
            AcoustID.Web.Recording recording = result.Recordings[j];
            AcoustID.Web.ReleaseGroup releaseGroup = recording.ReleaseGroups[k];

            Metadata data = new Metadata();

            data.ResultIndex = i;
            data.RecordingIndex = j;
            data.ReleaseGroupIndex = k;
            data.Score = Convert.ToSingle(results[i].Score);
            
            data.Entity.Track = new Track();
            data.Entity.Track.MBID = results[i].Recordings[j].Id;
            data.Entity.Track.Title = results[i].Recordings[j].Title;
            data.Entity.Track.Duration = results[i].Recordings[j].Duration;
            data.Entity.Track.Reliability = (int)(probability * 100);
            
            foreach (AcoustID.Web.Artist artist in results[i].Recordings[j].Artists)
                data.Entity.Artists.Add(new Artist(artist.Id, artist.Name));

            bool found = storedAlbums != null && storedAlbums.Exists(item => item.MBID == releaseGroup.Id);

            if (found)
            {
                data.Entity.Album = storedAlbums.First(item => item.MBID == releaseGroup.Id);
            }
            else
            {                  
                data.Entity.Album = new Album();
                data.Entity.Album.MBID = releaseGroup.Id;
                data.Entity.Album.Title = releaseGroup.Title;
                data.Entity.Album.Year = Utils.GetDateTime(date).Year;
            }

            data.Probability = 0.0f;
            data.Selected = selected;

            return data;
        }

        private static void PrintResults(List<Metadata> metadatas, Logger log = null)
        {
            if (log == null)
                return;

            if (metadatas.Count == 0)
            {
                log.WriteLine(" ----------------------------------------------------------");
                log.WriteLine(" **          NO HAY INFORMACIÓN DE MUSICBRAINZ           **");
                log.WriteLine(" ----------------------------------------------------------");
                return;
            }

            log.WriteLine(String.Empty);
            log.WriteLine(" ----------------------------------------------------------");
            log.WriteLine(" --         INFORMACIÓN DEL ÁLBUM (MUSICBRAINZ)          --");
            log.WriteLine(" ----------------------------------------------------------");
            for (int i = 0; i < metadatas.Count; i++)
            {
                Metadata metadata = metadatas[i];

                if (!metadata.Selected) 
                    continue;

                log.WriteLine(String.Format(" Id    : {0}", metadata.Entity.Album.MBID));
                log.WriteLine(String.Format(" Título: '{0}'", metadata.Entity.Album.Title));
                log.WriteLine(String.Format(" Año   : {1}", i + 1, metadata.Entity.Album.Year == 0 ? "????" : metadata.Entity.Album.Year.ToString())); 
                log.WriteLine(" ----------------------------------------------------------");
            }
        }

        public static List<Entity> GetEntities(Entity entity, int tolerance = 3, bool ignoreTitle = false, bool ignoreArtists = false, bool ignoreAlbum = false)
        {
            List<Entity> entities = new List<Entity>();

            string[] metadata = { "recordings", "releasegroups", "compress" };

            /* La duración del audio en segundos */

            int duration = (int)entity.Track.Duration;

            /* Obtenemos una lista de resultados posibles relacionados con la huella digital */

            string fp = Song.CalculateFingerprint(entity.Track.Path);
            if (String.IsNullOrEmpty(fp))
                return entities;

            AcoustID.Web.LookupService LookupService = new AcoustID.Web.LookupService();
            List<AcoustID.Web.LookupResult> results = LookupService.Get(fp, duration, metadata);

            Entity entityOut = null;
            entities = MetadataResolver2.Resolve(results, entity, out entityOut, ignoreTitle, ignoreArtists, ignoreAlbum);//, false, false);

            return entities;
        }
    }
}
