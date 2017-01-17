using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Generators.Common
{
    public static class ModifierGenerator
    {
        /// <summary>
        /// Create the syntax for modifier(s) to class, method, fields or properties.
        /// </summary>
        /// <param name="modifierses">Modifiers to create</param>
        /// <returns>The declared syntax list</returns>
        public static SyntaxTokenList Create(params Modifiers[] modifierses)
        {
            if (modifierses.Length == 0)
            {
                return SyntaxFactory.TokenList();
            }

            var tokens = new List<SyntaxToken>();
            foreach (var modifierse in modifierses)
            {
                switch (modifierse)
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return SyntaxFactory.TokenList(tokens);
        }
    }
}
