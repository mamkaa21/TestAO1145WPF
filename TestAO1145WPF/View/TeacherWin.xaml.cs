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
using TestAO1145WPF.ViewModel;

namespace TestAO1145WPF.View
{
    /// <summary>
    /// Логика взаимодействия для TeacherWin.xaml
    /// </summary>
    public partial class TeacherWin : Window
    {
        public TeacherWin(Model.Teacher? teacher)
        {
            InitializeComponent();
            (DataContext as TeacherWinVM).SetWindow(this);
            (DataContext as TeacherWinVM).SetTeacher(teacher);
        }
    }
}
