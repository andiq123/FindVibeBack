namespace Application.Songs.Commands.Reorder;

public record Reorder
{
    public Guid SongId { get; set; }
    public int Order { get; set; }
}