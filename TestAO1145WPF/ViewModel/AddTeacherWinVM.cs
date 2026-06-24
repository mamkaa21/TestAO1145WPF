using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

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
                if (teacher.subject.Count > 0)
                {
                    foreach (Subject sub in listSubjects.Items)
                    {
                        if (teacher.subject.FirstOrDefault(s => s.Id == sub.Id) != null)
                            listSubjects.SelectedItems.Add(sub);
                    }
                }
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
                if (value != null && classList != null && student.IdClass != null)
                {
                    ClassSelected = classList.FirstOrDefault(s => s.Id == student.IdClass.Value);
                }
                Signal(nameof(Student));
            }
        }
        public Visibility IsStudent
        {
            get => Student == null ? Visibility.Collapsed : Visibility.Visible;
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
        public Visibility IsSubject
        {
            get => Subject == null ? Visibility.Collapsed : Visibility.Visible;
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
        public Visibility IsClass
        {
            get => Clas == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public Command Back { get; }
        public Command SaveTea { get; }
        public Command SaveSt { get; }
        public Command SaveSubj { get; }
        public Command SaveCl { get; }
        public Command EditTea { get; }
        public Command EditSt { get; }
        public Command EditSubj { get; }
        public Command EditCl { get; }

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
        public Class ClassSelected {
            get => classSelected; set
            {
                classSelected = value;
                student.IdClass = classSelected.Id;
                Signal(nameof(ClassSelected));
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
        public async Task GetAllClass()
        {
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
                if (student != null && classSelected == null && student.IdClass != null)
                    ClassSelected = classList.FirstOrDefault(s => s.Id == student.IdClass.Value);
                return;
            }
        }
        public async Task GetAllSubject()
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
        TeacherWin teacherWin;
        public AddTeacherWinVM()
        {
            Task.Run(async () =>
            {
                await GetAllSubject();
                await GetAllClass();
            });
            Back = new Command(async() =>
            {
                CloseWindow();
            });
            SaveTea = new Command(async() =>// работает
            {
                Teacher.subject.Clear();
                foreach (var subj in listSubjects.SelectedItems)
                {
                    Teacher.subject.Add((Subject)subj);
                }
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
                    await GetAllSubject();
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
            EditTea = new Command(async() =>// 
            {
                Teacher.subject.Clear();
                foreach (var subj in listSubjects.SelectedItems)
                {
                    Teacher.subject.Add((Subject)subj);
                }
                string arg = JsonSerializer.Serialize(Teacher);
            var responce = await HttpClients.HttpClient.PutAsync($"Admin/SaveChangedByTeacher", new StringContent(arg, Encoding.UTF8, "application/json"));

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

            EditSt = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Student);
                var responce = await HttpClients.HttpClient.PutAsync($"Admin/SaveChangedByStudent", new StringContent(arg, Encoding.UTF8, "application/json"));

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

            EditSubj = new Command(async () => 
            {
                string arg = JsonSerializer.Serialize(Subject);
                var responce = await HttpClients.HttpClient.PutAsync($"Admin/SaveChangedBySubject", new StringContent(arg, Encoding.UTF8, "application/json"));

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

            EditCl = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Clas);
                var responce = await HttpClients.HttpClient.PutAsync($"Admin/SaveChangedByClass", new StringContent(arg, Encoding.UTF8, "application/json"));

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

        internal void UpdateVisible()
        {
            Signal(nameof(IsTeacher));
            Signal(nameof(IsSubject));
            Signal(nameof(IsClass));
            Signal(nameof(IsStudent));
        }
        ListBox listSubjects;
        private Class classSelected;

        internal void SetListSubjects(ListBox listSubjects)
        {
            this.listSubjects = listSubjects;
        }
        internal void CloseWindow()
        {
            this.teacherWin.Close();
        }
    }
}
