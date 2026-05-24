using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;

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

        public UserStWinVM()
        {

        }
    }
}
