using Azmoon.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Useful.Ultimite
{
    public static class MeliCode
    {
        public static ResultDto NationalCodeValidation(this string melli)
        {
            var inputUser = melli; 
            var message = "اعتبار سنجی موفق";
            bool isSucces = true;
            var result = new ResultDto {
                IsSuccess=isSucces,
            Message=message
            };
         
            try
            {
                char[] chArray = inputUser.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (inputUser)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                        message="کد ملی وارد شده صحیح نمی باشد";
                        isSucces = false;
                        break;
                }
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {
                    message="کد ملی صحیح می باشد";
                }
                else
                {
                    message ="کد ملی نامعتبر است";
                    isSucces = false;
                }
            }
            catch (Exception)
            {
                message = "لطفا یک عدد 10 رقمی وارد کنید";
                isSucces = false;
            }

            return result;
        }
    }
}
