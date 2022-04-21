using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers
{
    public class PvpTierMapper : XSpreadsheetEntityMapper<PvpTierSheetDto, PvpTier>
    {
        public PvpTierMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.PvpTier;

        protected override bool IsValid(PvpTierSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(PvpTierSheetDto dto)
        {
            return dto.Name;
        }

        protected override PvpTier MapEntity(PvpTierSheetDto dto, RowHash rowHash, PvpTier pvpTier = null)
        {
            pvpTier ??= new PvpTier();

            pvpTier.IdHash = rowHash.IdHash;
            pvpTier.Hash = rowHash.Hash;
            pvpTier.ImportSheetId = rowHash.ImportSheetId;
            pvpTier.Name = dto.Name;
            pvpTier.SortIndex = dto.SortIndex;

            return pvpTier;
        }
    }
}