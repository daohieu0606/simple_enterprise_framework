namespace UI.Views
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using UI.Helpers;
    using UI.Model;
    using Core.Database;

    public partial class UpdateForm : Window, RootForm
    {

        public IDatabase database { get; set; }
        public DataTable data { get; set; }
        public StyleOption styleOption { get; set; }
        public string tableName { get; set; }
        private DataRow currentRow { get; set; }



        private ReadForm readForm { get; set; }

        private List<Field> fields { get; set; }


        public UpdateForm(IDatabase database, DataRow row, string tableName, DataTable source, ReadForm readForm, StyleOption option)
        {
            InitializeComponent();
            this.database = database;
            this.currentRow = row;
            this.tableName = tableName;
            this.data = source;
            this.readForm = readForm;
            this.styleOption = option;
            List<Field> fields = DataHelper.GetALLFields(source, row);
            this.fields = fields;

            UpdateList.ItemsSource = fields;
            InitStyle();
            //List<Field> fields = connection.GetAllFields();
            //foreach (Field field in fields)
            //{
            //    field.Value = row[field.Title].ToString();
            //    if (field.IsPrimaryKey) field.Title += " (Primary Key)";

            //}

            //UpdateList.ItemsSource = fields;
        }

        public UpdateForm(DataTable source, ReadForm readForm, DataRow row, StyleOption option)
        {
            InitializeComponent();
            this.data = source;
            this.currentRow = row;
            this.readForm = readForm;
            this.styleOption = option;
            List<Field> fields = DataHelper.GetALLFields(source, row);
            this.fields = fields;

            UpdateList.ItemsSource = fields;
            InitStyle();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data != null)
                {
                    DataRow row = data.NewRow();
                    for (var i = 0; i < fields.Count; i++)
                    {
                        Type type = data.Columns[i].DataType;
                        row[fields[i].Title] = fields[i].Value == "" ? null : fields[i].Value;
                    }
                    if (database != null) database.Update(tableName, currentRow, row);
                    //find row
                    var array1 = this.currentRow.ItemArray;
                    foreach (DataRow row1 in data.Rows)
                    {
                        var array2 = row1.ItemArray;

                        if (array1.SequenceEqual(array2))
                        {
                            foreach (Field field in fields)
                                row1[field.Title] = row[field.Title];
                            break;
                        }

                    }
                    readForm.setData(data);
                    MessageBox.Show("Update successfully!");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public StyleOption OptionStyle
        {
            get { return styleOption; }
            set { styleOption = value; }
        }



        private void InitStyle()
        {
            if (this.styleOption != null)
            {
                if (styleOption.ButtonColor != null)
                {
                    byte a = styleOption.ButtonColor.a;
                    byte r = styleOption.ButtonColor.r;
                    byte g = styleOption.ButtonColor.g;
                    byte b = styleOption.ButtonColor.b;
                    ButtonUpdate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
                if (styleOption.CRUDWindowNames != null)
                {
                    if (database == null && styleOption.CRUDWindowNames.Count >= 3) LabelUpdate.Content = styleOption.CRUDWindowNames[2];
                }
            }
        }

        void RootForm.InitStyle()
        {
            throw new NotImplementedException();
        }
    }
}
