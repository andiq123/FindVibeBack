using API.Suggestions;
using Application.Suggetions.Queries.ListSuggetions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SuggestionsController(ISender mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSuggestions([FromQuery]SuggestionsRequest request)
    {
        var listSuggestionsQuery = new ListSuggestionsQuery(request.SearchQuery);

        var suggestionsResult = await mediator.Send(listSuggestionsQuery);

        return suggestionsResult.MatchFirst<IActionResult>(
            suggestions => Ok(new SuggestionsResponse(suggestions.Select(x=>x.Result).ToList())),
            error => NotFound()
        );
    }
}