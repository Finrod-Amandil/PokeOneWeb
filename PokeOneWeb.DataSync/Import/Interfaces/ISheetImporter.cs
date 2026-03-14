using System.Threading.Tasks;

namespace PokeOneWeb.DataSync.Import.Interfaces
{
    public interface ISheetImporter
    {
        /// <summary>
        /// Checks if the given sheet has changed since the last import,
        /// and imports and saves the changes.
        /// </summary>
        Task ImportSheet(string spreadsheetId, string sheetName);
    }
}