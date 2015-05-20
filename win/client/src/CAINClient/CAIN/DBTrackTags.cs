using System;                     
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBTrackTags : DBTable
    {
        public DBTrackTags(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long TrackID, long TagID)
        {
            string sql = "SELECT COUNT(*) FROM TrackTags WHERE TrackID = @TrackID AND TagID = @TagID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@TagID", TagID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false;
        }

        public int Count(long TagID)
        {
            string sql = "SELECT COUNT(*) FROM TrackTags WHERE TagID = @TagID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TagID", TagID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public List<long> List(long TrackID)
        {
            string sql = "SELECT TagID FROM TrackTags WHERE TrackID = @TrackID";

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
                long Id = reader.GetInt32(reader.GetOrdinal("TagID"));
                ids.Add(Id);
            }
            reader.Close();

            return ids;
        }
        
        public bool Insert(long TrackID, long TagID)
        {
            string sql = "INSERT INTO TrackTags (TrackID, TagID) VALUES (@TrackID, @TagID)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@TagID", TagID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public bool Delete(long TrackID, long TagID)
        {
            string sql = "DELETE FROM TrackTags WHERE TrackID = @TrackID AND TagID = @TagID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            cmd.Parameters.AddWithValue("@TagID", TagID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public void DeleteAll(long TrackID)
        {
            string sql = "DELETE FROM TrackTags WHERE TrackID = @TrackID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            int result = cmd.ExecuteNonQuery();
        }
    }
}
