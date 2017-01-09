namespace Testura.Code.Statements
{
    public static class Statement
    {
        static Statement()
        {
            Decleration = new DeclerationStatement();
            Jump = new JumpStatement();
            Selection = new SelectionStatement();
            Iteratio = new IterationStatement();
            Expression = new ExpressionStatement();
        }

        public static DeclerationStatement Decleration { get; private set; }

        public static JumpStatement Jump { get; private set; }

        public static SelectionStatement Selection { get; private set; }

        public static IterationStatement Iteratio { get; private set; }

        public static ExpressionStatement Expression { get; private set; }
    }
}
