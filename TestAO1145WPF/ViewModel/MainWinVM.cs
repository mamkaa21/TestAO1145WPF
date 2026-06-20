using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class MainWinVM : BaseVM
    {
        public Visibility SelectPrepod
        {
            get => selectPrepod;
            set
            {
                selectPrepod = value;
                Signal(nameof(SelectPrepod));
            }
        }
        public Visibility SelectTest
        {
            get => selectTest; set
            {
                selectTest = value;
                Signal(nameof(SelectTest));
            }
        }
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
        private ObservableCollection<Test> testList { get; set; }
        public ObservableCollection<Test> TestList
        {
            get => testList;
            set
            {
                testList = value;
                Signal(nameof(TestList));
            }
        }
        private List<string> prepodList { get; set; }
        public List<string> PrepodList
        {
            get => prepodList;
            set
            {
                prepodList = value;
                Signal(nameof(PrepodList));
            }
        }
        public string Prepod
        {
            get => prepod; set
            {
                prepod = value;
                Signal(nameof(Prepod));
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
        public ICommand DoubleClickCommandNext { get; private set; }
        public Command Back { get; private set; }
        public MainWinVM()
        {
            timerStart();
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
            Back = new Command(() =>
            {
                SelectTest = Visibility.Collapsed;
                SelectPrepod = Visibility.Visible;
                timerStart();
            });
            DoubleClickCommand = new RelayCommand(DoubleClickExecute);
            DoubleClickCommandNext = new RelayCommand(DoubleClickExecuteNext);
        }
        private void DoubleClickExecute(object parameter)
        {
            foreach (var item in TestList.Where(s => s.Teacher != Prepod).ToList())
            {
                TestList.Remove(item);
            }

            SelectPrepod = Visibility.Collapsed;
            SelectTest = Visibility.Visible;
        }
        private void DoubleClickExecuteNext(object parameter)
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
                TestList =new ObservableCollection<Test>( await responce.Content.ReadFromJsonAsync<List<Test>>());
                PrepodList = TestList.Select(s => s.Teacher).Distinct().ToList();
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
            Thread thread1 = new Thread(GetAllTest);
            thread1.Start();
            timer.Stop();
        }

        MainWindow mainWindow;
        private Visibility selectPrepod = Visibility.Visible;
        private Visibility selectTest = Visibility.Collapsed;
        private string prepod;

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
