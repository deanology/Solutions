using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Models.ViewModel
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Amount!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid Input")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Enter Remaining Balance!")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Invalid Input")]
        public double Balance { get; set; }
        [Required(ErrorMessage = "Enter Date payment was made")]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Enter Patient ID")]
        [Display(Name = "Patient ID")]
        public string PatientId { get; set; }
        [Required(ErrorMessage = "Please Select a Payment Type!")]
        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }
    }
}