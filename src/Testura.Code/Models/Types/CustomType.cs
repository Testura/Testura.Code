namespace Testura.Code.Models.Types
{
    public static class CustomType
    {
        public static CustomTypeProxy Create(string typeName)
        {
            return new CustomTypeProxy(typeName);
        }
    }
}
