namespace UI.Model
{
    using System.Collections.Generic;
    using System.Data;

    public class StyleOption
    {
        private ColorArgs buttonColor;

        private ColorArgs backgroundColor;

        private string fontFamily;

        private string logoUrl;

        private double rowHeight = 30d;

        private List<string> windowNames;

        private DataGridStyle dataGridStyle;

        public ColorArgs ButtonColor
        {
            get { return buttonColor; }
            set { buttonColor = value; }
        }

        public ColorArgs BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public double DatatRowHeight
        {
            get { return rowHeight; }
            set { rowHeight= value; }
        }
        public string FontFamily
        {
            get { return fontFamily; }
            set { fontFamily = value; }
        }

        public List<string> CRUDWindowNames
        {
            get { return windowNames; }
            set { windowNames = value; }
        }

        public string LogoUrl
        {
            get { return logoUrl; }
            set { logoUrl = value; }
        }

        public DataGridStyle DataGridStyle
        {
            get { return dataGridStyle; }
            set { dataGridStyle = value; }
        }


    }
}
