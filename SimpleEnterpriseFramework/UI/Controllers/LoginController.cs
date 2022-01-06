
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
            loginView.NameLoginIn.Focus();
        }

        public async void ButtonRoutedEventArgs(object sender, RoutedEventArgs e)
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
                        var x = await Membership.Authentication.validateAsync("username", "pasword");
                        if (await Membership.Authentication.validateAsync(username,pwd))
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
                                DBForm dbForm = new DBForm(loginView.OptionStyle);
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

        private async void ButtonSignUp()
        {
            string username = this.getUserNameInput(true);
            string pwd = this.getPasswordInput(true);
            if (!ValidationUser(username, pwd))
            {
                loginView.IncorrectSingUp.Text = "Chưa nhập đủ thông tin!";
                return;
            }
            User user = await Membership.HandleUser.findOneUserByFieldAsync("username", username);
            if(user != null)
            {
                loginView.IncorrectSingUp.Text = "Tài khoản đã tồn tại";
                return;
            }

            else
            {
                User newUser = User.getInstance(username, pwd, "email", "phone", "address", "role");
                await Membership.HandleUser.AddNewUserAsync(newUser);
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
