using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#pragma warning disable 1591

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes;

/// <summary>
/// Provides the functionality to generate lambda arguments. Example of generated code: <c>(n=>MyMethod())</c>
/// </summary>
public class LambdaArgument : Argument
{
    private readonly ExpressionSyntax _expressionSyntax;
    private readonly string _parameterName;
    private readonly BlockSyntax _blockSyntax;

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaArgument"/> class.
    /// </summary>
    /// <param name="expressionSyntax">Generated expression inside the lambda.</param>
    /// <param name="parameterName">Parameters in the lambda.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public LambdaArgument(ExpressionSyntax expressionSyntax, string parameterName, string namedArgument = null)
        : base(namedArgument)
    {
        _expressionSyntax = expressionSyntax ?? throw new ArgumentNullException(nameof(expressionSyntax));
        _parameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaArgument"/> class.
    /// </summary>
    /// <param name="blockSyntax">Generated block/body inside the lambda.</param>
    /// <param name="parameterName">Parameters in the lambda.</param>
    /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
    public LambdaArgument(BlockSyntax blockSyntax, string parameterName, string namedArgument = null)
        : base(namedArgument)
    {
        _blockSyntax = blockSyntax ?? throw new ArgumentNullException(nameof(blockSyntax));
        _parameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
    }

    protected override ArgumentSyntax CreateArgumentSyntax()
    {
        SimpleLambdaExpressionSyntax expression = null;

        if (_blockSyntax != null)
        {
            expression = SimpleLambdaExpression(Parameter(Identifier(_parameterName)), _blockSyntax);
        }
        else
        {
            expression = SimpleLambdaExpression(Parameter(Identifier(_parameterName)), _expressionSyntax);
        }

        return Argument(expression);
    }
}
