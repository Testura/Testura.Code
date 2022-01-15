namespace Testura.Code.Models.Types;

/// <summary>
/// Provides functionality to create a custom type
/// </summary>
public static class CustomType
{
    /// <summary>
    /// Create a custom type with a specific name.
    /// </summary>
    /// <param name="typeName">Name of the custom type.</param>
    /// <returns>Return a custom type.</returns>
    public static Type Create(string typeName)
    {
        return new CustomTypeProxy(typeName);
    }
}