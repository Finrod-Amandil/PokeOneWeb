using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;

namespace PokeOneWeb.DataSync.Import
{
    public static class GoogleCloudAuthentication
    {
        public static ICredential GetCredential()
        {
            string[] scopes = { SheetsService.Scope.Spreadsheets };

            using var stream = new FileStream("gcloud_credentials.json", FileMode.Open, FileAccess.Read);
            var credential = (ServiceAccountCredential)GoogleCredential.FromStream(stream).UnderlyingCredential;

            var initializer = new ServiceAccountCredential.Initializer(credential.Id)
            {
                Key = credential.Key,
                Scopes = scopes
            };

            return new ServiceAccountCredential(initializer);
        }
    }
}