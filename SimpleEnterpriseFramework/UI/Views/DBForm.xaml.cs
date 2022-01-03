namespace UI.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using UI.Controllers;
    using UI.Model;

    public partial class DBForm : Window
    {
        private ConnectDBController _Controller;

        private StyleOption styleOption;

        public DBForm(StyleOption option)
        {

            InitializeComponent();
            this.styleOption = option;
            HandleEvents();
            InitStyle();
        }

        public void HandleEvents()
        {
            _Controller = new ConnectDBController(this, styleOption);
            HostNameInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            UserNameInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            PwdInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            ButtonConnect.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonGenerate.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
           // DbTypeComboBox.SelectionChanged += new SelectionChangedEventHandler(_Controller.OnChangeComboBox);
            //TableNameComboBox.SelectionChanged += new SelectionChangedEventHandler(_Controller.OnChangeComboBox);
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
                    ButtonConnect.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    ButtonGenerate.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                }
            }
        }

    }
}
