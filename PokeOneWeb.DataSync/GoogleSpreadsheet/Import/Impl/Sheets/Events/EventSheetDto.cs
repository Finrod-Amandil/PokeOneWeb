using System;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Events
{
    public class EventSheetDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
