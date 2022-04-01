using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate
{
    public interface IReadModelMapper<T>
    {
        IDictionary<T, DbAction> MapFromDatabase(SpreadsheetImportReport importReport);
    }
}