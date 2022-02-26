using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveDamageClasses
{
    public class MoveDamageClassMapper : SpreadsheetEntityMapper<MoveDamageClassSheetDto, MoveDamageClass>
    {
        public MoveDamageClassMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.MoveDamageClass;

        protected override bool IsValid(MoveDamageClassSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(MoveDamageClassSheetDto dto)
        {
            return dto.Name;
        }

        protected override MoveDamageClass MapEntity(
            MoveDamageClassSheetDto dto, 
            RowHash rowHash, 
            MoveDamageClass moveDamageClass = null)
        {
            moveDamageClass ??= new MoveDamageClass();

            moveDamageClass.IdHash = rowHash.IdHash;
            moveDamageClass.Hash = rowHash.ContentHash;
            moveDamageClass.ImportSheetId = rowHash.ImportSheetId;
            moveDamageClass.Name = dto.Name;

            return moveDamageClass;
        }
    }
}
