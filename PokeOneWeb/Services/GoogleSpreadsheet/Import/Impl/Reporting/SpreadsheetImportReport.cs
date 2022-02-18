using System;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class SpreadsheetImportReport
    {
        public SpreadsheetImportReport()
        {
            ReportCreatedTime = DateTime.UtcNow;
        }

        public DateTime ReportCreatedTime { get; }

        public List<ImportUpdate> Updates { get; set; } = new();

        public List<ImportError> Errors { get; set; } = new();
    }
}
