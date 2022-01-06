namespace UI.Views
{
    using System;
    using System.Data;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using UI.Controllers;
    using UI.Model;

    public partial class Login : Window
    {
        private LoginController controller;

        private StyleOption styleOption;

        private DataTable data;

        public Login(StyleOption option)
        {
            InitializeComponent();
            this.styleOption = option;
            InitStyle();
            HandleEvents();
        }

        public Login(StyleOption option, DataTable source)
        {
            InitializeComponent();
            this.data = source;
            this.styleOption = option;
            InitStyle();
            HandleEvents();
        }

        public void HandleEvents()
        {
            controller = new LoginController(this, data);
            UsernameRegister.KeyDown += new KeyEventHandler(controller.KeyDownTextBox);
            UsernameLogin.KeyDown += new KeyEventHandler(controller.KeyDownTextBox);
            ButtonExit.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            ButtonLogin.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            ButtonToRegisterView.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            ButtonRegister.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            ButtonToLoginView.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
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
                    ButtonLogin.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    ButtonRegister.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
            }
        }
    }
}
