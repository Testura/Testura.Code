using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Testura.Code.Models.Types;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using DateTimeOffset = System.DateTimeOffset;

namespace Testura.Code.Generators.Common;

/// <summary>
/// Provides functionality to generate predefined and custom types.
/// </summary>
public static class TypeGenerator
{
    /// <summary>
    /// Create the syntax for a type.
    /// </summary>
    /// <param name="type">The type to create.</param>
    /// <returns>The declared type syntax.</returns>
    public static TypeSyntax Create(Type type)
    {
        switch (type)
        {
            case null:
                throw new ArgumentNullException(nameof(type));
            case CustomTypeProxy proxy:
                return ParseTypeName(proxy.TypeName);
        }

        if (type.IsArray)
        {
            return
                ArrayType(Create(type.GetElementType()))
                    .WithRankSpecifiers(
                        SingletonList(
                            ArrayRankSpecifier(
                                SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))));
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() != typeof(Nullable<>))
        {
            return CreateGenericType(type);
        }

        var typeSyntax = CheckPredefinedTypes(type);
        return typeSyntax ?? ParseTypeName(type.Name);
    }

    private static TypeSyntax? CheckPredefinedTypes(Type type)
    {
        TypeSyntax typeSyntax = null;
        var isNullable = false;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            type = type.GetGenericArguments()[0];
            isNullable = true;
        }

        if (type == typeof(int))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.IntKeyword));
        }

        if (type == typeof(double))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.DoubleKeyword));
        }

        if (type == typeof(long))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.LongKeyword));
        }

        if (type == typeof(ulong))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.ULongKeyword));
        }

        if (type == typeof(float))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.FloatKeyword));
        }

        if (type == typeof(byte))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.ByteKeyword));
        }

        if (type == typeof(string))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.StringKeyword));
        }

        if (type == typeof(sbyte))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.SByteKeyword));
        }

        if (type == typeof(ushort))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.UShortKeyword));
        }

        if (type == typeof(uint))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.UIntKeyword));
        }

        if (type == typeof(bool))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.BoolKeyword));
        }

        if (type == typeof(char))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.CharKeyword));
        }

        if (type == typeof(decimal))
        {
            typeSyntax = PredefinedType(Token(SyntaxKind.DecimalKeyword));
        }

        if (type == typeof(DateTime))
        {
            typeSyntax = IdentifierName("DateTime");
        }

        if (type == typeof(TimeSpan))
        {
            typeSyntax = IdentifierName("TimeSpan");
        }

        if (type == typeof(DateTimeOffset))
        {
            typeSyntax = IdentifierName("DateTimeOffset");
        }

        if (type.IsEnum || (typeSyntax == null && type.IsValueType))
        {
            typeSyntax = IdentifierName(type.Name);
        }

        if (typeSyntax != null && isNullable)
        {
            return NullableType(typeSyntax);
        }

        return typeSyntax;
    }

    private static TypeSyntax CreateGenericType(Type type)
    {
        return
            GenericName(Identifier(type.Name[..type.Name.LastIndexOf("`", StringComparison.Ordinal)])).WithTypeArgumentList(TypeArgumentList(
                SeparatedList<TypeSyntax>(GetGenericTypes(type.GetGenericArguments()))));
    }

    private static SyntaxNodeOrToken[] GetGenericTypes(IEnumerable<Type> genericTypes)
    {
        var list = new List<SyntaxNodeOrToken>();
        foreach (var genericType in genericTypes)
        {
            list.Add(Create(genericType));
            list.Add(Token(SyntaxKind.CommaToken));
        }

        list.RemoveAt(list.Count - 1);
        return list.ToArray();
    }
}
