namespace PokeOneWeb.Data.Entities.Interfaces
{
    /// <summary>
    /// Common interface for all entities which correspond to a record in a Google Spreadsheet.
    /// </summary>
    public interface IHashedEntity : IEntity
    {
        /// <summary>
        /// Gets or sets a hash value calculated over all fields. Can be used to detect changes on this entity.
        /// </summary>
        string Hash { get; set; }

        /// <summary>
        /// Gets or sets a hash value calculated over one or multiple fields which in combination are unique.
        /// Can be used to identify new and deleted entities.
        /// </summary>
        string IdHash { get; set; }
    }
}