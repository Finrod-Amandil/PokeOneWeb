using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelUpdateService
    {
        void UpdateReadModel(SpreadsheetImportReport importReport);
    }
}
