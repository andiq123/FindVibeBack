namespace API.Services;

public static class GenerateConnectionStrings
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var host = configuration["DBHost"];
        var port = configuration["PortDB"];
        var database = configuration["Database"];
        var username = configuration["DbUser"];
        var password = configuration["Password"];
        //Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}; SSL Mode=Require; Trust Server Certificate=true
     return $"Server={host};Port={port};User Id={username};Password={password};Database={database}; SSL Mode=Require; Trust Server Certificate=true";
    }
}