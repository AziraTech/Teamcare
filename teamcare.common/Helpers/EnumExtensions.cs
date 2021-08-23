using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using teamcare.common.Models;

namespace teamcare.common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        /// <summary>
        /// Enumerates all enum values
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>IEnumerable containing all enum values</returns>
        /// <see cref="http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values"/>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<EnumListItem> GetEnumListItems<T>()
        {
            var values = GetValues<T>();
            return values.Select(i => new EnumListItem
            {
                Text = (i as Enum).GetEnumDescription(),
                Value = Convert.ToInt32(i as Enum)
            });
        }
    }
}
