﻿namespace Testura.Code
{
    /// <summary>
    /// Describes all possible conditional statements.
    /// </summary>
    public enum ConditionalStatements
    {
        /// <summary>
        /// Generate with an equal conditional statement: <c>"="</c>.
        /// </summary>
        Equal,

        /// <summary>
        /// Generate with an not equal conditional statement: <c>"!="</c>.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Generate with a greater than conditional statement: <c>">"</c>.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Generate with a greater than or eqal conditional statement: <c>">="</c>.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Generate with a less than conditional statement: <c>"&lt;"</c>.
        /// </summary>
        LessThan,

        /// <summary>
        /// Generate with a less than or equal conditional statement: <c>"&lt;="</c>.
        /// </summary>
        LessThanOrEqual
    }

    /// <summary>
    /// Describes all the possible modifiers.
    /// </summary>
    public enum Modifiers
    {
        /// <summary>
        /// Generate with the <c>public</c> modifier.
        /// </summary>
        Public,

        /// <summary>
        /// Generate with the <c>private</c> modifier.
        /// </summary>
        Private,

        /// <summary>
        /// Generate with the <c>static</c> modifier.
        /// </summary>
        Static,

        /// <summary>
        /// Generate with the <c>abstract</c> modifier.
        /// </summary>
        Abstract,

        /// <summary>
        /// Generate with the <c>virtual</c> modifier.
        /// </summary>
        Virtual
    }

    /// <summary>
    /// Describes all possible parameter modifiers.
    /// </summary>
    public enum ParameterModifiers
    {
        /// <summary>
        /// Generate with no modifier.
        /// </summary>
        None,

        /// <summary>
        /// Generate with the <c>out</c> modifier.
        /// </summary>
        Out,

        /// <summary>
        /// Generate with the <c>ref</c> modifier.
        /// </summary>
        Ref,

        /// <summary>
        /// Generate with the <c>this</c> modifier/keyword.
        /// </summary>
        This
    }

    /// <summary>
    /// Defines different auto property types.
    /// </summary>
    public enum PropertyTypes
    {
        /// <summary>
        /// Generate a auto property with only <c>get</c>.
        /// </summary>
        Get,

        /// <summary>
        /// Generate a auto property with both <c>get</c> and <c>set</c>.
        /// </summary>
        GetAndSet
    }

    /// <summary>
    /// Defines different ways to generate a string.
    /// </summary>
    public enum StringType
    {
        /// <summary>
        /// Generate as a normal string: <c>"test"</c>.
        /// </summary>
        Normal,

        /// <summary>
        /// Generate as a path: <c>@"test"</c>.
        /// </summary>
        Path
    }

    /// <summary>
    /// Describes all all math operators.
    /// </summary>
    public enum MathOperators
    {
        /// <summary>
        /// Generate the add (<c>+</c>) operator.
        /// </summary>
        Add,

        /// <summary>
        /// Generate the subtract (<c>-</c>) operator.
        /// </summary>
        Subtract,

        /// <summary>
        /// Generate the divide (<c>/</c>) operator.
        /// </summary>
        Divide,

        /// <summary>
        /// Generate the multiply (<c>*</c>) operator.
        /// </summary>
        Multiply
    }

    internal enum AssertType
    {
        AreEqual,
        AreNotEqual,
        IsTrue,
        IsFalse,
        Contains,
        AreSame,
        AreNotSame
    }
}
