using System.Text.Json;
using Domain.Suggestions;

namespace Infrastructure.Suggestions.Services;
public class SuggestionsService(HttpClient httpClient)
{
    private const string BaseUrl = "https://clients1.google.com/complete/search?client=youtube&gs_ri=youtube&ds=yt&q=";


    public async Task<IReadOnlyList<Suggestion>> GetSuggestions(string searchQuery)
    {
        var fullLink = BaseUrl + searchQuery;
        var response = await httpClient.GetStringAsync(fullLink);
        var results = ParseGoogleResponse(response);
        
        return results.Select(result => new Suggestion(result)).ToList();
    }

    private IList<string> ParseGoogleResponse(string input)
    {
        var startIndex = input.IndexOf('[');
        var endIndex = input.LastIndexOf(']');

        if (startIndex == -1 || endIndex == -1)
        {
            return null;
        }

        var jsonString = input.Substring(startIndex, endIndex - startIndex + 1);
        var parsedArray = JsonSerializer.Deserialize<List<JsonElement>>(jsonString);

        return parsedArray[1].EnumerateArray().Select(item => item[0].GetString()).ToArray();
    }
}

