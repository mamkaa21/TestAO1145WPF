using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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


        public Command OpenResultWin { get; }

        public Command OpenAllTestWin { get; }
        public Command OpenUserStWin { get; }
        public ICommand DoubleClickCommand { get; private set; }
        public MainWinVM()
        {
            OpenResultWin = new Command(async () =>
            {
                //ResultWin resultWin = new ResultWin();
                //resultWin.Show();
                //Signal();   
            });
            OpenAllTestWin = new Command(async () =>
            {
                //AllTestWin  allTestWin  = new AllTestWin ();
                //allTestWin.Show();
                //Signal();   
            });
            OpenUserStWin = new Command(async () =>
            {
                UserStWin userStWin = new UserStWin();
                userStWin.Show();
                Signal();
            });
            //DoubleClickCommand = new RelayCommand(DoubleClickExecute);
        }

        //private void DoubleClickExecute(object parameter)
        //{

        //    if (parameter is Test Test)
        //    {
        //        //TestWin goodWin = new TestWin(Test);
        //        //goodWin.Show();
        //        //Signal();
        //    }
        //}
        MainWindow mainWindow;
        internal void SetWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        internal void CloseWindow(MainWindow mainWindow)
        {
            this.mainWindow.Close();
        }
    }

}
