using Testura.Code.Extensions.Naming;

namespace Testura.Code.Util.TypeNameFormatting;

internal static class InterfaceTypeNameFormatting
{
    /// <summary>
    /// Get the interface type in a correct string format
    /// </summary>
    /// <param name="type">Generic type to format</param>
    /// <returns>The formatted type name</returns>
    internal static string FormatType(Type type)
    {
        var name = FormatName(type);
        return name.FirstLetterToUpperCase();
    }

    /// <summary>
    /// Get the interface type as a name that follow normal conventions
    /// </summary>
    /// <param name="type">Generic type to format</param>
    /// <returns>The formatted name</returns>
    internal static string FormatName(Type type)
    {
        var typeName = type.Name;
        return typeName.Remove(0, 1);
    }
}