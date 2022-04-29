using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay
{
    public class TimeOfDayMapper : SpreadsheetEntityMapper<TimeOfDaySheetDto, TimeOfDay>
    {
        public TimeOfDayMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.TimeOfDay;

        protected override bool IsValid(TimeOfDaySheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Abbreviation);
        }

        protected override string GetUniqueName(TimeOfDaySheetDto dto)
        {
            return dto.Name;
        }

        protected override TimeOfDay MapEntity(TimeOfDaySheetDto dto, RowHash rowHash, TimeOfDay timeOfDay = null)
        {
            timeOfDay ??= new TimeOfDay();

            timeOfDay.IdHash = rowHash.IdHash;
            timeOfDay.Hash = rowHash.ContentHash;
            timeOfDay.ImportSheetId = rowHash.ImportSheetId;
            timeOfDay.SortIndex = dto.SortIndex;
            timeOfDay.Name = dto.Name;
            timeOfDay.Abbreviation = dto.Abbreviation;
            timeOfDay.Color = dto.Color;

            return timeOfDay;
        }
    }
}