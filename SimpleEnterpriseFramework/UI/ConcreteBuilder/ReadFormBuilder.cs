namespace UI.ConcreteBuilder
{
    using Core.Database;
    using System;
    using System.Data;
    using UI.Builder;
    using UI.Model;
    using UI.Views;

    public class ReadFormBuilder : FormBuilder
    {
        public IDatabase database { get; set; }

        public DataTable data { get; set; }

        public StyleOption styleOption { get; set; }

        public string tableName { get; set; }

        public ReadForm readForm { get; set; }

        public DataRow currentRow { get; set; }

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

        public FormBuilder setCurrentRow(DataRow row)
        {
            this.currentRow = row;
            return this;
        }

        public BaseForm build()
        {
            return new ReadForm(database, styleOption, data, tableName);
        }

        public FormBuilder setReadForm(ReadForm readForm)
        {
            this.readForm = readForm;
            return this;
        }
    }
}
