using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

public class AssignArgument : IArgument
{
    private readonly string _name;
    private readonly ExpressionSyntax _expressionSyntax;
    private readonly object _value;

    public AssignArgument(string name, ExpressionSyntax expressionSyntax)
    {
        _name = name;
        _expressionSyntax = expressionSyntax;
    }

    public AssignArgument(string name, object value)
    {
        _name = name;
        _value = value;
    }

    public AssignArgument(string name, string value, StringType stringType = StringType.Normal)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        _name = name;
        _value = stringType == StringType.Path ? $"@\"{value}\"" : $"\"{value}\"";
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
