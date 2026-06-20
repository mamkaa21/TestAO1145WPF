using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class TeacherWinVM : BaseVM
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
        private DispatcherTimer timer = null;
        public Command OpenResultWin { get; }
        public Command OpenAllTestWin { get; }
        public Command OpenUserTWin { get; }
        public Command OpenNewTestWin { get; }
        public ICommand DoubleClickCommand { get; private set; }

        public TeacherWinVM()
        {
            timerStart();
            OpenResultWin = new Command(async () =>
            {
                ResultWin resultWin = new ResultWin();
                resultWin.Show();
                Signal();
            });
            OpenAllTestWin = new Command(async () =>
            {
                AllTestWin allTestWin = new AllTestWin();
                allTestWin.Show();
                Signal();
            });
            OpenUserTWin = new Command(async () =>
            {
                UserTWin usertWin = new UserTWin();
                usertWin.Show();
                Signal();
            });
            OpenNewTestWin = new Command(async () =>
            {
                CreateTestWin createTestWin = new CreateTestWin();
                createTestWin.Show();
                Signal();
            });
            DoubleClickCommand = new RelayCommand(DoubleClickExecute);

            
        }
        public async void GetTest()
        {
            string arg = JsonSerializer.Serialize(Test);
            var responce = await HttpClients.HttpClient.GetAsync($"Teacher/GetTest");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TestList = await responce.Content.ReadFromJsonAsync<List<Test>>();
                return;
            }
        }
        public void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread1 = new Thread(GetTest);
            thread1.Start();
            timer.Interval = new TimeSpan(0, 0, 10);
        }
        private void DoubleClickExecute(object parameter)
        {

            if (parameter is Test Test)
            {
                CreateTWithQWin goodWin = new CreateTWithQWin(Test);
                goodWin.Show();
                Signal();
            }
        }
        TeacherWin teacherWin;
        internal void SetWindow(TeacherWin teacherWin)
        {
            this.teacherWin = teacherWin;
        }
        internal void CloseWindow()
        {
            this.teacherWin.Close();
        }

    }
}
