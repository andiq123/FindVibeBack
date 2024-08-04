using AngleSharp;
using AngleSharp.Dom;
using Domain.Songs;

namespace Infrastructure.Songs.Services;

public class SongsScrapperService(HttpClient httpClient)
{
    private const string BaseUrl = "https://muzkan.net/?q=";
    private readonly IBrowsingContext _context = new BrowsingContext(Configuration.Default);

    public async Task<IReadOnlyList<Song>> ScrapeSongsByQueryAsync(string querySearch)
    {
        var fullLink = BaseUrl + querySearch;
        var response = await httpClient.GetStringAsync(fullLink);

        var document = await _context.OpenAsync((req) => req.Content(response));

        var songsWrapper = document.QuerySelector(".files__wrapper");

        return (from song in songsWrapper!.Children
                let artist = song?.QuerySelector("h4")?.Text()
                let title = song?.QuerySelector("h5")?.Text()
                let image = song?.QuerySelector("img")?.GetAttribute("data-src")
                let link = song?.QuerySelector("div[mp3source]")?.Attributes["mp3source"]?.Value
                select new Song() { Artist = artist, Title = title, Image = image, Link = link, Id = Guid.NewGuid() })
            .ToList();
    }
}