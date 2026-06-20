using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
     class TestPostTestByStWinVM : BaseVM
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
        private Question question { get; set; }
        public Question Question
        {
            get => question;
            set
            {
                question = value;
                Signal(nameof(Question));
            }
        }
        private List<Question> questionList { get; set; }
        public List<Question> QuestionList
        {
            get => questionList;
            set
            {
                questionList = value;
                Signal(nameof(QuestionList));
            }
        }
        public Command  Back {  get; }
        public Command  Post {  get; }

        
        public TestPostTestByStWinVM()
        {
            Back = new Command(() =>
            {
            
            });
            Post = new Command(async() => 
            {
                string arg = JsonSerializer.Serialize(Question);
                var responce = await HttpClients.HttpClient.PostAsync($"Student/PostStAns", new StringContent(arg, Encoding.UTF8, "application/json"));

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
                QuestionList = await responce.Content.ReadFromJsonAsync<List<Question>>();
                return;
            }
        }

        internal void SetTest(Test? test)
        {
            Test = test;
            GetTestWithQ();
        }
        internal void SetWindow(TestWin testWin)
        {
        }  
    }
}
