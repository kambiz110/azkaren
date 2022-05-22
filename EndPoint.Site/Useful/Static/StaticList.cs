using EndPoint.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Useful.Static
{
    public static class StaticList
    {
        public static List<KeyValuePair<string, int>> listeDarajeh = new List<KeyValuePair<string, int>>() {
                new KeyValuePair<string, int>("گروهبان سوم", 5),
                new KeyValuePair<string, int>("گروهبان دوم", 6),
                new KeyValuePair<string, int>("گروهبان یکم", 7),
                new KeyValuePair<string, int>("استوار دوم", 8),
                new KeyValuePair<string, int>("استوار یکم", 9),
                new KeyValuePair<string, int>("ستون سوم", 10),
                new KeyValuePair<string, int>("ستوان دوم", 11),
                new KeyValuePair<string, int>("ستوان یکم", 12),
                new KeyValuePair<string, int>("سروان", 13),
                new KeyValuePair<string, int>("سرگرد", 14),
                new KeyValuePair<string, int>("سرهنگ دوم", 15),
                new KeyValuePair<string, int>("سرهنگ", 16),
                new KeyValuePair<string, int>("سرتیپ دوم", 17),
                new KeyValuePair<string, int>("سرتیپ", 18),
                new KeyValuePair<string, int>("روحانی", 20)

            };
        public static Listoption listObjDarajeh()
        {
            Listoption result = new Listoption();
            result.lstoption.Add(new option("گروهبان سوم", 5));
            result.lstoption.Add(new option("گروهبان دوم", 6));
            result.lstoption.Add(new option("گروهبان یکم", 7));
            result.lstoption.Add(new option("استوار دوم", 8));
            result.lstoption.Add(new option("استوار یکم", 9));
            result.lstoption.Add(new option("ستون سوم", 10));
            result.lstoption.Add(new option("ستوان دوم", 11));
            result.lstoption.Add(new option("ستوان یکم", 12));
            result.lstoption.Add(new option("سروان", 13));
            result.lstoption.Add(new option("سرگرد", 14));
            result.lstoption.Add(new option("سرهنگ دوم", 15));
            result.lstoption.Add(new option("سرهنگ", 16));
            result.lstoption.Add(new option("سرتیپ دوم", 17));
            result.lstoption.Add(new option("سرتیپ", 18));
            result.lstoption.Add(new option("روحانی", 20));
            return result;
        }
        public static Listoption listObjRotbeh() {
            Listoption result = new Listoption();
            result.lstoption.Add(new option("کارمند رتبه 5", 5));
            result.lstoption.Add(new option("کارمند رتبه 6", 6));
            result.lstoption.Add(new option("کارمند رتبه 7", 7));
            result.lstoption.Add(new option("کارمند رتبه 8", 8));
            result.lstoption.Add(new option("کارمند رتبه 9", 9));
            result.lstoption.Add(new option("کارمند رتبه 10", 10));
            result.lstoption.Add(new option("کارمند رتبه 11", 11));
            result.lstoption.Add(new option("کارمند رتبه 12", 12));
            result.lstoption.Add(new option("کارمند رتبه 13", 13));
            result.lstoption.Add(new option("کارمند رتبه 14", 14));
            result.lstoption.Add(new option("کارمند رتبه 15", 15));
            result.lstoption.Add(new option ("کارمند رتبه 16", 16));
            result.lstoption.Add(new option("کارمند رتبه 17", 17));
            result.lstoption.Add(new option("کارمند رتبه 18", 18));
            return result;
            }
        public static Listoption listObjRotbehRoohani()
        {
            Listoption result = new Listoption();
            result.lstoption.Add(new option("روحانی رتبه 5", 5));
            result.lstoption.Add(new option("روحانی رتبه 6", 6));
            result.lstoption.Add(new option("روحانی رتبه 7", 7));
            result.lstoption.Add(new option("روحانی رتبه 8", 8));
            result.lstoption.Add(new option("روحانی رتبه 9", 9));
            result.lstoption.Add(new option("روحانی رتبه 10", 10));
            result.lstoption.Add(new option("روحانی رتبه 11", 11));
            result.lstoption.Add(new option("روحانی رتبه 12", 12));
            result.lstoption.Add(new option("روحانی رتبه 13", 13));
            result.lstoption.Add(new option("روحانی رتبه 14", 14));
            result.lstoption.Add(new option("روحانی رتبه 15", 15));
            result.lstoption.Add(new option("روحانی رتبه 16", 16));
            result.lstoption.Add(new option("روحانی رتبه 17", 17));
            result.lstoption.Add(new option("روحانی رتبه 18", 18));
            result.lstoption.Add(new option("روحانی رتبه 19", 19));
            return result;
        }
        public static List<KeyValuePair<string, int>> listTypeDarajeh = new List<KeyValuePair<string, int>>() {
                new KeyValuePair<string, int>("نظامی", 1),
                new KeyValuePair<string, int>("روحانی", 2),
                new KeyValuePair<string, int>("کارمند", 0)
        };
      
    }
}
