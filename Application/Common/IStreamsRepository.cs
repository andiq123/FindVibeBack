namespace Application.Common;

public interface IStreamsRepository
{
    Task<string> GetStream(string songLink);
}