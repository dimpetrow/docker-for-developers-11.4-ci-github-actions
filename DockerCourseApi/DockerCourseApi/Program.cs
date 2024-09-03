using System.Data.SqlClient;
using Dapper;
using DockerCourseApi;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.Configure<Settings>(builder.Configuration);

//builder.Configuration[""]

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", async (IOptions<Settings> settings) =>
{
    var db = new SqlConnection(settings.Value.ConnectionString);

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
});
app.MapGet("/podcastsFull", async (IOptions<Settings> settings) =>
{
    var db = new SqlConnection(settings.Value.ConnectionString);

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts"));
});

app.Run();

public record Podcast(Guid Id, string Title);

public partial class Program
{
}
