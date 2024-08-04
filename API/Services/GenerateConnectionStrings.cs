namespace API.Services;

public static class GenerateConnectionStrings
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var host = configuration["DBHost"];
        var port = configuration["Port"];
        var database = configuration["Database"];
        var username = configuration["DbUser"];
        var password = configuration["Password"];
        return $"Host={host};Port={port};Username={username};Password={password};Database={database};";
    }
}