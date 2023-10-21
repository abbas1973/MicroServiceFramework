﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class ReshapeDataExtension
    {
        #region GetUrlFriendly
        public static string GetUrlFriendly(this string word)
        {
            if (word == null) return null;
            return word.Trim().Replace("  ", " ")
                        .Replace(" ", "-")
                        .Replace("_", "-")
                        .Replace("%", "-")
                        .Replace(":", "-")
                        .Replace("?", "")
                        .Replace(";", "-")
                        .Replace("*", "-")
                        .Replace("=", "-")
                        .Replace("^", "-")
                        .Replace("#", "-")
                        .Replace("/", "-")
                        .Replace(".", "-")
                        .Replace("\"", "-")
                        .Replace("\'", "-")
                        .Replace("---", "-")
                        .Replace("--", "-");
        }
        #endregion



        #region تبدیل متن با html  به متن خالی
        /// <summary>
        /// تبدیل متن با html  به متن خالی
        /// </summary>
        /// <param name="HtmlInput">متن با html</param>
        /// <returns></returns>
        public static string StripHTML(this string HtmlInput)
        {
            return Regex.Replace(HtmlInput, "<.*?>", String.Empty);
        }
        #endregion



        #region آیا متن وارد شده انگلیسی است؟
        /// <summary>
        /// آیا متن وارد شده انگلیسی است؟
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglishText(this string input)
        {
            var regext = new Regex("^[a-zA-Z]*$");
            var isEnglish = regext.IsMatch(input.Substring(0, 1));
            return isEnglish;
        }
        #endregion



        #region تبدیل اعداد با کاراکتر های فارسی و عربی به کاراکتر های انگلیسی
        /// <summary>
        /// تبدیل اعداد با کاراکتر های فارسی و عربی به کاراکتر های انگلیسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToEnglishNumber(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return null;

            input = input.Trim();

            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            string[] arabic = new string[10] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            string[] english = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (int i = 0; i < 10; i++)
            {
                input = input.Replace(persian[i], english[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                input = input.Replace(arabic[i], english[i]);
            }
            return input;
        }
        #endregion




        #region تبدیل کاراکتر های فارسی و عربی به کاراکتر های فارسی
        /// <summary>
        /// تبدیل کاراکتر های فارسی و عربی به کاراکتر های فارسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToPersianCharacter(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return null;

            input = input.Trim();

            input = input.Replace('ي', 'ی');
            input = input.Replace('ك', 'ک');

            return input;
        }
        #endregion




        #region گرفتن خلاصه از متن با تعداد کاراکتر های دلخواه
        /// <summary>
        /// گرفتن خلاصه از متن با تعداد کاراکتر های دلخواه
        /// </summary>
        /// <returns></returns>
        public static string GetSummary(this string Text, int Count)
        {
            if (string.IsNullOrEmpty(Text)) return "";
            if (Text.Length >= Count)
                return Text.Substring(0, Count) + " [...]";
            else
                return Text;
        }
        #endregion



        #region تبدیل متن به camel case
        public static string ToCamelCase(this string text)
        {
            return JsonNamingPolicy.CamelCase.ConvertName(text);
        }
        #endregion



        #region تبدیل متن به pascal case
        public static string ToPascalCase(this string text)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(text);
        }
        #endregion



        #region جایگزین کردن آخرین تطابق متنی
        public static string ReplaceEnd(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);

            if (place == -1)
                return source;

            return source.Remove(place, find.Length).Insert(place, replace);
        }
        #endregion



        #region بی علامت کردن یک رشته
        /// <summary>
        /// بی علامت کردن یک رشته
        /// </summary>
        /// <returns></returns>
        public static string GetNotSign(this string word)
        {
            word = word.Replace(".", "");
            word = word.Replace("-", "");
            word = word.Replace("_", "");
            word = word.Replace(" ", "");
            word = word.Replace("/", "");
            word = word.Replace("\\", "");
            word = word.Replace(",", "");
            word = word.Replace(")", "");
            word = word.Replace("(", "");
            word = word.Replace("#", "");
            word = word.Replace("+", "");
            word = word.Replace("*", "");
            word = word.Replace("?", "");
            word = word.Replace("‎", "");
            word = word.Replace("‌", "");
            word = word.Replace(" ", "");
            return word;
        }
        #endregion
    }
}
