namespace Testura.Code.Statements;

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
        ExceptionHandlingStatement = new ExceptionHandlingStatement();
    }

    /// <summary>
    /// Gets generator for deceleration statements.
    /// </summary>
    public static DeclarationStatement Declaration { get; }

    /// <summary>
    /// Gets the generator for jump statements.
    /// </summary>
    public static JumpStatement Jump { get; }

    /// <summary>
    /// Gets the generator for selection statements.
    /// </summary>
    public static SelectionStatement Selection { get; }

    /// <summary>
    /// Gets the generator for iteration statements.
    /// </summary>
    public static IterationStatement Iteration { get; }

    /// <summary>
    /// Gets the generator for expression statements.
    /// </summary>
    public static ExpressionStatement Expression { get; }

    /// <summary>
    /// Gets the generator for exception handling statements.
    /// </summary>
    public static ExceptionHandlingStatement ExceptionHandlingStatement { get; }
}
