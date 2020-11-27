using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSearcher.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int LetterCount(this String str)
        {
            return str.Count(char.IsLetter);
        }

        public static string LetterDistribution(this String str, int take = 7)
        {
            var list = str.ToLower().GroupBy(c => c).Where(c => Char.IsLetter(c.Key)).Select(c => new { Char = c.Key, Count = c.Count() }).OrderByDescending(c => c.Count).Take(take);

            string rez = "";

            foreach (var item in list)
            {
                rez += $"{item.Char} - {item.Count}, ";
            }

            return rez.Substring(0, rez.Length - 2);
        }
}
}
