using System;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Events
{
    public class EventDto : ISpreadsheetEntityDto
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
