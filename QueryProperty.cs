namespace Loxifi
{
    /// <summary>
    /// Tangible object version of query property with order, used to build output string for posts and URI's
    /// </summary>
    internal class QueryProperty
    {
        public QueryProperty(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The display name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The index of the property in the output
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The value of the property
        /// </summary>
        public string? Value { get; set; }
    }
}