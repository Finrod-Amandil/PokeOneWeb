﻿using PokeOneWeb.Data;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import
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

        public void StartImport();

        public void StopImport();

        public void StartImport(string entity);

        public void StopImport(string entity);

        public void StartIdle();

        public void StopIdle();

        public void StartReadModelUpdate();

        public void StopReadModelUpdate();

        public void StartReadModelUpdate(string entity);

        public void StopReadModelUpdate(string entity);
    }
}