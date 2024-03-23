using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.DataSync.Import.Interfaces;

namespace PokeOneWeb.DataSync.Import.Reporting
{
    public class SpreadsheetImportReporter : ISpreadsheetImportReporter
    {
        private readonly ILogger<SpreadsheetImportReporter> _logger;
        private readonly Dictionary<string, DateTime> _entityImportStarts = new();
        private readonly Dictionary<string, DateTime> _entityReadModelUpdateStarts = new();
        private SpreadsheetImportReport _report = new();
        private DateTime _importStart = DateTime.UtcNow;
        private DateTime _lastIdleStart = DateTime.UtcNow;
        private DateTime _readModelUpdateStart = DateTime.UtcNow;

        public SpreadsheetImportReporter(ILogger<SpreadsheetImportReporter> logger)
        {
            _logger = logger;
        }

        public void NewSession()
        {
            _report = new SpreadsheetImportReport();
        }

        public SpreadsheetImportReport GetReport()
        {
            return _report;
        }

        public void ReportError(string message)
        {
            ReportError(string.Empty, string.Empty, message);
        }

        public void ReportError(string entityTypeName, string hash, string message)
        {
            ReportError(entityTypeName, hash, message, "N/A");
        }

        public void ReportError(string entityTypeName, Exception exception)
        {
            ReportError(entityTypeName, string.Empty, exception);
        }

        public void ReportError(string entityTypeName, string hash, Exception exception)
        {
            ReportError(entityTypeName, hash, exception, "N/A");
        }

        public void ReportError(string entityTypeName, string hash, Exception exception, string entityName)
        {
            ReportError(entityTypeName, hash, $"{exception.GetType().Name}: {exception.Message}", entityName);
        }

        public void ReportError(string entityTypeName, string hash, string message, string entityName)
        {
            _report.Errors.Add(new ImportError
            {
                EntityTypeName = entityTypeName,
                EntityName = entityName,
                Hash = hash,
                Message = message
            });

            _logger.LogWarning(
                $"An error occurred while running Google Spreadsheet Import. " +
                $"Entity type: {entityTypeName}, " +
                $"Entity name: {entityName}, " +
                $"ID Hash: {hash}, " +
                $"Message: {message}");
        }

        public void StartImport()
        {
            _importStart = DateTime.UtcNow;
            _logger.LogInformation($"Starting Google Spreadsheet Import. Start Time: {_importStart}");
        }

        public void StartImport(string sheetName)
        {
            _entityImportStarts.Add(sheetName, DateTime.UtcNow);
            _logger.LogInformation($"Starting import of sheet {sheetName}.");
        }

        public void StopImport()
        {
            _report.TotalImportTime = DateTime.UtcNow - _importStart;
            _logger.LogInformation($"Finished Google Spreadsheet Import. Total Duration: {_report.TotalImportTime}");
        }

        public void StopImport(string sheetName, int insertedCount, int updatedCount, int deletedCount)
        {
            if (_entityImportStarts.ContainsKey(sheetName))
            {
                _report.ImportTimesBySheet.Add(sheetName, DateTime.UtcNow - _entityImportStarts[sheetName]);
                _logger.LogInformation(
                    $"Finished import of sheet {sheetName}. Duration: {_report.ImportTimesBySheet[sheetName]}\n" +
                    $"Inserted rows: {insertedCount}\n" +
                    $"Updated rows: {updatedCount}\n" +
                    $"Deleted rows: {deletedCount}\n");
            }
        }

        public void StartIdle()
        {
            _lastIdleStart = DateTime.UtcNow;
            _logger.LogInformation($">>> Idling to avoid hitting request quota. Total idle duration so far: {_report.TotalIdleTime}");
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