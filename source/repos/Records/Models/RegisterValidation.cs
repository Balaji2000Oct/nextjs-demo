using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class RegisterValidation
    {
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+",ErrorMessage ="Invalid Name")]
        [RegisterNameValidate]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterNameValidate : ValidationAttribute

    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {
            string Message = string.Empty;
            UserModel u = new UserModel();
            var data = u.FindAllUsers();
            // bool Istrue = true;
            foreach (var item in data)
            {
                if (value.ToString().TrimEnd().Equals(item.Name.TrimEnd()))
                {
                    return ValidationResult.Success;
                   
                    //Istrue = false;

                }
            }


            Message = "User Name Does Not Exists";

            return new ValidationResult(Message);


        }
    }
}