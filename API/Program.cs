using API.Hubs;
using API.Services;
using Application;
using Infrastructure;
using Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    var connectionString = GenerateConnectionStrings.GetConnectionString(builder.Configuration);
    builder.Services.AddApplication().AddInfrastructure(connectionString);
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins(builder.Configuration["FrontURL"] ?? string.Empty)
                .AllowAnyMethod()
                .AllowAnyHeader().AllowCredentials();
        });
    });

    builder.Services.AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    });
}

var app = builder.Build();
{
    var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FindVibeDbContext>();
    await context.Database.MigrateAsync();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.MapHub<PlayerHub>("/player");

    await app.RunAsync();
}