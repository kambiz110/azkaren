using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azmoon.Common.Useful
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
                new KeyValuePair<string, int>("سرتیپ", 18)
            };

        public static List<KeyValuePair<string, int>> listTypeDarajeh = new List<KeyValuePair<string, int>>() {
                new KeyValuePair<string, int>("نظامی", 1),
                new KeyValuePair<string, int>("روحانی", 2),
                new KeyValuePair<string, int>("کارمند", 0)
        };
    }
}
