#pragma warning disable 1591
namespace Testura.Code.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Examine if the object is a numeric type.
    /// </summary>
    /// <param name="obj">Object to examine.</param>
    /// <returns><c>true</c> if the object is numeric, otherwise <c>false</c>.</returns>
    public static bool IsNumeric(this object obj)
    {
        return obj is sbyte
               || obj is byte
               || obj is short
               || obj is ushort
               || obj is int
               || obj is uint
               || obj is long
               || obj is ulong
               || obj is float
               || obj is double
               || obj is decimal;
    }
}