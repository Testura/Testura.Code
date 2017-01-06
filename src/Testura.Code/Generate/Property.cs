using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace Testura.Code.Generate
{
    public enum PropertyTypes
    {
        Get,
        GetAndSet
    }

    public static class Property
    {

        /// <summary>
        /// Create a auto property for a class
        /// </summary>
        /// <param name="name">Name of property</param>
        /// <param name="type">Type of property</param>
        /// <returns>A property declaration</returns>
        public static PropertyDeclarationSyntax Create(string name, Type type, PropertyTypes propertyType)
        {
            var property = PropertyDeclaration(
                ParseTypeName(type.Name), Identifier(name)).
                AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).
                    WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
            if (propertyType == PropertyTypes.GetAndSet)
            {
               property = property.AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).
                    WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                );
            }

            return property;
        }

        /// <summary>
        /// Create a new expression to get a property value 
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static MemberAccessExpressionSyntax GetValue(string variableName, string propertyName)
        {
            return MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(variableName),
                IdentifierName(propertyName));
        }

        /// <summary>
        /// Create a new expression to set a property value
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax SetValue(string variableName, string propertyName, object value, ArgumentType other)
        {
            if (other == ArgumentType.String && value is string)
            {
                value = $"\"{value}\"";
            }

            return
                ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(variableName),
                        IdentifierName(propertyName)).WithOperatorToken(Token(
                            SyntaxKind.DotToken)),
                    IdentifierName(
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
                expressionSyntax = CastExpression(IdentifierName(castTo.Name), expressionSyntax);
            }
            return ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(variableName),
                    IdentifierName(propertyName)), expressionSyntax));
        }
    }
}
