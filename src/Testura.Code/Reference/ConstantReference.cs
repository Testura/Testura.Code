using System;

namespace Testura.Code.Reference
{
    public class ConstantReference : VariableReference
    {
        public ConstantReference(object value)
            : base(value.ToString(), value.GetType())
        {
        }

        protected ConstantReference(string variableName, Type variableType, MemberReference member)
            : base(variableName, variableType, member)
        {
        }
    }
}
