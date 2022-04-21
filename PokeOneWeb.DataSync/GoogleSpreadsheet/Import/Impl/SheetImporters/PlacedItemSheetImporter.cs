using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Repositories;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Attributes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetImporters
{
    [SheetName("placed_items")]
    public class PlacedItemSheetImporter : SheetImporter<PlacedItem>
    {
        public PlacedItemSheetImporter(
            ISpreadsheetDataLoader dataLoader,
            IImportSheetRepository importSheetRepository,
            IHashedEntityRepository<PlacedItem> repository,
            ISheetMapper<PlacedItem> mapper,
            IHashListComparator hashListComparator)
            : base(dataLoader, importSheetRepository, repository, mapper, hashListComparator)
        {
        }
    }
}