﻿namespace PokeOneWeb.Data.Entities.Interfaces
{
    /// <summary>
    /// Common interface for all entities which correspond to a record in a Google Spreadsheet.
    /// </summary>
    public interface IHashedEntity : IEntity
    {
        /// <summary>
        /// Gets or sets a hash value calculated over all fields. Can be used to detect changes on this entity.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets a hash value calculated over one or multiple fields which in combination are unique.
        /// Can be used to identify new and deleted entities.
        /// </summary>
        public string IdHash { get; set; }

        /// <summary>
        /// Gets or sets the Spreadsheet, from which this entity was imported.
        /// </summary>
        public ImportSheet ImportSheet { get; set; }

        /// <summary>
        /// Gets or sets database foreign key of the spreadsheet, from which this entity was imported.
        /// </summary>
        public int ImportSheetId { get; set; }
    }
}