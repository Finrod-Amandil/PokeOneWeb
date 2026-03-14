namespace PokeOneWeb.Data.Entities.Interfaces
{
    /// <summary>
    /// Common interface for all entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the auto-incremented database ID.
        /// </summary>
        int Id { get; set; }
    }
}