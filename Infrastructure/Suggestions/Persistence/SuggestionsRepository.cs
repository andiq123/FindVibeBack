using Application.Common;
using Domain.Suggestions;
using Infrastructure.Suggestions.Services;

namespace Infrastructure.Suggestions.Persistence;

public class SuggestionsRepository(SuggestionsService suggestionsService) :ISuggestionsRepository
{
    public async Task<IReadOnlyList<Suggestion>> GetSuggestionsAsync(string searchQuery)
    {
        return await suggestionsService.GetSuggestions(searchQuery);
    }
}