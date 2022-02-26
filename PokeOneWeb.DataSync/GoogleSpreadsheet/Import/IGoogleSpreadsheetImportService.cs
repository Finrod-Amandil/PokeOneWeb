using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface IGoogleSpreadsheetImportService
    {
        Task<SpreadsheetImportReport> ImportSpreadsheetData();
    }
}
