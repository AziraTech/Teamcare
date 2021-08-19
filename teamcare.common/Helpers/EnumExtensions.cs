using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace teamcare.common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescriptionFromEnumValue(this Enum value)
        {
            return !(value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() is DisplayAttribute attribute) ? value.ToString() : attribute.Description;
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
    }
}
