namespace UI.Controllers
{
    using Core.Database;
    using Core.Utils;
    using IoC.DI;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using UI.Model;
    using UI.Views;

    internal class ConnectDBController
    {
        private DBForm dbForm;

        private IDatabase database;

        private StyleOption styleOption;

        public ConnectDBController(DBForm dbForm, StyleOption option)
        {
            this.dbForm = dbForm;
            this.styleOption = option;
            dbForm.DbTypeComboBox.SelectedIndex = 0;
        }

        public async void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ButtonConnect":
                    string host = dbForm.HostNameInput.Text;
                    string username = dbForm.UserNameInput.Text;
                    string pwd = dbForm.PwdInput.Text;
                    string dbname = dbForm.DatabaseNameInput.Text;
                    ComboBoxItem selectedItem = (ComboBoxItem)dbForm.DbTypeComboBox.SelectedItem;
                    string dbType = selectedItem.Content.ToString();
                    if (!ValidateInput(host, pwd, dbname))
                    {
                        // dbForm.IncorrectConnectDB.Text = "Chưa điền đủ thông tin!";
                    }
                    else
                    {

                        switch (dbType)
                        {
                            case "MySQL":
                                CurrentFrameworkState.Instance.ChangeDataBase(
                                    DatabaseType.MySql,
                                    host: host,
                                    dbName: dbname,
                                    username: username,
                                    password: pwd);

                                var db = ServiceLocator.Instance.Get<IDatabase>();

                                if (!db.OpenConnection()) { dbForm.IncorrectConnectDB.Text = "Could not connect to database"; break; }
                                this.database = db;
                                //var list = await db.GetAllTableNames();
                                List<string> tablesName = new List<string>() { "user_entity", "task_entity"};
                                dbForm.TableNameComboBox.ItemsSource = tablesName;
                                if (tablesName.Count > 0) dbForm.TableNameComboBox.SelectedIndex = 0;

                                //DataTable result = await db.GetTable("accounts");
                                break;
                            case "Postgres":
                                break;
                            default:
                                break;

                        }
                        ClearFields();



                    }
                    break;
                case "ButtonGenerate":
                    if (this.database != null)
                    {
                        string currentTable = dbForm.TableNameComboBox.SelectedItem.ToString();
                        DataTable data = await database.GetTable(currentTable);

                        ReadForm readForm = new ReadForm(database, styleOption, data,currentTable);
                        readForm.ShowDialog();
                        //dbForm.Hide();
                    }


                    break;

                default: dbForm.Close(); break;
            }
        }

        public void KeyDownTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) dbForm.ButtonConnect.Focus();
        }

        public void OnChangeComboBox(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private bool ValidateInput(string host, string username, string database)
        {
            return host.Length > 0 && username.Length > 0 && database.Length > 0;
        }

        private void ClearFields()
        {
            dbForm.IncorrectConnectDB.Text = "";
        }
    }
}
