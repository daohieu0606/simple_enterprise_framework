namespace UI.Views
{
    using Core.Database;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows;
    using System.Windows.Media;
    using UI.Helpers;
    using UI.Model;

    public partial class CreateForm : Window, RootForm
    {
        public IDatabase database { get; set; }
        public DataTable data { get; set; }
        public StyleOption styleOption { get; set; }
        public string tableName { get; set; }

        public ReadForm readForm { get; set; }

        public List<Field> fields { get; set; }


        public CreateForm(IDatabase database, ReadForm readForm, StyleOption option, DataTable source, string tableName)
        {
            InitializeComponent();
            this.database = database;
            this.readForm = readForm;
            this.styleOption = option;
            this.tableName = tableName;
            this.data = source;

            fields = DataHelper.GetALLFields(source, null);
            CreateList.ItemsSource = fields;

            InitStyle();
        }

        public CreateForm(DataTable source, ReadForm readForm, StyleOption option)
        {
            InitializeComponent();
            this.data = source;
            this.readForm = readForm;
            this.styleOption = option;

            fields = DataHelper.GetALLFields(data, null);
            InitStyle();

            CreateList.ItemsSource = fields;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DataRow row = data.NewRow();
                for (var i = 0; i < fields.Count; i++)
                {
                    Type type = data.Columns[i].DataType;
                    row[fields[i].Title] = fields[i].Value == "" ? null : fields[i].Value;
                }
                data.Rows.Add(row);
                if (database != null)
                {
                    database.Insert(tableName, row);

                }
                readForm.setData(data);
                MessageBox.Show("Create successfully!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public void InitStyle()
        {
            if (this.styleOption != null)
            {
                if (styleOption.ButtonColor != null)
                {
                    byte a = styleOption.ButtonColor.a;
                    byte r = styleOption.ButtonColor.r;
                    byte g = styleOption.ButtonColor.g;
                    byte b = styleOption.ButtonColor.b;
                    ButtonCreate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }

                if (styleOption.CRUDWindowNames != null)
                {
                    if (styleOption.CRUDWindowNames.Count >= 2) LabelCreate.Content = styleOption.CRUDWindowNames[1];
                }
            }
        }

    }

    public class Field
    {
        public string Title { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public string Value { get; set; }

        public string DataType { get; set; }
    }
}
