﻿namespace Testura.Code.Statements
{
    /// <summary>
    /// Provides a way to generate different statements.
    /// </summary>
    public static class Statement
    {
        static Statement()
        {
            Declaration = new DeclarationStatement();
            Jump = new JumpStatement();
            Selection = new SelectionStatement();
            Iteration = new IterationStatement();
            Expression = new ExpressionStatement();
        }

        /// <summary>
        /// Gets generator for deceleration statements.
        /// </summary>
        public static DeclarationStatement Declaration { get; private set; }

        /// <summary>
        /// Gets the generator for jump statements.
        /// </summary>
        public static JumpStatement Jump { get; private set; }

        /// <summary>
        /// Gets the generator for selection statements.
        /// </summary>
        public static SelectionStatement Selection { get; private set; }

        /// <summary>
        /// Gets the generator for iteration statements.
        /// </summary>
        public static IterationStatement Iteration { get; private set; }

        /// <summary>
        /// Gets the generator for expression statements.
        /// </summary>
        public static ExpressionStatement Expression { get; private set; }
    }
}
