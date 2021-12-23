using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Core.Database
{
    public interface IDatabase
    {
        bool OpenConnection();

        bool CloseConnection();

        public bool IsOpened();

        IList<string> GetAllTableNames();

        public Task<int> ExecuteNoQueryAsync(string query);

        public Task<DataTable> ExecuteSqlAsync(string query);
    }
}