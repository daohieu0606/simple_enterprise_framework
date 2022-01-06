﻿
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Membership;
using System.Data;
using Core.Database;
using Core.Utils;
using IoC.DI;
using UI.Views;

namespace UI.Controllers
{
    class LoginController
    {
        private Login loginView;
        private DataTable data;

        public LoginController(Login loginView, DataTable source)
        {
            //connect user database
            CurrentFrameworkState.Instance.ChangeDataBase(
                DatabaseType.MySql,
                host: "localhost",
                dbName: "usermanage",
                username: "root",
                password: "123456");

            var db = ServiceLocator.Instance.Get<IDatabase>();
            db.OpenConnection();

            this.loginView = loginView;
            this.data = source;
            loginView.UsernameLogin.Focus();
        }

        public async void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "ButtonLogin":
                    {
                        string username = this.GetUsernameInput(false);
                        string pwd = this.GetPasswordInput(false);
                        if (!ValidationUser(username,pwd))
                        {
                            loginView.ErrorLogin.Text = "Chưa điền đủ thông tin!";
                            return;
                        }
                        var x = await Membership.Authentication.validateAsync("username", "pasword");
                        if (await Membership.Authentication.validateAsync(username,pwd))
                        {
                            loginView.Hide();
                            if (data != null)
                            {
                                ReadForm readForm = new ReadForm(data, loginView.OptionStyle);
                                readForm.ShowDialog();
                            }
                            else
                            {
                                DBForm dbForm = new DBForm(loginView.OptionStyle);
                                dbForm.ShowDialog();
                            }


                            loginView.Close();
                        }
                        else loginView.ErrorLogin.Text = "Tài khoản người dùng không tồn tại";
                        break;
                    }
                case "ButtonToLoginView": ShowLoginView(); break;
                case "ButtonToRegisterView": ShowRegisterView(); break;
                case "ButtonRegister": ButtonRegister(); break;
                default: loginView.Close(); break;
            }
        }

        public void KeyDownTextBox(object sender, KeyEventArgs e)
        {
            switch (((TextBox)sender).Name)
            {
                case "UsernameRegister":
                    if (e.Key == Key.Enter) loginView.PasswordRegister.Focus();
                    break;
                case "UsernameLogin":
                    if (e.Key == Key.Enter) loginView.PasswordLogin.Focus();
                    break;
            }
        }

        private bool ValidationUser(string username, string pwd)
        {
            return username.Length > 0 && pwd.Length > 0;
        }


        public string GetUsernameInput(bool isRegisterMode)
        {
            if (isRegisterMode) return loginView.UsernameRegister.Text.Trim().Replace(" ", "");
            return loginView.UsernameLogin.Text.Trim().Replace(" ", "");
            
        }

        public string GetPasswordInput(bool isRegisterMode)
        {
            if (isRegisterMode) return loginView.PasswordRegister.Password.Trim().Replace(" ", "");
            return loginView.PasswordLogin.Password.Trim().Replace(" ", "");
        }
 

        public void ShowRegisterView()
        {
            loginView.GridLogin.Visibility = Visibility.Collapsed;
            loginView.GridRegister.Visibility = Visibility.Visible;
            loginView.UsernameRegister.Focus();
        }

        public void ShowLoginView()
        {
            loginView.GridRegister.Visibility = Visibility.Collapsed;
            loginView.GridLogin.Visibility = Visibility.Visible;
            ClearFields();
        }

        private async void ButtonRegister()
        {
            string username = this.GetUsernameInput(true);
            string pwd = this.GetPasswordInput(true);
            if (!ValidationUser(username, pwd))
            {
                loginView.ErrorRegister.Text = "Chưa nhập đủ thông tin!";
                return;
            }
            User user = await Membership.HandleUser.findOneUserByFieldAsync("username", username);
            if(user != null)
            {
                loginView.ErrorRegister.Text = "Tài khoản đã tồn tại";
                return;
            }

            else
            {
                User newUser = User.getInstance(username, pwd, "email", "phone", "address", "role");
                await Membership.HandleUser.AddNewUserAsync(newUser);
                ShowLoginView();
            }
        }

        private void ClearFields()
        {
            loginView.UsernameLogin.Clear();
            loginView.UsernameRegister.Clear();
            loginView.PasswordLogin.Clear();
            loginView.PasswordRegister.Clear();
            loginView.ErrorRegister.Text = "";
            loginView.ErrorLogin.Text = "";
        }


    }
}
