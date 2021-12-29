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

        public Task<DataTable> GetTable(string tableName); // Lấy 1 bảng

        public Task<DataTable> GetOneRow(string tableName, string props, string val); //Lấy 1 dòng

        public bool Insert(string tableName, DataRow row, DataRow newRow = null);

        public bool Delete(string tableName, DataRow row, DataRow newRow = null);

        public bool Update(string tableName, DataRow row, DataRow newRow);
    }
}