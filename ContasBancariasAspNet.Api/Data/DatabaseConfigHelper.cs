public static class DatabaseConfigHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Development")
        {
            var dbServer = Environment.GetEnvironmentVariable("DB_DEV_SERVER");
            var dbName = Environment.GetEnvironmentVariable("DB_DEV_NAME");

            return $"Server={dbServer};Database={dbName};Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true";
            // Ou, se usar usuário/senha:
            // return $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=true";
        }
        else // Production
        {
            var dbServer = Environment.GetEnvironmentVariable("DB_PROD_SERVER");
            var dbName = Environment.GetEnvironmentVariable("DB_PROD_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_PROD_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PROD_PASSWORD");

            return $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=true";
        }
    }
}