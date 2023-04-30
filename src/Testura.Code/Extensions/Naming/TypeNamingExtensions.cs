using Testura.Code.Models.Types;
using Testura.Code.Util.TypeNameFormatting;
#pragma warning disable 1591

namespace Testura.Code.Extensions.Naming;

public static class TypeNamingExtensions
{
    /// <summary>
    /// Get the type name in a format that follow field naming conventions
    /// </summary>
    /// <param name="type">Type to create field name from</param>
    /// <returns>The formatted field name by removing I from interfaces, change first letter to lower, etc.</returns>
    public static string FormattedFieldName(this Type type)
    {
        var typeName = type.Name;

        if (type.IsGenericType)
        {
            typeName = GenericTypeNameFormatting.FormatName(type);
        }

        if (type.IsInterface)
        {
            typeName = InterfaceTypeNameFormatting.FormatName(type);
        }

        return typeName.FirstLetterToLowerCase();
    }

    /// <summary>
    /// Get the type name in a format that follow class naming conventions
    /// </summary>
    /// <param name="type">Type to create class name from</param>
    /// <returns>The formatted class name</returns>
    public static string FormattedClassName(this Type type)
    {
        var index = type.Name.IndexOf("`");
        if (index <= 0)
        {
            return type.Name;
        }

        return type.Name.Remove(index, type.Name.Length - index);
    }

    /// <summary>
    /// Get the type name in correct type type convention.
    /// </summary>
    /// <param name="type">Type to created formatted type name from.</param>
    /// <returns>The formatted type name.</returns>
    public static string FormattedTypeName(this Type type)
    {
        if(type is CustomTypeProxy customTypeProxy)
        {
            return customTypeProxy.TypeName;
        }

        var typeName = type.Name;

        if (type.IsGenericType)
        {
            return GenericTypeNameFormatting.FormatType(type);
        }

        if (type.IsValueType || typeName == "String")
        {
            return ValueTypeNameFormatting.FormatType(type);
        }

        return typeName;
    }
}
