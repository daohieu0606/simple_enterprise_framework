using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI.Model;
using UI.Views;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BaseForm baseForm = new BaseForm();
            List<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18, Country = "Poland"  } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 22, Country = "Poland"  } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, Country = "USA"  } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, Country = "USA"  } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21, Country = "Germany"  }
            };
            //baseForm.setDataSource<Student>(studentList);
            StyleOption option = new StyleOption();
            ColorArgs buttonColor = new ColorArgs(255, 0, 0, 144);
            option.CRUDWindowNames = new List<string> { "Danh sách sinh viên", "Tạo mới sinh viên", "Cập nhật sinh viên" };
            option.ButtonColor = buttonColor;
            baseForm.setStyle(option);
            baseForm.startForm();


            //Login mw = new Login();
            //mw.SetButtonColor(255, 255, 0, 255);
            //mw.SetFont("Comic Sans MS");
            //mw.Show();
        }

        public class Student
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public int Age { get; set; }
            public string Country { get; set; }

            public override string ToString()
            {
                return $"StudentID: {this.StudentID} - StudentName: {this.StudentName} - Age: {this.Age} - Country: {this.Country}";
            }
        }
    }
}
