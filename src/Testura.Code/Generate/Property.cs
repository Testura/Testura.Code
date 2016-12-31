using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Generate
{
    public static class Property
    {

        /// <summary>
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(string name, Type type)
        {
            return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type.Name), SyntaxFactory.Identifier(name)).
                AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword)).
                //Get
                AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .
                //Set
                AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                    WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                );
        }

        /// <summary>
        /// Create a new expression to get a property value 
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static MemberAccessExpressionSyntax GetValue(string variableName, string propertyName)
        {
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(variableName),
                SyntaxFactory.IdentifierName(propertyName));
        }

        /// <summary>
        /// Create a new expression to set a property value
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax SetValue(string variableName, string propertyName, object value,
            ArgumentType other)
        {
            return
                SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(variableName),
                        SyntaxFactory.IdentifierName(propertyName)).WithOperatorToken(SyntaxFactory.Token(
                            SyntaxKind.DotToken)),
                    SyntaxFactory.IdentifierName(
                        value.ToString())));
        }

        /// <summary>
        /// Create a new expression to set a property value
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="expressionSyntax"></param>
        /// <param name="castTo"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax SetValue(string variableName, string propertyName, ExpressionSyntax expressionSyntax, Type castTo)
        {
            if (castTo != null && castTo != typeof(void))
            {
                expressionSyntax = SyntaxFactory.CastExpression(SyntaxFactory.IdentifierName(castTo.Name), expressionSyntax);
            }
            return SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(variableName),
                    SyntaxFactory.IdentifierName(propertyName)), expressionSyntax));
        }
    }
}
