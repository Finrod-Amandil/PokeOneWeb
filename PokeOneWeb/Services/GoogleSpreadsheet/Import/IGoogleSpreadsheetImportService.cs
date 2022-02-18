using System.Threading.Tasks;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface IGoogleSpreadsheetImportService
    {
        Task<SpreadsheetImportReport> ImportSpreadsheetData();
    }
}
