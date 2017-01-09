namespace Testura.Code.Helpers.Common.References
{
    /// <summary>
    /// The reference classes are used to simpify call to methods, properties etc. 
    /// 
    /// An example of this could be this: 
    /// 
    /// myVariable.MyMethod().AProperty; 
    /// 
    /// This could be represented like this: 
    /// new VariableReference("myVariable", typeof(MyClass), new MemberReference("MyMethod", typeof(MyOtherClass), typeof(MyClass), 
    ///                            MemberReferenceTypes.Method, new MemberReference("AProperty", typeof(string), typeof(MyOtherClass), MemberReferenceTypes.Property);
    /// 
    /// </summary>
    public class VariableReference
    {
        /// <summary>
        /// Gets or sets the name of the refence
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the memeber references
        /// </summary>
        public MemberReference Member { get; set; }

        public VariableReference(string variableName)
        {
            Name = variableName;
        }

        public VariableReference(string variableName, MemberReference member) : this(variableName)
        {
            Member = member;
        }

        public override string ToString()
        {           
            if(Member != null)
            {
                return $"{Name}.{Member}";
            }
            return Name;
        }
    }
}
