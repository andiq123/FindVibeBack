using Application.Common;
using Infrastructure.Streams.Services;

namespace Infrastructure.Streams.Persistence;

public class StreamsRepository(StreamsService streamsService):IStreamsRepository
{
    public Task<string> GetStream(string songLink)
    {
        throw new NotImplementedException();
    }
}