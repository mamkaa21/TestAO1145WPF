using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class TestWinVM : BaseVM
    {
        private Test test { get; set; }
        public Test Test
        {
            get => test;
            set
            {
                test = value;
                Signal(nameof(Test));
            }
        }
        private INothing question { get; set; }
        public INothing Question
        {
            get => question;
            set
            {
                question = value;
                Signal(nameof(Question));
            }
        }

        private List<INothing> questions;

        public ObservableCollection<INothing> QuestionList { get; } = new();
        int pageIndex = 0;
        int pageCount = 0;
        public ICommand QBack { get; }
        public ICommand QNext { get; }
        public ICommand SendResult { get; }
        public TestWinVM()
        {

            QBack = new RelayCommand(s => {
                if (pageIndex > 0)
                    pageIndex--;

                QuestionList.Clear();
                foreach (var item in questions.Skip(pageIndex * 5).Take(5))
                    QuestionList.Add(item);

            });
            QNext = new RelayCommand(s => {
                if (pageIndex < pageCount - 1)
                    pageIndex++;
                QuestionList.Clear();
                foreach (var item in questions.Skip(pageIndex * 5).Take(5))
                    QuestionList.Add(item);
            });
            SendResult = new RelayCommand(async s =>
            {
                foreach (var q in questions)
                {
                    // предпроверка ручного ввода
                    if (q is QTestType3 type3)
                    {
                        var right = type3.Question.Answers.FirstOrDefault(s => s.Text.Trim().ToLower() == type3.Question.StudentAnswer?.Trim().ToLower());
                        if (right != null)
                            right.IsChecked = true;
                    }
                }

                // отправка ответов
                Studentanswer studentanswer = new();
                studentanswer.IdStudent = CurrentUser.student.Id;
                studentanswer.IdTest = Test.Id;
                studentanswer.StudentName = CurrentUser.student.FirstName;
                studentanswer.StudentLastName = CurrentUser.student.LastName;
                studentanswer.Name = Test.Name;
                studentanswer.DateTime = DateTime.Now;
                studentanswer.Testcrossquestions = new List<Testcrossquestion>();
                foreach (var q in questions)
                {
                    var selected = q.Question.Answers.Where(s => s.IsChecked).ToList();
                    foreach (var item in selected)
                        studentanswer.Testcrossquestions.Add(new Testcrossquestion
                        {
                            IdQuestion = q.Question.Id,
                            IdStudent = studentanswer.IdStudent.Value,
                            IdAnswer = item.Id
                        });
                }

                var responce = await HttpClients.HttpClient.PostAsJsonAsync("Student/PostStAns", studentanswer);
                if (responce.IsSuccessStatusCode)
                {
                    var result = await responce.Content.ReadFromJsonAsync<Studentanswer>();
                    MessageBox.Show(result.Mark.ToString(), "Оценка за тестирование");
                   CloseWindow();
                }
                else
                    MessageBox.Show("Ошибка. Свяжитесь с разработчиком.");
            });

        }
        public async void GetTestWithQ()
        {
            var responce = await HttpClients.HttpClient.GetAsync($"Teacher/GetTestWithQ?id={test.Id}");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var allQ = await responce.Content.ReadFromJsonAsync<List<Question>>();
                questions = new();
                foreach (var q in allQ)
                    switch (q.Type)
                    {
                        case 1: questions.Add(new QTestType1 { Question = q }); break;
                        case 2: questions.Add(new QTestType2 { Question = q }); break;
                        case 3: questions.Add(new QTestType3 { Question = q }); break;
                    }
                pageCount = questions.Count / 5 + ((questions.Count % 5 != 0) ? 1 : 0);
                QBack?.Execute(null);
                return;
            }
        }

        internal void SetTest(Test? test)
        {
            Test = test;
            GetTestWithQ();
        }
        TestWin testWin;
        internal void SetWindow(TestWin testWin)
        {
            this.testWin = testWin;
        }
        private void CloseWindow()
        {
            testWin.Close();
        }
     //измения всех сделать в апи, окнам нормальный вид, соеденить уже рабочее с апи в впф, обаить таблицу админа, проверка на вход для админа, подумать над тестами и справочниками  
   
    }

    public interface INothing
    {
        Question Question { get; set; }
    }

    public class QTestType1 : INothing
    {
        public Question Question { get; set; }
    }
    public class QTestType2 : INothing
    {
        public Question Question { get; set; }
    }
    public class QTestType3 : INothing
    {
        public Question Question { get; set; }
    }
}
