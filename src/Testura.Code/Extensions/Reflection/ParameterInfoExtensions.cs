using System.Reflection;
using Testura.Code.Models;

namespace Testura.Code.Extensions.Reflection
{
    public static class ParameterInfoExtensions
    {
        public static Parameter ToParameter(this ParameterInfo parameterInfo)
        {
            return new Parameter(parameterInfo.Name, parameterInfo.ParameterType);
        }
    }
}
