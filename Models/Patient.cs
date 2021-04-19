using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Patient Firstname")]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Patient Lastname")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Patient Email Address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Enter Patient Phone Number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Patient Date of Birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public string GenerateID { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}