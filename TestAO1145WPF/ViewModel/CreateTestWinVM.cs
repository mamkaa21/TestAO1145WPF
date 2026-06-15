using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;

namespace TestAO1145WPF.ViewModel
{
    class CreateTestWinVM : BaseVM
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
    }

}
