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

        void StartImport();

        void StartImport(string sheetName);

        void StopImport();

        void StopImport(string sheetName, int insertedCount, int updatedCount, int deletedCount);

        void StartIdle();

        void StopIdle();

        void StartReadModelUpdate();

        void StartReadModelUpdate(string entity);

        void StopReadModelUpdate();

        void StopReadModelUpdate(string entity);
    }
}