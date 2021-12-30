using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.Controllers;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for DBForm.xaml
    /// </summary>
    public partial class DBForm : Window
    {
        private ConnectDBController _Controller;
        public DBForm()
        {
            InitializeComponent();
            HandleEvents();
        }

        public void HandleEvents()
        {
            _Controller = new ConnectDBController(this);
            HostNameInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            UserNameInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            PwdInput.KeyDown += new KeyEventHandler(_Controller.KeyDownTextBox);
            ButtonConnect.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            ButtonGenerate.Click += new RoutedEventHandler(_Controller.ButtonRoutedEventArgs);
            DbNameComboBox.SelectionChanged += new SelectionChangedEventHandler(_Controller.OnChangeComboBox);

        }

        

        private void DbTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void DbNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
