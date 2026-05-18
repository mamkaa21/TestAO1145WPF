using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TestAO1145WPF.Model;
using TestAO1145WPF.View;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestAO1145WPF.ViewModel
{
    class EnterWinVM : BaseVM
    {
        public Student Student { get; set; } = new();
        public Command Enter { get; }
        public Command RegisterWinOpen { get; }
        public EnterWinVM()
        {
            Enter = new Command(async () =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Signal();
                enterWindow.Close();
                //string arg = JsonSerializer.Serialize(Student);
                //var responce = await HttpClients.HttpClient.PostAsync($"Auth/CheckAccountIsExist", new StringContent(arg, Encoding.UTF8, "application/json"));

                //if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{
                //    var result = await responce.Content.ReadAsStringAsync();


                //    MessageBox.Show("Вы ввели неверный логин или пароль");
                //    return;
                //}

                //if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    var token = await responce.Content.ReadAsStringAsync();
                //    HttpClients.SetToken(token);
                //    responce = await HttpClients.HttpClient.GetAsync($"Students");
                //    var d = await responce.Content.ReadFromJsonAsync<StModel>();
                //    MessageBox.Show("Оk");
                //    return;
                //if (d.RoleId == 1)
                //{
                //    //AdminWin adminWin = new AdminWin();
                //    //adminWin.Show();
                //    Signal();
                //    //enterWindow.Close();
                //}
                //else
                //{
                //    //UserMenu userMenu = new UserMenu();
                //    //userMenu.Show();
                //    Signal();
                //    //enterWindow.Close();
                //}
                //}
                //else
                //{
                //    var result = await responce.Content.ReadAsStringAsync();
                //    MessageBox.Show("Ошибка подключения");
                //    return;
                //}
            });
            //запрос для входа + проверка на роль юзера? если роль 2 - окно юзера открывается
            // если роль 1 - то окно админа

            RegisterWinOpen = new Command(async () =>
            {
                RegisterWin registerWin = new RegisterWin();
                registerWin.Show();
                Signal();
                enterWindow.Close();
            });
        }

       
        EnterWin enterWindow;
        internal void SetWindow(EnterWin enterWindow)
        {
            this.enterWindow = enterWindow;
        }
        internal void CloseWindow(EnterWin enterWindow)
        {
            this.enterWindow.Close();
        }
    }
}

    
