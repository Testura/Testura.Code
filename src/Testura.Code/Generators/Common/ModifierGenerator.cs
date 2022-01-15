using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Generators.Common;

/// <summary>
/// Provides the functionality to generate modifiers (public, protected, etc).
/// </summary>
public static class ModifierGenerator
{
    /// <summary>
    /// Create the syntax for modifier(s) to class, method, fields or properties.
    /// </summary>
    /// <param name="modifiers">Modifiers to create.</param>
    /// <returns>The declared syntax list.</returns>
    public static SyntaxTokenList Create(params Modifiers[] modifiers)
    {
        if (modifiers.Length == 0)
        {
            return SyntaxFactory.TokenList();
        }

        var tokens = new List<SyntaxToken>();
        foreach (var modifier in modifiers)
        {
            switch (modifier)
            {
                case Modifiers.Public:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                    break;
                case Modifiers.Private:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                    break;
                case Modifiers.Static:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
                    break;
                case Modifiers.Abstract:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.AbstractKeyword));
                    break;
                case Modifiers.Virtual:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.VirtualKeyword));
                    break;
                case Modifiers.Async:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.AsyncKeyword));
                    break;
                case Modifiers.Override:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
                    break;
                case Modifiers.Readonly:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
                    break;
                case Modifiers.Sealed:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword));
                    break;
                case Modifiers.New:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.NewKeyword));
                    break;
                case Modifiers.Partial:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
                    break;
                case Modifiers.Internal:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
                    break;
                case Modifiers.Protected:
                    tokens.Add(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return SyntaxFactory.TokenList(tokens);
    }
}
