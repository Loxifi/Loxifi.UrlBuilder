using Loxifi.Services;

namespace Loxifi
{
    /// <summary>
    /// Base class for an object designed to represent an HttpQuery
    /// </summary>
    public abstract class HttpQuery
    {
        /// <inheritdoc/>
        public override string ToString() => QueryPropertyService.ToString(this);
    }
}