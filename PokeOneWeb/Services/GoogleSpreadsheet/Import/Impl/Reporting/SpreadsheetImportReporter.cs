using System;
using System.Collections.Generic;
using PokeOneWeb.Data;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting
{
    public class SpreadsheetImportReporter : ISpreadsheetImportReporter
    {
        private SpreadsheetImportReport _report = new();
        private DateTime _importStart = DateTime.UtcNow;
        private DateTime _lastIdleStart = DateTime.UtcNow;
        private readonly Dictionary<string, DateTime> _entityImportStarts = new();
        private DateTime _readModelUpdateStart = DateTime.UtcNow;
        private readonly Dictionary<string, DateTime> _entityReadModelUpdateStarts = new();

        public void NewSession()
        {
            _report = new SpreadsheetImportReport();
        }

        public SpreadsheetImportReport GetReport()
        {
            return _report;
        }

        public void ReportDeleted(Entity entity, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                Entity = entity,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Delete
            });
        }

        public void ReportAdded(Entity entity, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                Entity = entity,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Create,
            });
        }

        public void ReportUpdated(Entity entity, string hash, int applicationDbId)
        {
            _report.Updates.Add(new ImportUpdate
            {
                Entity = entity,
                Hash = hash,
                ApplicationDbId = applicationDbId,
                DbAction = DbAction.Update
            });
        }

        public void ReportError(Entity entity, string hash, string message)
        {
            _report.Errors.Add(new ImportError
            {
                Entity = entity,
                Hash = hash,
                Message = message
            });
        }

        public void ReportError(Entity entity, string hash, Exception exception)
        {
            ReportError(entity, hash, $"{exception.GetType().Name}: {exception.Message}");
        }

        public void StartImport()
        {
            _importStart = DateTime.UtcNow;
        }

        public void StopImport()
        {
            _report.TotalImportTime = DateTime.UtcNow - _importStart;
        }

        public void StartImport(string entity)
        {
            _entityImportStarts.Add(entity, DateTime.UtcNow);
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

        public void StopReadModelUpdate()
        {
            _report.TotalReadModelUpdateTime = DateTime.UtcNow - _readModelUpdateStart;
        }

        public void StartReadModelUpdate(string entity)
        {
            _entityReadModelUpdateStarts.Add(entity, DateTime.UtcNow);
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
