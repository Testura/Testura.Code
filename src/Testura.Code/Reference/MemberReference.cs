using System;

namespace Testura.Code.Reference
{
    public class MemberReference : VariableReference
    {
        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        public MemberReferenceTypes ReferenceType { get; set; }


        public MemberReference(string name, MemberReferenceTypes referenceType) : base(name)
        {
            Name = name;
            ReferenceType = referenceType;
        }

        public MemberReference(string name, MemberReferenceTypes referenceType, MemberReference memberReference) : this(name, referenceType)
        {
            Member = memberReference;
        }

        public override string ToString()
        {
            string name = Name;
            if (ReferenceType == MemberReferenceTypes.Method)
                name = $"{name}()";
            if (Member != null)
                return $"{name}.{Member}";
            return name;
        }
    }
}
