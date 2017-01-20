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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LitJson;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;


namespace TaskWPF
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class EnterTheCabinet : Page
    {
        NavigationService nav;

        private string jsonReading;
        private JsonData jsonItem;
        // Номер пользователя для передачи в личный кабинет
        private static int unumber;
        MD5 hash = MD5.Create();
        // Имя пользователя для передачи в личный кабинет
        private static string name;




        public EnterTheCabinet()
        {
            InitializeComponent();
            Loaded += Page1_Loaded;
            butReg.Click += ButReg_Click;

            PropertyChangedNotification.RegEnableEvent += PropertyChangedNotification_RegEnableEvent;
            ClassUser initiateValid = new ClassUser(0, "Antoha", "3560", 0, "");
            login.DataContext = initiateValid;
            password.DataContext = initiateValid;



        }

        private void PropertyChangedNotification_RegEnableEvent(Func<string, bool> func, string error)
        {


            butEnter.IsEnabled = func(error) ? true: false;
            
            
            
        }

        private bool AllowEnter()
        {
            if (txtblockLogin.Text == String.Empty & txtblockPassword.Text == String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Загрузка страницы
        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {

            jsonReading = File.ReadAllText("UserBiblio.json");

            jsonItem = JsonMapper.ToObject(jsonReading);

            nav = NavigationService.GetNavigationService(this);

        }
        // Кнопка регистрации
        private void ButReg_Click(object sender, RoutedEventArgs e)
        {
           
                nav.Navigate(new Uri("UserWindow.xaml", UriKind.RelativeOrAbsolute));
        }


        // Проверка, существует ли пользователь с такими именем и паролем
        private bool GetItem(string username, string password)
        {

            for (int i = 0; i < jsonItem.Count; i++)
            {

                if (jsonItem[i]["UsernameProp"].ToString() == UserWindow.GetMd5Hash(hash, username) & jsonItem[i]["PasswordProp"].ToString() == UserWindow.GetMd5Hash(hash, password))
                {
                    // Номер пользователя
                    unumber = i;
                    // Имя пользователя
                    name = username;
                    return true;
                }
            }
            
            return false;
        }

        // Кнопка входа в личный кабинет
        private void butEnter_Click_1(object sender, RoutedEventArgs e)
        {
            if (GetItem(login.Text, password.Text))
            {
                nav.Navigate(new Uri("PrivateCabinet.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show(@"You have failed the entrance. Если вы не зарегестрированы, нажмите ""Зарегестрироваться""! ", "Ошибка входа!");
            }
        }

        // Получение номера вошедшего пользователя
        public static int GetNumber()
        {
            return unumber;
            
        }

        public static string GetName()
        {
            return name;
        }

        
    }
}
