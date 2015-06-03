using System;                
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBTracks : DBTable, IDBTable<Track>
    {
        public DBTracks(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long ID)
        {
            string sql = "SELECT COUNT(*) FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false;
        }

        public bool Exists(Track track)
        {
            return GetID(track) > 0 ? true : false;
            /*string sql = "SELECT COUNT(*) FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", track.ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false;*/
        }

        public long GetID(Track track)
        {
            /*if (String.IsNullOrEmpty(track.MBID))
                return 0;*/

            string sql = "SELECT ID FROM Tracks WHERE UPPER(Title) = UPPER(@Title) AND Duration = @Duration";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@Title", track.Title);
            cmd.Parameters.AddWithValue("@Duration", track.Duration);
            object result = cmd.ExecuteScalar();

            if (result == null) return 0;

            return Convert.ToUInt32(result);
        }

        public DateTime GetVersion(long ID)
        {
            string sql = "SELECT Version FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            if (result == null) return DateTime.Now;

            return Convert.ToDateTime(result);
        }

        public string GetPathDst(long ID)
        {
            string sql = "SELECT PathDst FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            if (result == null) return String.Empty;

            return Convert.ToString(result);
        }

        public Track Get(long ID)
        {
            string sql = "SELECT * FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows || !reader.Read())
            {
                reader.Close();
                return null;
            }

            Track track = new Track();
            track.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            track.MBID = reader.GetString(reader.GetOrdinal("MBID"));
            track.Title = reader.GetString(reader.GetOrdinal("Title"));
            track.Duration = reader.GetInt32(reader.GetOrdinal("Duration"));
            track.Path = reader.GetString(reader.GetOrdinal("Path"));
            track.Status = (Track.StatusTypes)reader.GetInt32(reader.GetOrdinal("Status"));
            track.Reliability = reader.GetInt32(reader.GetOrdinal("Reliability"));
            reader.Close();

            return track;
        }

        public int Count()
        {
            string sql = "SELECT COUNT(*) FROM Tracks";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result);
        }
        
        public List<Track> List(int count = 0)
        {
            string sql = count > 0 ? "SELECT * FROM Tracks LIMIT @Count" : "SELECT * FROM Tracks";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            if (count > 0) cmd.Parameters.AddWithValue("@Count", count);         
            
            List<Track> tracks = new List<Track>();                

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    return tracks;
                }

                while (reader.Read())
                {
                    Track track = new Track();
                    track.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    track.MBID = reader.GetString(reader.GetOrdinal("MBID"));
                    track.Title = reader.GetString(reader.GetOrdinal("Title"));
                    track.Duration = reader.GetInt32(reader.GetOrdinal("Duration"));
                    track.Path = reader.GetString(reader.GetOrdinal("Path"));
                    track.Status = (Track.StatusTypes)reader.GetInt32(reader.GetOrdinal("Status"));
                    track.Reliability = reader.GetInt32(reader.GetOrdinal("Reliability"));

                    tracks.Add(track);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {

            }

            return tracks;
        }

        public bool Insert(ref Track track)
        {
            string sql = "INSERT INTO Tracks (MBID, Title, Duration, Path, Status, Reliability) VALUES (@MBID, @Title, @Duration, @Path, @Status, @Reliability)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@MBID", track.MBID);
            cmd.Parameters.AddWithValue("@Title", track.Title);
            cmd.Parameters.AddWithValue("@Duration", track.Duration);
            cmd.Parameters.AddWithValue("@Path", track.Path);
            cmd.Parameters.AddWithValue("@Status", track.Status);
            cmd.Parameters.AddWithValue("@Reliability", track.Reliability);
            int result = cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            track.ID = Convert.ToUInt32(cmd.ExecuteScalar());

            return result == 1 ? true : false;
        }

        public bool Edit(Track track)
        {
            string sql = "UPDATE Tracks SET Title = @Title, Duration = @Duration, Path = @Path, Status = @Status, Reliability = @Reliability WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql.ToString(), this.Connection);
            cmd.Parameters.AddWithValue("@Title", track.Title);
            cmd.Parameters.AddWithValue("@Duration", track.Duration);
            cmd.Parameters.AddWithValue("@Path", track.Path);
            cmd.Parameters.AddWithValue("@Status", track.Status);
            cmd.Parameters.AddWithValue("@Reliability", track.Reliability);
            cmd.Parameters.AddWithValue("@ID", track.ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public bool Delete(long ID)
        {
            string sql = "DELETE FROM Tracks WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public void DeleteAll()
        {
            string sql = "DELETE FROM Tracks";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            int result = cmd.ExecuteNonQuery();
        }
    }
}
