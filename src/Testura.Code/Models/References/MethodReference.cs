using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Models.References;

/// <summary>
/// Represent a method reference.
/// </summary>
public class MethodReference : MemberReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodReference"/> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="arguments">Arguments passed into the method.</param>
    /// <param name="genericTypes">Generic type of the method.</param>
    public MethodReference(
        string methodName,
        IEnumerable<IArgument> arguments = null,
        IEnumerable<Type> genericTypes = null)
        : this(methodName, null, arguments, genericTypes)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodReference"/> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="member">Member to call on the method.</param>
    /// <param name="arguments">Arguments passed into the method.</param>
    /// <param name="genericTypes">Generic type of the method.</param>
    public MethodReference(
        string methodName,
        MemberReference member,
        IEnumerable<IArgument>? arguments = null,
        IEnumerable<Type>? genericTypes = null)
        : base(methodName, member)
    {
        Arguments = arguments == null ? new List<IArgument>() : new List<IArgument>(arguments);
        GenericTypes = genericTypes == null ? new List<Type>() : new List<Type>(genericTypes);
    }

    /// <summary>
    /// Gets or sets the arguments sent into the method.
    /// </summary>
    public IList<IArgument> Arguments { get; set; }

    /// <summary>
    /// Gets or sets the generic types of the method.
    /// </summary>
    public IList<Type> GenericTypes { get; set; }
}
