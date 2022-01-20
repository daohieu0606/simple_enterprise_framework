using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Model
{
    public class DataGridStyle
    {
        private ColorArgs headerBackground;

        private ColorArgs cellsBackground;

        private double headerHeight;

        private double rowHeight;

        private ColorArgs headerColor;

        private ColorArgs cellsColor;

        private string headerFontFamily;

        private string cellsFontFamily;

        private string headerFontSize;

        private string cellsFontSize;

        private List<string> columnNames;


        public DataGridStyle(ColorArgs headerBackground = null, ColorArgs cellsBackground = null, double headerHeight = 40d, double rowHeight = 30d, ColorArgs cellsColor = null, ColorArgs headerColor = null, List<string> columnName = null)
        {
            this.headerBackground = headerBackground;
            this.cellsBackground = cellsBackground;
            this.headerHeight = headerHeight;
            this.rowHeight = rowHeight;
            this.headerColor = headerColor;
            this.cellsColor = cellsColor;
            this.columnNames = columnName;
        }

        public ColorArgs CellsBackground
        {
            get { return cellsBackground; }
            set { cellsBackground = value; }
        }

        public ColorArgs HeaderBackground
        {
            get { return headerBackground; }
            set { headerBackground = value; }
        }

        public double RowHeight
        {
            get { return rowHeight; }
            set { rowHeight = value; }
        }

        public double HeaderHeight
        {
            get { return headerHeight; }
            set { headerHeight = value; }
        }

        public List<string> ColumnNames
        {
            get { return columnNames; }
            set { columnNames = value; }
        }
    }
}
