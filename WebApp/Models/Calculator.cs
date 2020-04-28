using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Calculator
    {
        [Display(Name = "Executed number")]
        [Required]
        [Range(-100, 100, ErrorMessage = "Can only be between -100 .. 100")]
        //[RegularExpression("([1-9][0-4]*)", ErrorMessage = "Count must be a natural number")]
        public int Factor { get; set; }
        [Display(Name = "Result")]
        [Range(-0, 0, ErrorMessage = "Can only be between -100 .. 100")]
        public string Result { get; set; }
    }
}