using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public class GoogleSpreadsheetService : IGoogleSpreadsheetService
    {
        private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private static string ApplicationName = "PokeOneWeb Guide Import Service";

        public async Task<List<Location>> ReadLocations()
        {
            UserCredential credential;

            using (var stream = new FileStream("Services/GoogleSpreadsheet/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "Services/GoogleSpreadsheet/token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true));
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            var spreadsheetId = "1MDHewtqu5ABMW1oi7SEIXwxsZ_gtooDsA4f6l85R3w8";
            var request = service.Spreadsheets.Get(spreadsheetId);
            request.IncludeGridData = true;

            var spreadsheet = request.Execute();

            
        }
    }
}
