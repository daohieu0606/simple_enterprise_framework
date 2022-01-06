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
        public FormBuilder setDatabase(IDatabase database);
        public FormBuilder setStyleOption(StyleOption option);

        public FormBuilder setData(DataTable data);

        public FormBuilder setTableName(string tableName);

        //public FormBuilder setCurrentRow(DataRow row);

        //public FormBuilder setReadForm(ReadForm readForm);

        //public FormBuilder setFields(List<Field> fields);

        public RootForm build();

    }
}
