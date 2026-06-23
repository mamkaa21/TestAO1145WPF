using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestAO1145WPF.Model;
using TestAO1145WPF.ViewModel;

namespace TestAO1145WPF.View
{
    /// <summary>
    /// Логика взаимодействия для AddTeacherWin.xaml
    /// </summary>
    public partial class AddTeacherWin : Window
    {
        public AddTeacherWin(object arg)
        {
            InitializeComponent();
            var vm = (DataContext as AddTeacherWinVM);

            vm.SetListSubjects(listSubjects);

            if (arg == null)
            {
                vm.Student = new Student();
                vm.Teacher = new Teacher();
                vm.Clas = new Class();
                vm.Subject = new Subject();
            }
            if (arg is Student)
            {
                vm.Student = (Student)arg;
                tabs.SelectedIndex = 1;
            }
            else if (arg is Teacher)
            {
                vm.Teacher = (Teacher)arg;
            }
            else if (arg is Class)
            {
                vm.Clas = (Class)arg;
                tabs.SelectedIndex = 3;
            }
            else if (arg is Subject)
            {
                vm.Subject = (Subject)arg;
                tabs.SelectedIndex = 2;
            }
            vm.UpdateVisible();
        }

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
