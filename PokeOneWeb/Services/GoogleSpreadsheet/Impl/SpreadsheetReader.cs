using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public abstract class SpreadsheetReader<T> : ISpreadsheetReader<T> where T : ISpreadsheetDto
    {
        private readonly ILogger _logger;

        protected SpreadsheetReader(ILogger logger)
        {
            _logger = logger;
        }

        public virtual IEnumerable<T> Read(Spreadsheet spreadsheet, string sheetPrefix)
        {
            var sheets = spreadsheet.Sheets
                .Where(s => s.Properties.Title?.ToLowerInvariant().StartsWith(sheetPrefix.ToLowerInvariant()) ?? false)
                .ToList();

            _logger.LogDebug($"Reading data of type {typeof(T).FullName} from Google Spreadsheet. Sheets: {string.Join(',', sheets.Select(s => s.Properties.Title))}");

            var records = new List<T>();

            foreach (var sheet in sheets)
            {
                var rowData = sheet.Data[0].RowData;
                var rowIndex = 1;

                while (rowIndex < rowData.Count)
                {
                    try
                    {
                        var record = ReadRow(rowData[rowIndex]);
                        records.Add(record);
                    }
                    catch (InvalidRowDataException ex)
                    {
                        //Log and skip record.
                        _logger.LogWarning($"Encountered invalid record of type {typeof(T).FullName}. {ex}.");
                    }

                    rowIndex++;
                }
            }

            if (!records.Any())
            {
                _logger.LogWarning($"No valid data of type {typeof(T).FullName} was found. Did not read any data.");
            }

            _logger.LogDebug($"Read {records.Count} data entries.");
            return records;
        }

        /// <summary>
        /// Maps the data of a single row of sheet data into the corresponding DTO.
        /// </summary>
        /// <param name="rowData">The data of the current row of the sheet</param>
        /// <returns>The DTO containing the values of the given row.</returns>
        protected abstract T ReadRow(RowData rowData);
    }
}
