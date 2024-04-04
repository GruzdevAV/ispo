namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Нужно лишь для удобного хранения данных для создания администратора по умолчанию
    /// </summary>
    public static class DefaultData
    {
        public const string InstructorPassword = "Pass_1";
        public const string AdminPassword = "Admin_Password1";
        public const string AdminName = "Admin";
        public const string AdminEmail = "admin@example.com";
        //public static string ClientId = "312116279547-gt582710b7mufl2d28vtpmq75p73e8fe.apps.googleusercontent.com";
        //public static string ClientSecret = "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";
        public static string ClientId = "312116279547-n4n004sb2dhn9ra4r4bbivuf9phb9bgm.apps.googleusercontent.com";
        public static string ClientSecret = "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";
        public static string[] Names = { "Jones", "Louis", "Gill", "Curtis", "Jamie", "Evans", "Jeremy", "Smith", "Eleanor", "Brown", "Timothy", "Stacey", "Amy", "Dorothy", "Crawford", "Mattie", "Washington", "Sara", "Nelson", "Miles", "Saunders", "Rodriguez", "Murphy", "Elaine", "Morgan", "Brooks", "Chambers", "Vivian", "Charlotte", "Mary", "Flores", "Angela", "Douglas", "Myers", "Jackson", "Williams", "Rodney", "Morales", "Ronnie" };
        public static string RandomName => Names[new Random().Next(Names.Length)];
        public static GoogleSheetApiClient ApiClient { get; set; } = new GoogleSheetApiClient(ClientId, ClientSecret);
        public static List<ApplicationUser> Users
        {
            get
            {
                var users = new List<ApplicationUser>()
                {
                    new ApplicationUser() {}
                };
                return users;
            }
        }
    }
}
