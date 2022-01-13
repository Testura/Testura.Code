using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

public class AssignArgument : IArgument
{
    private readonly string _name;
    private readonly ExpressionSyntax _expressionSyntax;

    public AssignArgument(string name, object value)
    {
        _name = name;
        _expressionSyntax = IdentifierName(value is bool ? value.ToString().ToLower() : value.ToString());
    }

    public AssignArgument(string name, string value, StringType stringType = StringType.Normal)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _name = name;
        _expressionSyntax = IdentifierName(stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"");
    }

    public AssignArgument(string name, ExpressionSyntax expressionSyntax)
    {
        _name = name;
        _expressionSyntax = expressionSyntax;
    }

    public ArgumentSyntax GetArgumentSyntax()
    {
        return Argument(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(_name),
                _expressionSyntax));
    }
}
