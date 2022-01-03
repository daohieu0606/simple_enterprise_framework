namespace UI.Views
{
    using Core.Database;
    using System;
    using System.Data;
    using System.Windows;
    using System.Windows.Media;
    using UI.Model;

    public partial class ReadForm : Window
    {
        private IDatabase database;

        private DataTable data;

        private StyleOption styleOption;

        private string tableName;

        public ReadForm(IDatabase database, StyleOption option, DataTable source, string tableName)
        {
            InitializeComponent();
            this.database = database;
            this.styleOption = option;
            this.tableName = tableName;
            this.data = source;
            DatagridView.ItemsSource = data.AsDataView();
            InitStyle();
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
            if (database != null)
            {
                database.OpenConnection();
                CreateForm createForm = new CreateForm(database, this, styleOption, data, tableName);
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
            if (database != null)
            {
                database.OpenConnection();
                updateForm = new UpdateForm(database, row, tableName, data, this, styleOption);
            }
            else updateForm = new UpdateForm(data, this, row, styleOption);
            updateForm.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatagridView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select row first");
                    return;
                }
                DataRowView rowView = (DataRowView)DatagridView.SelectedItems[0];
                DataRow row = (DataRow)rowView.Row;
                if (database != null)
                {
                    database.Delete(tableName, row);
                }

                data.Rows.Remove(row);

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
                    ButtonCreate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    ButtonUpdate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    ButtonDelete.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
                if (styleOption.CRUDWindowNames != null)
                {

                    if (database == null && styleOption.CRUDWindowNames.Count >= 1) LabelRead.Content = styleOption.CRUDWindowNames[0];
                    else if (database != null) LabelRead.Content += $" {tableName}";
                }

            }
        }
    }
}
