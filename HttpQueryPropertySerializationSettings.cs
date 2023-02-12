using Loxifi.Services;

namespace Loxifi
{
    /// <summary>
    /// Serialization settings for string serialization of Http Query objects
    /// </summary>
    public class HttpQueryPropertySerializationSettings
    {
        /// <summary>
        /// Should default values be included in the string output
        /// </summary>
        public bool SerializeDefaultValues { get; set; }

        internal bool IncludeValue(object? val) => SerializeDefaultValues || !ObjectService.IsDefault(val);
    }
}