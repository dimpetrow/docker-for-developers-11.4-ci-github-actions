using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace DockerCourseApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //Environment.SetEnvironmentVariable("DOCKERCOURSEAPI_ConnectionString", "Server=tcp:database;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("ConnectionString", "Server=tcp:localhost;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"),
            });
            configBuilder.AddEnvironmentVariables("DOCKERCOURSEAPI_");
        });
    }
}
