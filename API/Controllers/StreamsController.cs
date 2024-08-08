using System.Net.Http.Headers;
using API.Streams;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StreamsController(HttpClient client) : ApiController
{
    [HttpGet("{*url}")]
    public async Task<IActionResult> GetWebContentStream(string url)
    {
        try
        {
            // Adding common headers that a browser might include
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            var content = await response.Content.ReadAsStreamAsync();
            return Ok(content);

        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Request failed: {ex.Message}");
        }
    }
}