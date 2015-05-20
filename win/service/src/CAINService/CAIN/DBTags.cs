using System;                      
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBTags : DBTable, IDBTable<Tag>
    {
        public DBTags(DBConnection connection)
            : base(connection)
        {
        }

        public bool Exists(long ID)
        {
            string sql = "SELECT COUNT(*) FROM Tags WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }

        public bool Exists(Tag tag)
        {
            return GetID(tag) > 0 ? true : false;
            /*string sql = "SELECT COUNT(*) FROM Tags WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", tag.ID);
            object result = cmd.ExecuteScalar();

            return Convert.ToUInt32(result) > 0 ? true : false; */
        }

        public long GetID(Tag tag)
        {
            /*if (String.IsNullOrEmpty(tag.Name))
                return 0;*/

            string sql = "SELECT ID FROM Tags WHERE UPPER(Name) = UPPER(@Name) AND UPPER(Value) = UPPER(@Value)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@Name", tag.Name);
            cmd.Parameters.AddWithValue("@Value", tag.Value);
            object result = cmd.ExecuteScalar();

            if (result == null) return 0;

            return Convert.ToUInt32(result);
        }

        public Tag Get(long ID)
        {
            string sql = "SELECT * FROM Tags WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows || !reader.Read())
            {
                reader.Close();
                return null;
            }

            Tag tag = new Tag();
            tag.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            tag.Name = reader.GetString(reader.GetOrdinal("Name"));
            tag.Value = reader.GetString(reader.GetOrdinal("Value"));
            reader.Close();

            return tag;
        }

        public List<Tag> List(int count = 0)
        {
            string sql = count > 0 ? "SELECT * FROM Tags LIMIT @Count" : "SELECT * FROM Tags";
            
            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            if (count > 0) cmd.Parameters.AddWithValue("@Count", count);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Tag> tags = new List<Tag>();  

            if (!reader.HasRows)
            {
                reader.Close();
                return tags;
            }

            while (reader.Read())
            {          
                Tag tag = new Tag();
                tag.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                tag.Name = reader.GetString(reader.GetOrdinal("Name"));
                tag.Value = reader.GetString(reader.GetOrdinal("Value"));

                tags.Add(tag);
            }
            reader.Close();

            return tags;
        }

        public List<string> ListNames()
        {
            string sql = "SELECT DISTINCT Name FROM Tags";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> names = new List<string>();

            if (!reader.HasRows)
            {
                reader.Close();
                return names;
            }

            while (reader.Read())
            {
                string name = reader.GetString(reader.GetOrdinal("Name"));

                names.Add(name);
            }
            reader.Close();

            return names;
        }

        public bool Insert(ref Tag tag)
        {
            string sql = "INSERT INTO Tags (Name, Value) VALUES (@Name, @Value)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@Name", tag.Name);
            cmd.Parameters.AddWithValue("@Value", tag.Value);
            int result = cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            tag.ID = Convert.ToUInt32(cmd.ExecuteScalar());

            return result == 1 ? true : false;
        }

        public bool Edit(Tag tag)
        {
            string sql = "UPDATE Tags SET Value = @Value WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql.ToString(), this.Connection);
            cmd.Parameters.AddWithValue("@Value", tag.Value);
            cmd.Parameters.AddWithValue("@ID", tag.ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }
        
        public bool Delete(long ID)
        {
            string sql = "DELETE FROM Tags WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            int result = cmd.ExecuteNonQuery();

            return result == 1 ? true : false;
        }

        public void DeleteAll(long TrackID)
        {
            string sql = "DELETE FROM Tags WHERE ID IN (SELECT TagID FROM TrackTags WHERE TrackID = @TrackID)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.Parameters.AddWithValue("@TrackID", TrackID);
            int result = cmd.ExecuteNonQuery();
        }

        public void DeleteNotUsed()
        {
            string sql = "DELETE FROM Tags WHERE ID NOT IN (SELECT TagID FROM TrackTags)";

            MySqlCommand cmd = new MySqlCommand(sql, this.Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
