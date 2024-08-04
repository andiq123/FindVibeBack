using Domain.Songs;

namespace API.Songs;

public record SongsResponse(IReadOnlyList<SongDto> Songs);
