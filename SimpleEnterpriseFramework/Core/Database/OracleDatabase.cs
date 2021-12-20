using System;
using System.Collections.Generic;
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

        public IList<string> GetAllTableNames()
        {
            throw new NotImplementedException();
        }

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
    }
}
