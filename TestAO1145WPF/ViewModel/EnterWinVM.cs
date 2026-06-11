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
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
                //Signal();
                //enterWindow.Close();

                //возможно стоит сделать таблицу юзера и таблицу ролей и разделять учителей и студентов так я хз 
                Student.Password = enterWindow.passwordBox.Password;
                string arg = JsonSerializer.Serialize(Student);
                var responce = await HttpClients.HttpClient.PostAsync($"Auth/CheckAccountIsExist", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await responce.Content.ReadAsStringAsync();


                    MessageBox.Show("Вы ввели неверный логин или пароль");
                    return;
                }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await responce.Content.ReadFromJsonAsync<List<string>>();
                    var token = data[1];
                    var type = data[0];
                    HttpClients.SetToken(token);

                    //
                    //var d = await responce.Content.ReadFromJsonAsync<StModel>();
                    if (type == "1")
                    {
                        var studResponce = await HttpClients.HttpClient.GetAsync($"Student");
                        var student = await studResponce.Content.ReadFromJsonAsync<Student>();
                        MainWindow mainWindow = new MainWindow(student);
                        mainWindow.Show();
                    }
                    else if (type == "2")
                    {
                        TeacherWin teacherWin = new TeacherWin();
                        teacherWin.Show();
                    }
                    return;
                    //if (d.RoleId == 1) //сначала лезем к студентам ищем потом к учителям потом к админу
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
                }
                else
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка подключения");
                    return;
                }
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


