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

        public Task<int> ExecuteNoQueryAsync(string query); //Thêm xóa sửa

        public Task<DataTable> ExecuteQueryAsync(string query); // Lấy dữ liệu

        public bool Insert(string tableName, DataRow row, DataRow newRow);

        public bool Delete(string tableName, DataRow row, DataRow newRow = null);

        public bool Update(string tableName, DataRow row, DataRow newRow = null);
    }
}