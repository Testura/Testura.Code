namespace Testura.Code.Statements
{
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

        public static DeclarationStatement Declaration { get; private set; }

        public static JumpStatement Jump { get; private set; }

        public static SelectionStatement Selection { get; private set; }

        public static IterationStatement Iteration { get; private set; }

        public static ExpressionStatement Expression { get; private set; }
    }
}
