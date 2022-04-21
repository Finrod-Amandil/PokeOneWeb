using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeMapper : XSpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType>
    {
        public ElementalTypeMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.ElementalType;

        protected override bool IsValid(ElementalTypeSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(ElementalTypeSheetDto dto)
        {
            return dto.Name;
        }

        protected override ElementalType MapEntity(ElementalTypeSheetDto dto, RowHash rowHash, ElementalType elementalType = null)
        {
            elementalType ??= new ElementalType();

            elementalType.IdHash = rowHash.IdHash;
            elementalType.Hash = rowHash.Hash;
            elementalType.ImportSheetId = rowHash.ImportSheetId;
            elementalType.Name = dto.Name;

            return elementalType;
        }
    }
}