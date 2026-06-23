using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class UserTWinVM : BaseVM
    {
        private Teacher teacher { get; set; }
        public Teacher Teacher
        {
            get => teacher;
            set
            {
                teacher = value;
                Signal(nameof(Teacher));
            }
        }

        private Class clas {  get; set; }
        public Class Clas
        {
            get => clas;
            set
                { clas = value;
            Signal(nameof(Clas));
            }
        }

        private List<Class> classlist { get; set; }
        public List<Class> ClassList
        {
            get => classlist;
            set
            {
                classlist = value;
                Signal(nameof(ClassList));
            }
        }
        public Command OpenResultWin { get; }
        public Command OpenAllTestWin { get; }
        public Command Back { get; }

        public UserTWinVM()
        {
           
            OpenAllTestWin = new Command(async () =>
            {
                AllTestWin allTestWin = new AllTestWin();
                allTestWin.Show();
                Signal();
            });                  
            Back = new Command(async () =>
            {

            });
            //сюда метод геттеаче
        }
    }
}
