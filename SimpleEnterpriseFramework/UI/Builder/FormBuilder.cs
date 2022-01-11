using Core.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UI.Model;
using UI.Views;

namespace UI.Builder
{
    public interface FormBuilder
    {
        public IDatabase database { get; set; }

        public DataTable data { get; set; }

        public StyleOption styleOption { get; set; }

        public string tableName { get; set; }

        public ReadForm readForm { get; set; }

        public DataRow currentRow { get; set; }
        public  FormBuilder setDatabase(IDatabase database);
        public  FormBuilder setStyleOption(StyleOption option);

        public  FormBuilder setData(DataTable data);

        public  FormBuilder setTableName(string tableName);

        public  FormBuilder setCurrentRow(DataRow row);

        public  FormBuilder setReadForm(ReadForm readForm);

        public BaseForm build();

    }
}
