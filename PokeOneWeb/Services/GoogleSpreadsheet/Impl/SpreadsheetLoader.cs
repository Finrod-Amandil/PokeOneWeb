using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public class SpreadsheetLoader : ISpreadsheetLoader
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string ApplicationName = "PokeOneWeb Guide Import Service";

        public async Task<Spreadsheet> LoadSpreadsheet(string spreadsheetId)
        {
            UserCredential credential;

            await using (var stream = new FileStream("Services/GoogleSpreadsheet/credentials.json", FileMode.Open, FileAccess.Read))
            {
                const string credPath = "Services/GoogleSpreadsheet/token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true));
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            var request = service.Spreadsheets.Get(spreadsheetId);
            request.IncludeGridData = true;

            var spreadsheet = await request.ExecuteAsync();

            return spreadsheet;
        }
    }
}
