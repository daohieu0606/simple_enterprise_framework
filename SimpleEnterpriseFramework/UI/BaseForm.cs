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
    public class BaseForm
    {
        private Login loginForm;
        private StyleOption styleOption;
        private DataTable data;


        public void startForm()
        {
            if(this.data==null)
            loginForm = new Login(styleOption);
            else loginForm = new Login(styleOption, data);
            loginForm.Show();
        }

        public void setDataSource<T>(IList<T> dataSource)
        {
            data = DataHelper.ToDataTable(dataSource);
        }

        public void setStyle(StyleOption option)
        {
            styleOption = option;
        }





    }
}
