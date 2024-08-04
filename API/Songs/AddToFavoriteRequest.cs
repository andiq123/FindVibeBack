using Domain.Songs;

namespace API.Songs;

public record AddToFavoriteRequest(string Title, string Artist, string Image, string Link,Guid Id, Guid UserId);