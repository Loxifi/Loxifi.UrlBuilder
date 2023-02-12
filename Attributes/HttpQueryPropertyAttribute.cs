namespace Loxifi.Attributes
{
    /// <summary>
    /// Defines how an object property should be serialized to a query string value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class HttpQueryPropertyAttribute : Attribute
    {
        /// <summary>
        /// The name to use when serializing the property
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// If specified, the order is used to determine the order of serialization 
        /// in the query string
        /// </summary>
        public int Order { get; internal set; }

        /// <summary>
        /// Constructs a new instance of this attribute
        /// </summary>
        /// <param name="name"></param>
        /// <param name="order"></param>
        public HttpQueryPropertyAttribute(string name, int order = 0)
        {
            Order = order;
            Name = name;
        }
    }
}