﻿using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Models.Properties
{
    /// <summary>
    /// Represent a property with a get/sets that have a multi line body/block.
    /// </summary>
    public class BodyProperty : Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyProperty"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">The property type.</param>
        /// <param name="getBody">The generated get body.</param>
        /// <param name="setBody">The generated set body.</param>
        /// <param name="modifiers">Modifiers of the property.</param>
        /// <param name="attributes">Attributes of the property.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyProperty"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">The property type.</param>
        /// <param name="getBody">The generated get body.</param>
        /// <param name="modifiers">Modifiers of the property.</param>
        /// <param name="attributes">Attributes of the property.</param>
        public BodyProperty(string name, Type type, BlockSyntax getBody, IEnumerable<Modifiers> modifiers = null, IEnumerable<Attribute> attributes = null)
            : this(name, type, getBody, null, modifiers, attributes)
        {
        }

        /// <summary>
        /// Gets or sets the generated body of the get in a property
        /// </summary>
        public BlockSyntax GetBody { get; set; }

        /// <summary>
        /// Gets or sets the generated body of the set in a property
        /// </summary>
        public BlockSyntax SetBody { get; set; }
    }
}
