using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestAO1145WPF.View;
using TestAO1145WPF.Model;

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
        public Command OpenResultWin { get; }
        public Command OpenAllTestWin { get; }
        public Command OpenUserTWin { get; }
        public Command OpenNewTestWin { get; }
        public ICommand DoubleClickCommand { get; private set; }

        public TeacherWinVM()
        {
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

            //сюда тоже геталлтест
        }

        private void DoubleClickExecute(object parameter)
        {

            if (parameter is Test Test)
            {
                //TestWin goodWin = new TestWin(Test);
                //goodWin.Show();
                //Signal();
            }
        }
        TeacherWin teacherWin;
        internal void SetWindow(TeacherWin teacherWin)
        {
            this.teacherWin = teacherWin;
        }
        internal void CloseWindow(TeacherWin teacherWin)
        {
            this.teacherWin.Close();
        }

    }
}
