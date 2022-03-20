using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using System.Threading.Tasks;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface IGoogleSpreadsheetImportService
    {
        /// <summary>
        /// Compares the Google Spreadsheets with the data in the database and imports
        /// all changes.
        /// </summary>
        Task<SpreadsheetImportReport> ImportSpreadsheetData();
    }
}
