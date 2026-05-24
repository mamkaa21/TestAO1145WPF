using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;

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

        public UserTWinVM()
        {

        }
    }
}
