using System;

namespace Testura.Code.Reference
{
    public class ConstantReference : VariableReference
    {
        public ConstantReference(object value)
            : base(value.ToString())
        {
        }

        protected ConstantReference(string variableName, MemberReference member)
            : base(variableName, member)
        {
        }
    }
}
