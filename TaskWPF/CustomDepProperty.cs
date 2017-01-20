using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskWPF
{
    class CustomDepProperty: DependencyObject
    {
        public static DependencyProperty RegEnableProperty;

            static CustomDepProperty()
        {
            RegEnableProperty = DependencyProperty.Register("Data", typeof(bool), typeof(CustomDepProperty));
        }


        public bool IsRegEnableProp
        {
            get { return (bool)GetValue(RegEnableProperty); }

            set { SetValue(RegEnableProperty, value); }
        }
    }
}
