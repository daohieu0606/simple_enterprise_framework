using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using UI.Helpers;
using UI.Model;
using UI.Views;

namespace UI
{
    public class RealForm
    {
        private StyleOption styleOption;
        private DataTable data;

        public RealForm(StyleOption option, DataTable source)
        {
            this.styleOption = option;
            this.data = source;
        }
        public void startForm()
        {
            LoginWindow login;
            if (this.data==null)
            login = new LoginWindow(styleOption);
            else login = new LoginWindow(styleOption, data);
            login.Show();
        }
    }
}
