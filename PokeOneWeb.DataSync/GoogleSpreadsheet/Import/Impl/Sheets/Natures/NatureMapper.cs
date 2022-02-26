using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Natures
{
    public class NatureMapper : SpreadsheetEntityMapper<NatureSheetDto, Nature>
    {
        public NatureMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Nature;

        protected override bool IsValid(NatureSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(NatureSheetDto dto)
        {
            return dto.Name;
        }

        protected override Nature MapEntity(NatureSheetDto dto, RowHash rowHash, Nature nature = null)
        {
            nature ??= new Nature();

            nature.IdHash = rowHash.IdHash;
            nature.Hash = rowHash.ContentHash;
            nature.ImportSheetId = rowHash.ImportSheetId;
            nature.Name = dto.Name;
            nature.Attack = dto.Attack;
            nature.SpecialAttack = dto.SpecialAttack;
            nature.Defense = dto.Defense;
            nature.SpecialDefense = dto.SpecialDefense;
            nature.Speed = dto.Speed;

            return nature;
        }
    }
}
