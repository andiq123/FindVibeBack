namespace API.Songs;

public class ReorderRequest
{
    public IList<Application.Songs.Commands.Reorder.Reorder> Reorders { get; set; }
}