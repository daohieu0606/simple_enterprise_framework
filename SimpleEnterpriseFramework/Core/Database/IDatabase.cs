using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Core.Database
{
    public interface IDatabase
    {
        public bool OpenConnection();

        public bool CloseConnection();

        public bool IsOpened();

        public IList<string> GetAllTableNames();

        public Task<int> ExecuteNoQueryAsync(string query); //Thêm xóa sửa

        public Task<DataTable> ExecuteQueryAsync(string query); // Lấy dữ liệu

        public Task<DataTable> GetTable(string tableName, string[] props = null, string[] val = null); // Lấy 1 bảng

        public Task<DataRow> GetOneRow(string tableName, string props, string val); //Lấy 1 dòng
        public Task<DataRow> GetOneRow(string tableName, string[]props, string[] val); //Lấy 1 dòng

        public Task<DataTable> FindDataFrom(string tableName1, string key1, string tableName2, string key2, string valueOfKey);

        public bool Insert(string tableName, DataRow row, DataRow newRow = null);

        public bool Delete(string tableName, DataRow row, DataRow newRow = null);

        public bool Update(string tableName, DataRow row, DataRow newRow);
    }
}