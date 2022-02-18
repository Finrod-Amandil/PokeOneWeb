using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelMapper<T>
    {
        IDictionary<T, DbAction> MapFromDatabase(SpreadsheetImportReport importReport);
    }
}
