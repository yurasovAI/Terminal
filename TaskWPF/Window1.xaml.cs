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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskWPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        PersonModel Tom;

        public Window1()
        {
            InitializeComponent();
            //Me me = new Me { Num = 12 };
            Tom = new PersonModel { Age = 10, Name = "f"};
            this.DataContext = Tom;
            
        }

        private void bsrScroll_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("EEE");
        }
    }


    public class PersonModel : IDataErrorInfo
    {
        RegularExpressionAttribute reg = new RegularExpressionAttribute(@"[a-z]");
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if ((Age < 0) || (Age > 100))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        if (!reg.IsValid(Name))
                        {
                            error = "UUUUUUU";
                        }
                        //Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }


    //public class Me : IDataErrorInfo
    //    {
    //        int n;

    //        public string this[string columnName]
    //        {
    //            get
    //            {
    //                string error = String.Empty;
    //                switch (columnName)
    //                {
    //                    case "Num":
    //                        {
    //                            if (Num < 10 || Num > 20)
    //                            {
    //                                error = "EEEEEE";
    //                            }

    //                            break;
    //                        }

    //                }
    //                return error;
    //            }
    //        }

    //        public string Error
    //        {
    //            get
    //            {
    //                throw new NotImplementedException();
    //            }
    //        }

    //        public int Num
    //        {
    //            set
    //            {
    //                n = value;
    //            }
    //            get
    //            {
    //                return n;
    //            }
    //        }
    //    }
    
}
