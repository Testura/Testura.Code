using System.Reflection;
using Testura.Code.Models;
#pragma warning disable 1591

namespace Testura.Code.Extensions.Reflection;

public static class ParameterInfoExtensions
{
    /// <summary>
    /// Convert paramter info to Testura parameter object.
    /// </summary>
    /// <param name="parameterInfo">Paramter info object to convert.</param>
    /// <returns>The new parameter object.</returns>
    public static Parameter ToParameter(this ParameterInfo parameterInfo)
    {
        return new Parameter(parameterInfo.Name, parameterInfo.ParameterType);
    }
}