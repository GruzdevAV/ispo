using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Api-клиент для подключения к Google-таблицам
    /// </summary>
    public class GoogleSheetApiClient
    {
        string GoogleClientId;
        string GoogleClientSecret;
        string[] scopes = { SheetsService.Scope.Spreadsheets };
        UserCredential credential;
        public GoogleSheetsManager Manager { get; set; }
        public GoogleSheetApiClient(string googleClientId, string googleClientSecret)
        {
            GoogleClientId=googleClientId;
            GoogleClientSecret=googleClientSecret;
            credential = GoogleAuthentication.Login(googleClientId, googleClientSecret, scopes);
            Manager = new GoogleSheetsManager(credential);
        }
    }
}
