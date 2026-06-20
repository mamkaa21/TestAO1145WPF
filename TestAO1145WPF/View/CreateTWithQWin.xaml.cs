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
    /// Логика взаимодействия для CreateTWithQWin.xaml
    /// </summary>
    public partial class CreateTWithQWin : Window
    {
        public CreateTWithQWin(Model.Test test)
        {
            InitializeComponent();
            (DataContext as CreateTWithQWinVM).Test = test;
            (DataContext as CreateTWithQWinVM).SetControl(answersControl);
            (DataContext as CreateTWithQWinVM).SetWindow(this);
        }

     

        private void OneAnswer_Checked(object sender, RoutedEventArgs e)
        {
            (DataContext as CreateTWithQWinVM).OneAnswerChecked(((Control)sender).Tag as Answer);
        }
    }
}
