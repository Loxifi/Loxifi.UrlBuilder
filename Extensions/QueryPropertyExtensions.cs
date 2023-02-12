using Loxifi.Attributes;
using System.Reflection;

namespace Loxifi.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static string GetQueryPropertyName(this PropertyInfo pi)
        {
            if (pi is null)
            {
                throw new ArgumentNullException(nameof(pi));
            }

            return pi.GetCustomAttribute<HttpQueryPropertyAttribute>() is HttpQueryPropertyAttribute qpi
                ? string.IsNullOrEmpty(qpi.Name)
                    ? throw new ArgumentException("Query propery can not have null or empty name: " + pi.Name)
                    : qpi.Name
                : pi.Name;
        }

        public static string GetQueryPropertyName(this PropertyInfo pi, DynamicHttpQuery queryObject)
        {
            if (pi is null)
            {
                throw new ArgumentNullException(nameof(pi));
            }

            if (queryObject is null)
            {
                throw new ArgumentNullException(nameof(queryObject));
            }

            string name = GetQueryPropertyName(pi);

            return queryObject.RenamedProperties.TryGetValue(name, out string rename) ? rename : name;
        }

        public static int GetQueryPropertyOrder(this PropertyInfo pi) => pi.GetCustomAttribute<HttpQueryPropertyAttribute>() is HttpQueryPropertyAttribute qpi ? qpi.Order : 0;
    }
}