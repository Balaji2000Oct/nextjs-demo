using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class UserValidation
    {
        public int Id { get; set; }
        [Required]
        //[UserNameValidate(ErrorMessage ="User Name Already Taken")]
        [RegularExpression(@"^[A-Za-z\s]+$",ErrorMessage ="Invalid UserName")]
        [UserNameValidate(ErrorMessage ="UserName Already Exits")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        public string rPassword { get; set; }
       
        public static implicit operator UserValidation(User user)
        {
            return new UserValidation
            {
                Id = user.Id,
                Name = user.Name.TrimEnd(),
                Email = user.EmailId.TrimEnd(),
                Password = user.Password.TrimEnd(),
                rPassword=null,
                

            };
        }
    }
   
    public class UserNameValidate : ValidationAttribute

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
                    Message = "User Name Already Exists";

                    return new ValidationResult(Message);
                    //Istrue = false;
                   
                }
            }
           
            
           

                return ValidationResult.Success;
            

        }
    }
   
}