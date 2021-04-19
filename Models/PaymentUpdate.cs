using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Models
{
    public class PaymentUpdate
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
    }
}