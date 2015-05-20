using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace CAIN
{
    public class DBConnection
    {
        public MySqlConnection Connection { get; private set; }
        public string Error { get; private set; }               
        private MySqlTransaction Transaction;  

        public DBConnection(string connection)
        {      
            try
            {                    
                this.Connection = new MySqlConnection();
                this.Connection.ConnectionString = connection;
                this.Connection.Open();
            }
            catch (MySqlException ex)
            {
                this.Error = ex.Message;
            }
        }

        ~DBConnection()
        {
            if (this.Connection.State == ConnectionState.Open)
                this.Connection.Close();

            this.Connection.Dispose();
        }

        public bool IsOpen() 
        { 
            return this.Connection.State == ConnectionState.Open;
        }

        public bool BeginTransaction()
        {
            if (this.Transaction != null) return true;

            this.Transaction = this.Connection.BeginTransaction();
            return this.Transaction != null ? true : false;
        }

        public void Rollback()
        {
            if (this.Transaction == null) return;

            this.Transaction.Rollback();

            this.Transaction.Dispose();
            this.Transaction = null;
        }

        public void Commit()
        {
            if (this.Transaction == null) return;

            this.Transaction.Commit();

            this.Transaction.Dispose();
            this.Transaction = null;
        }
    }
}
