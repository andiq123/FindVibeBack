namespace API.Services;

public static class GenerateConnectionStrings
{
    public static string GetConnectionString(ConfigurationManager configurationManager)
    {
        var host = configurationManager.GetSection("ConnectionStrings")["Hostname"];
        var port = configurationManager.GetSection("ConnectionStrings")["Port"];
        var database = configurationManager.GetSection("ConnectionStrings")["Database"];
        var username = configurationManager.GetSection("ConnectionStrings")["Username"];
        var password = configurationManager.GetSection("ConnectionStrings")["Password"];
        return $"Host={host};Port={port};Username={username};Password={password};Database={database};";
    }
}