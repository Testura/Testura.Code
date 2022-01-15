namespace Testura.Code.Util.TypeNameFormatting;

internal static class ValueTypeNameFormatting
{
    /// <summary>
    /// Get the value type in a correct string format
    /// </summary>
    /// <param name="type">Generic type to format</param>
    /// <returns>The formatted type name</returns>
    internal static string FormatType(Type type)
    {
        switch (type.Name)
        {
            case "Int32":
                return "int";
            case "Double":
                return "double";
            case "Int64":
                return "long";
            case "UInt64":
                return "ulong";
            case "Single":
                return "float";
            case "Byte":
                return "byte";
            case "String":
                return "string";
            case "SByte":
                return "sbyte";
            case "UInt16":
                return "ushort";
            case "UInt32":
                return "uint";
            case "Boolean":
                return "bool";
            case "Char":
                return "char";
            case "Decimal":
                return "decimal";
            default:
                return type.Name;
        }
    }
}