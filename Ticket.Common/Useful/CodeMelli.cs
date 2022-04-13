using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Azmoon.Common.Useful
{

    public class CodeMelli : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                string meli = value.ToString();

                if (IsValidNationalCode(meli))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("لطفا کد ملی را بدرستی وارد کنید");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " اجباری است");
            }
        }
        public bool IsValidNationalCode(string nationalcode)
        {
            if (string.IsNullOrEmpty(nationalcode)) return false;
            if (!new Regex(@"\d{10}").IsMatch(nationalcode)) return false;
            var array = nationalcode.ToCharArray();
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalcode)) return false;
            var j = 10;
            var sum = 0;
            for (var i = 0; i < array.Length - 1; i++)
            {
                sum += Int32.Parse(array[i].ToString(CultureInfo.InvariantCulture)) * j;
                j--;
            }
            var div = sum / 11;
            var r = div * 11;
            var diff = Math.Abs(sum - r);
            if (diff <= 2)
            {
                return diff == Int32.Parse(array[9].ToString(CultureInfo.InvariantCulture));
            }
            var temp = Math.Abs(diff - 11);
            return temp == Int32.Parse(array[9].ToString(CultureInfo.InvariantCulture));
        }
    }
}
