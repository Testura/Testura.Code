namespace Testura.Code.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Check if the object is a numeric type.
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if the object is numeric, otherwise false.</returns>
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
}
