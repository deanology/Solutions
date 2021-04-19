using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Payment Type")]
        [Display(Name = "Payment Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Enter Payment Type Description")]
        [Display(Name = "Payment Description")]
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}