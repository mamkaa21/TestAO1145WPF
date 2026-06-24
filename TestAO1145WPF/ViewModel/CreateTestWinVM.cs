using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class CreateTestWinVM : BaseVM
    {
        private Test test { get; set; } = new();
        public Test Test
        {
            get => test;
            set
            {
                test = value;
                Signal(nameof(Test));
            }
        }
        private List<Test> testList { get; set; }
        public List<Test> TestList
        {
            get => testList;
            set
            {
                testList = value;
                Signal(nameof(TestList));
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
        private Student student { get; set; }
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                Signal(nameof(student));
            }
        }

        public Teacher Teacher { get; set; }
        public Command OpenCreateQForT { get; }


        public CreateTestWinVM()
        {
            Teacher = CurrentUser.teacher;
            LoadSubjects();
            OpenCreateQForT = new Command(async () => 
            {
                if (Subject == null)
                {
                    MessageBox.Show("Выберите предмет!");
                    return;
                }
                if (Test.CountQuestionTest == null)
                {
                    MessageBox.Show("Укажите кол-во вопросов!");
                    return;
                }
                Test.IdSubject = Subject.Id;
                test.IdTeacher = CurrentUser.teacher.Id;
                var responce = await HttpClients.HttpClient.PostAsJsonAsync($"Teacher/AddTest", Test);
                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show(result);
                    return;
                }
                Test.Id = await responce.Content.ReadFromJsonAsync<int>();

                CreateTWithQWin createTWith = new CreateTWithQWin(Test);
                createTWith.Show();
                CloseWindow();

            });
           
        }

        private async void LoadSubjects()
        {
            var responce = await HttpClients.HttpClient.GetAsync($"Teacher/GetSubjects");

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
        CreateTestWin createTestWin;
        internal void SetWindow(CreateTestWin createTestWin)
        {
            this.createTestWin = createTestWin;
        }
        internal void CloseWindow()
        {
            this.createTestWin.Close();
        }
    }

}
