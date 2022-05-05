using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Models
{
    public class PasswordInputViewModel
    {
        [Required (ErrorMessage ="ورود این فیلد اجباری است.")]
        [StringLength(
            ModelValidations.Password.PasswordMaxLength,
            ErrorMessage = ModelValidations.Error.RangeMessage,
            MinimumLength = ModelValidations.Password.PasswordMinLength)]
        public string Password { get; set; }

        public string Error { get; set; }


        [Required(ErrorMessage = "ورود این فیلد اجباری است.")]
        public long quizId { get; set; }
    }
}
