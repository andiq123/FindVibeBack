namespace API.Models;

public class Song
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Artist { get; init; }
    public string Image { get; init; }
    public int Order { get; set; }
    public string Link { get; init; }
}