using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
   public class AdminWinVM : BaseVM
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
        private Class clas { get; set; }
        public Class Class
        {
            get => clas;
            set
            {
                clas = value;
                Signal(nameof(Class));
            }
        }
        private List<Class> classList { get; set; }
        public List<Class> ClassList
        {
            get => classList;
            set
            {
                classList = value;
                Signal(nameof(ClassList));
            }
        }
        private DispatcherTimer timer = null;
        AdminWin adminWin;
        public Command OpenAddTeacher { get; }
        public Command OpenDelete { get; }
        public ICommand DoubleClickCommand { get; }

        JsonSerializerOptions options = new JsonSerializerOptions();
        public AdminWinVM() {
            timerStart();
            OpenAddTeacher = new Command(async () =>
            {
                AddTeacherWin addTeacherWin = new AddTeacherWin(null);
                addTeacherWin.Show();
                Signal();   
            });
            OpenDelete = new Command(async () =>
            {
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
                Signal();
            });

            DoubleClickCommand = new RelayCommand(arg => {
                var win = new AddTeacherWin(arg);
                win.Show();
            });
        }

        public async void GetAllClass()
        {
            string arg = JsonSerializer.Serialize(Teacher);
            var responce = await HttpClients.HttpClient.GetAsync($"Admin/GetAllClass");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ClassList = await responce.Content.ReadFromJsonAsync<List<Class>>();
                return;
            }
        }

        public async void GetAllTeacher()
        {
            string arg = JsonSerializer.Serialize(Teacher);
            var responce = await HttpClients.HttpClient.GetAsync($"Admin/GetAllTeacher");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TeacherList = await responce.Content.ReadFromJsonAsync<List<Teacher>>();
                return;
            }
        }

        public async void GetAllStudent()
        {
            string arg = JsonSerializer.Serialize(Student);
            var responce = await HttpClients.HttpClient.GetAsync($"Admin/GetAllStudent");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StudentList = await responce.Content.ReadFromJsonAsync<List<Student>>();
                return;
            }
        }

        public async void GetAllSubject()
        {
            string arg = JsonSerializer.Serialize(Subject);
            var responce = await HttpClients.HttpClient.GetAsync($"Admin/GetAllSubject");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SubjectList = await responce.Content.ReadFromJsonAsync<List<Subject>>();
                return;
            }
        }
        public void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread1 = new Thread(GetAllStudent);
            thread1.Start();
            Thread thread3 = new Thread(GetAllSubject);
            thread3.Start();
            Thread thread2 = new Thread(GetAllTeacher);
            thread2.Start();
            Thread thread4 = new Thread(GetAllClass);
            thread4.Start();
        }

        internal void SetWindow(AdminWin adminWin)
        {
            this.adminWin = adminWin;
        }
    }
}
