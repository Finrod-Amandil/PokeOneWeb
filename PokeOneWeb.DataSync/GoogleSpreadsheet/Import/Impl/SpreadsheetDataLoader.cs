using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Logging;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
{
    public class SpreadsheetDataLoader : ISpreadsheetDataLoader
    {
        private const string APPLICATION_NAME = "PokeOneWeb Guide Import Service";
        private const int MAX_CALLS_PER_MINUTE = 50;

        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private static readonly HashSet<DateTime> ApiCallTimestamps = new();

        private readonly ILogger<SpreadsheetDataLoader> _logger;
        private readonly ISpreadsheetImportReporter _reporter;

        private UserCredential _credential;

        public SpreadsheetDataLoader(
            ILogger<SpreadsheetDataLoader> logger,
            ISpreadsheetImportReporter reporter)
        {
            _logger = logger;
            _reporter = reporter;
        }

        public async Task<string> LoadSheetHash(string spreadsheetId, string sheetName)
        {
            var ranges = new List<string> { $"{sheetName}!A1" };
            var values = await LoadData(spreadsheetId, ranges);

            return values[0].Values[0][0].ToString();
        }

        public async Task<List<RowHash>> LoadHashes(string spreadsheetId, string sheetName, int sheetId)
        {
            var ranges = new List<string> { $"{sheetName}!A2:B" };
            var values = await LoadData(spreadsheetId, ranges);

            var hashes = new List<RowHash>();

            var i = 0;
            foreach (var row in values[0].Values)
            {
                i++;
                if (row.Count < 2)
                {
                    _logger.LogWarning($"Missing hashes in row {i} of sheet {sheetName}.");
                    continue;
                }

                hashes.Add(new RowHash
                {
                    ContentHash = row[0].ToString(),
                    IdHash = row[1].ToString(),
                    ImportSheetId = sheetId
                });
            }

            return hashes;
        }

        public async Task<List<List<object>>> LoadRows(string spreadsheetId, string sheetName, List<int> rows = null)
        {
            var ranges = rows != null ? GetRangesForRows(rows, sheetName) : new List<string> { $"{sheetName}!C2:ZZ" };
            var values = await LoadData(spreadsheetId, ranges);

            var valuesCollapsed = new List<List<object>>();

            foreach (var valueRange in values)
            {
                valuesCollapsed.AddRange(valueRange.Values.Select(row => row.ToList()));
            }

            return valuesCollapsed;
        }

        public async Task<List<List<object>>> LoadRange(string spreadsheetId, string sheetName, string range)
        {
            var ranges = new List<string> { range };
            var values = await LoadData(spreadsheetId, ranges);

            return values[0].Values.Select(row => row.ToList()).ToList();
        }

        private async Task<IList<ValueRange>> LoadData(string spreadsheetId, List<string> ranges)
        {
            EnsureQuotaLimitNotReached();

            _credential ??= await GetCredentials();
            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = APPLICATION_NAME
            });

            var request = service.Spreadsheets.Values.BatchGet(spreadsheetId);

            request.Ranges = ranges;
            request.MajorDimension = SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum.ROWS;

            var result = await request.ExecuteAsync();

            return result.ValueRanges;
        }

        private static async Task<UserCredential> GetCredentials()
        {
            await using var stream = new FileStream("GoogleSpreadsheet/credentials.json", FileMode.Open, FileAccess.Read);
            const string credPath = "GoogleSpreadsheet/token.json";
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets, Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            return credential;
        }

        private static List<string> GetRangesForRows(List<int> rows, string sheetName)
        {
            var ranges = new List<string>();
            rows = rows.OrderBy(r => r).ToList();
            var startRow = rows[0];

            for (var i = 0; i <= rows.Count; i++)
            {
                if (i == rows.Count || (i > 0 && rows[i] - rows[i - 1] > 1))
                {
                    ranges.Add($"{sheetName}!C{startRow + 2}:{rows[i - 1] + 2}");

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
                ApiCallTimestamps.RemoveWhere(timeStamp => timeStamp < DateTime.UtcNow - TimeSpan.FromMinutes(1));

                if (ApiCallTimestamps.Count < MAX_CALLS_PER_MINUTE)
                {
                    ApiCallTimestamps.Add(DateTime.UtcNow);
                    return;
                }

                _reporter.StartIdle();
                Thread.Sleep(1000);
                _reporter.StopIdle();
            }
        }
    }
}