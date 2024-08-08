namespace Infrastructure.Streams.Services;

public class StreamsService(HttpClient client)
{
    public  Task<string> GetStream(string songLink)
    {
        const string outputPath = "temp/file.mp3";
        return Task.FromResult(outputPath);
    }
}