﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI;
using UI.Model;

namespace ClientDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ProxyForm baseForm = new ProxyForm();
            // Nguồn dữ liệu dạng List

            List<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18, Country = "Poland"  } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 22, Country = "Poland"  } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, Country = "USA"  } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, Country = "USA"  } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21, Country = "Germany"  }
            };
            //baseForm.setDataSource<Student>(studentList);

            //Nguồn dữ liệu dạng DataTable

            //Set style cho các form
            StyleOption option = new StyleOption();
            //1. Set color cho button
            option.ButtonColor = new ColorArgs(255, 0, 0, 144);
            //2. Set tên các màn hình CRU (dành cho nguồn dữ liệu người dùng truyền vào)
            option.CRUDWindowNames = new List<string> { "Danh sách sinh viên", "Tạo mới sinh viên", "Cập nhật sinh viên" };
            //3. Set color cho background
            option.BackgroundColor = new ColorArgs(255, 215, 123, 144);
            //4. Set font
            option.FontFamily = "Comic Sans MS";
            //Set style cho các form
            //baseForm.setStyle(option);

            //Chạy chương trình
            baseForm.startForm();


            //Login mw = new Login();
            //mw.SetButtonColor(255, 255, 0, 255);
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
