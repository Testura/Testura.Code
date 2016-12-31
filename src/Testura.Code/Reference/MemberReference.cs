using System;

namespace Testura.Code.Reference
{
    public class MemberReference : VariableReference
    {
        /// <summary>
        /// Gets or sets the reference type
        /// </summary>
        public MemberReferenceTypes ReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the parent reference
        /// </summary>
        public Type ParentType { get; set; }

        public MemberReference(string name, Type memberType, Type parentType, MemberReferenceTypes referenceType) : base(name, memberType)
        {
            Name = name;
            ParentType = parentType;
            ReferenceType = referenceType;
        }

        public MemberReference(string name, Type memberType, Type parentType, MemberReferenceTypes referenceType, MemberReference memberReference) : this(name, memberType, parentType, referenceType)
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
