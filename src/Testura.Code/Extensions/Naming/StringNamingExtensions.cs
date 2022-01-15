#pragma warning disable 1591
namespace Testura.Code.Extensions.Naming;

public static class StringNamingExtensions
{
    /// <summary>
    /// Turn the first letter in a string to lower case.
    /// </summary>
    /// <param name="value">String to change.</param>
    /// <returns>A new string with the first letter in lower case.</returns>
    public static string FirstLetterToLowerCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return char.ToLowerInvariant(value[0]) + value.Substring(1);
    }

    /// <summary>
    /// Turn the first letter in a string to upper case
    /// </summary>
    /// <param name="value">String to change</param>
    /// <returns>A new string with the first letter in upper case</returns>
    public static string FirstLetterToUpperCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return char.ToUpperInvariant(value[0]) + value.Substring(1);
    }
}