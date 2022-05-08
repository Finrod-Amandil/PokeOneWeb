using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class SpreadsheetImportReport
    {
        public DateTime ReportCreatedTime { get; } = DateTime.UtcNow;

        public List<ImportUpdate> Updates { get; set; } = new();

        public List<ImportError> Errors { get; set; } = new();

        public TimeSpan TotalImportTime { get; set; } = TimeSpan.Zero;

        public TimeSpan TotalIdleTime { get; set; } = TimeSpan.Zero;

        public Dictionary<string, TimeSpan> ImportTimesBySheet { get; set; } = new();

        public TimeSpan TotalReadModelUpdateTime { get; set; } = TimeSpan.Zero;

        public Dictionary<string, TimeSpan> ReadModelUpdateTimesByEntity { get; set; } = new();
    }
}