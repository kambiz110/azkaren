using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Azmoon.Common.Useful
{
   public class MobliPhon : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                string mobli = value.ToString();

                if (IsValidPhone(mobli))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("لطفا شماره موبایل را به درستی وارد کنید");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " اجباری است");
            }
        }
        public bool IsValidPhone(string mobli)
        {

            try
            {
                if (string.IsNullOrEmpty(mobli))
                    return false;
                var r = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
                return r.IsMatch(mobli);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}