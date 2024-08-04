using Application.Common;
using Application.Suggetions.Queries.ListSuggetions;
using Domain.Suggestions;
using ErrorOr;
using MediatR;

namespace Application.Suggestions.Queries.ListSuggetions;

public class ListSuggestionsQueryHandler(ISuggestionsRepository suggestionsRepository) :IRequestHandler<ListSuggestionsQuery, ErrorOr<IReadOnlyList<Suggestion>>>
{
    public async Task<ErrorOr<IReadOnlyList<Suggestion>>> Handle(ListSuggestionsQuery request, CancellationToken cancellationToken)
    {
        var suggestions = await suggestionsRepository.GetSuggestionsAsync(request.SearchQuery);

        if (!suggestions.Any())
        {
            return Error.NotFound();
        }

        return suggestions.ToList();
    }
}