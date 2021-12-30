using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Views;

namespace UI.Controllers
{
    class ConnectDBController
    {
        private DBForm dbForm;
        private IDatabase connection= null;
        public ConnectDBController(DBForm dbForm)
        {
            this.dbForm= dbForm;
            dbForm.DbTypeComboBox.SelectedIndex = 0;
        }

        public void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ButtonConnect":
                    string host = dbForm.HostNameInput.Text;
                    string username = dbForm.UserNameInput.Text;
                    string pwd = dbForm.PwdInput.Text;
                    ComboBoxItem selectedItem = (ComboBoxItem)dbForm.DbTypeComboBox.SelectedItem;
                    string dbType = selectedItem.Content.ToString();
                    if (!ValidateInput(host, pwd))
                    {
                       // dbForm.IncorrectConnectDB.Text = "Chưa điền đủ thông tin!";
                    }
                    else
                    {
                        IDatabase connection = new IDatabase(dbType, host, username, pwd);
                        if (!connection.Connect())
                        {
                            dbForm.IncorrectConnectDB.Text = "Không thể kết nối đến database!";
                        }
                        else
                        {
                            this.connection = connection;
                            ClearFields();
                            List<string> databaseNames = connection.GetAllDBNames();
                            dbForm.DbNameComboBox.ItemsSource = databaseNames;
                            if (databaseNames.Count > 0) dbForm.DbNameComboBox.SelectedIndex = 0;
                        }

                    }
                    break;
                case "ButtonGenerate":
                    if(this.connection !=null)
                    {
                        ReadForm readForm = new ReadForm(connection);
                        //string currentTable = dbForm.TableNameComboBox.SelectedItem.ToString();
                        DataTable data = connection.GetData();
                        readForm.DatagridView.ItemsSource = data.AsDataView();
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
            //dbForm.ButtonConnect.Focus();

        }
        
        public void OnChangeComboBox(object sender,  SelectionChangedEventArgs  e)
        {
            if(((ComboBox)sender).Name== "DbNameComboBox")
            {
                if (this.connection !=null)
                {
                    //string dbName = dbForm.DbNameComboBox.SelectedItem.ToString();
                    List<string> tableNames = connection.GetAllTableNames();
                    dbForm.TableNameComboBox.ItemsSource = tableNames;
                    if (tableNames.Count > 0) dbForm.TableNameComboBox.SelectedIndex = 0;
                }
      
            }

        }

        private bool ValidateInput(string host, string username)
        {
            return host.Length > 0 && username.Length > 0;
        }

        private void ClearFields()
        {
            dbForm.IncorrectConnectDB.Text = "";
        }

    }
}
