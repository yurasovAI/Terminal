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
//using System.Windows.Shapes;
using LitJson;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;




namespace TaskWPF
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Page
    {
        Random rand = new Random();
        JsonData jsonItem, jsonIdDelete;
        int idCapacity = 100000;
        ClassUser[] newUser, newUserNext, newUserAfterDel;
        private int massiveCapacity = 0;
        string jsonTextBox;
        // Переменная для осуществления запрета на создание невалидного пользователя
         public static bool validationTrigger = false;
        MD5 hash = MD5.Create();
        NavigationService nav;

        

        
        


        


        public UserWindow()
        {
            InitializeComponent();
            OriginInitiating();
            Loaded += UserWindow_Loaded;
            
            ClassUser.RegEnableEvent += PropertyChangedNotification_RegEnableEvent; 
            //Инициализация элементов страницы для осуществления валидации
            ClassUser initiateValid = new ClassUser(0, "", "", 0, "");
            balance.DataContext = initiateValid;
            username.DataContext = initiateValid;
            password.DataContext = initiateValid;


        }

        private void PropertyChangedNotification_RegEnableEvent(Func<string, bool> func, string error)
        {
            createUser.IsEnabled = func(error) ? true : false;
           
        }

        
        private bool AllowReg()
        {
            if (blockLogin.Text == string.Empty & blockPassword.Text == String.Empty & blockBalance.Text.ToString() == String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    

        private void UserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);
            createUser.IsEnabled = AllowReg();
        }


        // Начальная инициализация
        private void OriginInitiating()
        {

            // Подгружаем данные из JSON-файла в переменную типа JsonData.
            jsonTextBox = File.ReadAllText("UserBiblio.json");
            jsonItem = JsonMapper.ToObject(jsonTextBox);

            // Инициализируем переменную massiveCapacity, которая хранит количество данных в массиве.
            massiveCapacity = jsonItem.Count;

            // Создаем массив действующей величины.
            newUser = new ClassUser[massiveCapacity];

            // Подгружаем в массив данные из JSON-файла.
            for (int i = 0; i < jsonItem.Count; i++)
            {
                newUser[i] = new ClassUser(Convert.ToInt32(jsonItem[i]["IDProp"].ToString()), jsonItem[i]["UsernameProp"].ToString(), jsonItem[i]["PasswordProp"].ToString(), Convert.ToInt32(jsonItem[i]["BalanceProp"].ToString()), jsonItem[i]["PathProp"].ToString());
                
            }

        }


        // Добавление аватара
        private string GetNewImagePath()
        {
            OpenFileDialog openImage = new OpenFileDialog();

            openImage.Filter = "(.jpg)|*.jpg|(.png)|*.png)";

            if ((bool)openImage.ShowDialog())
            {

                return Path.GetFullPath(openImage.FileName);

            }
            else
            {
                return "7.jpg";
            }
        }


        // Проверка существования пользователя с таким именем
        private bool IsExistUserName(string isNameExist)
        {
            if (jsonItem == null)
            {
                jsonItem = JsonMapper.ToObject(jsonTextBox);
            }
            
            for (int i = 0; i < jsonItem.Count; i++)
            {
                if (jsonItem[i]["UsernameProp"].ToString() == UserWindow.GetMd5Hash(hash, isNameExist))
                {
                    return true;
                }
               
            }
            return false;
            
           

        }


        //Создание нового пользователя методом занесения данных в формате JSON  в виде массива
        private void createUser_Click(object sender, RoutedEventArgs e)
        {
            if (IsExistUserName(username.Text))
            {
                MessageBox.Show("The user with such username has been already existed!", "InvalidRegistration");
            }
            else
            {
                OriginInitiating();
                massiveCapacity++;
                newUserNext = new ClassUser[massiveCapacity];

                if (newUser != null)
                {
                    for (int i = 0; i < newUser.Length; i++)
                    {
                        newUserNext[i] = newUser[i];
                    }

                }
   
                    int randNumber = rand.Next(idCapacity);
                    while (IsExistId(randNumber))
                    {
                        randNumber = rand.Next(idCapacity);
                    }
                    
                    newUserNext[massiveCapacity - 1] = new ClassUser(randNumber, UserWindow.GetMd5Hash(hash, username.Text), UserWindow.GetMd5Hash(hash, password.Text), Int32.Parse(balance.Text), GetNewImagePath());
                    jsonItem = JsonMapper.ToJson(newUserNext);
                    File.WriteAllText("UserBiblio.json", jsonItem.ToString());
                    newUser = newUserNext;
                    MessageBoxResult result = MessageBox.Show("You have been successfully registrated!", "ValidRegistration", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        nav.Navigate(new Uri("EnterTheCabinet.xaml", UriKind.RelativeOrAbsolute));
                    }

            }


        }

        // Удаление пользователя по ID
        private void delete_Click(object sender, RoutedEventArgs e)
        {

            // Создание нового массива пользователей с количеством элементов на один меньше, чем в исходном
            newUserAfterDel = new ClassUser[newUser.Length - 1];
            
            int userNumber;
            int arrayValueOne;
            int arrayValueTwo;

            if (IsExistId(Int32.Parse(textId.Text), out userNumber))
            {
                for (int i = 0; i < newUserAfterDel.Length; i++)
                {
                    if (userNumber >= 0 & i != userNumber)
                    {
                        newUserAfterDel[i] = newUser[i];
                    }
                    else
                    {
                        if (i != newUser.Length - 1)
                        {
                            arrayValueOne = i;
                            arrayValueTwo = i;
                            arrayValueTwo++;

                            newUserAfterDel[arrayValueOne] = newUser[arrayValueTwo];
                        }
                    }
                }

                jsonIdDelete = JsonMapper.ToJson(newUserAfterDel);
                File.WriteAllText("UserBiblio.json", jsonIdDelete.ToString());
                MessageBox.Show("Пользователь был успешно удален!");
            }
            else
            {
                MessageBox.Show("Пользователя с таким ID не существует.", "Error");
            }
        }

        // Кнопка возврата на главную страницу
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new Uri("EnterTheCabinet.xaml", UriKind.RelativeOrAbsolute));
        }


        

    



        // Метод проверки существования пользоватея с таким id.
        private bool IsExistId(int id, out int userNumber)
        {
            string jsonContent = File.ReadAllText("UserBiblio.json");
            jsonIdDelete = JsonMapper.ToObject(jsonContent);

            for (int i = 0; i < jsonIdDelete.Count; i++)
            {
                if (jsonIdDelete[i]["IDProp"].ToString() == UserWindow.GetMd5Hash(hash, id.ToString()))
                {
                    userNumber = i;
                    return true;
                }

            }
            userNumber = -1;
            return false;
        }


        // Перегрузка метода IsExistId
        private bool IsExistId(int id)
        {
            string jsonContent = File.ReadAllText("UserBiblio.json");
            jsonIdDelete = JsonMapper.ToObject(jsonContent);

            for (int i = 0; i < jsonIdDelete.Count; i++)
            {
                if (jsonIdDelete[i]["IDProp"].ToString() == UserWindow.GetMd5Hash(hash, id.ToString()))
                {

                    return true;
                }
            }
            return false;
        }


        // Реализация получения хэш-кода для введенной строки
        static public string GetMd5Hash(MD5 hash, string source)
        {
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            // Создание из массива байтов с помощью StringBuilder хэш-значения для введенной строки
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                
            }

            return sBuilder.ToString();
        }

    }

    #region It is possibly useful code.
    // Модель пользователя
    //class ClassUser: IDataErrorInfo
    //{ 
    //    int id;
    //    string username;
    //    string password;
    //    int balance;
    //    string path;
    //    RegularExpressionAttribute regUsername = new RegularExpressionAttribute(@"[a-zA-Z]+");
    //    RegularExpressionAttribute regPassword = new RegularExpressionAttribute(@"[a-z0-9]+");

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public ClassUser(int id, string username, string password, int balance, string path)
    //    {
    //        IDProp = id;
    //        UsernameProp = username;
    //        PasswordProp = password;
    //        BalanceProp = balance;
    //        PathProp = path;
    //    }


    //    public int IDProp
    //    {
    //        set
    //        {
    //            id = value;
    //        }

    //        get
    //        {
    //            return id;
    //        }

    //    }

    //    [Required(ErrorMessage ="Это поле обязательно для заполнения!")]
    //    public string UsernameProp
    //    {
    //        set
    //        {           
    //            username = value;

    //        }

    //        get

    //        {
    //            return username;
    //        }
    //    }
    //    [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
    //    public string PasswordProp
    //    {
    //        set
    //        {
    //            password = value;
    //        }

    //        get
    //        {
    //            return password;
    //        }
    //    }

    //    public int BalanceProp
    //    {
    //        set
    //        {
    //            balance = value;

    //        }
    //        get
    //        {
    //            return balance;
    //        }
    //    }

    //    public string PathProp
    //    {
    //        set
    //        {
    //            path = value;
    //        }

    //        get

    //        {
    //            return path;
    //        }
    //    }


    //    #region  Реализация интерфейса  IDataErrorInfo
    //    public string Error
    //    {
    //        get
    //        {
    //            return null;

    //        }
    //    }

    //    // Переменная columnName содержит имя свойства, которое сгенерировало икслючение
    //    public string this[string columnName]
    //    {
    //        get
    //        {
    //            string error = String.Empty;
    //             switch (columnName)
    //            {
    //                case "BalanceProp":
    //                    {
    //                        if(BalanceProp<10 || BalanceProp>10000)
    //                        {
    //                            error = "Balance must be from 10 to 10000!";
    //                            if(UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = false;

    //                        }
    //                        else
    //                        {
    //                            if (!UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = true;

    //                        }
    //                        break;
    //                    }
    //                case "UsernameProp":
    //                    {
    //                        if(!regUsername.IsValid(UsernameProp))
    //                        {
    //                            error = "Username must be consist only from one letter!";
    //                            if (UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = false;
    //                        }
    //                        else
    //                        {
    //                            if (!UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = true;

    //                        }
    //                        break;
    //                    }

    //                case "PasswordProp":
    //                    {
    //                        if(!regPassword.IsValid(PasswordProp))
    //                        {
    //                            error = "Password is not valid!";
    //                            if (UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = false;
    //                        }
    //                        else
    //                        {
    //                            if (!UserWindow.validationTrigger)
    //                                UserWindow.validationTrigger = true;

    //                        }
    //                        break;
    //                    }
    //            }
    //            return error;
    //        }
    //    }
    //    #endregion
    //}
    #endregion
}
