using Domain.Suggestions;

namespace Application.Common;

public interface ISuggestionsRepository
{
    Task<IReadOnlyList<Suggestion>> GetSuggestionsAsync(string searchQuery);
}