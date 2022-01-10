namespace UI.Model
{
    using System.Collections.Generic;
    using System.Data;

    public class StyleOption
    {
        ColorArgs buttonColor;

        ColorArgs backgroundColor;

        string fontFamily;

        List<string> windowNames;

        List<string> columnNames;

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

        public List<string> ColumnNames
        {
            get { return columnNames; }
            set { columnNames = value; }
        }


    }
}
