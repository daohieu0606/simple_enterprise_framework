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
    public class ProxyForm
    {
        private RealForm form;
        private StyleOption styleOption;
        private DataTable data;


        public void startForm()
        {

            if (form == null)
            {
                form = new RealForm(styleOption, data);
            }
            form.startForm();
        }

        public void setDataSource<T>(IList<T> dataSource)
        {
            data = DataHelper.ToDataTable(dataSource);
        }

        public void setDataSource(DataTable dataSource)
        {
            data = dataSource;
        }


        public void setStyle(StyleOption option)
        {
            styleOption = option;
        }

    }
}
