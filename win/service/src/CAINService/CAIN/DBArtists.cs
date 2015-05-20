using System;                      
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBArtists : DBTable, IDBTable<Artist>
    {
        public DBArtists(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long ID)
        {
            string sql = "SELECT COUNT(*) FROM Artists WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }

        public bool Exists(Artist artist)
        {
            return GetID(artist) > 0 ? true : false;
            /*string sql = "SELECT COUNT(*) FROM Artists WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", artist.ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;*/
        }

        public long GetID(Artist artist)
        {
            /*if (String.IsNullOrEmpty(artist.MBID))
                return 0;*/

            string sql = "SELECT ID FROM Artists WHERE UPPER(Name) = UPPER(@Name)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@Name", artist.Name);
            object result = cmd.ExecuteScalar();

            if (result == null) return 0;

            return Convert.ToUInt32(result);
        }

        public Artist Get(long ID)
        {
            string sql = "SELECT * FROM Artists WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows || !reader.Read())
            {
                reader.Close();
                return null;
            }

            Artist artist = new Artist();            
            artist.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            artist.MBID = reader.GetString(reader.GetOrdinal("MBID"));
            artist.Name = reader.GetString(reader.GetOrdinal("Name"));
            reader.Close();

            return artist;
        }

        public List<Artist> List(int count = 0)
        {
            string sql = count > 0 ? "SELECT * FROM Artists LIMIT @Count" : "SELECT * FROM Artists";
            
            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            if (count > 0) cmd.Parameters.AddWithValue("@Count", count);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Artist> artists = new List<Artist>();

            if (!reader.HasRows)
            {
                reader.Close();
                return artists;
            }

            while (reader.Read())
            {
                Artist artist = new Artist();             
                artist.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                artist.MBID = reader.GetString(reader.GetOrdinal("MBID"));
                artist.Name = reader.GetString(reader.GetOrdinal("Name"));

                artists.Add(artist);
            }
            reader.Close();

            return artists;
        }

        public bool Insert(ref Artist artist)
        {
            string sql = "INSERT INTO Artists (MBID, Name) VALUES (@MBID, @Name)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@MBID", artist.MBID);
            cmd.Parameters.AddWithValue("@Name", artist.Name);
            int result = cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            artist.ID = Convert.ToUInt32(cmd.ExecuteScalar());

            return result == 1 ? true : false;
        }

        public bool Edit(Artist artist)
        {
            string sql = "UPDATE Artists SET Name = @Name WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql.ToString(), this.Connection);
            cmd.Parameters.AddWithValue("@Name", artist.Name);
            cmd.Parameters.AddWithValue("@ID", artist.ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }
        
        public bool Delete(long ID)
        {
            string sql = "DELETE FROM Artists WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public void DeleteNotUsed()
        {
            string sql = "DELETE FROM Artists WHERE ID NOT IN (SELECT ArtistID FROM TrackArtists)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
