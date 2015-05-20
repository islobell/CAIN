using System;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public abstract class DBTable
    {
        protected MySqlConnection Connection;

        public DBTable(DBConnection connection)
        {
            this.Connection = connection.Connection;
        }
    }
}
