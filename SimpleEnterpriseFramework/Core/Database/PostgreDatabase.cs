using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Core.Query;
using Npgsql;

namespace Core.Database
{
    public class PostgreDatabase : IDatabase
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
                using (_con)
                {
                    NpgsqlCommand command = new NpgsqlCommand(query, _con);
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
            using (var cmd = new NpgsqlCommand(commmand, _con))
            {
                var reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);
                reader.Close();

                //DataTable result = new DataTable();
                //var cmd = new NpgsqlCommand(query, _con);
                //NpgsqlDataReader rdr = cmd.ExecuteReader();
                //result.Load(rdr);
                //rdr.Close();


                return dt;
            }
        }

        public IList<string> GetAllTableNames()
        {
            try
            {
                IList<string> result = new List<string>();
                using (_con)
                {
                    string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";

                    NpgsqlCommand command = new NpgsqlCommand(query, _con);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
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
            return _con != null && _con.State == ConnectionState.Open;
        }

        public bool OpenConnection()
        {
            if (_con != null && _con.State == ConnectionState.Open)
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

        public async Task<DataTable> GetTable(string tableName, string[] props = null, string[] val = null) //Có tên bảng 
        {
            try
            {
                StringBuilder paramsString = new StringBuilder();
                if (props != null)
                {
                    paramsString.Append(" where ");
                    for (int i = 0; i < props.Length; ++i)
                    {
                        paramsString.Append(props[i])
                            .Append(" = '")
                            .Append(val[i])
                            .Append("'");

                        if (i < props.Length - 1)
                        {
                            paramsString.Append(" AND ");
                        }
                    }
                }

                var query = "select * from " + tableName + paramsString.ToString();

                var result = await ExecuteQueryAsync(query);

                return result;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<DataRow> GetOneRow(string tableName, string[] props, string[] val)
        {
            try
            {
                StringBuilder paramsString = new StringBuilder();
                paramsString.Append(" where ");
                for (int i = 0; i < props.Length; ++i)
                {
                    paramsString.Append(props[i])
                        .Append(" = '")
                        .Append(val[i])
                        .Append("'");

                    if (i < props.Length - 1)
                    {
                        paramsString.Append(" AND ");
                    }
                }

                var query = "select * from " + tableName + paramsString.ToString();
                Console.WriteLine(query);

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
            NpgsqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).CreatePostgres(tableName, row, newRow).GetQuery();
            cmd.Connection = _con;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
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

        public Task<DataRow> GetOneRow(string tableName, string props, string val)
        {
            throw new NotImplementedException();
        }
    }
}
