using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for a class
    /// </summary>
    public static class Class
    {
        /// <summary>
        /// Create the constructor for a class
        /// </summary>
        /// <param name="className">Name of the class</param>
        /// <param name="parameters">Parameters of the constructor</param>
        /// <param name="body">Body of the constructor</param>
        /// <returns>A constructor decleration</returns>
        public static ConstructorDeclarationSyntax Constructor(string className, ParameterListSyntax parameters, BlockSyntax body)
        {
            ConstructorDeclarationSyntax c = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.Identifier(className))
                        .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                        .WithBody(body);
            if (parameters != null)
            {
                return c.WithParameterList(parameters);
            }
            return c;
        }
    }
}
