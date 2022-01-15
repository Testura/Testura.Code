using Testura.Code.Generators.Common;

namespace Testura.Code.Util.TypeNameFormatting;

internal static class GenericTypeNameFormatting
{
    /// <summary>
    /// Get the generic type in a correct string format
    /// </summary>
    /// <param name="type">Generic type to format</param>
    /// <returns>The formatted type name</returns>
    internal static string FormatType(Type type)
    {
        return GenericGenerator.ConvertGenericTypeName(type);
    }

    /// <summary>
    /// Get the generic type as a name that follow normal conventions
    /// </summary>
    /// <param name="type">Generic type to format</param>
    /// <returns>The formatted name</returns>
    internal static string FormatName(Type type)
    {
        var index = type.Name.IndexOf("`", StringComparison.Ordinal);
        return index == -1 ? type.Name : type.Name[..index];
    }
}
