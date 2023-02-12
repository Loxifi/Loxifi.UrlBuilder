using Loxifi.Attributes;
using Loxifi.Services;
using System.Reflection;

namespace Loxifi
{
    /// <summary>
    /// An HttpQuery object that can have properties added to it at run time
    /// </summary>
    public class DynamicHttpQuery : HttpQuery
    {
        [HttpQueryPropertyIgnore]
        internal IReadOnlyDictionary<string, object> AdditionalProperties => _additionalProperties;

        [HttpQueryPropertyIgnore]
        private readonly Dictionary<string, object> _additionalProperties = new();

        [HttpQueryPropertyIgnore]
        internal IReadOnlyDictionary<string, string> RenamedProperties => _renamedProperties;

        [HttpQueryPropertyIgnore]
        private readonly Dictionary<string, string> _renamedProperties = new();

        /// <summary>
        /// Updates the internal mapping for a property to force
        /// a new property name on serialization
        /// </summary>
        /// <param name="oldPropertyName">The current defined property name</param>
        /// <param name="newPropertyName">The name that should be used when serializing</param>
        public void RenameProperty(string oldPropertyName, string newPropertyName)
        {
            if (_renamedProperties.ContainsKey(oldPropertyName))
            {
                _renamedProperties[oldPropertyName] = newPropertyName;
            }
            else
            {
                _renamedProperties.Add(oldPropertyName, newPropertyName);
            }
        }

        /// <summary>
        /// Adds a new dynamic property to the underlying data store
        /// </summary>
        /// <param name="key">The query property key</param>
        /// <param name="value">The query property value</param>
        public void AddProperty(string key, string value) => _additionalProperties.Add(key, value);

        /// <summary>
        /// Adds a new dynamic property to the underlying data store,
        /// or updates an existing property
        /// </summary>
        /// <param name="key">The query property key</param>
        /// <param name="value">The query property value</param>
        public void AddOrUpdateProperty(string key, object value)
        {
            foreach (PropertyInfo pi in QueryPropertyService.GetQueryPropertyInfos(this))
            {
                if (key == pi.Name)
                {
                    pi.SetValue(this, value);
                    return;
                }
            }

            if (_additionalProperties.ContainsKey(key))
            {
                _additionalProperties[key] = value;
            }
            else
            {
                _additionalProperties.Add(key, value);
            }
        }
    }
}