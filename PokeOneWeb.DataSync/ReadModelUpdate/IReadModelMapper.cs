using PokeOneWeb.Data;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.ReadModelUpdate
{
    public interface IReadModelMapper<T>
    {
        IDictionary<T, DbAction> MapFromDatabase(SpreadsheetImportReport importReport);
    }
}
