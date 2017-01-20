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



namespace TaskWPF
  
{
    /// <summary>
    /// Логика взаимодействия для PrivateCabinet.xaml
    /// </summary>
    public partial class PrivateCabinet : Page
    {
        NavigationService nav;
        JsonData jsonCollection;
        private string path = File.ReadAllText("UserBiblio.json");
        ClassUser[] users;



        public PrivateCabinet()
        {
            InitializeComponent();
            dataGrid.Loaded += DataGrid_Loaded;
            jsonCollection = JsonMapper.ToObject(path);
            users = new ClassUser[jsonCollection.Count];
            this.Loaded += PrivateCabinet_Loaded;

            // Установка аватарки, имени и баланса
            userName.Content = EnterTheCabinet.GetName();
            userBalance.Content = jsonCollection[EnterTheCabinet.GetNumber()]["BalanceProp"].ToString();
            ImageSet();

        }

        private void PrivateCabinet_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);
        }


        // Загрузка таблицы данных
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new ClassUser(Convert.ToInt32(jsonCollection[i]["IDProp"].ToString()), jsonCollection[i]["UsernameProp"].ToString(), jsonCollection[i]["PasswordProp"].ToString(), Convert.ToInt32(jsonCollection[i]["BalanceProp"].ToString()), jsonCollection[i]["PathProp"].ToString()); 
            }
            dataGrid.ItemsSource = users;
        }

        // Установка аватара
        private void ImageSet()
        {
           
            BitmapImage bitMap = new BitmapImage();
            bitMap.BeginInit();
            bitMap.UriSource = new Uri(jsonCollection[EnterTheCabinet.GetNumber()]["PathProp"].ToString(), UriKind.RelativeOrAbsolute);
            bitMap.CacheOption = BitmapCacheOption.OnLoad;
            bitMap.EndInit();
            userImage.Stretch = Stretch.Fill;
            userImage.Source = bitMap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new Uri("EnterTheCabinet.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
