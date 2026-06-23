using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Subject subject { get; set; }
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;
                Signal(nameof(Subject));
            }
        }
        private List<Subject> subjectList { get; set; }
        public List<Subject> SubjectList
        {
            get => subjectList;
            set
            {
                subjectList = value;
                Signal(nameof(SubjectList));
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
           
        }
      
    }
}
