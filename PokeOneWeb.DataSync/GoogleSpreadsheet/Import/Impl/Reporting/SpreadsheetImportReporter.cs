using System;
using System.Collections.Generic;
using PokeOneWeb.Data;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class SpreadsheetImportReporter : ISpreadsheetImportReporter
    {
        private readonly Dictionary<string, DateTime> _entityImportStarts = new();
        private readonly Dictionary<string, DateTime> _entityReadModelUpdateStarts = new();
        private SpreadsheetImportReport _report = new();
        private DateTime _importStart = DateTime.UtcNow;
        private DateTime _lastIdleStart = DateTime.UtcNow;
        private DateTime _readModelUpdateStart = DateTime.UtcNow;

        public void NewSession()
        {
            _report = new SpreadsheetImportReport();
        }

        public SpreadsheetImportReport GetReport()
        {
            return _report;
        }

        public void ReportDeleted(string entityName, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                EntityName = entityName,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Delete
            });
        }

        public void ReportAdded(string entityName, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                EntityName = entityName,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Create,
            });
        }

        public void ReportUpdated(string entityName, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                EntityName = entityName,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Update
            });
        }

        public void ReportError(string entityName, string hash, string message)
        {
            _report.Errors.Add(new ImportError
            {
                EntityName = entityName,
                Hash = hash,
                Message = message
            });
        }

        public void ReportError(string entityName, string hash, Exception exception)
        {
            ReportError(entityName, hash, $"{exception.GetType().Name}: {exception.Message}");
        }

        public void StartImport()
        {
            _importStart = DateTime.UtcNow;
        }

        public void StartImport(string entity)
        {
            _entityImportStarts.Add(entity, DateTime.UtcNow);
        }

        public void StopImport()
        {
            _report.TotalImportTime = DateTime.UtcNow - _importStart;
        }

        public void StopImport(string entity)
        {
            if (_entityImportStarts.ContainsKey(entity))
            {
                _report.ImportTimesByEntity.Add(entity, DateTime.UtcNow - _entityImportStarts[entity]);
            }
        }

        public void StartIdle()
        {
            _lastIdleStart = DateTime.UtcNow;
        }

        public void StopIdle()
        {
            _report.TotalIdleTime += DateTime.UtcNow - _lastIdleStart;
        }

        public void StartReadModelUpdate()
        {
            _readModelUpdateStart = DateTime.UtcNow;
        }

        public void StartReadModelUpdate(string entity)
        {
            _entityReadModelUpdateStarts.Add(entity, DateTime.UtcNow);
        }

        public void StopReadModelUpdate()
        {
            _report.TotalReadModelUpdateTime = DateTime.UtcNow - _readModelUpdateStart;
        }

        public void StopReadModelUpdate(string entity)
        {
            if (_entityReadModelUpdateStarts.ContainsKey(entity))
            {
                _report.ReadModelUpdateTimesByEntity.Add(entity, DateTime.UtcNow - _entityReadModelUpdateStarts[entity]);
            }
        }
    }
}