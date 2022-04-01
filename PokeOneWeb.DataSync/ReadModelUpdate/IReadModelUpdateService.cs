using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate
{
    public interface IReadModelUpdateService
    {
        void UpdateReadModel(SpreadsheetImportReport importReport);
    }
}