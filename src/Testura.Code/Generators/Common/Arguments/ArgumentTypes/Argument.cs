using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Testura.Code.Generators.Common.Arguments.ArgumentTypes
{
    public abstract class Argument : IArgument
    {
        private readonly string _namedArgument;

        protected Argument(string namedArgument = null)
        {
            _namedArgument = namedArgument;
        }

        public ArgumentSyntax GetArgumentSyntax()
        {
            var argumentSyntax = CreateArgumentSyntax();
            if (_namedArgument != null)
            {
                return argumentSyntax.WithNameColon(NameColon(IdentifierName(_namedArgument)));
            }

            return argumentSyntax;
        }

        protected abstract ArgumentSyntax CreateArgumentSyntax();

    }
}
