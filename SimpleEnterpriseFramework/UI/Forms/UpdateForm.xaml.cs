namespace UI.Views
{
    using Core.Database;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using UI.Helpers;
    using UI.Model;

    public partial class UpdateForm : Window, BaseForm
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


        public void InitStyle()
        {
            if (tableName != null)
                LabelUpdate.Content = LabelUpdate.Content + " " + tableName;
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

                if (styleOption.BackgroundColor != null)
                {
                    byte a = styleOption.BackgroundColor.a;
                    byte r = styleOption.BackgroundColor.r;
                    byte g = styleOption.BackgroundColor.g;
                    byte b = styleOption.BackgroundColor.b;
                    UpdateFormGrid.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));

                }
                if (styleOption.FontFamily != null)
                {
                    LabelUpdate.FontFamily = new FontFamily(styleOption.FontFamily);
                    UpdateList.FontFamily = new FontFamily(styleOption.FontFamily);
                }
                if (styleOption.CRUDWindowNames != null)
                {
                    if (database == null && styleOption.CRUDWindowNames.Count >= 3) LabelUpdate.Content = styleOption.CRUDWindowNames[2];
                }
            }
        }
    }
}
