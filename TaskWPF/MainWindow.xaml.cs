using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;

namespace TaskWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Подписываем на событие закрытия окна метод обработки закрытия
            Closing += MainWindow_Closing;
            // Событие закрытия окна
            item.Click += Item_Click;
            // Скрыть рамку окна Windows
            //this.WindowStyle = WindowStyle.None;
            //  На весь экран
            this.WindowState = WindowState.Maximized;
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        // Метод обработки события закрытия формы
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to close the program?", "Information", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                //e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region Принцип создания, открытия и сохранения текстового файла
        // Метод обработки события нажатия на кнопку Create.txt
        private void button3_Click(object sender, RoutedEventArgs e)
        {

            FileStream stream = File.Create("Text.txt");
            stream.Close();
        }

        // Метод обработки события нажатия на копку Open.txt
        private void button4_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog of = new OpenFileDialog();

            of.Filter = "(*.txt)|*.txt";

            if ((bool)(of.ShowDialog()))

            {
                Stream st = of.OpenFile();


                if (st != null)
                {

                    StreamReader streamOpen = new StreamReader(st);

                    //textBoxForFile.Text = streamOpen.ReadToEnd();


                    streamOpen.Close();
                }
            }
        }

        // Метод обработки события нажатия на кнопку Save.txt
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            FileStream streamSave = File.Open("Text.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamWriter writerSave = new StreamWriter(streamSave);

            //writerSave.WriteLine(textBoxForFile.Text);

            writerSave.Close();
        }

        #endregion
    }
}
