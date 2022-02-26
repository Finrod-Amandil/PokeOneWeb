using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityMapper<S, T> where S : ISpreadsheetEntityDto where T : class
    {
        IEnumerable<T> Map(IDictionary<RowHash, S> dtosWithHashes);

        IEnumerable<T> MapOnto(IList<T> entities, IDictionary<RowHash, S> dtosWithHashes);
    }
}
