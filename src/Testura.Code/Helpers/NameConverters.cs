using System;
using System.Linq;
using System.Text;

namespace Testura.Code.Helpers
{
    /// <summary>
    /// Help class for converting names
    /// </summary>
    public static class NameConverters
    {
        /// <summary>
        /// This method is used to convert generic type names (for example list&lt;string&gt; would normally give list`1String) to
        /// a more readable name.
        /// </summary>
        /// <param name="type">The type we want to convert name from</param>
        /// <returns>Converted name</returns>
        public static string ConvertGenericTypeName(Type type)
        {
            if (!type.IsGenericType)
                return type.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.Ordinal)));
            sb.Append(type.GetGenericArguments().Aggregate("<", (aggregate, genericType) => aggregate + (aggregate == "<" ? "" : ",") + ConvertGenericTypeName(genericType)));
            sb.Append(">");
            return sb.ToString();
        }
    }
}
