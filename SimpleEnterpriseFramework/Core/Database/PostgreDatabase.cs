using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Core.Query;
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

        public async Task<int> ExecuteNoQueryAsync(string query)
        {
            try
            {
                using (var cmd = new NpgsqlCommand(query, _con))
                {
                    var result = await cmd.ExecuteNonQueryAsync();

                    return result;
                }
            }
            catch(Exception e)
            {
                return -1;
            }
        }

        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            using (var cmd = new NpgsqlCommand(query, _con))
            {
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dt = new DataTable();

                dt.Load(reader);

                return dt;
            }
        }

        public bool IsOpened()
        {
            return _con != null && _con.State == ConnectionState.Open;
        }

        public bool OpenConnection()
        {
            if(_con != null && _con.State == ConnectionState.Open)
            {
                return true;
            }

            var cs = string.Format(
                "Server={0};Port=5432;Username={1};Password={2};Database={3}",
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

        public bool CloseConnection()
        {
            if (_con == null || _con.State == ConnectionState.Closed)
            {
                return true;
            }

            try
            {
                _con.Close();
                _con.Dispose();
                return true;
            }
            catch (Exception e)
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
                    //var query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";

                    //NpgsqlCommand command = new NpgsqlCommand(query, _con);
                    //using (NpgsqlDataReader reader = command.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        result.Add(reader.GetString(0));
                    //    }
                    //}
                    var cmd = new NpgsqlCommand("show tables", _con);
                    NpgsqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result.Add(rdr[0].ToString());
                    }
                    rdr.Close();
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetTable(string tableName)
        {
            try
            {
                var query = "select * from " + tableName;
                DataTable result = new DataTable();
                var cmd = new NpgsqlCommand(query, _con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                result.Load(rdr);
                rdr.Close();

                return result;

            } catch (Exception e)
            {
                return null;
            }
        }

        public DataRow GetOneRow(string tableName, string props, string val)
        {
            try
            {
                var query = string.Format("select * from {0} where {1} = {2}", tableName, props, val); ;
                DataTable result = new DataTable();
                var cmd = new NpgsqlCommand(query, _con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                result.Load(rdr);
                rdr.Close();

                return result?.Rows[0];

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Insert(string tableName, DataRow row, DataRow newRow = null)
        {
            NpgsqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).CreatePostgres(tableName, row, newRow).GetQuery();
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete(string tableName, DataRow row, DataRow newRow = null)
        {
            NpgsqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).CreatePostgres(tableName, row, newRow).GetQuery();
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string tableName, DataRow row, DataRow newRow)
        {
            NpgsqlCommand cmd = QueryFactory.GetFactory(QueryType.update).CreatePostgres(tableName, row, newRow).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
