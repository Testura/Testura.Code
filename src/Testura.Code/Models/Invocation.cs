using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Models;

/// <summary>
/// Represent an invocation and used to convert between statement and expression.
/// </summary>
public class Invocation
{
    private readonly InvocationExpressionSyntax _invocation;

    /// <summary>
    /// Initializes a new instance of the <see cref="Invocation"/> class.
    /// </summary>
    /// <param name="invocation">The generated invocation expression.</param>
    public Invocation(InvocationExpressionSyntax invocation)
    {
        _invocation = invocation;
    }

    /// <summary>
    /// Convert the invocation expression to a statement.
    /// </summary>
    /// <param name="await">If we should await expression statement</param>
    /// <returns>A statement.</returns>
    public ExpressionStatementSyntax AsStatement(bool await = false)
    {
        if (await)
        {
            return ExpressionStatement(AwaitExpression(_invocation));
        }

        return ExpressionStatement(_invocation);
    }

    /// <summary>
    /// Convert the invocation expression to an expression.
    /// </summary>
    /// <returns>An expression.</returns>
    public InvocationExpressionSyntax AsExpression()
    {
        return _invocation;
    }
}
