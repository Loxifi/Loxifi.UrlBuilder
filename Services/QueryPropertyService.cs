using Loxifi.Attributes;
using Loxifi.Extensions;
using System.Reflection;

namespace Loxifi.Services
{
    internal static class QueryPropertyService
    {
        /// <summary>
        /// Gets all properties to be included in a query, in order
        /// </summary>
        /// <param name="o">The object to get the properties for</param>
        /// <param name="settings">The query serialization settings</param>
        /// <returns>The ordered properties</returns>
        public static IEnumerable<QueryProperty> GetQueryProperties(this object o, HttpQueryPropertySerializationSettings? settings = null)
        {
            settings ??= new HttpQueryPropertySerializationSettings();

            List<QueryProperty> Properties = new();

            foreach (PropertyInfo pi in GetQueryPropertyInfos(o))
            {
                object propVal = pi.GetValue(o);

                if (!settings.IncludeValue(propVal))
                {
                    continue;
                }

                string name = o is DynamicHttpQuery d ? pi.GetQueryPropertyName(d) : pi.GetQueryPropertyName();

                QueryProperty qpi = new(name)
                {
                    Order = pi.GetQueryPropertyOrder(),
                    Value = propVal?.ToString()
                };

                Properties.Add(qpi);
            }

            if (o is DynamicHttpQuery e)
            {
                foreach (KeyValuePair<string, object> kvp in e.AdditionalProperties)
                {
                    if (!settings.IncludeValue(kvp.Value))
                    {
                        continue;
                    }

                    QueryProperty qpi = new(kvp.Key)
                    {
                        Order = 0,
                        Value = kvp.Value?.ToString()
                    };

                    Properties.Add(qpi);
                }
            }

            List<string> formParts = new();

            foreach (QueryProperty qp in Properties.OrderBy(q => q.Order))
            {
                yield return qp;
            }
        }

        /// <summary>
        /// Gets the property infos for properties to be serialized as an HttpQuery
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetQueryPropertyInfos(this object o)
        {
            if (o is null)
            {
                throw new System.ArgumentNullException(nameof(o));
            }

            foreach (PropertyInfo pi in o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (pi.GetGetMethod() is null)
                {
                    continue;
                }

                if (pi.GetCustomAttribute<HttpQueryPropertyIgnoreAttribute>() != null)
                {
                    continue;
                }

                yield return pi;
            }

            foreach (PropertyInfo pi in o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                if (pi.GetGetMethod(true) is null)
                {
                    continue;
                }

                if (pi.GetCustomAttribute<HttpQueryPropertyAttribute>() != null)
                {
                    yield return pi;
                }
            }
        }

        /// <summary>
        /// Converts an object to its URI/POST implementation
        /// </summary>
        /// <param name="o"></param>
        /// <param name="settings">The query serialization settings</param>
        /// <returns></returns>
        public static string ToString(this object o, HttpQueryPropertySerializationSettings? settings = null) => o.GetQueryProperties(settings).BuildQueryString();
    }
}