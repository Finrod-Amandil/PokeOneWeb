using System;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetImportReporter
    {
        void NewSession();

        SpreadsheetImportReport GetReport();

        void ReportError(string message);

        void ReportError(string entityName, Exception exception);

        void ReportError(string entityName, string hash, string message);

        void ReportError(string entityName, string hash, Exception exception);

        public void StartImport();

        public void StartImport(string sheetName);

        public void StopImport();

        public void StopImport(string sheetName, int insertedCount, int updatedCount, int deletedCount);

        public void StartIdle();

        public void StopIdle();

        public void StartReadModelUpdate();

        public void StartReadModelUpdate(string entity);

        public void StopReadModelUpdate();

        public void StopReadModelUpdate(string entity);
    }
}