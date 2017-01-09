using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Testura.Code.Helpers.Common
{
    public static class Modifiers
    {
        public static SyntaxTokenList Create(params Code.Modifiers[] modifierses)
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
                    case Code.Modifiers.Public:
                        tokens.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                        break;
                    case Code.Modifiers.Private:
                        tokens.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                        break;
                    case Code.Modifiers.Static:
                        tokens.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
                        break;
                    case Code.Modifiers.Abstract:
                        tokens.Add(SyntaxFactory.Token(SyntaxKind.AbstractKeyword));
                        break;
                    case Code.Modifiers.Virtual:
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
