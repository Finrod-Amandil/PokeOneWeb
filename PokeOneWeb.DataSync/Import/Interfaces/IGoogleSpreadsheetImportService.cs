using System.Threading.Tasks;
using PokeOneWeb.DataSync.Import.Reporting;

namespace PokeOneWeb.DataSync.Import.Interfaces
{
    public interface IGoogleSpreadsheetImportService
    {
        /// <summary>
        /// Compares the Google Spreadsheets with the data in the data store and imports
        /// all changes.
        /// </summary>
        Task<SpreadsheetImportReport> ImportSpreadsheetData();
    }
}