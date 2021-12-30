using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.Helpers;
using UI.Model;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for CreateForm.xaml
    /// </summary>
    public partial class CreateForm : Window
    {
        private IDatabase connection;
        StyleOption styleOption;
        private ReadForm readForm;
        private DataTable data;
        private List<Field> fields;
        public CreateForm(IDatabase connection)
        {
            InitializeComponent();
            this.connection = connection;
            List<Field> items = connection.GetAllFields();
            CreateList.ItemsSource = items;
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
                    row[fields[i].Title] = fields[i].Value ==""?null:fields[i].Value;
                }
                data.Rows.Add(row);
                readForm.setData(data);
                MessageBox.Show("Create successfully!");
                this.Close();
            }
            catch (Exception ex){
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
    }

}
