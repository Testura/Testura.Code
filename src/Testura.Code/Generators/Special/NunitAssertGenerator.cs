using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;
using Testura.Code.Models.References;
using Testura.Code.Statements;

namespace Testura.Code.Generators.Special;

/// <summary>
/// Provides the functionality to generate Nunit asserts.
/// </summary>
public static class NunitAssertGenerator
{
    /// <summary>
    /// Create a NUnit AreEqual assert.
    /// </summary>
    /// <param name="expected">The expected argument.</param>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for <c>Assert.AreEqual</c>.</returns>
    public static ExpressionStatementSyntax AreEqual(IArgument expected, IArgument actual, string message = null)
    {
        return Are(AssertType.AreEqual, expected, actual, message);
    }

    /// <summary>
    /// Create a NUnit AreNotEqual assert.
    /// </summary>
    /// <param name="expected">The expected argument.</param>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for  <c>Assert.AreNotEqual</c>.</returns>
    public static ExpressionStatementSyntax AreNotEqual(IArgument expected, IArgument actual, string message = null)
    {
        return Are(AssertType.AreNotEqual, expected, actual, message);
    }

    /// <summary>
    /// Create a NUnit AreSame assert.
    /// </summary>
    /// <param name="expected">The expected argument.</param>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for <c>Assert.AreSame</c>.</returns>
    public static ExpressionStatementSyntax AreSame(IArgument expected, IArgument actual, string message)
    {
        return Are(AssertType.AreSame, expected, actual, message);
    }

    /// <summary>
    /// Create a NUnit AreNotSame assert.
    /// </summary>
    /// <param name="expected">The expected argument.</param>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for <c>Assert.AreNotSame</c>.</returns>
    public static ExpressionStatementSyntax AreNotSame(IArgument expected, IArgument actual, string message = null)
    {
        return Are(AssertType.AreNotSame, expected, actual, message);
    }

    /// <summary>
    /// Create a NUnit IsTrue assert.
    /// </summary>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for <c>Assert.IsTrue</c>.</returns>
    public static ExpressionStatementSyntax IsTrue(IArgument actual, string message = null)
    {
        return Is(true, actual, message);
    }

    /// <summary>
    /// Create a NUnit IsTrue assert.
    /// </summary>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax for Assert.IsFalse</returns>
    public static ExpressionStatementSyntax IsFalse(IArgument actual, string message = null)
    {
        return Is(false, actual, message);
    }

    /// <summary>
    /// Create an IsTrue assert that check if a string contains some content.
    /// </summary>
    /// <param name="expectedContain">Text that the string should contain.</param>
    /// <param name="actual">The actual argument.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax.</returns>
    public static ExpressionStatementSyntax Contains(IArgument expectedContain, IArgument actual, string message = null)
    {
        if (expectedContain == null)
        {
            throw new ArgumentNullException(nameof(expectedContain));
        }

        if (actual == null)
        {
            throw new ArgumentNullException(nameof(actual));
        }

        var arguments = new List<IArgument>
        {
            new InvocationArgument(Statement.Expression.Invoke(actual.GetArgumentSyntax().ToString(), "Contains", new List<IArgument> { expectedContain }).AsExpression()),
            new ValueArgument(message)
        };
        return Statement.Expression.Invoke("Assert", "IsTrue", arguments).AsStatement();
    }

    /// <summary>
    /// Create a Assert.Throws assert to check if a method throws and exception.
    /// </summary>
    /// <param name="variableReference">The reference chain to check for exception.</param>
    /// <param name="exception">The expected exception type.</param>
    /// <param name="message">Message if test fails.</param>
    /// <returns>The declared expression statement syntax.</returns>
    public static ExpressionStatementSyntax Throws(VariableReference variableReference, Type exception, string message = null)
    {
        if (variableReference == null)
        {
            throw new ArgumentNullException(nameof(variableReference));
        }

        if (exception == null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        if (variableReference is not MethodReference)
        {
            var member = variableReference.GetLastMember();
            if (member is not MethodReference)
            {
                throw new ArgumentException($"{variableReference} or last member in chain must be a method");
            }
        }

        var arguments = new List<IArgument>
        {
            new ParenthesizedLambdaArgument(Statement.Expression.Invoke(variableReference).AsExpression()),
            new ValueArgument(message ?? string.Empty)
        };
        return Statement.Expression.Invoke("Assert", "Throws", arguments, new List<Type> { exception }).AsStatement();
    }

    private static ExpressionStatementSyntax Are(AssertType assertType, IArgument expected, IArgument actual, string? message)
    {
        if (expected == null)
        {
            throw new ArgumentNullException(nameof(expected));
        }

        if (actual == null)
        {
            throw new ArgumentNullException(nameof(actual));
        }

        var arguments = new List<IArgument>
        {
            expected,
            actual,
            new ValueArgument(message ?? string.Empty)
        };
        return Statement.Expression.Invoke("Assert", Enum.GetName(typeof(AssertType), assertType), arguments).AsStatement();
    }

    private static ExpressionStatementSyntax Is(bool expected, IArgument actual, string? message)
    {
        if (actual == null)
        {
            throw new ArgumentNullException(nameof(actual));
        }

        var argument = new List<IArgument>
        {
            actual,
            new ValueArgument(message ?? string.Empty)
        };
        return Statement.Expression.Invoke("Assert", expected ? "IsTrue" : "IsFalse", argument).AsStatement();
    }
}
