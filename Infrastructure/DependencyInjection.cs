using Application.Common;
using Infrastructure.Common.Persistence;
using Infrastructure.Songs.Persistence;
using Infrastructure.Songs.Services;

using Infrastructure.Suggestions.Persistence;
using Infrastructure.Suggestions.Services;
using Infrastructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<FindVibeDbContext>(options => { options.UseNpgsql(connectionString); });
        services.AddHttpClient<SongsScrapperService>();
        services.AddHttpClient<SuggestionsService>();
        services.AddScoped<ISongsRepository, SongsRepository>();
        services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<FindVibeDbContext>());

        return services;
    }
}