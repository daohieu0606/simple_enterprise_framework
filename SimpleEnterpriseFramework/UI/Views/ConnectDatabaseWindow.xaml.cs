namespace UI.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using UI.Controllers;
    using UI.Model;

    public partial class ConnectDatabaseWindow : Window
    {
        private HandleDataController controller;

        private StyleOption styleOption;

        public ConnectDatabaseWindow(StyleOption option)
        {

            InitializeComponent();
            this.styleOption = option;
            HandleEvents();
            InitStyle();
        }

        public void HandleEvents()
        {
            controller = new HandleDataController(this, styleOption);
            ButtonConnect.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            ButtonGenerate.Click += new RoutedEventHandler(controller.ButtonRoutedEventArgs);
            HostNameInput.KeyDown += new KeyEventHandler(controller.KeyDownTextBox);
            UserNameInput.KeyDown += new KeyEventHandler(controller.KeyDownTextBox);
            PwdInput.KeyDown += new KeyEventHandler(controller.KeyDownPasswordBox);
            DatabaseNameInput.KeyDown += new KeyEventHandler(controller.KeyDownTextBox);
        }

        private void InitStyle()
        {
            HostNameInput.Focus();
            //if (this.styleOption != null)
            //{
            //    if (styleOption.ButtonColor != null)
            //    {
            //        byte a = styleOption.ButtonColor.a;
            //        byte r = styleOption.ButtonColor.r;
            //        byte g = styleOption.ButtonColor.g;
            //        byte b = styleOption.ButtonColor.b;
            //        ButtonConnect.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            //        ButtonGenerate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            //    }
            //}
        }

    }
}
