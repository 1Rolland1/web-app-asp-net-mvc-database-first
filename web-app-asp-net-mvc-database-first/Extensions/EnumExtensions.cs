using System;
using System.ComponentModel.DataAnnotations;

namespace web_app_asp_net_mvc_database_first.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
            {
                return value.ToString();
            }

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null)
            {
                return value.ToString();
            }

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }


    }
}