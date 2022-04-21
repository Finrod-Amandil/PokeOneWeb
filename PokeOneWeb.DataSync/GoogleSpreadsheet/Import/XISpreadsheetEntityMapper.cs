using System.Collections.Generic;
using PokeOneWeb.Data;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface XISpreadsheetEntityMapper<TDto, TEntity> where TDto : XISpreadsheetEntityDto where TEntity : class
    {
        /// <summary>
        /// Maps a list of DTOs to newly created database entities.
        /// </summary>
        IEnumerable<TEntity> Map(IDictionary<RowHash, TDto> dtosWithHashes);

        /// <summary>
        /// Maps a list of DTOs onto the given existing database entities.
        /// </summary>
        IEnumerable<TEntity> MapOnto(IList<TEntity> entities, IDictionary<RowHash, TDto> dtosWithHashes);
    }
}