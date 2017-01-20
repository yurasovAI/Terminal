using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using TaskWPF.Attributes;


namespace TaskWPF
{
  public class ClassUser : PropertyChangedNotification
    {

      
        string a;

        public ClassUser(int id, string username, string password, int balance, string path)
        {
            IDProp = id;
            UsernameProp = username;
            PasswordProp = password;
            BalanceProp = balance;
            PathProp = path;
        }

        
        public int IDProp
        {
            set
            {
                SetValue(() => IDProp, value);
            }

            get
            {
                return GetValue(() => IDProp);
            }

        }


        [Required(ErrorMessage = "Login is Required!")]
        [MaxLength(15, ErrorMessage = "Login exceeded 15 letters!")]
        [MinLength(4, ErrorMessage = "Login should consist of at least 4 letters!")]
        [InvalidChars("#.&;№:*@/\\( )^,!?%${}[]", ErrorMessage = "Login should not contain such characters as '#.&;№:*@/\\( )^,!?%$[]' ")]
        //[EmailAddress(ErrorMessage = "It is invalid EmailAdress!")]
        public string UsernameProp
        {
            set
            {
                SetValue(() => UsernameProp, value);
                
            }

            get
            {

                return GetValue(() => UsernameProp);
            }
        }

        [Required(ErrorMessage = "Password is Required!")]
        [MaxLength(15, ErrorMessage = "Password exceeded 15 letters!")]
        [MinLength(4, ErrorMessage = "Password should consist of at least 4 letters!")]
        [InvalidChars(" ", ErrorMessage = "Password should not contain a space!")]
        public string PasswordProp
        {
            set
            {

                SetValue(() => PasswordProp, value);
            }

            get
            {
                
                return GetValue(() => PasswordProp);
            }
        }


        [Range(10, 1000, ErrorMessage = "Balance should not be less than 10 and more than 1000!")]
        public int BalanceProp
        {
            set
            {

                SetValue(() => BalanceProp, value);
            }

            get
            {
                
                return GetValue(() => BalanceProp);
            }
        }

        public string PathProp
        {
            set
            {
                SetValue(() => PathProp, value);
            }

            get
            {
                return GetValue(() => PathProp);
            }
        }


    }
}
