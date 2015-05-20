using System;                     
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBTrackAlbums : DBTable
    {
        public DBTrackAlbums(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long TrackID, long AlbumID)
        {
            string sql = "SELECT COUNT(*) FROM TrackAlbums WHERE TrackID = @TrackID AND AlbumID = @AlbumID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false;
        }

        public int Count(long AlbumID)
        {
            string sql = "SELECT COUNT(*) FROM TrackAlbums WHERE AlbumID = @AlbumID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result);
        }
        
        public List<long> List(long TrackID)
        {
            string sql = "SELECT AlbumID FROM TrackAlbums WHERE TrackID = @TrackID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<long> ids = new List<long>();

            if (!reader.HasRows)
            {
                reader.Close();
                return ids;
            }

            while (reader.Read())
            {
                long Id = reader.GetInt32(reader.GetOrdinal("AlbumID"));
                ids.Add(Id);
            }
            reader.Close();

            return ids;
        }

        public bool Insert(long TrackID, long AlbumID)
        {
            string sql = "INSERT INTO TrackAlbums (TrackID, AlbumID) VALUES (@TrackID, @AlbumID)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public bool Delete(long TrackID, long AlbumID)
        {
            string sql = "DELETE FROM TrackAlbums WHERE TrackID = @TrackID AND AlbumID = @AlbumID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@AlbumID", AlbumID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }
    }
}
