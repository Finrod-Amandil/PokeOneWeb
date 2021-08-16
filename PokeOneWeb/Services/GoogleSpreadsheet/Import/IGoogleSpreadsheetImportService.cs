using System.Threading.Tasks;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface IGoogleSpreadsheetImportService
    {
        Task<int> ImportSpreadsheetData();
    }
}
