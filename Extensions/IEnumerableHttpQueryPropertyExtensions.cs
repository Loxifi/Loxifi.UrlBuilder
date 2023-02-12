namespace Loxifi.Extensions
{
    internal static class IEnumerableHttpQueryPropertyExtensions
    {
        public static string BuildQueryString(this IEnumerable<QueryProperty> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            List<string> formParts = new();

            foreach (QueryProperty qp in properties.OrderBy(p => p.Order))
            {
                formParts.Add($"{qp.Name.FormEncode()}={qp.Value?.FormEncode()}");
            }

            return string.Join("&", formParts);
        }

        /// <summary>
        /// Checks if a given query property exists in the collection
        /// </summary>
        /// <param name="source">The collection to search</param>
        /// <param name="key">The name of the property to search for</param>
        /// <returns>True if a property with the given name exists</returns>
        public static bool ContainsKey(this IEnumerable<QueryProperty> source, string key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (QueryProperty p in source)
            {
                if (p.Name == key)
                {
                    return true;
                }
            }

            return false;
        }
    }
}