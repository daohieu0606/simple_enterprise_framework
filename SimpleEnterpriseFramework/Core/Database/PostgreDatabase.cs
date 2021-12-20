using System;
using System.Collections.Generic;
using Npgsql;

namespace Core.Database
{
    public class PostgreDatabase: IDatabase
    {
        private NpgsqlConnection _con;

        private string _host;
        private string _dbName;
        private string _username;
        private string _password;

        public PostgreDatabase()
        {
            //do nothing
        }

        public PostgreDatabase(string host, string dbName, string username, string password)
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
            try
            {
                IList<string> result = new List<string>();
                using (_con)
                {
                    var query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";

                    NpgsqlCommand command = new NpgsqlCommand(query, _con);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0));
                        }
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool OpenConnection()
        {
            var cs = string.Format(
                "Host={0};Username={1};Password={2};Database={3}",
                _host,
                _username,
                _password,
                _dbName);

            _con = new NpgsqlConnection(cs);

            try
            {
                _con.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
