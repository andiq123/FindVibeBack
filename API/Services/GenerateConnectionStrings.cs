namespace API.Services;

public static class GenerateConnectionStrings
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Development")
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
        var connUrl = configuration["DATABASE_URL"];
        
        var pgHost = configuration["PGHost"];
        var pgDatabase = configuration["PGDatabase"];
        var pgUser = configuration["PGUser"];
        var pgPass = configuration["PGPassword"];
        var pgPort = configuration["PGPort"];
        return $"Server={pgHost};Port={pgPort};Database={pgDatabase};Userid={pgUser};Password={pgPass};SSL=true;SslMode=Require;";
    }
}