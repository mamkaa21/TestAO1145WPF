using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;

namespace TestAO1145WPF.ViewModel
{
    class RegisterWinVM : BaseVM
    {
        public Student Student { get; set; } = new();
        public Command EnterWinOpen { get; }
        public RegisterWinVM()
        {
            EnterWinOpen = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Student);
                var responce = await HttpClients.HttpClient.PostAsync($"Auth/AddNewUser", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Пароль или логин не может быть пустым");
                    return;
                }
                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    EnterWin enterWin = new EnterWin();
                    enterWin.Show();
                    Signal();
                }
            });
        }
        RegisterWin registerWindow;
        internal void SetWindow(RegisterWin registerWindow)
        {
            this.registerWindow = registerWindow;
        }
        internal void CloseWindow(RegisterWin registerWindow)
        {
            this.registerWindow.Close();
        }

    }
}
