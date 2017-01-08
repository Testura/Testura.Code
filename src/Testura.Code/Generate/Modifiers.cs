using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generate
{
    public static class Modifiers
    {
        public static SyntaxTokenList Create(params Code.Modifiers[] modifierses)
        {
            if (modifierses.Length == 0)
            {
                return TokenList();
            }

            var tokens = new List<SyntaxToken>();
            foreach (var modifierse in modifierses)
            {
                switch (modifierse)
                {
                    case Code.Modifiers.Public:
                        tokens.Add(Token(SyntaxKind.PublicKeyword));
                        break;
                    case Code.Modifiers.Private:
                        tokens.Add(Token(SyntaxKind.PrivateKeyword));
                        break;
                    case Code.Modifiers.Static:
                        tokens.Add(Token(SyntaxKind.StaticKeyword));
                        break;
                    case Code.Modifiers.Abstract:
                        tokens.Add(Token(SyntaxKind.AbstractKeyword));
                        break;
                    case Code.Modifiers.Virtual:
                        tokens.Add(Token(SyntaxKind.VirtualKeyword));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return TokenList(tokens);
        }
    }
}
