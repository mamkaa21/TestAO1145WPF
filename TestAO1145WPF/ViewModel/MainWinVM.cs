using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    class MainWinVM : BaseVM
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

        private DispatcherTimer timer = null;
        public Command OpenResultForOneStWin { get; }
        public Command OpenAllTestForOneStWin { get; }
        public Command OpenUserStWin { get; }
        public ICommand DoubleClickCommand { get; private set; }
        public MainWinVM()
        {
            GetAllTest();
            OpenResultForOneStWin = new Command(async () =>
            {
                ResultForOneStWin resultWin = new ResultForOneStWin();
                resultWin.Show();
                Signal();
            });
            OpenAllTestForOneStWin = new Command(async () =>
            {
                AllTestForOneStWin allTestWin = new AllTestForOneStWin();
                allTestWin.Show();
                Signal();
            });
            OpenUserStWin = new Command(async () =>
            {
                UserStWin userStWin = new UserStWin(Student);
                userStWin.Show();
                Signal();
            });
            DoubleClickCommand = new RelayCommand(DoubleClickExecute);          
        }

        private void DoubleClickExecute(object parameter)
        {

            if (parameter is Test Test)
            {
                TestWin goodWin = new TestWin(Test);
                goodWin.Show();
                Signal();
            }
        }
        public async void GetAllTest()
        {
            string arg = JsonSerializer.Serialize(Test);
            var responce = await HttpClients.HttpClient.GetAsync($"Student/GetAllTest");

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
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread1 = new Thread(GetAllTest);
            thread1.Start();           
        }


        MainWindow mainWindow;
        internal void SetWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        internal void CloseWindow(MainWindow mainWindow)
        {
            this.mainWindow.Close();
        }

        internal void SetStudent(Student? student)
        {
            Student = student;
        }
    }

}
