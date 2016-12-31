using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Builder.Factory;
using Testura.Code.Generate.ArgumentTypes;
using Testura.Code.Reference;

namespace Testura.Code.Generate
{
    /// <summary>
    /// Generate code for a variable
    /// </summary>
    public static class Variable
    {
        /// <summary>
        /// Create a local predefined variable 
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="type">Type of the variable</param>
        /// <param name="value">Value of variable</param>
        /// <param name="valueIsVariable"></param>
        /// <param name="useVarKeyword"></param>
        /// <returns>The generated local declaration statement</returns>
        public static LocalDeclarationStatementSyntax CreateType(string name, Type type, object value, bool valueIsVariable = false, bool useVarKeyword = true)
        {
            if (type == typeof(string) && !valueIsVariable)
            {
                value = $@"""{value}""";
            }
            return SyntaxFactory.LocalDeclarationStatement(SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName(useVarKeyword ? "var" : type.Name))
                .WithVariables(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(name))
                    .WithInitializer(EqualsValueClauseFactory.GetEqualsValueClause(value).WithEqualsToken(SyntaxFactory.Token(SyntaxKind.EqualsToken))))));
        }

        /// <summary>
        /// Create a local predefined variable 
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="type">Type of the variable</param>
        /// <param name="reference">Value of the variable</param>
        /// <param name="useVarKeyword">If we should created the variable with the var keyword</param>
        /// <returns>The generated local declaration statement</returns>
        public static LocalDeclarationStatementSyntax CreateType(string name, Type type, VariableReference reference, bool useVarKeyword = true)
        {
            return SyntaxFactory.LocalDeclarationStatement(SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName(useVarKeyword ? "var" : type.Name))
                .WithVariables(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(name))
                    .WithInitializer(EqualsValueClauseFactory.GetEqualsValueClause(reference).WithEqualsToken(SyntaxFactory.Token(SyntaxKind.EqualsToken))))));
        }

        /// <summary>
        /// Create a local class variable 
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="type">Type of the variable</param>
        /// <param name="arguments">Arguments to use when creating the variable</param>
        /// <param name="useVarKeyword">If we should created the variable with the var keyword</param>
        /// <returns>The generated local declaration statement</returns>
        public static LocalDeclarationStatementSyntax CreateInstance(string name, Type type, ArgumentListSyntax arguments, bool useVarKeyword = true)
        {
            var typeName = NameConverters.ConvertGenericTypeName(type);
            return SyntaxFactory.LocalDeclarationStatement(SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName(useVarKeyword ? "var" : typeName))
                .WithVariables(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(name))
                    .WithInitializer(
                        SyntaxFactory.EqualsValueClause(
                            SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName(typeName)).WithArgumentList(arguments)
                                .WithNewKeyword(SyntaxFactory.Token(SyntaxKind.NewKeyword)))))));
        }

        /// <summary>
        /// Create a local class variable 
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="type">Type of the variable</param>
        /// <returns>The generated local declaration statement</returns>
        public static LocalDeclarationStatementSyntax CreateInstance(string name, Type type)
        {
            return CreateInstance(name, type, Argument.Create(new List<IArgument>()));
        }

        /// <summary>
        /// Create a variable and assign it to an already created expression/statement
        /// </summary>
        /// <param name="name">Name of the variable</param>
        /// <param name="type">Type of the variable </param>
        /// <param name="expressionSyntax">An already created expression to assign to the variable</param>
        /// <param name="castTo">Cast the expression to this type</param>
        /// <param name="useVarKeyword">If we should create the variable with the var keyword</param>
        /// <returns>The generated local decleration statement</returns>
        public static LocalDeclarationStatementSyntax Create(string name, Type type, ExpressionSyntax expressionSyntax, Type castTo = null, bool useVarKeyword = true)
        {
            if (castTo != null && castTo != typeof(void))
                expressionSyntax = SyntaxFactory.CastExpression(SyntaxFactory.IdentifierName(castTo.FullName), expressionSyntax);
            
            return SyntaxFactory.LocalDeclarationStatement(SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName(useVarKeyword ? "var" : type.Name))
                .WithVariables(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(name))
                    .WithInitializer(SyntaxFactory.EqualsValueClause(expressionSyntax)))));
        }

        /// <summary>
        /// Create initialization of a new class variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax InitializeVariable(string name, Type type, ArgumentListSyntax arguments)
        {
            var typeName = NameConverters.ConvertGenericTypeName(type);
            return SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName(name),
                SyntaxFactory.ObjectCreationExpression(SyntaxFactory.IdentifierName(typeName)).WithArgumentList(arguments)
                    .WithNewKeyword(SyntaxFactory.Token(SyntaxKind.NewKeyword))));
        }

        /// <summary>
        /// Create an assignation of an already existing variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expressionSyntax"></param>
        /// <param name="castTo"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax AssignVariable(string name,
            ExpressionSyntax expressionSyntax, Type castTo = null)
        {
            if (castTo != null && castTo != typeof (void))
            {
                expressionSyntax = SyntaxFactory.CastExpression(SyntaxFactory.IdentifierName(castTo.Name), expressionSyntax);
            }
            return
                SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName(name),
                    expressionSyntax));
        }

        /// <summary>
        /// Create initialization of a new class without any arguments
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ExpressionStatementSyntax InitializeVariable(string name, Type type)
        {
            return InitializeVariable(name, type, Argument.Create(new List<IArgument>()));
        }
    }
}
