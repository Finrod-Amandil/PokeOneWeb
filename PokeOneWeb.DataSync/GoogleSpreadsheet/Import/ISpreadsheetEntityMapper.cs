using System.Collections.Generic;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityMapper<TDto, TEntity> where TDto : ISpreadsheetEntityDto where TEntity : class
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