namespace PokeOneWeb.Data.Entities.Interfaces
{
    /// <summary>
    /// Common interface for all entities which can be unequivocally identified by their display name.
    /// </summary>
    public interface INamedEntity : IEntity
    {
        /// <summary>
        /// Gets or sets a unique display name.
        /// </summary>
        string Name { get; set; }
    }
}