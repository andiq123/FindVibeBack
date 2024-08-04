namespace API.Services;

public static class GenerateConnectionStrings
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (string.IsNullOrEmpty(connUrl))
        {
            connUrl = configuration["DATABASE_URL"];
        }

        Console.WriteLine(connUrl);

        // Parse connection URL to connection string for Npgsql
        connUrl = connUrl.Replace("postgres://", string.Empty);
        var pgUserPass = connUrl.Split('@')[0];
        var pgHostPortDb = connUrl.Split('@')[1];
        var pgHostPort = pgHostPortDb.Split('/')[0];
        var pgDb = pgHostPortDb.Split('/')[1];
        var pgUser = pgUserPass.Split(':')[0];
        var pgPass = pgUserPass.Split(':')[1];
        var pgHost = pgHostPort.Split(':')[0];
        var pgPort = pgHostPort.Split(':')[1];
        
        return
            $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
    }
}