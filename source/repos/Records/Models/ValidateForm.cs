using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class ValidateForm
    {
        public int DinnerId { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [RegularExpression(@"[A-Za-z\s]+",ErrorMessage ="Invalid Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "HostedBy Required")]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Invalid HostedBy")]
        public string HostedBy { get; set; }
        [Required(ErrorMessage = "Description Required")]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Invalid Description")]
        public string Description { get; set; }
        [Required(ErrorMessage ="EventDate required")]
        [dateValidate(ErrorMessage ="Eventdate is invalid")]
        public DateTime EventDate { get; set; }
        [Required(ErrorMessage = "ContactPhone  Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((\+\d{2})?(\-)?)(\d{10}|\d{3}\-\d{3}\-\d{4}$)", ErrorMessage = "Invalid PhoneNUmber")]
        public string ContactPhone { get; set; }
        [Required(ErrorMessage = "Address Required")]
        [RegularExpression(@"[A-Za-z\s\-0-9\,\.\#]+", ErrorMessage = "Invalid Address")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Country required")]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Invalid Country")]
        public string Country { get; set; }
        [Required(ErrorMessage ="Lattitude  Required")]
        [Range( -180,  180)]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Longtitude Required")]
        [Range(-90, 90)]
        public double Longtitude{ get; set; }
        public string n { get; set; }
        public string name { get; set; }

        public static implicit operator ValidateForm(Client v)
        {
            return new ValidateForm
            {
                DinnerId =Convert.ToInt32( v.DinnerID),
                Title = v.Title.TrimEnd(),
                HostedBy = v.HostedBy.TrimEnd(),
            Description = v.Description.TrimEnd(),
            EventDate =Convert.ToDateTime( v.EventDate),
            ContactPhone = v.ContactPhone.TrimEnd(),
            Address = v.Address.TrimEnd(),
            Country = v.Country.TrimEnd(),
            Latitude = Convert.ToDouble(v.Latitude),
            Longtitude =Convert.ToDouble( v.Longtitude)
        };
        }

        

    }
    public class dateValidate : ValidationAttribute

    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            DateTime CurrentDate = DateTime.Now;

            string Message = string.Empty;

            if (Convert.ToDateTime(value) < CurrentDate)

            {

                Message = "Dinner Date cannot be less than current date";

                return new ValidationResult(Message);

            }

            return ValidationResult.Success;

        }
    }
   
}