using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Seasons
{
    public class SeasonMapper : XSpreadsheetEntityMapper<SeasonSheetDto, Season>
    {
        public SeasonMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Season;

        protected override bool IsValid(SeasonSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Abbreviation);
        }

        protected override string GetUniqueName(SeasonSheetDto dto)
        {
            return dto.Name;
        }

        protected override Season MapEntity(SeasonSheetDto dto, RowHash rowHash, Season season = null)
        {
            season ??= new Season();

            season.IdHash = rowHash.IdHash;
            season.Hash = rowHash.Hash;
            season.ImportSheetId = rowHash.ImportSheetId;
            season.SortIndex = dto.SortIndex;
            season.Name = dto.Name;
            season.Abbreviation = dto.Abbreviation;
            season.Color = dto.Color;

            return season;
        }
    }
}