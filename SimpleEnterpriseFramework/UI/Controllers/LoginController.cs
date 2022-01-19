
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Membership;
using System.Data;
using Core.Database;
using Core.Utils;
using IoC.DI;
using UI.Views;
using UI.ConcreteBuilder;

namespace UI.Controllers
{
    class LoginController
    {
        private LoginWindow loginView;
        private DataTable data;

        public LoginController(LoginWindow loginView, DataTable source)
        {
            //connect user database
            CurrentFrameworkState.Instance.ChangeDataBase(
                DatabaseType.MySql,
                host: "us-cdbr-east-04.cleardb.com",
                dbName: "heroku_97ce2639eb80fdc",
                username: "b3f0d16b8782a5",
                password: "ae60240e");

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
                        if (!ValidateUser(username,pwd))
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
                                ReadForm readForm = (ReadForm) new ReadFormBuilder().setData(data).setStyleOption(loginView.StyleOption).build();// ReadForm(data, loginView.OptionStyle);
                                readForm.ShowDialog();
                            }
                            else
                            {
                                ConnectDatabaseWindow dbWindow = new ConnectDatabaseWindow(loginView.StyleOption);
                                dbWindow.ShowDialog();
                            }


                            loginView.Close();
                        }
                        else loginView.ErrorLogin.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
                        break;
                    }
                case "ButtonToLoginView": ShowLoginView(); break;
                case "ButtonToRegisterView": ShowRegisterView(); break;
                case "ButtonRegister":
                    {
                        string username = this.GetUsernameInput(true);
                        string pwd = this.GetPasswordInput(true);
                        if (!ValidateUser(username, pwd))
                        {
                            loginView.ErrorRegister.Text = "Chưa nhập đủ thông tin!";
                            return;
                        }
                        User user = await Membership.HandleUser.findOneUserByFieldAsync("username", username);
                        if (user != null)
                        {
                            loginView.ErrorRegister.Text = "Tài khoản đã tồn tại";
                            return;
                        }

                        else
                        {
                            User newUser = User.getInstance(username, pwd, null, null, null, "user");
                            await Membership.HandleUser.AddNewUserAsync(newUser);
                            ShowLoginView();
                        }
                        break;
                    }
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

        public void KeyDownPasswordBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) loginView.ButtonLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
                            

        private bool ValidateUser(string username, string pwd)
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
            loginView.UsernameLogin.Focus();
            ClearFields();
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
