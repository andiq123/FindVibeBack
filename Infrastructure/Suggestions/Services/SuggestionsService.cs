using System.Xml;
using Domain.Suggestions;

namespace Infrastructure.Suggestions.Services;

public class SuggestionsService(HttpClient httpClient)
{
    private const string BaseUrl = "https://suggestqueries.google.com/complete/search?output=toolbar&ds=yt&q=";


    public async Task<IReadOnlyList<Suggestion>> GetSuggestions(string searchQuery)
    {
        var fullLink = BaseUrl + searchQuery;
        var response = await httpClient.GetStringAsync(fullLink);
        
        XmlDocument _xmlDocument = new XmlDocument();
        _xmlDocument.LoadXml(response);

        var suggestions = _xmlDocument.SelectNodes("//CompleteSuggestion/suggestion");

        var suggestionsList = new List<Suggestion>();
        foreach (XmlNode suggestion in suggestions!)
        {
            var suggestionModel = new Suggestion(suggestion.Attributes!["data"]!.Value);
            suggestionsList.Add(suggestionModel);
        }

        return suggestionsList;
    }
}