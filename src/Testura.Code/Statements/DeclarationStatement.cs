using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Factories;
using Testura.Code.Generators.Common;
using Testura.Code.Generators.Common.BinaryExpressions;
using Testura.Code.Models.References;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Statements;

/// <summary>
/// Provides the functionality to generate declaration statements.
/// </summary>
public class DeclarationStatement
{
    /// <summary>
    /// Create the local declaration statement syntax to declare a variable and assign it a value.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    /// <param name="name">Name of variable.</param>
    /// <param name="value">Value to assign variable.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign<T>(string name, T value, bool useVarKeyword = true)
        where T : struct
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(typeof(T)))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(EqualsValueClauseFactory.GetEqualsValueClause(value).WithEqualsToken(Token(SyntaxKind.EqualsToken))))));
    }

    /// <summary>
    /// Create the local declaration statement syntax to declare a local variable and assign it a string value.
    /// </summary>
    /// <param name="name">Name of variable.</param>
    /// <param name="value">Value to assign variable.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign(string name, string value, bool useVarKeyword = true)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(typeof(string)))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(EqualsValueClauseFactory.GetEqualsValueClause($@"""{value}""").WithEqualsToken(Token(SyntaxKind.EqualsToken))))));
    }

    /// <summary>
    /// Create a local declaration statement which declare a local variable and assign it a complex reference.
    /// </summary>
    /// <param name="name">Name of variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <param name="reference">Value of the variable.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign(string name, Type type, VariableReference reference, bool useVarKeyword = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(type))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(EqualsValueClauseFactory.GetEqualsValueClause(reference).WithEqualsToken(Token(SyntaxKind.EqualsToken))))));
    }

    /// <summary>
    /// Create the local declaration statement syntax to declare a local variable and assign it a new class instance.
    /// </summary>
    /// <param name="name">Name of variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <param name="arguments">Arguments to use when creating the variable.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign(string name, Type type, ArgumentListSyntax arguments, bool useVarKeyword = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(type))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(
                    EqualsValueClause(
                        ObjectCreationExpression(TypeGenerator.Create(type)).WithArgumentList(arguments)
                            .WithNewKeyword(Token(SyntaxKind.NewKeyword)))))));
    }

    /// <summary>
    /// Create the local declaration statement syntax to declare a local variable and assign it an already created expression.
    /// </summary>
    /// <param name="name">Name of the variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <param name="expressionSyntax">An already created expression to assign to the variable.</param>
    /// <param name="castTo">Cast the expression to this type.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign(string name, Type type, ExpressionSyntax expressionSyntax, Type? castTo = null, bool useVarKeyword = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (expressionSyntax == null)
        {
            throw new ArgumentNullException(nameof(expressionSyntax));
        }

        if (castTo != null && castTo != typeof(void))
        {
            expressionSyntax = CastExpression(TypeGenerator.Create(castTo), expressionSyntax);
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(type))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(EqualsValueClause(expressionSyntax)))));
    }

    /// <summary>
    /// Create the local declaration statement syntax to declare a local variable and assign it to a binary expression.
    /// </summary>
    /// <param name="name">Name of the variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <param name="binaryExpression">The binary expression.</param>
    /// <param name="useVarKeyword">Will use "var" keyword if true, otherwise the type.</param>
    /// <returns>The generated local declaration statement.</returns>
    public LocalDeclarationStatementSyntax DeclareAndAssign(string name, Type type, IBinaryExpression binaryExpression, bool useVarKeyword = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (binaryExpression == null)
        {
            throw new ArgumentNullException(nameof(binaryExpression));
        }

        return LocalDeclarationStatement(VariableDeclaration(useVarKeyword ? IdentifierName("var") : TypeGenerator.Create(type))
            .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier(name))
                .WithInitializer(EqualsValueClause(binaryExpression.GetBinaryExpression())))));
    }

    /// <summary>
    /// Create the expression statement syntax to assign a variable a new instance with the "new" keyword.
    /// </summary>
    /// <param name="name">Name of the variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <param name="arguments">Arguments in the class constructor.</param>
    /// <returns>The generated assign declaration statement.</returns>
    public ExpressionStatementSyntax Assign(string name, Type type, ArgumentListSyntax arguments)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        return ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(name),
                ObjectCreationExpression(TypeGenerator.Create(type)).WithArgumentList(arguments).WithNewKeyword(Token(SyntaxKind.NewKeyword))));
    }

    /// <summary>
    /// Create the expression statement syntax to assign a variable to another expression.
    /// </summary>
    /// <param name="name">Name of variable.</param>
    /// <param name="expressionSyntax">The expression syntax.</param>
    /// <param name="castTo">If we should do a cast while assign the variable.</param>
    /// <returns>The generated assign declaration syntax.</returns>
    public ExpressionStatementSyntax Assign(string name, ExpressionSyntax expressionSyntax, Type castTo = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (expressionSyntax == null)
        {
            throw new ArgumentNullException(nameof(expressionSyntax));
        }

        return Assign(new VariableReference(name), expressionSyntax, castTo);
    }

    /// <summary>
    /// Create the expression statement syntax to assign a variable to another reference.
    /// </summary>
    /// <param name="name">Name of variable.</param>
    /// <param name="reference">Reference value to assign to the variable.</param>
    /// <param name="castTo">If we should do a cast while assign the variable.</param>
    /// <returns>The generated assign declaration syntax.</returns>
    public ExpressionStatementSyntax Assign(string name, VariableReference reference, Type castTo = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        }

        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        return Assign(name, ReferenceGenerator.Create(reference), castTo);
    }

    /// <summary>
    /// Create the expression statement syntax to assign a reference to another reference. For example a property to a property.
    /// </summary>
    /// <param name="reference">Reference that should be assigned.</param>
    /// <param name="valueReference">Reference that we should assign to another reference.</param>
    /// <param name="castTo">If we should do a cast while assign the variable.</param>
    /// <returns>The generated assign declaration syntax.</returns>
    public ExpressionStatementSyntax Assign(VariableReference reference, VariableReference valueReference, Type castTo = null)
    {
        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        if (valueReference == null)
        {
            throw new ArgumentNullException(nameof(valueReference));
        }

        if (reference is MethodReference || reference.GetLastMember() is MethodReference)
        {
            throw new ArgumentException($"{nameof(reference)} to assign can't be a method");
        }

        return Assign(reference, ReferenceGenerator.Create(valueReference), castTo);
    }

    /// <summary>
    /// Create the expression statement syntax to assign a reference to another expression.
    /// </summary>
    /// <param name="reference">Reference that should be assigned.</param>
    /// <param name="expressionSyntax">Expression that we should assign to reference.</param>
    /// <param name="castTo">If we should do a cast while assign the variable.</param>
    /// <returns>The generated assign declaration syntax.</returns>
    public ExpressionStatementSyntax Assign(VariableReference reference, ExpressionSyntax expressionSyntax, Type castTo = null)
    {
        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        if (expressionSyntax == null)
        {
            throw new ArgumentNullException(nameof(expressionSyntax));
        }

        if (castTo != null && castTo != typeof(void))
        {
            expressionSyntax = CastExpression(TypeGenerator.Create(castTo), expressionSyntax);
        }

        return
            ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, ReferenceGenerator.Create(reference), expressionSyntax));
    }

    /// <summary>
    /// Create the expression statement syntax to assign a reference to a binary expression.
    /// </summary>
    /// <param name="reference">Reference that should be assigned.</param>
    /// <param name="binaryExpression">The binary expression.</param>
    /// <returns>The generated assign declaration syntax.</returns>
    public ExpressionStatementSyntax Assign(VariableReference reference, IBinaryExpression binaryExpression)
    {
        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference));
        }

        if (binaryExpression == null)
        {
            throw new ArgumentNullException(nameof(binaryExpression));
        }

        return
            ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, ReferenceGenerator.Create(reference), binaryExpression.GetBinaryExpression()));
    }

    /// <summary>
    /// Create the expression statement syntax to declare a local variable.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="type">Type of the variable.</param>
    /// <returns>The declared expression statement syntax.</returns>
    public LocalDeclarationStatementSyntax Declare(string variableName, Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (string.IsNullOrEmpty(variableName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(variableName));
        }

        return LocalDeclarationStatement(
            VariableDeclaration(TypeGenerator.Create(type))
                .WithVariables(SingletonSeparatedList(
                    VariableDeclarator(Identifier(variableName)))));
    }
}
