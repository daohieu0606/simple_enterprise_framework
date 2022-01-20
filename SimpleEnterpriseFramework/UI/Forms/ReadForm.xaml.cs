namespace UI.Views
{
    using Core.Database;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using UI.ConcreteBuilder;
    using UI.Model;

    public partial class ReadForm : Window, BaseForm
    {
        public IDatabase database { get; set; }

        public DataTable data { get; set; }

        public StyleOption styleOption { get; set; }

        public string tableName { get; set; }

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
                CreateForm createForm = (CreateForm)new CreateFormBuilder().setDatabase(database).setStyleOption(styleOption).setData(data).setTableName(tableName).setReadForm(this).build();
                createForm.ShowDialog();
            }
            else
            {
                CreateForm createForm = (CreateForm)new CreateFormBuilder().setData(data).setReadForm(this).setStyleOption(styleOption).build();
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
                updateForm = (UpdateForm)new UpdateFormBuilder().setDatabase(database).setTableName(tableName).setData(data).setReadForm(this).setCurrentRow(row).setStyleOption(styleOption).build();
            }
            else updateForm = (UpdateForm)new UpdateFormBuilder().setData(data).setReadForm(this).setCurrentRow(row).setStyleOption(styleOption).build();
            updateForm.Show();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView rowView = (DataRowView)DatagridView.SelectedItems[0];
            DataRow row = (DataRow)rowView.Row;
            UpdateForm updateForm = null;
            if (database != null)
            {
                database.OpenConnection();
                updateForm = (UpdateForm)new UpdateFormBuilder().setDatabase(database).setTableName(tableName).setData(data).setReadForm(this).setCurrentRow(row).setStyleOption(styleOption).build();
            }
            else updateForm = (UpdateForm)new UpdateFormBuilder().setData(data).setReadForm(this).setCurrentRow(row).setStyleOption(styleOption).build();
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
                    database.OpenConnection();

                    database.Delete(tableName, row);
                }

                data.Rows.Remove(row);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void InitStyle()
        {

            if (tableName != null)
                LabelRead.Content = LabelRead.Content + " " + tableName;
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

                if (styleOption.BackgroundColor != null)
                {
                    byte a = styleOption.BackgroundColor.a;
                    byte r = styleOption.BackgroundColor.r;
                    byte g = styleOption.BackgroundColor.g;
                    byte b = styleOption.BackgroundColor.b;
                    ReadFormGrid.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));

                }

                if (styleOption.FontFamily != null)
                {
                    LabelRead.FontFamily = new FontFamily(styleOption.FontFamily);
                    ButtonCreate.FontFamily = new FontFamily(styleOption.FontFamily);
                    ButtonUpdate.FontFamily = new FontFamily(styleOption.FontFamily);
                    ButtonDelete.FontFamily = new FontFamily(styleOption.FontFamily);
                    DatagridView.FontFamily = new FontFamily(styleOption.FontFamily);
                }

                if (styleOption.CRUDWindowNames != null)
                {

                    if (database == null && styleOption.CRUDWindowNames.Count >= 1) LabelRead.Content = styleOption.CRUDWindowNames[0];
                    else if (database != null) LabelRead.Content += $" {tableName}";
                }

                if (styleOption.DataGridStyle != null)
                {
                    Style rowStyle= new Style();
                    Style headerStyle = new Style();
                    rowStyle.Setters.Add(new Setter(HeightProperty, styleOption.DataGridStyle.RowHeight));
                    rowStyle.Setters.Add(new EventSetter() { Event = DataGridRow.MouseDoubleClickEvent, Handler = new MouseButtonEventHandler(Row_DoubleClick) });
                    rowStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
                    headerStyle.Setters.Add(new Setter(HeightProperty, styleOption.DataGridStyle.HeaderHeight));
                    headerStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                    headerStyle.Setters.Add(new Setter(FontWeightProperty, FontWeights.SemiBold));
                    
                    if (styleOption.DataGridStyle.CellsBackground != null)
                    {
                        rowStyle.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Color.FromArgb(styleOption.DataGridStyle.CellsBackground.a, styleOption.DataGridStyle.CellsBackground.r, styleOption.DataGridStyle.CellsBackground.g, styleOption.DataGridStyle.CellsBackground.b))));
                    }

                    DatagridView.RowStyle = rowStyle;

                    if (styleOption.DataGridStyle.HeaderBackground != null)
                    {
                        headerStyle.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Color.FromArgb(styleOption.DataGridStyle.HeaderBackground.a, styleOption.DataGridStyle.HeaderBackground.r, styleOption.DataGridStyle.HeaderBackground.g, styleOption.DataGridStyle.HeaderBackground.b))));
                    }

                    if(database==null && styleOption.DataGridStyle.ColumnNames != null)
                    {
                        List<string> columnNames = styleOption.DataGridStyle.ColumnNames;
                        int length = data.Columns.Count > columnNames.Count ? columnNames.Count : data.Columns.Count;
                        for (var i=0; i<length;i++)
                        {
                            if (columnNames[i] != null) data.Columns[i].ColumnName = columnNames[i];
                        }
                    }

                    DatagridView.ColumnHeaderStyle = headerStyle;

                }
            }
        }
    }
}
