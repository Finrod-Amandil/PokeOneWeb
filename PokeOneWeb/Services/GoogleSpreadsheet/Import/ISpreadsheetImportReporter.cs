﻿using System;
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