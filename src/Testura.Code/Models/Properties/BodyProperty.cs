using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Models.Properties
{
    public class BodyProperty : Property
    {
        public BodyProperty(string name, Type type, BlockSyntax getBody, BlockSyntax setBody, IEnumerable<Modifiers> modifiers = null, IEnumerable<Attribute> attributes = null)
            : base(name, type, modifiers, attributes)
        {
            if (getBody == null)
            {
                throw new ArgumentNullException(nameof(getBody));
            }

            GetBody = getBody;
            SetBody = setBody;
        }

        public BodyProperty(string name, Type type, BlockSyntax getBody, IEnumerable<Modifiers> modifiers = null, IEnumerable<Attribute> attributes = null)
            : this(name, type, getBody, null, modifiers, attributes)
        {
        }

        public BlockSyntax GetBody { get; }

        public BlockSyntax SetBody { get; }
    }
}
