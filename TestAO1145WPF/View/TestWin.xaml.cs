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
    /// Логика взаимодействия для TestWin.xaml
    /// </summary>
    public partial class TestWin : Window
    {
        public TestWin(Model.Test? test)
        {
            InitializeComponent();
            (DataContext as TestWinVM).SetWindow(this);
            (DataContext as TestWinVM).SetTest(test);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Answer a in e.AddedItems)
                a.IsChecked = true;
            foreach (Answer a in e.RemovedItems)
                a.IsChecked = false;
        }
    }
}
