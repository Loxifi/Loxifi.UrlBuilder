namespace Loxifi.Attributes
{
    /// <summary>
    /// Do not serialize this property as part of an http URI or POST
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class HttpQueryPropertyIgnoreAttribute : Attribute
    {
    }
}