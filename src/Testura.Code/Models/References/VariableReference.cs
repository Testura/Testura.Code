using System;

namespace Testura.Code.Models.References
{
    /// <summary>
    /// The reference classes are used to simpify call to methods, fields, properties etc.
    ///
    /// An example of this could be this:
    ///
    /// myVariable.MyMethod().AProperty;
    ///
    /// This could be represented like this:
    /// new VariableReference("myVariable", new MethodReference("MyMethod",new MemberReference("AProperty");
    ///
    /// </summary>
    public class VariableReference
    {
        public VariableReference(string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException(nameof(variableName));
            }

            Name = variableName;
        }

        public VariableReference(string variableName, MemberReference member)
            : this(variableName)
        {
            Member = member;
        }

        /// <summary>
        /// Gets or sets the name of the refence
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the memeber references
        /// </summary>
        public MemberReference Member { get; set; }

        /// <summary>
        /// Go through the chain of members and return last.
        /// </summary>
        /// <returns>Returns last member in the reference chain</returns>
        public VariableReference GetLastMember()
        {
            var child = Member;
            while (child?.Member != null)
            {
                child = child.Member;
            }

            return child;
        }
    }
}
