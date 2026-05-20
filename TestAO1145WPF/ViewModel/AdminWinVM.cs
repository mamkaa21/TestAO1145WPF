using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class AdminWinVM : BaseVM
    {

        AdminWin adminWin;
        public Command OpenAddNewTeacher { get; }
        public Command OpenEditTeacher { get; }
        public Command OpenAddNewStudent { get; }
        public Command OpenEditStudent { get; }
        public Command OpenAddNewSubject { get; }
        public Command OpenEditSubject { get; }

        JsonSerializerOptions options = new JsonSerializerOptions();

        internal void SetWindow(AdminWin adminWin)
        {
            this.adminWin = adminWin;
        }
    }
}
