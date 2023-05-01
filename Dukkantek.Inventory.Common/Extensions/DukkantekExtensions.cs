using System;
using System.ComponentModel;
using System.Reflection;

namespace Dukkantek.Inventory.Common.Extensions
{
    public static class DukkantekExtensions
    {
        public static string Slugify(this string str)
        {
            return str.ToLower().Replace(" ", "-").Replace(".", "").Replace("/", "-").Replace("\\", "-").Replace("@", "-").Replace("!", "").Replace(",", "").Replace("~", "").Replace("`", "").Replace("'", "").Replace("\"", "").Replace("#", "").Replace("&", "") + Guid.NewGuid().ToString().Substring(0, 12);
        }

        public static string GenerateRef(this string str)
        {
            return $"{str}{Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10)}";
        }

        public static string GenerateReferralCode(this string str)
        {
            return $"{str}{Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 7)}";
        }

        public static string ToYesNo(this bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return null;
            }
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }

        public static int ToAge(this DateTime date)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - date.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (date.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}