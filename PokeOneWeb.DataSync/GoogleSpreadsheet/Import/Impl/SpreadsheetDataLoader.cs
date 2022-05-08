﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.GoogleSpreadsheet.DataTypes;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class SpreadsheetDataLoader : ISpreadsheetDataLoader
    {
        private const string ApplicationName = "PokeOneWeb Guide Import Service";
        private const int MaxCallsPerMinute = 50;

        private static readonly string[] SCOPES = { SheetsService.Scope.Spreadsheets };
        private static readonly HashSet<DateTime> API_CALL_TIMESTAMPS = new();

        private readonly ISpreadsheetImportReporter _reporter;

        private UserCredential _credential;

        public SpreadsheetDataLoader(ISpreadsheetImportReporter reporter)
        {
            _reporter = reporter;
        }

        public async Task<List<Sheet>> LoadSheetList(string sheetListSpreadsheetId, string sheetListSheetName)
        {
            var ranges = new List<string> { $"{sheetListSheetName}!B2:C" };
            var values = await LoadData(sheetListSpreadsheetId, ranges);

            return values
                .Select(row => new Sheet
                {
                    SpreadsheetId = row[0] as string,
                    SheetName = row[1] as string
                })
                .ToList();
        }

        public async Task<string> LoadSheetHash(string spreadsheetId, string sheetName)
        {
            var ranges = new List<string> { $"{sheetName}!A1" };
            var values = await LoadData(spreadsheetId, ranges);

            return values[0][0].ToString();
        }

        public async Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId)
        {
            var ranges = new List<string> { $"{sheetName}!A2:B" };
            var values = await LoadData(spreadsheetId, ranges);

            var hashes = new List<RowHash>();

            var i = 0;
            foreach (var row in values)
            {
                i++;
                if (row.Count < 2)
                {
                    _reporter.ReportError($"Missing hashes in row {i} of sheet {sheetName}.");
                    continue;
                }

                hashes.Add(new RowHash
                {
                    Hash = row[0].ToString(),
                    IdHash = row[1].ToString(),
                    ImportSheetId = sheetId
                });
            }

            return hashes;
        }

        public async Task<List<SheetDataRow>> LoadDataRows(ImportSheet sheet, List<string> selectedIdHashes, List<string> allIdHashes)
        {
            var rowIndexes = GetRowIndexesForIdHashes(selectedIdHashes, allIdHashes);
            var ranges = GetRangesForRows(rowIndexes, sheet.SheetName, "A", true);

            var valuesRanges = await LoadData(sheet.SpreadsheetId, ranges);

            var columnNames = valuesRanges[0].Skip(2).Select(x => x.ToString() ?? string.Empty).ToList();
            valuesRanges.RemoveAt(0);

            var dataRows = new List<SheetDataRow>();

            foreach (var row in valuesRanges)
            {
                var rowHash = new RowHash
                {
                    IdHash = row[0].ToString(),
                    Hash = row[1].ToString(),
                    ImportSheetId = sheet.Id
                };

                var values = row.Skip(2).ToList();

                var dataRow = new SheetDataRow(columnNames, rowHash, values);
                dataRows.Add(dataRow);
            }

            return dataRows;
        }

        private async Task<List<List<object>>> LoadData(string spreadsheetId, List<string> ranges)
        {
            EnsureQuotaLimitNotReached();

            _credential ??= await GetCredentials();
            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = ApplicationName
            });

            var request = service.Spreadsheets.Values.BatchGet(spreadsheetId);

            request.Ranges = ranges;
            request.MajorDimension = SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum.ROWS;

            var result = await request.ExecuteAsync();

            var values = new List<List<object>>();

            foreach (var valueRange in result.ValueRanges)
            {
                values.AddRange(valueRange.Values.Select(row => row.ToList()));
            }

            return values;
        }

        private static async Task<UserCredential> GetCredentials()
        {
            await using var stream = new FileStream("GoogleSpreadsheet/credentials.json", FileMode.Open, FileAccess.Read);
            const string credPath = "GoogleSpreadsheet/token.json";
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets, SCOPES,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            return credential;
        }

        private static List<string> GetRangesForRows(List<int> rows, string sheetName, string startColumn, bool includeHeader)
        {
            var ranges = new List<string>();
            rows = rows
                .OrderBy(r => r)
                .Select(r => r + 2) // Offset 2: Header row and 0-indicated to 1-indicated indexes.
                .ToList();

            var startRow = rows[0];

            if (includeHeader)
            {
                ranges.Add($"{sheetName}!{startColumn}1:1");
            }

            for (var i = 0; i <= rows.Count; i++)
            {
                if (i == rows.Count || (i > 0 && rows[i] - rows[i - 1] > 1))
                {
                    ranges.Add($"{sheetName}!{startColumn}{startRow}:{rows[i - 1]}");

                    if (i < rows.Count)
                    {
                        startRow = rows[i];
                    }
                }
            }

            return ranges;
        }

        private void EnsureQuotaLimitNotReached()
        {
            while (true)
            {
                API_CALL_TIMESTAMPS.RemoveWhere(timeStamp => timeStamp < DateTime.UtcNow - TimeSpan.FromMinutes(1));

                if (API_CALL_TIMESTAMPS.Count < MaxCallsPerMinute)
                {
                    API_CALL_TIMESTAMPS.Add(DateTime.UtcNow);
                    return;
                }

                _reporter.StartIdle();
                Thread.Sleep(1000);
                _reporter.StopIdle();
            }
        }

        private static List<int> GetRowIndexesForIdHashes(IEnumerable<string> selectedHashes, IList<string> allHashes)
        {
            return selectedHashes.Select(allHashes.IndexOf).OrderBy(i => i).ToList();
        }
    }
}