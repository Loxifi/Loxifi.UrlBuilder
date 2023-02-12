using Loxifi.Extensions;
using Loxifi.Services;
using System.Text;
using System.Web;

namespace Loxifi
{
    /// <summary>
    /// A class used to build/update uris for http posts
    /// </summary>
    public class UrlBuilder
    {
        private readonly QueryPropertyCollection _parameters = new();

        /// <summary>
        /// Any parameters included as a string
        /// </summary>
        public string QueryString => _parameters.BuildQueryString();

        /// <summary>
        /// The absolute path to use as the base for the url builder
        /// </summary>
        public string AbsolutePath { get; private set; }

        /// <summary>
        /// Constructs a new instance of this Url Builder
        /// </summary>
        /// <param name="root">The absolute path to use as the base for the url builder</param>
        public UrlBuilder(string root)
        {
            AbsolutePath = root;
        }

        /// <summary>
        /// Adds a single key value pair to the parameter list
        /// </summary>
        /// <param name="key">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <param name="order"></param>
        public void AddParameter(string key, string? value, int order = 0)
        {
            _parameters.Add(new QueryProperty(key)
            {
                Value = value,
                Order = order
            });
        }

        /// <summary>
        /// Adds an existing parameters string to the URI object
        /// </summary>
        /// <param name="queryString">The &amp; = delimited string to add to the object as parameters</param>
        public void AddParameters(string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return;
            }

            foreach (string kvp in queryString.TrimStart('?').Split('&'))
            {
                AddParameter(kvp.Split('=')[0], HttpUtility.UrlDecode(kvp.Split('=')[1]));
            }
        }

        /// <summary>
        /// Adds an object as a collection of query parameters
        /// </summary>
        /// <param name="definition">The object to add as a definition</param>
        /// <param name="settings">The optional serialization settings</param>
        public void AddParameters(object definition, HttpQueryPropertySerializationSettings? settings = null)
        {
            foreach (QueryProperty qpd in QueryPropertyService.GetQueryProperties(definition, settings))
            {
                _parameters.Add(qpd);
            }
        }

        /// <summary>
        /// Converts this object to a URI string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_parameters.Count == 0)
            {
                return AbsolutePath;
            }
            else
            {
                StringBuilder sb = new();

                _ = sb.Append(AbsolutePath);

                string Query = QueryString;

                if (!string.IsNullOrWhiteSpace(QueryString))
                {
                    _ = sb.Append('?');
                    _ = sb.Append(Query);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Adds or updates a uri parameter
        /// </summary>
        /// <param name="key">The parameter key</param>
        /// <param name="value">The parameter value</param>
        public void UpdateParameter(string key, string? value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key].Value = value;
            }
            else
            {
                AddParameter(key, value);
            }
        }

        /// <summary>
        /// Adds or updates Uri parameters based on object properties
        /// </summary>
        /// <param name="definition">The object to use as a property source</param>
        /// <param name="settings">Optional serialization settings</param>
        public void UpdateParameters(object definition, HttpQueryPropertySerializationSettings? settings = null)
        {
            foreach (QueryProperty qpd in QueryPropertyService.GetQueryProperties(definition, settings))
            {
                UpdateParameter(qpd.Name, qpd.Value);
            }
        }
    }
}