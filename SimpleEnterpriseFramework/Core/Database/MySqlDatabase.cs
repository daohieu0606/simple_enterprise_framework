using Core.Query;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Core.Database
{
    public class MySqlDatabase: IDatabase
    {
        private MySqlConnection _con;

        private string _host;
        private string _dbName;
        private string _username;
        private string _password;

        public MySqlDatabase()
        {
            //do nothing
        }

        public MySqlDatabase(string host, string dbName, string username, string password)
        {
            _host = host;
            _dbName = dbName;
            _username = username;
            _password = password;
        }

        public bool CloseConnection()
        {
            if (_con == null || _con.State == ConnectionState.Closed)
                return true;

            try
            {
                _con.Close();
                _con.Dispose();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<int> ExecuteNoQueryAsync(string query)
        {
            try
            {
                using (_con)
                {
                    MySqlCommand command = new MySqlCommand(query, _con);
                    var result = await command.ExecuteNonQueryAsync();

                    return result;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public async Task<DataTable> ExecuteQueryAsync(string commmand)
        {
            OpenConnection();
            MySqlCommand cmd = new MySqlCommand(commmand, _con);
            DataTable dt = new DataTable();

            var reader = await cmd.ExecuteReaderAsync();

            dt.Load(reader);

            reader.Close();

            return dt;
        }

        public IList<string> GetAllTableNames()
        {
            try
            {
                IList<string> result = new List<string>();
                using (_con)
                {
                    string query = string.Format("show tables from {0}", _dbName);

                    MySqlCommand command = new MySqlCommand(query, _con);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0));
                        }

                        reader.Close();
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool IsOpened()
        {
            return _con != null && _con.State == System.Data.ConnectionState.Open;
        }

        //TODO: look this function
        public bool OpenConnection()
        {
            if(_con != null && _con.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            
            //init
            string connectionString;
            connectionString = "SERVER=" + _host + ";" + "DATABASE=" +
            _dbName + ";" + "UID=" + _username + ";" + "PASSWORD=" + _password + ";";

            _con = new MySqlConnection(connectionString);
            //connect
            try
            {
                _con.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1042:
                        //No connection to host
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public async Task<DataTable> GetTable(string tableName, string[] props = null, string[] val = null) //Có tên bảng 
        {
            try
            {
                var query = "select * from " + tableName;

                var result = await ExecuteQueryAsync(query);

                return result;

            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<DataRow> GetOneRow(string tableName, string props, string val)
        {
            try
            {
                var query = string.Format("select * from {0} where {1} = '{2}'", tableName, props, val); ;

                var result = await ExecuteQueryAsync(query);

                return result?.Rows[0];

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<DataTable> FindDataFrom(string tableName1, string key1, string tableName2, string key2, string valueOfKey)
        {
            try
            {
                var query = string.Format("select * from {0} inner join {1} on {2} = {3} where {2} = '{4}'", tableName1, tableName2, key1, key2, valueOfKey);

                var result = await ExecuteQueryAsync(query);

                return result;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Insert(string tableName, DataRow row, DataRow newRow = null)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).CreateMySql(tableName, row, newRow).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex) {
                throw ex;
            }
        }

        public bool Delete(string tableName, DataRow row, DataRow newRow = null)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).CreateMySql(tableName, row, newRow).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(string tableName, DataRow row, DataRow newRow)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.update).CreateMySql(tableName, row, newRow).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


   

        Task<DataRow> IDatabase.GetOneRow(string tableName, string[] props, string[] val)
        {
            throw new NotImplementedException();
        }


    }
}
