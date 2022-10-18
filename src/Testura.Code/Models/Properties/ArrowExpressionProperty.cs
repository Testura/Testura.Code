using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Testura.Code.Models.Properties;

public class ArrowExpressionProperty : Property
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArrowExpressionProperty"/> class.
    /// </summary>
    /// <param name="name">Name of the property.</param>
    /// <param name="type">The property type.</param>
    /// <param name="expressionSyntax">The expression syntax of the arrow</param>
    /// <param name="modifiers">The property modifiers.</param>
    /// <param name="attributes">Attributes on the property.</param>
    /// <param name="getModifiers">The get modifiers.</param>
    /// <param name="setModifiers">The set modifiers.</param>
    /// <param name="summary">XML documentation summary</param>
    /// <param name="addSemicolon">If we should add semicolon after expression</param>
    public ArrowExpressionProperty(
        string name,
        Type type,
        ExpressionSyntax expressionSyntax,
        IEnumerable<Modifiers> modifiers = null,
        IEnumerable<Attribute> attributes = null,
        IEnumerable<Modifiers> getModifiers = null,
        IEnumerable<Modifiers> setModifiers = null,
        string summary = null,
        bool addSemicolon = true)
        : base(name, type, modifiers, attributes, getModifiers, setModifiers, summary)
    {
        ExpressionSyntax = expressionSyntax;
        AddSemicolon = addSemicolon;
    }

    public ExpressionSyntax ExpressionSyntax { get; set; }

    public bool AddSemicolon { get; set; }
}
