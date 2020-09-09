using System.Threading.Tasks;

namespace PokeOneWeb.Services.GoogleSpreadsheet
{
    public interface IGoogleSpreadsheetService
    {
        Task SynchronizeSpreadsheetData();
    }
}
