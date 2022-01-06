using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Core.Database
{
    public class OracleDatabase: IDatabase
    {
        private OracleConnection _con;

        private string _host;
        private string _dbName;
        private string _username;
        private string _password;

        public OracleDatabase()
        {
            //do nothing
        }

        public OracleDatabase(string host, string dbName, string username, string password)
        {
            _host = host;
            _dbName = dbName;
            _username = username;
            _password = password;
        }

        public bool CloseConnection()
        {
            try
            {
                _con.Close();
                _con.Dispose();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //TODO: Hieu Dao implement later
        public Task<int> ExecuteNoQueryAsync(string query)
        {
            throw new NotImplementedException();
        }

        //TODO: Hieu Dao implement later
        public Task<DataTable> ExecuteQueryAsync(string quey)
        {
            throw new NotImplementedException();
        }

        //TODO: Hieu Dao implement later
        public IList<string> GetAllTableNames()
        {
            throw new NotImplementedException();
        }

        //TODO: Hieu Dao implement later
        public bool IsOpened()
        {
            throw new NotImplementedException();
        }

        //TODO: lock this function
        public bool OpenConnection()
        {
            try
            {
                _con = new OracleConnection();
                _con.ConnectionString = string.Format(
                    "User Id=<username>;Password=<password>;Data Source=<datasource>"
                    ,_username,
                    _password,
                    _dbName);

                _con.Open();

                Console.WriteLine("Connected to Oracle" + _con.ServerVersion);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //TODO: Hieu Nguyen implement later
        public Task<DataTable> GetTable(string tableName, string[] props = null, string[] val = null)
        {
            return null;
        }

        //TODO: Hieu Nguyen implement later
        public Task<DataRow> GetOneRow(string tableName, string[] props, string[] val)
        {
            return null;
        }

        public Task<DataTable> FindDataFrom(string tableName1, string key1, string tableName2, string key2, string valueOfKey)
        {
            return null;
        }

        //TODO: Hieu Nguyen implement later
        public bool Insert(string tableName, DataRow row, DataRow newRow = null)
        {
            return false;
        }

        //TODO: Hieu Nguyen implement later
        public bool Delete(string tableName, DataRow row, DataRow newRow = null)
        {
            return false;
        }

        //TODO: Hieu Nguyen implement later
        public bool Update(string tableName, DataRow row, DataRow newRow)
        {
            return false;
        }

        public Task<DataRow> GetOneRow(string tableName, string props, string val)
        {
            throw new NotImplementedException();
        }
    }
}
