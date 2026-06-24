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
    /// Логика взаимодействия для AllTestWin.xaml
    /// </summary>
    public partial class AllTestWin : Window
    {
        public AllTestWin()
        {
            InitializeComponent();
            (DataContext as AllTestWinVM).SetWindow(this);
        }
    }
}
