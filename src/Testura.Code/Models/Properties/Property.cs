namespace Testura.Code.Models.Properties
{
    /// <summary>
    /// Provides the base class from which classes that represent properties are derived.
    /// </summary>
    public abstract class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">The property type.</param>
        /// <param name="modifiers">The property modifiers.</param>
        /// <param name="attributes">Attributes on the property.</param>
        /// <param name="getModifiers">The get modifiers.</param>
        /// <param name="setModifiers">The set modifiers.</param>
        /// <param name="summary">XML documentation summary</param>
        protected Property(
            string name,
            Type type,
            IEnumerable<Modifiers> modifiers = null,
            IEnumerable<Attribute> attributes = null,
            IEnumerable<Modifiers> getModifiers = null,
            IEnumerable<Modifiers> setModifiers = null,
            string summary = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Modifiers = modifiers;
            Attributes = attributes;
            GetModifiers = getModifiers;
            SetModifiers = setModifiers;
            Summary = summary;
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the modifiers of the property.
        /// </summary>
        public IEnumerable<Modifiers> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the property.
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the get modifiers.
        /// </summary>
        public IEnumerable<Modifiers> GetModifiers { get; set; }

        /// <summary>
        /// Gets the set modifiers.
        /// </summary>
        public IEnumerable<Modifiers> SetModifiers { get; }

        /// <summary>
        /// Gets the property summary
        /// </summary>
        public string Summary { get; }
    }
}
