using System;                     
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBTrackArtists : DBTable
    {
        public DBTrackArtists(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long TrackID, long ArtistID)
        {
            string sql = "SELECT COUNT(*) FROM TrackArtists WHERE TrackID = @TrackID AND ArtistID = @ArtistID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@ArtistID", ArtistID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false;
        }

        public int Count(long ArtistID)
        {
            string sql = "SELECT COUNT(*) FROM TrackArtists WHERE ArtistID = @ArtistID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ArtistID", ArtistID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public List<long> List(long TrackID)
        {
            string sql = "SELECT ArtistID FROM TrackArtists WHERE TrackID = @TrackID";

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
                long Id = reader.GetInt32(reader.GetOrdinal("ArtistID"));
                ids.Add(Id);
            }
            reader.Close();

            return ids;
        }
        
        public bool Insert(long TrackID, long ArtistID)
        {
            string sql = "INSERT INTO TrackArtists (TrackID, ArtistID) VALUES (@TrackID, @ArtistID)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@ArtistID", ArtistID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }
        
        public bool Delete(long TrackID, long ArtistID)
        {
            string sql = "DELETE FROM TrackArtists WHERE TrackID = @TrackID AND ArtistID = @ArtistID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@ArtistID", ArtistID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        } 

        public void DeleteAll(long TrackID)
        {
            string sql = "DELETE FROM TrackArtists WHERE TrackID = @TrackID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            int result = cmd.ExecuteNonQuery();
        }          
    }
}
