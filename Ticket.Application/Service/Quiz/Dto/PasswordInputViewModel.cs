using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Quiz.Dto
{
    public class PasswordInputViewModel
    {
        [Required]
        [StringLength(
           16,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        public string Password { get; set; }

        public string Error { get; set; }
    }
}
