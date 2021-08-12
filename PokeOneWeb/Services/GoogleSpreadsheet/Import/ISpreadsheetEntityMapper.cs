using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityMapper<S, T> where S : ISpreadsheetEntityDto where T : class
    {
        IEnumerable<T> Map(IDictionary<RowHash, S> dtosWithHashes);

        IEnumerable<T> MapOnto(IList<T> entities, IDictionary<RowHash, S> dtosWithHashes);
    }
}
