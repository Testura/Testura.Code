namespace Testura.Code.Statements
{
    public static class Statement
    {
        static Statement()
        {
            Variable = new DeclerationStatement();
            Return = new JumpStatement();
            Conditional = new SelectionStatement();
            Control = new IterationStatement();
            Expression = new ExpressionStatement();
        }

        public static DeclerationStatement Variable { get; private set; }

        public static JumpStatement Return { get; private set; }

        public static SelectionStatement Conditional { get; private set; }

        public static IterationStatement Control { get; private set; }

        public static ExpressionStatement Expression { get; private set; }
    }
}
