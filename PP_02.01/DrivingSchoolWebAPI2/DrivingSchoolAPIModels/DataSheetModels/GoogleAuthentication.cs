using Google.Apis.Auth.OAuth2;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Класс, осуществляющий авторизацию клиента Api Google-таблиц
    /// </summary>
    public static class GoogleAuthentication
    {
        public static UserCredential Login(string googleClientId, string googleClientSecret, string[] scopes)
        {
            //ClientSecrets secrets = new ClientSecrets()
            //{
            //    ClientId = googleClientId,
            //    ClientSecret = googleClientSecret
            //};
            //return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None).Result;
            return LoginAsync(googleClientId, googleClientSecret, scopes).Result;
        }

        public static async Task<UserCredential> LoginAsync(string googleClientId, string googleClientSecret, string[] scopes)
        {
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None);
        }
    }
}
