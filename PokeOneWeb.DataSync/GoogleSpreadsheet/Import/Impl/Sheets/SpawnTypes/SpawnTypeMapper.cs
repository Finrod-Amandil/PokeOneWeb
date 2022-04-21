using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes
{
    public class SpawnTypeMapper : XSpreadsheetEntityMapper<SpawnTypeSheetDto, SpawnType>
    {
        public SpawnTypeMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.SpawnType;

        protected override bool IsValid(SpawnTypeSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(SpawnTypeSheetDto dto)
        {
            return dto.Name;
        }

        protected override SpawnType MapEntity(SpawnTypeSheetDto dto, RowHash rowHash, SpawnType spawnType = null)
        {
            spawnType ??= new SpawnType();

            spawnType.IdHash = rowHash.IdHash;
            spawnType.Hash = rowHash.Hash;
            spawnType.ImportSheetId = rowHash.ImportSheetId;
            spawnType.Name = dto.Name;
            spawnType.SortIndex = dto.SortIndex;
            spawnType.IsSyncable = dto.IsSyncable;
            spawnType.IsInfinite = dto.IsInfinite;
            spawnType.Color = dto.Color;

            return spawnType;
        }
    }
}