using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class UserStWinVM : BaseVM
    {
        private Student student { get; set; }
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                Signal(nameof(Student));
            }
        }
        public Command OpenResultForOneStWin { get; }
        public Command OpenAllTestForOneStWin { get; }
        public Command Back { get; }
        public UserStWinVM()
        {
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
            Back = new Command(async () =>
            { 
                
            });

            //сюда гетстудент


        }

        
    }
}
