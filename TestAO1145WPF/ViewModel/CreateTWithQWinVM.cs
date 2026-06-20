using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    internal class CreateTWithQWinVM : BaseVM
    {
        private Test test;
        private ObservableCollection<Question> questions;
        private Question question;

        public Command OneAnswer { get; private set; }
        public Command ManyAnswer { get; private set; }
        public Command HandAnswer { get; private set; }
        public Command AddAnswer { get; private set; }
        public Command SaveQuestion { get; private set; }
        public Test Test
        {
            get => test; set
            {
                test = value;
                Signal(nameof(Test));
                if (test.Id != 0)
                    LoadQuestions(test.Id);
            }
        }

        public ObservableCollection<Question> Questions
        {
            get => questions; set
            {
                questions = value;
                Signal(nameof(Questions));
            }
        }
        public Question Question
        {
            get => question;
            set
            {
                question = value; 
                Signal(nameof(Question));
                if (question == null)
                {
                    answersControl.Template = null;
                    return;
                }
                if (question.Type == 1)
                    answersControl.Template = createTWithQWin.FindResource("TemplateOneAnswer") as ControlTemplate;
                else if (question.Type == 2)
                    answersControl.Template = createTWithQWin.FindResource("TemplateManyAnswer") as ControlTemplate;
                else if (question.Type == 3)
                    answersControl.Template = createTWithQWin.FindResource("TemplateHandAnswer") as ControlTemplate;
                
                foreach (var answer in question.Answers)
                    answer.IdQuestionNavigation = question;
            }
        }

        void Commands()
        {
            AddAnswer = new Command(() =>
            {
                if (Question != null)
                {
                    Question.Answers.Add(new Answer { IdQuestion = Question.Id , IdQuestionNavigation = Question, RightAnswer = Question.Type == 3 });
                }
            });
            SaveQuestion = new Command(async () =>
            {
                if (Question == null)
                    return;

                HttpResponseMessage response = null; 
                if (Question.Id == 0)
                {
                    response = await HttpClients.HttpClient.PostAsJsonAsync($"Teacher/AddQ", Question);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(await response.Content.ReadAsStringAsync());
                        return;
                    }
                }
                else
                {
                    response = await HttpClients.HttpClient.PostAsJsonAsync($"Teacher/EditQ", Question);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(await response.Content.ReadAsStringAsync());
                        return;
                    }
                }
                Question readyQ = await response.Content.ReadFromJsonAsync<Question>();
                int index = Questions.IndexOf(Question);
                Questions[index] = readyQ;
                Question = readyQ;
                foreach (var answer in readyQ.Answers)
                    answer.IdQuestionNavigation = readyQ;
            });

            OneAnswer = new Command(() =>
            {
                var ques = new Question { Name = "Новый вопрос", IdTest = test.Id, Answers = new ObservableCollection<Answer>(), Type = 1 };
                Questions.Add(ques);
                Question = ques;
                answersControl.Template = createTWithQWin.FindResource("TemplateOneAnswer") as ControlTemplate;
            });
            ManyAnswer = new Command(() =>
            {
                var ques = new Question { Name = "Новый вопрос", IdTest = test.Id, Answers = new ObservableCollection<Answer>(), Type = 2 };
                Questions.Add(ques);
                Question = ques;
                answersControl.Template = createTWithQWin.FindResource("TemplateManyAnswer") as ControlTemplate;
            });
            HandAnswer = new Command(() =>
            {
                var ques = new Question { Name = "Новый вопрос", IdTest = test.Id, Answers = new ObservableCollection<Answer>(), Type = 3 };
                Questions.Add(ques);
                Question = ques;
                answersControl.Template = createTWithQWin.FindResource("TemplateHandAnswer") as ControlTemplate;
            });
        }

        public CreateTWithQWinVM()
        {
            Questions = new();
            Commands();
        }

        private async void LoadQuestions(int testId)
        {
            var responce = await HttpClients.HttpClient.GetAsync($"Teacher/GetTestWithQ?id=" + testId);
            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
            }

            Questions = new ObservableCollection<Question>(await responce.Content.ReadFromJsonAsync<List<Question>>());
        }
        ItemsControl answersControl;
        internal void SetControl(ItemsControl answersControl)
        {
            this.answersControl = answersControl;
        }
        CreateTWithQWin createTWithQWin;
        internal void SetWindow(CreateTWithQWin createTWithQWin)
        {
            this.createTWithQWin = createTWithQWin;
        }

        internal void OneAnswerChecked(Answer? answer)
        {
            foreach (var item in Question.Answers)
            {
                item.RightAnswer = item == answer;
            }
            Signal(nameof(Question.Answers));
        }
    }
}
