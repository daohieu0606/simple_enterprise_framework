using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Membership;
using UI.Views;
using UI.Model;
using System.Data;

namespace UI.Controllers
{
    class LoginController
    {
        private Membership.Authenticaiton authentication;
        private Login loginView;
        private DataTable data;

        public LoginController(Login loginView, DataTable source)
        {
            this.loginView = loginView;
            this.data = source;
            authentication = new Membership.Authenticaiton();
            loginView.NameLoginIn.Focus();
        }

        public void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ButtonLogin":
                    {
                        string username = this.getUserNameInput(false);
                        string pwd = this.getPasswordInput(false);
                        if (!ValidationUser(username,pwd))
                        {
                            loginView.IncorrectLoginIn.Text = "Chưa điền đủ thông tin!";
                            return;
                        }
                        if (authentication.validate(username,pwd))
                        {
                            //listUserModel.Clear();
                            loginView.Hide();
                            if (data != null)
                            {
                                ReadForm readForm = new ReadForm(data, loginView.OptionStyle);
                                readForm.ShowDialog();
                            }
                            else
                            {
                                DBForm dbForm = new DBForm();
                                dbForm.ShowDialog();
                            }


                            loginView.Close();
                        }
                        else loginView.IncorrectLoginIn.Text = "Tài khoản người dùng không tồn tại";
                        break;
                    }
                case "ButtonVolverLogin": GridLoginVisiblility(); break;
                case "ButtonRegistro": GridRegiterVisiblility(); break;
                case "ButtonSignUp": ButtonSignUp(); break;
                default: loginView.Close(); break;
            }
        }

        public void _KeyDownTextBox(object sender, KeyEventArgs e)
        {
            switch (((TextBox)sender).Name)
            {
                case "NameSingUp":
                    if (e.Key == Key.Enter) loginView.PasswordSingUp.Focus();
                    break;
                case "NameLoginIn":
                    if (e.Key == Key.Enter) loginView.PasswordLoginIn.Focus();
                    break;
            }
        }

        private bool ValidationUser(string username, string pwd)
        {
            return username.Length > 0 && pwd.Length > 0;
        }

        private void _MouseDown(object sender, MouseButtonEventArgs e) => loginView.DragMove();

        public string getUserNameInput(bool v)
        {
            if (v) return loginView.NameSingUp.Text.Trim().Replace(" ", "");
            return loginView.NameLoginIn.Text.Trim().Replace(" ", "");
            
        }

        public string getPasswordInput(bool v)
        {
            if (v) return loginView.PasswordSingUp.Password.Trim().Replace(" ", "");
            return loginView.PasswordLoginIn.Password.Trim().Replace(" ", "");
        }
 

        public void GridRegiterVisiblility()
        {
            loginView.GridLogin.Visibility = Visibility.Collapsed;
            loginView.GridRegister.Visibility = Visibility.Visible;
            loginView.NameSingUp.Focus();
        }

        public void GridLoginVisiblility()
        {
            loginView.GridRegister.Visibility = Visibility.Collapsed;
            loginView.GridLogin.Visibility = Visibility.Visible;
            ClearFields();
        }

        private void ButtonSignUp()
        {
            string username = this.getUserNameInput(true);
            string pwd = this.getPasswordInput(true);
            if (!ValidationUser(username, pwd))
            {
                loginView.IncorrectSingUp.Text = "Chưa nhập đủ thông tin!";
                return;
            }
            if (authentication.validateExistenceUser(username,pwd))
            {
                loginView.IncorrectSingUp.Text = "Tài khoản đã tồn tại";
                return;
            }
            else
            {
                authentication.createUser(username, pwd);
                GridLoginVisiblility();
            }
        }

        private void ClearFields()
        {
            loginView.NameLoginIn.Clear();
            loginView.NameSingUp.Clear();
            loginView.PasswordLoginIn.Clear();
            loginView.PasswordSingUp.Clear();
            loginView.IncorrectSingUp.Text = "";
            loginView.IncorrectLoginIn.Text = "";
        }


    }
}
