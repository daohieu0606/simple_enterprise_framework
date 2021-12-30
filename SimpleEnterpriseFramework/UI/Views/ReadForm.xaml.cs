namespace UI.Views
{
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using UI.Model;

    public partial class ReadForm : Window
    {
        private IDatabase connection;

        private DataTable data;

        private StyleOption styleOption;

        public ReadForm(IDatabase connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        public ReadForm(DataTable source, StyleOption option)
        {
            InitializeComponent();
            this.data = source;
            this.styleOption = option;
            DatagridView.ItemsSource = data.AsDataView();
            InitStyle();
        }

        public void setData(DataTable source)
        {
            data = source;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (data == null)
            {
                CreateForm createForm = new CreateForm(connection);
                createForm.ShowDialog();
            }
            else
            {
                CreateForm createForm = new CreateForm(data, this, styleOption);
                createForm.ShowDialog();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select row first");
                return;
            }
            DataRowView rowView = (DataRowView)DatagridView.SelectedItems[0];
            DataRow row = (DataRow)rowView.Row;
            UpdateForm updateForm = null;
            if (data == null) updateForm = new UpdateForm(connection, row);
            else updateForm = new UpdateForm(data, this, row, styleOption);
            updateForm.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select row first");
                return;
            }
            DataRowView rowView = (DataRowView)DatagridView.SelectedItems[0];
            DataRow row = (DataRow)rowView.Row;
            if (data == null)
            {

            }
            else
            {
                data.Rows.Remove(row);
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
                    ButtonUpdate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    ButtonDelete.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
                if (styleOption.CRUDWindowNames != null)
                {
                    if (styleOption.CRUDWindowNames.Count >= 1) LabelRead.Content = styleOption.CRUDWindowNames[0];
                }

            }
        }
    }
}
