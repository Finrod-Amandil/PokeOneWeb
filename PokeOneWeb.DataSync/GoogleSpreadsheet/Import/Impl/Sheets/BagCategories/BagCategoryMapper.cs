using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories
{
    public class BagCategoryMapper : SpreadsheetEntityMapper<BagCategorySheetDto, BagCategory>
    {
        public BagCategoryMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.BagCategory;

        protected override bool IsValid(BagCategorySheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(BagCategorySheetDto dto)
        {
            return dto.Name;
        }

        protected override BagCategory MapEntity(BagCategorySheetDto dto, RowHash rowHash, BagCategory bagCategory = null)
        {
            bagCategory ??= new BagCategory();

            bagCategory.IdHash = rowHash.IdHash;
            bagCategory.Hash = rowHash.ContentHash;
            bagCategory.ImportSheetId = rowHash.ImportSheetId;
            bagCategory.Name = dto.Name;
            bagCategory.SortIndex = dto.SortIndex;

            return bagCategory;
        }
    }
}
