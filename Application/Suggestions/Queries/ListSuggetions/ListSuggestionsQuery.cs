using Domain.Suggestions;
using ErrorOr;
using MediatR;

namespace Application.Suggetions.Queries.ListSuggetions;

public record ListSuggestionsQuery(string SearchQuery) : IRequest<ErrorOr<IReadOnlyList<Suggestion>>>;