using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes
{
    public class ElementalTypeMapper : SpreadsheetEntityMapper<ElementalTypeSheetDto, ElementalType>
    {
        public ElementalTypeMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

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
            elementalType.Hash = rowHash.ContentHash;
            elementalType.ImportSheetId = rowHash.ImportSheetId;
            elementalType.Name = dto.Name;
            elementalType.PokeApiName = dto.PokeApiName;

            return elementalType;
        }
    }
}
