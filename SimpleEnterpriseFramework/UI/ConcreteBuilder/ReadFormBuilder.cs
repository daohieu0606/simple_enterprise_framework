using Core.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UI.Builder;
using UI.Model;
using UI.Views;

namespace UI.ConcreteBuilder
{
    public class ReadFormBuilder : FormBuilder
    {

        public IDatabase database { get; set; }

        public DataTable data { get; set; }

        public StyleOption styleOption { get; set; }

        public string tableName { get; set; }
        public FormBuilder setDatabase(IDatabase database)
        {
            this.database = database;
            return this;
        }

        public FormBuilder setStyleOption(StyleOption option)
        {
            this.styleOption = option;
            return this;
        }

        public FormBuilder setData(DataTable data)
        {
            this.data = data;
            return this;
        }

        public FormBuilder setTableName(string tableName)
        {
            this.tableName = tableName;
            return this;
        }

        public RootForm build()
        {
            return new ReadForm(database, styleOption, data, tableName);
        }
    }
}
