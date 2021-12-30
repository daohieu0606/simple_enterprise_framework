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
        private LoginController _Controller;

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
            _Controller = new LoginController(this, data);
            NameSingUp.KeyDown += new KeyEventHandler(_Controller._KeyDownTextBox);
            NameLoginIn.KeyDown += new KeyEventHandler(_Controller._KeyDownTextBox);
            ButtonExit.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonLogin.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonSignUp.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonRegistro.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonVolverLogin.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
        }

        private void GridPrincipalMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        public void SetButtonColor(byte a, byte r, byte g, byte b)
        {
            ButtonLogin.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        public void SetFont(string fontFamily)
        {
            FontFamily font = new FontFamily(fontFamily);
            NameLoginIn.FontFamily = font;
            NameSingUp.FontFamily = font;
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
                    ButtonSignUp.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
            }
        }
    }
}
