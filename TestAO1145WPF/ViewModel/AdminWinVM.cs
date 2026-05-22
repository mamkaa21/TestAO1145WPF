using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class AdminWinVM : BaseVM
    {
        private Teacher teacher { get; set; }
        public Teacher Teacher
        {
            get => teacher;
            set
            {
                teacher = value;
                Signal(nameof(Teacher));
            }
        }
        private List<Teacher> teacherList { get; set; }
        public List<Teacher> TeacherList
        {
            get => teacherList;
            set
            {
                teacherList = value;
                Signal(nameof(TeacherList));
            }
        }
        private Student student { get; set; }
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                Signal(nameof(Student));
            }
        }
        private List<Student> studentList { get; set; }
        public List<Student> StudentList
        {
            get => studentList;
            set
            {
                studentList = value;
                Signal(nameof(StudentList));
            }
        }
        private Subject subject { get; set; }
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;
                Signal(nameof(Subject));
            }
        }
        private List<Subject> subjectList { get; set; }
        public List<Subject> SubjectList
        {
            get => subjectList;
            set
            {
                subjectList = value;
                Signal(nameof(SubjectList));
            }
        }

        AdminWin adminWin;
        public Command OpenAddTeacher { get; }
        //public Command OpenEditTeacher { get; }
        //public Command OpenAddNewStudent { get; }
        public Command OpenDelete { get; }
        //public Command OpenDeleteT { get; }
        //public Command OpenDeleteST { get; }

        JsonSerializerOptions options = new JsonSerializerOptions();
        public AdminWinVM() {
            //тут добавить метод получения учителей студентов и предметов с апи 
            OpenAddTeacher = new Command(async () =>
            {
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
                Signal();
                
            });
            OpenDelete = new Command(async () =>
            {
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
                Signal();

            });
        }
         
      

        
        internal void SetWindow(AdminWin adminWin)
        {
            this.adminWin = adminWin;
        }
    }
}
