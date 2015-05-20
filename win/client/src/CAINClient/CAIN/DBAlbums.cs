using System;                   
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{       
    /// <summary>
    /// Clase que permite la manipulación de información de la base de datos relacionada con los álbumes.
    /// </summary>
    public class DBAlbums : DBTable, IDBTable<Album>
    {
        /// <summary>
        /// Constructor.
        /// </summary>      
        /// <param name="connection">
        ///    Conexión abierta con la base de datos.
        /// </param> 
        public DBAlbums(DBConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Comprueba si existe un álbum en la base de datos en base a su identificador.
        /// </summary>      
        /// <param name="ID">
        ///    Identificador del álbum
        /// </param>
        /// <returns>
        ///    True, si el identificador existe en la base de datos. False, sino.
        /// </returns> 
        public bool Exists(long ID)
        {
            string sql = "SELECT COUNT(*) FROM Albums WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0;
        }

        /// <summary>
        /// Comprueba si existe un álbum en la base de datos en base a su información.
        /// </summary>      
        /// <param name="album">
        ///    Objeto de tipo <see cref="Album" />.
        /// </param> 
        /// <returns>
        ///    True, si la entidad existe en la base de datos. False, sino.
        /// </returns>
        public bool Exists(Album album)
        {
            return GetID(album) > 0;
            /*if (String.IsNullOrEmpty(album.MBID))
                return false;

            string sql = "SELECT COUNT(*) FROM Albums WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", album.ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false; */
        }

        /// <summary>
        /// Obtiene el identificador de un álbum en base a su información.
        /// </summary>      
        /// <param name="album">
        ///    Objeto de tipo <see cref="Album" />.
        /// </param>
        /// <returns>
        ///    El identificador del álbum, si existe en la base de datos. 0, sino.
        /// </returns> 
        public long GetID(Album album)
        {
            string sql = "SELECT ID FROM Albums WHERE UPPER(Title) = UPPER(@Title) AND Year = @Year";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@Title", album.Title);
            cmd.Parameters.AddWithValue("@Year", album.Year);
            object result = cmd.ExecuteScalar();

            if (result == null) return 0;

            return Convert.ToUInt32(result);
        }

        /// <summary>
        /// Obtiene un álbum a partir de la información relacionada con un identificador.
        /// </summary>      
        /// <param name="ID">
        ///    Identificador del álbum.
        /// </param>  
        /// <returns>
        ///    Objeto de tipo <see cref="Album" />. Null, sino.
        /// </returns> 
        public Album Get(long ID)
        {
            string sql = "SELECT * FROM Albums WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows || !reader.Read())
            {
                reader.Close();
                return null;
            }

            Album album = new Album(); 
            album.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            album.MBID = reader.GetString(reader.GetOrdinal("MBID"));
            album.Title = reader.GetString(reader.GetOrdinal("Title"));
            album.Year = reader.GetInt32(reader.GetOrdinal("Year"));
            if (!reader.IsDBNull(reader.GetOrdinal("Cover")))
            {
                long len = reader.GetBytes(reader.GetOrdinal("Cover"), 0, null, 0, 0);
                byte[] buffer = new byte[len];
                long bytesRead = reader.GetBytes(reader.GetOrdinal("Cover"), 0, buffer, 0, (int)len);
                album.Cover = Utils.ByteArrayToImage(buffer);
            }
            reader.Close();

            return album;
        }

        /// <summary>
        /// Obtiene una lista de álbumes a partir de la información relacionada con un identificador.
        /// </summary>      
        /// <param name="count">
        ///    Número de objetos a devolver. Por defecto se devuelven todos.
        /// </param>  
        /// <returns>
        ///    Lista de objetos de tipo <see cref="Album" />.
        /// </returns>
        public List<Album> List(int count = 0)
        {
            string sql = count > 0 ? "SELECT * FROM Albums LIMIT @Count" : "SELECT * FROM Albums";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            if (count > 0) cmd.Parameters.AddWithValue("@Count", count);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Album> albums = new List<Album>();

            if (!reader.HasRows)
            {
                reader.Close();
                return albums;
            }

            while (reader.Read())
            {
                Album album = new Album();
                album.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                album.MBID = reader.GetString(reader.GetOrdinal("MBID"));
                album.Title = reader.GetString(reader.GetOrdinal("Title"));
                album.Year = reader.GetInt32(reader.GetOrdinal("Year"));                   
                if (!reader.IsDBNull(reader.GetOrdinal("Cover")))
                {
                    long len = reader.GetBytes(reader.GetOrdinal("Cover"), 0, null, 0, 0);
                    byte[] buffer = new byte[len];
                    long bytesRead = reader.GetBytes(reader.GetOrdinal("Cover"), 0, buffer, 0, (int)len);
                    album.Cover = Utils.ByteArrayToImage(buffer);
                }

                albums.Add(album);
            }
            reader.Close();

            return albums;
        }

        /// <summary>
        /// Inserta un álbum en la base de datos.
        /// </summary>      
        /// <param name="album">
        ///    Objeto de tipo <see cref="Album" />.
        /// </param>
        /// <returns>
        ///    True, si la inserción se realizo correctamente. False, sino.
        /// </returns>
        public bool Insert(ref Album album)
        {
            string sql = "INSERT INTO Albums (MBID, Title, Year, Cover) VALUES (@MBID, @Title, @Year, @Cover)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@MBID", album.MBID);
            cmd.Parameters.AddWithValue("@Title", album.Title);
            cmd.Parameters.AddWithValue("@Year", album.Year);
            cmd.Parameters.AddWithValue("@Cover", Utils.ImageToByteArray(album.Cover));
            int result = cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            album.ID = Convert.ToUInt32(cmd.ExecuteScalar());

            return result == 1 ? true : false;
        }

        /// <summary>
        /// Modifica un álbum de la base de datos.
        /// </summary>      
        /// <param name="album">
        ///    Objeto de tipo <see cref="Album" />.
        /// </param>
        /// <returns>
        ///    True, si la modificación se realizo correctamente. False, sino.
        /// </returns>
        public bool Edit(Album album)
        {
            string sql = "UPDATE Albums SET Title = @Title, Year = @Year, Cover = @Cover WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql.ToString(), this.Connection);
            cmd.Parameters.AddWithValue("@Title", album.Title);
            cmd.Parameters.AddWithValue("@Year", album.Year);
            cmd.Parameters.AddWithValue("@Cover", Utils.ImageToByteArray(album.Cover));
            cmd.Parameters.AddWithValue("@ID", album.ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        /// <summary>
        /// Elimina un álbum de la base de datos.
        /// </summary>      
        /// <param name="album">
        ///    Identificador del álbum.
        /// </param>
        /// <returns>
        ///    True, si la eliminación se realizo correctamente. False, sino.
        /// </returns>
        public bool Delete(long ID)
        {
            string sql = "DELETE FROM Albums WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        /// <summary>
        /// Elimina todos los álbumes de la base de datos.
        /// </summary> 
        public void DeleteAll()
        {
            string sql = "DELETE FROM Albums";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.ExecuteNonQuery();
        } 

        /// <summary>
        /// Elimina los álbumes huérfanos de la base de datos.
        /// </summary>
        public void DeleteNotUsed()
        {
            string sql = "DELETE FROM Albums WHERE ID NOT IN (SELECT AlbumID FROM TrackAlbums)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
