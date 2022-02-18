using System;
using PokeOneWeb.Data;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetImportReporter
    {
        void NewSession();

        SpreadsheetImportReport GetReport();

        void ReportDeleted(Entity entity, string hash, int applicationDbId);

        void ReportAdded(Entity entity, string hash, int applicationDbId);

        void ReportUpdated(Entity entity, string hash, int applicationDbId);

        void ReportError(Entity entity, string hash, string message);

        void ReportError(Entity entity, string hash, Exception exception);
    }
}
