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

    public partial class UpdateForm : Window
    {
        private IDatabase connection;

        private DataRow currentRow;

        private DataTable data;

        private ReadForm readForm;

        private List<Field> fields;

        private StyleOption styleOption;

        public UpdateForm(IDatabase connection, DataRow row)
        {
            InitializeComponent();
            this.connection = connection;
            this.currentRow = row;
            List<Field> fields = connection.GetAllFields();
            foreach (Field field in fields)
            {
                field.Value = row[field.Title].ToString();
                if (field.IsPrimaryKey) field.Title += " (Primary Key)";

            }

            UpdateList.ItemsSource = fields;
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
                    if (styleOption.CRUDWindowNames.Count >= 3) LabelUpdate.Content = styleOption.CRUDWindowNames[2];
                }
            }
        }
    }
}
