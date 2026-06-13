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
        private Teacher teacher { get; set; } = new Teacher();
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
        private Student student { get; set; } = new Student();
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                Signal(nameof(Student));
            }
        }
        public Visibility IsStudent
        {
            get => Student == null ? Visibility.Collapsed : Visibility.Visible;
        }
        private Subject subject { get; set; } = new Subject();
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;
                Signal(nameof(Subject));
            }
        }
        public Visibility IsSubject
        {
            get => Subject == null ? Visibility.Collapsed : Visibility.Visible;
        }

        private Class clas { get; set; } = new Class();
        public Class Clas
        {
            get => clas;
            set
            {
                clas = value;
                Signal(nameof(Clas));
            }
        }
        public Visibility IsClass
        {
            get => Clas == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public Command SaveTea { get; }
        public Command SaveSt { get; }
        public Command SaveSubj { get; }
        public Command SaveCl { get; }
        public AddTeacherWinVM()
        {
            SaveTea = new Command(async() =>// NO работает
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

            SaveSt = new Command(async () =>//работает
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

            SaveSubj = new Command(async () => //работает
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

            SaveCl = new Command(async () =>//работает
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
