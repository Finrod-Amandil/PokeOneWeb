using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityMapper<S, T> where S : ISpreadsheetEntityDto where T : class
    {
        /// <summary>
        /// Maps a list of DTOs to newly created database entities.
        /// </summary>
        IEnumerable<T> Map(IDictionary<RowHash, S> dtosWithHashes);

        /// <summary>
        /// Maps a list of DTOs onto the given existing database entities.
        /// </summary>
        IEnumerable<T> MapOnto(IList<T> entities, IDictionary<RowHash, S> dtosWithHashes);
    }
}
