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
    /// Clase para catalogar una canción en base a la información del álbúm que aparece en los tags del archivo.
    /// </summary>
    public static class MetadataResolver1
    {
        /// <summary>
        ///    Método para catalogar la información relacionada con la canción a partir de una lista de resultados obtenidos desde AcoustID.
        /// </summary>
        /// <param name="option">
        ///    La opción elegida: 0 - obtener la información del álbum a partir de los metadatos ; 1 - obtener la información del álbum original.
        /// </param> 
        /// <param name="results">
        ///    Lista de resultados obtenidos desde AcoustID.
        /// </param> 
        /// <param name="file">
        ///    Objeto de tipo <see cref="TagLib.File" />.
        /// </param> 
        /// <param name="entityOut">
        ///    Objeto de tipo <see cref="Entity" /> que contendrá los información que más se ajuste a los metadatos de la canción.
        /// </param>       
        /// <param name="storedAlbums">
        ///    Lista de álbumes que ya han sido catalogados.
        /// </param>
        /// <param name="log">
        ///    Objeto de tipo <see cref="Logger" /> que registrá los diferentes pasos del proceso de catalogación.
        /// </param>
        public static void Resolve(List<AcoustID.Web.LookupResult> results, TagLib.File file, out Entity entityOut, List<Album> storedAlbums = null, Logger log = null)
        {
            entityOut = null;

            /* Construimos un entidad a partir de los metadatos de la canción */
                    
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

            MetadataResolver1.Resolve(results, entityIn, out entityOut, 3, false, false, false, true, true, true, true, storedAlbums, log);
        }

        /// <summary>
        ///    Método para catalogar la información relacionada con la canción a partir de una lista de resultados obtenidos desde AcoustID.
        /// </summary>
        /// <param name="option">
        ///    La opción elegida: 0 - obtener la información del álbum a partir de los metadatos ; 1 - obtener la información del álbum original.
        /// </param> 
        /// <param name="results">
        ///    Lista de resultados obtenidos desde AcoustID.
        /// </param>
        /// <param name="entityIn">
        ///    Objeto de tipo <see cref="Entity" /> que contendrá la información a catalogar.
        /// </param> 
        /// <param name="entityOut">
        ///    Objeto de tipo <see cref="Entity" /> que contendrá la información que más se ajuste a la información de la entidad de entrada.
        /// </param>      
        /// <param name="tolerance">
        ///    Lista de álbumes que ya han sido catalogados.
        /// </param>      
        /// <param name="ignoreAlbum">
        ///    Indica si vamos a tener en cuenta la información del álbum en el proceso de catalogación.
        /// </param> 
        /// <param name="ignoreTitle">                   
        ///    Indica si vamos a tener en cuenta la información del título en el proceso de catalogación.
        /// </param> 
        /// <param name="ignoreArtists">                 
        ///    Indica si vamos a tener en cuenta la información de los artistas en el proceso de catalogación.
        /// </param>     
        /// <param name="deleteNoDuration">
        ///    Indica si hay que ignorar los metadatos cuya duración sea 0.
        /// </param>     
        /// <param name="deleteNoYear">
        ///    Indica si hay que ignorar los metadatos cuyo año sea 0.
        /// </param>     
        /// <param name="searchCover">
        ///    Indicas si hay que buscar la carátula del álbum.
        /// </param>       
        /// <param name="printResults">
        ///    Indica si hay que mostrar los resultados de la catalogación.
        /// </param>      
        /// <param name="storedAlbums">
        ///    Lista de álbumes que ya han sido catalogados.
        /// </param>
        /// <param name="log">
        ///    Objeto de tipo <see cref="Logger" /> que registrá los diferentes pasos del proceso de catalogación.
        /// </param>
        public static List<Entity> Resolve(List<AcoustID.Web.LookupResult> results, 
                                    Entity entityIn, 
                                    out Entity entityOut,
                                    int tolerance,   
                                    bool ignoreTitle,
                                    bool ignoreArtists,
                                    bool ignoreAlbum,
                                    bool deleteNoDuration = false,
                                    bool deleteNoYear = false,
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

            //int tolerance = 3;    /* Cogemos un valor intermedio, entre 0 y 9 */
            bool found = false;
            List<AcoustIDInfo> infos = new List<AcoustIDInfo>(); 

            try
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

                            /* Comparamos los títulos de los álbumes, sino coinciden no continuamos */

                            float reliability = 0.0f;
                            if (!ignoreAlbum)
                            {
                                found = MetadataComparer.CompareAlbums(entityIn.Album.Title, releaseGroup.Title, tolerance / 10.0f, out reliability);

                                if (!found)
                                    continue;
                            }

                            /* Si coincide, lo guardamos en la lista de álbumes */

                            infos.Add(new AcoustIDInfo(String.Format("{0};{1};{2}", i, j, k), releaseGroup.Id, reliability));
                        }
                    }
                }

                //Console.WriteLine(String.Empty);

                found = false;

                if (log != null)
                    log.WriteLine(String.Format(" Tolerancia [0-9]: {0}", tolerance));

                /* Si hemos obtenido resultados, los filtramos y los mostramos */

                if (infos.Count > 0)
                {
                    //DateTime dt1 = DateTime.Now;

                    /*  */

                    List<Metadata> metadatas = new List<Metadata>();
                    MetadataResolver1.ResolveAlbum(results, infos, ref metadatas, storedAlbums);
                    if (printResults) MetadataResolver1.PrintResults(metadatas, log);

                    /* Quitamos los metadatos cuya duración de la pista sea 0 o cuyo año del álbum sea 0 o ambos */

                    if (deleteNoDuration && deleteNoYear)
                        metadatas = metadatas.Where(item => item.Entity.Track.Duration > 0 && item.Entity.Album.Year > 0).ToList();
                    else if (deleteNoDuration && !deleteNoYear)
                        metadatas = metadatas.Where(item => item.Entity.Track.Duration > 0).ToList();
                    else if (!deleteNoDuration && deleteNoYear)
                        metadatas = metadatas.Where(item => item.Entity.Album.Year > 0).ToList();

                    /* Si hay metadatos, cogemos el álbum del primer elemento */

                    if (metadatas.Count > 0)
                    {
                        /* Como ya están ordenados, cogemos el primero de la lista */

                        entityOut = metadatas[0].Entity;  
                        entityOut.Track.Path = entityIn.Track.Path;
                        entityOut.Album.Cover = entityIn.Album.Cover;
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
                    
                if  (!found)
                {
                    entityOut = entityIn;
                    entityOut.Track.Status = Track.StatusTypes.NotCataloged;

                    if (log != null)
                    {
                        log.WriteLine(" ··························································");
                        log.WriteLine(" ················· ARCHIVO NO CATALOGADO ··················");
                        log.WriteLine(" ··························································");
                    }
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Debug.Assert(entityOut != null);
            return entities;
        }

        private static void ResolveAlbum(List<AcoustID.Web.LookupResult> results, List<AcoustIDInfo> infos, ref List<Metadata> metadatas, List<Album> storedAlbums = null)
        {
            Metadata data = null;

            /* Si sólo hay un álbum, no tenemos que procesar nada */

            if (infos.Count == 1)
            {
                data = MetadataResolver1.CreateMetadata(results, infos[0], 1.0f, true, storedAlbums != null, storedAlbums); 
                metadatas.Add(data);  
                return;
            }
            
            /* Agrupamos los álbumes por su Id */

            var groups = infos.GroupBy(item => item.ReleaseGroupId).ToList();

            /* Calculamos la probabilidad de cada grupo */

            foreach (var group in groups)
            {
                /* Calculamos la probabilidad de cada grupo */

                float a = group.Count();
                int b = infos.Count();

                /* Obtenemos, de la lista de álbumes, el álbum que tiene la misma clave que el elemento actual */

                AcoustIDInfo info = infos.First(item => item.Indexes == group.First().Indexes);

                data = MetadataResolver1.CreateMetadata(results, info, a / b, true, storedAlbums != null, storedAlbums);
                metadatas.Add(data);
            }

            /* Seleccionamos los álbumes que tienen más posibilidades de ser correctos */

            float reliability = 0.0f;
            float probability = 0.0f;

            foreach (Metadata metadata in metadatas)
            {
                if (!metadata.Selected)
                    continue;

                if (metadata.Entity.Track.Reliability > reliability ||
                    Utils.FloatEquals(metadata.Entity.Track.Reliability, reliability) && metadata.Probability > probability ||
                    Utils.FloatEquals(metadata.Entity.Track.Reliability, reliability) && Utils.FloatEquals(metadata.Probability, probability))
                {
                    //metadata.Selected = true;
                    reliability = metadata.Entity.Track.Reliability;
                    probability = metadata.Probability;
                }
            }

            /* Ordenamos los metadatos por fiabilidad y probabilidad (de mayor a menor) */

            metadatas = metadatas.OrderByDescending(item => item.Entity.Track.Reliability).ThenByDescending(item => item.Probability).OrderByDescending(item => item.Selected).ToList(); 
        }

        private static Metadata CreateMetadata(List<AcoustID.Web.LookupResult> results, AcoustIDInfo info, float probability, bool selected, bool searchYear, List<Album> storedAlbums = null)
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
            
            //data.Entity = new Entity();

            data.Entity.Track = new Track();
            data.Entity.Track.MBID = results[i].Recordings[j].Id;
            data.Entity.Track.Title = results[i].Recordings[j].Title;
            data.Entity.Track.Duration = results[i].Recordings[j].Duration;
            data.Entity.Track.Reliability = (int)(info.Reliability * 100);

            foreach (AcoustID.Web.Artist artist in results[i].Recordings[j].Artists)
                data.Entity.Artists.Add(new Artist(artist.Id, artist.Name));

            /*data.Entity.Album = new Album();
            data.Entity.Album.MBID = releaseGroup.Id;
            data.Entity.Album.Title = releaseGroup.Title;*/

            bool found = storedAlbums != null && storedAlbums.Exists(item => item.MBID == releaseGroup.Id);

            if (found)
            {
                data.Entity.Album = storedAlbums.First(item => item.MBID == releaseGroup.Id);
                /*Album album = storedAlbums.First(item => item.MBID == releaseGroup.Id);
                data.Entity.Album.Year = album.Year;*/
            }
            else
            {
                data.Entity.Album = new Album();
                data.Entity.Album.MBID = releaseGroup.Id;
                data.Entity.Album.Title = releaseGroup.Title;

                if (searchYear)
                {
                    MusicBrainz.Data.Release release = Utils.GetMusicBrainzDataById(data.Entity.Album.MBID);

                    if (release != null && release.Data.Count > 0)
                    {
                        if (String.IsNullOrEmpty(release.Data[0].Date))
                            selected = false;
                        else
                            data.Entity.Album.Year = Utils.GetDateTime(release.Data[0].Date).Year;
                    }
                } 
            }

            data.Probability = probability;
            data.Selected = selected;

            return data;
        }

        private static void PrintResults(List<Metadata> metadatas, Logger log = null)
        {
            if (metadatas.Count == 0)
                return;

            if (log == null)
                return;

            log.WriteLine(" ==========================================================");
            log.WriteLine(" ==                  ARCHIVO CATALOGADO                  ==");
            log.WriteLine(" ==========================================================");  
            for (int i = 0; i < metadatas.Count; i++)
            {
                Metadata metadata = metadatas[i];

                log.WriteLine(String.Format(" Result Index      : {0,3:d} - Título    : [ '{1}' - Score: {2:0.000000} ]", metadata.ResultIndex, metadata.Entity.Track.Title, metadata.Score));
                log.WriteLine(String.Format(" Recording Index   : {0,3:d} - Álbum     : [ {1} - '{2}' - Fiabilidad: {3,7:0.00%} - Probabilidad: {4,7:0.00%} ] {5}", metadata.RecordingIndex, metadata.Entity.Album.MBID, metadata.Entity.Album.Title, metadata.Entity.Track.Reliability / 100.0f, metadata.Probability, metadata.Selected ? String.Format("({0})", i + 1) : String.Empty));
                log.WriteLine(String.Format(" ReleaseGroup Index: {0,3:d} - Artista(s): [ '{1}' ]", metadata.ReleaseGroupIndex, String.Join("', '", metadata.Entity.Artists)));
                log.WriteLine(" ----------------------------------------------------------");
            }

            if (metadatas.Count(item => item.Selected) == 0)
            {
                //log.WriteLine(" ----------------------------------------------------------");
                log.WriteLine(" **          NO HAY INFORMACIÓN DE MUSICBRAINZ           **");
                log.WriteLine(" ----------------------------------------------------------");
                return;
            }

            log.WriteLine(" --         INFORMACIÓN DEL ÁLBUM (MUSICBRAINZ)          --");
            log.WriteLine(" ----------------------------------------------------------");

            for (int i = 0; i < metadatas.Count; i++)
            {
                Metadata metadata = metadatas[i]; 
 
                if (!metadata.Selected) continue;

                log.WriteLine(String.Format(" ({0}) Año: {1}", i + 1, metadata.Entity.Album.Year == 0 ? "????" : metadata.Entity.Album.Year.ToString()));//, Utils.GetDateTime(metadata.Album.Date).ToString(@"dd/MM/yyyy")));
                log.WriteLine(" ----------------------------------------------------------");
            } 
        }

        public static List<Entity> GetEntities(Entity entity, int tolerance = 3, bool ignoreTitle = false, bool ignoreArtists = false, bool ignoreAlbum = false)//, bool deleteNoDuration = false, bool deleteNoYear = false)
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
            entities = MetadataResolver1.Resolve(results, entity, out entityOut, tolerance, ignoreTitle, ignoreArtists, ignoreAlbum);//, deleteNoDuration, deleteNoYear);//, false, false);

            List<string> ids = entities.Select(item => item.Album.MBID).ToList();
            MusicBrainz.Data.Release release = CAIN.Utils.GetMusicBrainzDataByIdList(ids);

            if (release == null)
                return entities;

            foreach (CAIN.Entity ent in entities)
            {                                  
                MusicBrainz.Data.ReleaseData data = release.Data.Find(item => item.Releasegroup.Id == ent.Album.MBID);

                if (data != null && !String.IsNullOrEmpty(data.Date))
                    ent.Album.Year = CAIN.Utils.GetDateTime(data.Date).Year;
            }

            return entities;
        }
    }

}

       