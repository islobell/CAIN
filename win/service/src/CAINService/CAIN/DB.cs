using System;            
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace CAIN
{
    /// <summary>
    /// Clase para la gestinar el de la información de la base de datos.
    /// </summary>
    public class DB
    {
        private DBConnection DBConnection;          ///< Conexión abierta con la base de datos
        private DBTracks DBTracks;                  ///< Objeto que permite la manipulación de información de la base de datos relacionada con las canciones/pistas 
        private DBAlbums DBAlbums;                  ///< Objeto que permite la manipulación de información de la base de datos relacionada con los álbumes 
        private DBArtists DBArtists;                ///< Objeto que permite la manipulación de información de la base de datos relacionada con los artistas/grupos 
        private DBTags DBTags;                      ///< Objeto que permite la manipulación de información de la base de datos relacionada con las etiquetas       
        private DBTrackAlbums DBTrackAlbums;        ///< Objeto que permite la manipulación de las relaciones entre las canciones/pistas y los álbumes 
        private DBTrackArtists DBTrackArtists;      ///< Objeto que permite la manipulación de las relaciones entre las canciones/pistas y los artistas/grupos
        private DBTrackTags DBTrackTags;            ///< Objeto que permite la manipulación de las relaciones entre las canciones/pistas y las etiquetas 

        /// <summary>
        /// Constructor.
        /// </summary>      
        /// <param name="connection">
        ///    Conexión abierta con la base de datos.
        /// </param> 
        public DB(DBConnection connection)
        {
            this.DBConnection = connection;

            /* Pasamos la conexión a todas las clases que la necesitan */

            this.DBAlbums = new DBAlbums(connection);
            this.DBTracks = new DBTracks(connection);
            this.DBArtists = new DBArtists(connection);
            this.DBTags = new DBTags(connection);
            this.DBTrackArtists = new DBTrackArtists(connection);
            this.DBTrackAlbums = new DBTrackAlbums(connection);
            this.DBTrackTags = new DBTrackTags(connection);
        }

        /// <summary>
        /// Carga la información de las canciones y la devuelve como una lista de objetos de tipo <see cref="Entity" />.
        /// </summary>
        public List<Entity> Load()
        {
            List<Entity> entities = new List<Entity>();

            /* Obtenemos la lista de pistas */

            List<Track> tracks = this.DBTracks.List();

            /* Para cada pista... */

            foreach (Track track in tracks)
            {
                /* Obtenemos la lista de álbumes (de momento sólo habrá uno) */

                List<long> albumIds = this.DBTrackAlbums.List(track.ID);

                Debug.Assert(albumIds.Count > 0 && albumIds.Count < 2);

                Album album = this.DBAlbums.Get(albumIds[0]);

                /* Obtenemos la lista de artistas */
                
                List<long> artistIds = this.DBTrackArtists.List(track.ID);

                Debug.Assert(artistIds.Count > 0);

                List<Artist> artists = new List<Artist>();
                if (artistIds.Count > 0)
                {
                    foreach (long artistId in artistIds)
                        artists.Add(this.DBArtists.Get(artistId));
                }

                /* Obtenemos la lista de etiquetas */

                List<long> tagIds = this.DBTrackTags.List(track.ID);

                List<Tag> tags = new List<Tag>();
                if (tagIds.Count > 0)
                {
                    foreach (long tagId in tagIds)
                        tags.Add(this.DBTags.Get(tagId));
                }

                Entity entity = new Entity();
                entity.Track = track;
                entity.Album = album;
                entity.Artists = artists;
                entity.Tags = tags;

                entities.Add(entity);
            }

            return entities;
        } 

        /// <summary>
        ///    Comprueba si la entidad existe en la base de datos.
        /// </summary>   
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la entidad existe en la base de datos. False, sino.
        /// </returns>
        public bool Exists(Entity entity)
        {
            return this.DBTracks.Exists(entity.Track);
        }    

        /// <summary>
        ///    Inserta una canción/pista en la base de datos.
        /// </summary>     
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la inserción en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        private bool InsertTrack(ref Entity entity)
        {
            bool done = false;

            /* Las canciones NO se pueden repetir */

            bool valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Track.IsValid() : true;
            //Debug.Assert(valid);

            /* Asumimos que la base de datos estará vacía */

            if (valid)
            {
                long trackID = this.DBTracks.GetID(entity.Track);
                Debug.Assert(trackID == 0);

                done = this.DBTracks.Insert(ref entity.Track);
            }

            //Debug.Assert(done);
            return done;
        }

        /// <summary>
        ///    Inserta un álbum en la base de datos.
        /// </summary>    
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la inserción en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        private bool InsertAlbum(ref Entity entity)
        {
            bool done = false;

            /* Los álbumes se pueden repetir */

            bool valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Album.IsValid() : true;
            //Debug.Assert(valid);

            if (valid)
            {
                long albumID = this.DBAlbums.GetID(entity.Album);

                if (albumID == 0)
                    done = this.DBAlbums.Insert(ref entity.Album);
                else
                {
                    entity.Album = this.DBAlbums.Get(albumID);
                    done = true;
                }

                if (done && !this.DBTrackAlbums.Exists(entity.Track.ID, entity.Album.ID))
                    done = this.DBTrackAlbums.Insert(entity.Track.ID, entity.Album.ID);
            }

            //Debug.Assert(done);
            return done;
        }

        /// <summary>
        ///    Inserta un artista/grupo en la base de datos.
        /// </summary>    
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la inserción en la base de datos se realizó correctamente. False, sino.
        /// </returns>         
        private bool InsertArtists(ref Entity entity)
        {
            bool done = true;

            /* Los artistas se pueden repetir */

            bool valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Artists.TrueForAll(item => item.IsValid()) : true;
            //Debug.Assert(valid);

            if (valid)
            {
                for (int i = 0; i < entity.Artists.Count && done; i++)
                {
                    CAIN.Artist tmp = entity.Artists[i];

                    long artistID = this.DBArtists.GetID(entity.Artists[i]);

                    if (artistID == 0)
                    {
                        done = this.DBArtists.Insert(ref tmp);
                        entity.Artists[i] = tmp;
                    }
                    else
                    {
                        entity.Artists[i] = this.DBArtists.Get(artistID);
                        done = true;
                    }

                    if (!this.DBTrackArtists.Exists(entity.Track.ID, entity.Artists[i].ID))
                        done = this.DBTrackArtists.Insert(entity.Track.ID, entity.Artists[i].ID);
                }
            }

            //Debug.Assert(done);
            return done;
        }

        /// <summary>
        ///    Inserta una etiqueta en la base de datos.
        /// </summary>   
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la inserción en la base de datos se realizó correctamente. False, sino.
        /// </returns>        
        private bool InsertTags(ref Entity entity)
        {
            bool done = true;

            /* Los tags se pueden repetir */

            bool valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Tags.TrueForAll(item => item.IsValid()) : true;
            //Debug.Assert(valid);

            if (valid)
            {
                for (int i = 0; i < entity.Tags.Count && done; i++)
                {
                    CAIN.Tag tmp = entity.Tags[i];

                    long tagID = this.DBTags.GetID(entity.Tags[i]);
                    
                    if (tagID == 0)
                    {
                        done = this.DBTags.Insert(ref tmp);
                        entity.Tags[i] = tmp;
                    }
                    else
                    {
                        entity.Tags[i] = this.DBTags.Get(tagID); 
                        done = true;
                    }

                    if (!this.DBTrackTags.Exists(entity.Track.ID, entity.Tags[i].ID))
                        done = this.DBTrackTags.Insert(entity.Track.ID, entity.Tags[i].ID);
                }
            }

            //Debug.Assert(done);
            return done;
        }

        /// <summary>
        ///    Inserta un objeto de tipo <see cref="Entity" /> en la base de datos.
        /// </summary>    
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la inserción en la base de datos se realizó correctamente. False, sino.
        /// </returns>         
        public bool Insert(Entity entity)
        {
            this.DBConnection.BeginTransaction();
                          
            bool done = false;

            done = InsertTrack(ref entity);
            done = InsertAlbum(ref entity);
            done = InsertArtists(ref entity);
            done = InsertTags(ref entity);

            if (done)
            {
                this.DBConnection.Commit();  
                return true;
            }
            else
            {
                this.DBConnection.Rollback();
                return false;
            }
        }

        /// <summary>
        ///    Modifica un objeto de tipo <see cref="Entity" /> en la base de datos.
        /// </summary>    
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la modificación en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        public bool Edit(Entity entity)
        {
            this.DBConnection.BeginTransaction();
                      
            bool valid;
            bool done = false;

            /* Modificamos la información de la canción/pista */

            valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Track.IsValid() : true;
            Debug.Assert(valid);

            if (valid)
            {
                /* El ID debe existir en la base de datos */
                                                  
                Debug.Assert(this.DBTracks.Exists(entity.Track.ID)); 
                done = this.DBTracks.Edit(entity.Track);
            }

            /* Modificamos la información del álbum */

            valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Album.IsValid() : true;
            Debug.Assert(valid);

            if (done && valid)
            {
                /* El álbum puede ser haber cambiado */

                long albumID = this.DBAlbums.GetID(entity.Album);
                //Debug.Assert(albumID > 0);

                if (albumID > 0)
                {
                    /* Si el álbum ya existe en la base de datos, lo asociamos con él; sino existe, lo editamos. Si no existe, lo creamos */

                    if (albumID != entity.Album.ID)
                    {
                        if (this.DBTrackAlbums.Count(entity.Album.ID) == 1)
                            this.DBAlbums.Delete(entity.Album.ID);

                        this.DBTrackAlbums.Delete(entity.Track.ID, entity.Album.ID); 
                                                                    
                        entity.Album = this.DBAlbums.Get(albumID);

                        done = InsertAlbum(ref entity);  
                    }
                    else
                    {
                        done = this.DBAlbums.Edit(entity.Album);
                    }
                }
                else
                {
                    this.DBTrackAlbums.Delete(entity.Track.ID, entity.Album.ID); 
                    
                    done = InsertAlbum(ref entity);
                }
            }

            /* Modificamos la información de los artistas/grupo */

            valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Artists.TrueForAll(item => item.IsValid()) : true;
            Debug.Assert(valid);

            if (done && valid)
            {
                /* Los artistas pueden haber cambiado */

                this.DBTrackArtists.DeleteAll(entity.Track.ID);

                for (int i = 0; i < entity.Artists.Count && done; i++)
                {
                    CAIN.Artist tmp = entity.Artists[i]; 
                    
                    done = this.DBArtists.Insert(ref tmp);
                    entity.Artists[i] = tmp;

                    if (done && !this.DBTrackArtists.Exists(entity.Track.ID, entity.Artists[i].ID))
                        done = this.DBTrackArtists.Insert(entity.Track.ID, entity.Artists[i].ID); 
                }

                this.DBArtists.DeleteNotUsed();
            }

            /* Modificamos la información de las etiquetas */

            valid = entity.Track.Status == Track.StatusTypes.Cataloged ? entity.Tags.TrueForAll(item => item.IsValid()) : true;
            Debug.Assert(valid);

            if (done && valid)
            {
                /* Las etiquetas pueden haber cambiado */

                this.DBTrackTags.DeleteAll(entity.Track.ID);

                for (int i = 0; i < entity.Tags.Count && done; i++)
                {
                    CAIN.Tag tmp = entity.Tags[i];

                    done = this.DBTags.Insert(ref tmp);
                    entity.Tags[i] = tmp;

                    if (done && !this.DBTrackTags.Exists(entity.Track.ID, entity.Tags[i].ID))
                        done = this.DBTrackTags.Insert(entity.Track.ID, entity.Tags[i].ID); 
                }

                this.DBTags.DeleteNotUsed();
            }

            if (done)
            {
                this.DBConnection.Commit();     
                return true;
            }
            else
            {
                this.DBConnection.Rollback();
                return false;
            }
        }

        /// <summary>
        ///    Modifica una lista de objetos de tipo <see cref="Entity" /> en la base de datos.
        /// </summary>    
        /// <param name="entities">             
        ///    Lista de objetos de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la modificación en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        public bool Edit(List<Entity> entities)
        {
            bool result = false;

            foreach (Entity entity in entities)
            {
                result = Edit(entity);
                Debug.Assert(result);
            }

            return result;
        }

        /// <summary>
        ///    Elimina la información relacionada con un objeto de tipo <see cref="Entity" /> de la base de datos.
        /// </summary>   
        /// <param name="entity">             
        ///    Un objeto de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la eliminación en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        public bool Delete(Entity entity)
        {
            this.DBConnection.BeginTransaction();

            bool done = this.DBTracks.Delete(entity.Track.ID);
            
            if (done)
            {
                this.DBConnection.Commit();
                return true;
            }
            else
            {
                this.DBConnection.Rollback();
                return false;
            }
        }
           
        /// <summary>
        ///    Elimina la información relacionada con una lista de objetos de tipo <see cref="Entity" /> de la base de datos.
        /// </summary>    
        /// <param name="entities">             
        ///    Lista de objetos de tipo <see cref="Entity" />.
        /// </param> 
        /// <returns>
        ///    True, si la eliminación en la base de datos se realizó correctamente. False, sino.
        /// </returns>
        public bool Delete(List<Entity> entities)
        {
            this.DBConnection.BeginTransaction();
                               
            bool done = false;
            foreach (Entity entity in entities)
            {
                done = this.DBTracks.Delete(entity.Track.ID);
                if (!done) break;
            }

            if (done)
            {
                this.DBConnection.Commit();
                return true;
            }
            else
            {
                this.DBConnection.Rollback();
                return false;
            }
        }   

        /// <summary>
        ///    Elimina toda la información de la base de datos.
        /// </summary> 
        public bool DeleteAll()
        {
            this.DBConnection.BeginTransaction();

            try
            {
                this.DBTracks.DeleteAll();
                this.DBAlbums.DeleteAll();
                this.DBArtists.DeleteAll();
                this.DBTags.DeleteAll(); 
            
                this.DBConnection.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.DBConnection.Rollback();
                return false;
            }             
        }   
           
        /// <summary>
        ///    Obtiene una lista con todos los nombre de etiquetas que hay en la base de datos.
        /// </summary> 
        /// <returns>
        ///    Lista de nombres de etiqueta.
        /// </returns>
        public List<string> GetTagNames()
        {
            return this.DBTags.ListNames();
        }
    }
}
