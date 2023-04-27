using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.Arguments.ArgumentTypes;

namespace Testura.Code.Statements;

/// <summary>
/// Provides the functionality to generate exception handling statements.
/// </summary>
public class ExceptionHandlingStatement
{
    /// <summary>
    /// Create the statement syntax for a try-catch.
    /// </summary>
    /// <param name="tryBlock">The try block</param>
    /// <param name="catchBlock">The catch block</param>
    /// <param name="exceptionTypeToCatch">The exception type to catch</param>
    /// <param name="parameterName">The parameter name when catching</param>
    /// <returns>The created statement syntax</returns>
    public StatementSyntax TryCatch(
        BlockSyntax tryBlock,
        BlockSyntax catchBlock,
        Type exceptionTypeToCatch,
        string parameterName)
    {
        if (tryBlock == null)
        {
            throw new ArgumentNullException(nameof(tryBlock));
        }

        if(catchBlock == null)
        {
            throw new ArgumentNullException(nameof(catchBlock));
        }

        return
            SyntaxFactory.TryStatement(
                    SyntaxFactory.SingletonList(SyntaxFactory.CatchClause()
                        .WithDeclaration(SyntaxFactory.CatchDeclaration(
                            TypeGenerator.Create(exceptionTypeToCatch),
                            SyntaxFactory.Identifier(parameterName)))
                        .WithBlock(catchBlock)))
                .WithBlock(tryBlock);
    }

    /// <summary>
    /// Generate a throw statement.
    /// </summary>
    /// <returns>The generated throw statement</returns>
    public StatementSyntax Throw()
    {
        return SyntaxFactory.ThrowStatement();
    }

    /// <summary>
    /// Generate a throw statement with a new exception.
    /// </summary>
    /// <param name="type">The type of exception to initialize</param>
    /// <param name="arguments">The arguments when initializing the object</param>
    /// <returns>The generated throw statement</returns>
    public StatementSyntax ThrowNew(Type type, params IArgument[] arguments)
    {
        var objectCreation =
            SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName(type.Name))
                .WithArgumentList(ArgumentGenerator.Create(arguments));

        return SyntaxFactory.ThrowStatement(objectCreation);
    }

    /// <summary>
    ///  Generate a throw statement with a new exception.
    /// </summary>
    /// <param name="objectCreation">The object creation</param>
    /// <returns>The generated throw statement</returns>
    public StatementSyntax ThrowNew(ExpressionSyntax objectCreation)
    {
        return SyntaxFactory.ThrowStatement(objectCreation);
    }
}
