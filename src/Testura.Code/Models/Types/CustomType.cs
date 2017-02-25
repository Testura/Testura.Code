using System;

namespace Testura.Code.Models.Types
{
    public static class CustomType
    {
        public static Type Create(string typeName)
        {
            return new CustomTypeProxy(typeName);
        }
    }
}
