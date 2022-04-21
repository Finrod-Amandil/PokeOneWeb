using System;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetImportReporter
    {
        void NewSession();

        SpreadsheetImportReport GetReport();

        void ReportDeleted(string entity, string hash, int applicationDbId);

        void ReportAdded(string entity, string hash, int applicationDbId);

        void ReportUpdated(string entity, string hash, int applicationDbId);

        void ReportError(string entity, string hash, string message);

        void ReportError(string entity, string hash, Exception exception);

        public void StartImport();

        public void StartImport(string entity);

        public void StopImport();

        public void StopImport(string entity);

        public void StartIdle();

        public void StopIdle();

        public void StartReadModelUpdate();

        public void StartReadModelUpdate(string entity);

        public void StopReadModelUpdate();

        public void StopReadModelUpdate(string entity);
    }
}