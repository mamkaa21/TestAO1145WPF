using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TestAO1145WPF.Model;

namespace TestAO1145WPF.ViewModel
{
    public class AddTeacherWinVM: BaseVM
    {
        private Teacher teacher { get; set; }
        public Teacher Teacher
        {
            get => teacher;
            set
            {
                teacher = value;
                Signal(nameof(Teacher));
                Signal(nameof(IsTeacher));
            }
        }
        public Visibility IsTeacher
        {
            get => Teacher == null ? Visibility.Collapsed : Visibility.Visible;
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
        public Class Clas
        {
            get => clas;
            set
            {
                clas = value;
                Signal(nameof(Clas));
            }
        }
        private List<Class> clasList { get; set; }
        public List<Class> ClasList
        {
            get => clasList;
            set
            {
                clasList = value;
                Signal(nameof(ClasList));
            }
        }

        public Command SaveTea { get; }
        public Command SaveSt { get; }
        public Command SaveSubj { get; }
        public Command SaveCl { get; }
        public AddTeacherWinVM()
        {
            SaveTea = new Command(async() =>
            {
            string arg = JsonSerializer.Serialize(Teacher);
            var responce = await HttpClients.HttpClient.PostAsync($"Admin/AddNewTeacher", new StringContent(arg, Encoding.UTF8, "application/json"));

            if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var result = await responce.Content.ReadAsStringAsync();


                MessageBox.Show("error1");
                return;
            }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Оk");
                    return;
                }
                else
                    MessageBox.Show("Error2");
                return;
            });

            SaveSt = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Student);
                var responce = await HttpClients.HttpClient.PostAsync($"Admin/AddNewStudent", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await responce.Content.ReadAsStringAsync();


                    MessageBox.Show("error1");
                    return;
                }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Оk");
                    return;
                }
                else
                    MessageBox.Show("Error2");
                return;
            });

            SaveSubj = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Subject);
                var responce = await HttpClients.HttpClient.PostAsync($"Admin/AddSubject", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await responce.Content.ReadAsStringAsync();


                    MessageBox.Show("error1");
                    return;
                }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Оk");
                    return;
                }
                else
                    MessageBox.Show("Error2");
                return;
            });

            SaveCl = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Clas);
                var responce = await HttpClients.HttpClient.PostAsync($"Admin/AddClass", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await responce.Content.ReadAsStringAsync();


                    MessageBox.Show("error1");
                    return;
                }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Оk");
                    return;
                }
                else
                    MessageBox.Show("Error2");
                return;
            });


        }

        
    }
}
