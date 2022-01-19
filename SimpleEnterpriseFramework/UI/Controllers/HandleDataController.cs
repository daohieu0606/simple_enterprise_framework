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
    using UI.ConcreteBuilder;
    using UI.Model;
    using UI.Views;

    internal class HandleDataController
    {
        private ConnectDatabaseWindow dbWindow;

        private IDatabase database;

        private StyleOption styleOption;

        public HandleDataController(ConnectDatabaseWindow dbWindow, StyleOption option)
        {
            this.dbWindow = dbWindow;
            this.styleOption = option;
            dbWindow.DbTypeComboBox.SelectedIndex = 0;
        }

        public async void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ButtonConnect":
                    string host = dbWindow.HostNameInput.Text;
                    string username = dbWindow.UserNameInput.Text;
                    string pwd = dbWindow.PwdInput.Password;
                    string dbname = dbWindow.DatabaseNameInput.Text;
                    ComboBoxItem selectedItem = (ComboBoxItem)dbWindow.DbTypeComboBox.SelectedItem;
                    string dbType = selectedItem.Content.ToString();
                    if (!ValidateInput(host, pwd, dbname))
                    {
                        dbWindow.IncorrectConnectDB.Text = "Chưa điền đủ thông tin!";
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

                                database = ServiceLocator.Instance.Get<IDatabase>();

                                if (!database.OpenConnection()) {
                                    dbWindow.IncorrectConnectDB.Text = "Không thể kết nối đến database";
                                    dbWindow.TableNameComboBox.ItemsSource = null; return;
                                }

                                break;
                            case "Postgres":
                                CurrentFrameworkState.Instance.ChangeDataBase(
                                    DatabaseType.Postgres,
                                    host: host,
                                    dbName: dbname,
                                    username: username,
                                    password: pwd);

                                database = ServiceLocator.Instance.Get<IDatabase>();

                                if (!database.OpenConnection())
                                {
                                    dbWindow.IncorrectConnectDB.Text = "Không thể kết nối đến database";
                                    dbWindow.TableNameComboBox.ItemsSource = null; return;
                                }
                                break;
                            default:
                                break;

                        }
                        if (database != null)
                        {
                            List<string> tablesName = (List<string>)database.GetAllTableNames();
                            dbWindow.TableNameComboBox.ItemsSource = tablesName;
                            if (tablesName.Count > 0) dbWindow.TableNameComboBox.SelectedIndex = 0;
                            ClearFields();
                        }

                    }
                    break;
                case "ButtonGenerate":
                    if (this.database != null)
                    {
                        string currentTable = dbWindow.TableNameComboBox.SelectedItem.ToString();
                        DataTable data = await database.GetTable(currentTable);

                        ReadForm readForm = (ReadForm)new ReadFormBuilder().setDatabase(database).setStyleOption(styleOption).setData(data).setTableName(currentTable).build();
                        readForm.ShowDialog();
                    }

                    break;

                default: dbWindow.Close(); break;
            }
        }

        public void KeyDownTextBox(object sender, KeyEventArgs e)
        {
            switch (((TextBox)sender).Name)
            {
                case "HostNameInput":
                    if (e.Key == Key.Enter) dbWindow.UserNameInput.Focus();
                    break;
                case "UserNameInput":
                    if (e.Key == Key.Enter) dbWindow.PwdInput.Focus();
                    break;
                case "DatabaseNameInput":
                    if (e.Key == Key.Enter) dbWindow.ButtonConnect.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                default:
                    break;
            }
        }

        public void KeyDownPasswordBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) dbWindow.DatabaseNameInput.Focus();
        }

        private bool ValidateInput(string host, string username, string database)
        {
            return host.Length > 0 && username.Length > 0 && database.Length > 0;
        }

        private void ClearFields()
        {
            dbWindow.IncorrectConnectDB.Text = "";
        }
    }
}
