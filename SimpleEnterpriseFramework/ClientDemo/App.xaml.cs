namespace ClientDemo
{
    using System.Collections.Generic;
    using System.Data;
    using System.Windows;
    using UI;
    using UI.Model;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ProxyForm baseForm = new ProxyForm();
            // Nguồn dữ liệu dạng List
            //baseForm.setDataSource<Student>(GetList());
            //Nguồn dữ liệu dạng DataTable
            //baseForm.setDataSource(GetDataTable());
            //Nguồn dữ liệu dạng Array
            baseForm.setDataSource(GetArray());

            //Set style cho các form
            StyleOption option = new StyleOption();
            //1. Set color cho button
            //option.ButtonColor = new ColorArgs(255, 0, 0, 144);
            //2. Set tên các màn hình CRU (dành cho nguồn dữ liệu người dùng truyền vào)
           // option.CRUDWindowNames = new List<string> { "Danh sách sinh viên", "Tạo mới sinh viên", "Cập nhật sinh viên" };
            //3. Set color cho background
            //option.BackgroundColor = new ColorArgs(255, 215, 123, 144);
            //4. Set font
            //option.FontFamily = "Comic Sans MS";

            //5. Set datagrid style

            DataGridStyle style = new DataGridStyle(
                headerBackground: new ColorArgs(255, 255, 222, 173),
                cellsBackground: new ColorArgs(255, 255, 106, 106),
                rowHeight: 40,
                headerHeight: 45,
                columnName: new List<string>() { "MSSV", "Tên", null, "Quốc Gia"}
                );
            option.DataGridStyle = style;
            //Set style cho các form
            baseForm.setStyle(option);

            //Chạy chương trình
            baseForm.startForm();
        }

        public DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));
            table.Columns.Add("CITY", typeof(string));
            table.Rows.Add(111, "Devesh", "Ghaziabad");
            table.Rows.Add(222, "ROLI", "KANPUR");
            table.Rows.Add(102, "ROLI", "MAINPURI");
            table.Rows.Add(212, "DEVESH", "KANPUR");
            return table;
        }

        public Student[] GetArray()
        {
            return new[] {
               new Student() { StudentID = 1, StudentName = "John", Age = 18, Country = "Poland", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 22, Country = "Poland", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, Country = "USA", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, Country = "USA", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21, Country = "Germany", Birthday="11/01/2000", Gender=1  }

            };
        }

        public List<Student> GetList()
        {
            return new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18, Country = "Poland", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 22, Country = "Poland", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, Country = "USA", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, Country = "USA", Birthday="11/01/2000", Gender=1  } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21, Country = "Germany", Birthday="11/01/2000", Gender=1  }
            };
        }

        public class Student
        {
            public int StudentID { get; set; }

            public string StudentName { get; set; }

            public int Age { get; set; }

            public string Country { get; set; }

            public string Birthday { get; set; }

            public int Gender { get; set; }


            public override string ToString()
            {
                return $"StudentID: {this.StudentID} - StudentName: {this.StudentName} - Age: {this.Age} - Country: {this.Country}";
            }
        }
    }
}
