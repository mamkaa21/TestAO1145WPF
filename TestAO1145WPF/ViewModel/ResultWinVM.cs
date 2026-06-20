using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class ResultWinVM : BaseVM
    {
        private Studentanswer studentanswer { get; set; }
        public Studentanswer Studentanswer
        {
            get => studentanswer;
            set
            {
                studentanswer = value;
                Signal(nameof(Studentanswer));
            }
        }
        private List<Studentanswer> studentanswerList { get; set; }
        public List<Studentanswer> StudentanswerList
        {
            get => studentanswerList;
            set
            {
                studentanswerList = value;
                Signal(nameof(StudentanswerList));
            }
        }
        private DispatcherTimer timer = null;
        public ResultWinVM()
        {
            //timerStart();
            GetResults();
        }
        public async void GetResults()
        {
            string arg = JsonSerializer.Serialize(Studentanswer);
            var responce = await HttpClients.HttpClient.GetAsync($"Teacher/GetResults");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StudentanswerList = await responce.Content.ReadFromJsonAsync<List<Studentanswer>>();
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
            Thread thread1 = new Thread(GetResults);
            thread1.Start();
        }

        internal void SetWindow(ResultWin resultWin)
        {
           this.resultWin = resultWin;
        }
        ResultWin resultWin;
      
    }
}
