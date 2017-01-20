using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskWPF.Attributes
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
   public class InvalidCharsAttribute : ValidationAttribute
    {
        private string input;


        public InvalidCharsAttribute (string _input)
        {
            this.input = _input;

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string testingString = value.ToString();

            if (testingString != null)
            {
                for (int i = 0; i < testingString.Length; i++)
                {
                    if (input.Contains(testingString[i]))
                    {
                        
                            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                            return new ValidationResult(errorMessage);
                       
                    }
               
                }   
            }

            return ValidationResult.Success;


        }

        public string Input
        {
             get { return input; }
        }


    }
}
