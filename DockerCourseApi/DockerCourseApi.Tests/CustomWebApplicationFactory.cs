//using Ductus.FluentDocker.Builders;
//using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace DockerCourseApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IDisposable//, IAsyncLifetime
{

    //private readonly string _databaseIntegrationTestPort;
    //private readonly ICompositeService _compositeService;

    //public CustomWebApplicationFactory()
    //{
    //    //var DOCKER_COMPOSE_YAML = "docker-compose.yaml";
    //    //var DOCKER_COMPOSE_INTEGRATION_TEST_OVERRIDE_YAML = "docker-compose.integration-test.override.yaml";

    //    //var dockerComposeDirectory = GetDirectoryPath(Directory.GetCurrentDirectory(), DOCKER_COMPOSE_YAML);
    //    //var composeFile = Path.Combine(dockerComposeDirectory, DOCKER_COMPOSE_YAML);
    //    //var composeIntegrationTestOverrideFile = Path.Combine(dockerComposeDirectory, DOCKER_COMPOSE_INTEGRATION_TEST_OVERRIDE_YAML);

    //    //_databaseIntegrationTestPort = GetDatabaseIntegrationTestPort();

    //    //var portEnvVarNameValue = $"{DATABASE_INTEGRATION_TEST_PORT_ENVVAR_NAME}:{_databaseIntegrationTestPort}";
    //    //_compositeService = new Builder()
    //    //    .UseContainer()
    //    //    .UseCompose()
    //    //    .FromFile(composeFile, composeIntegrationTestOverrideFile)
    //    //    .WithEnvironment(portEnvVarNameValue)
    //    //    .Build();

    //    //_compositeService.Start();
    //}

    //public async Task InitializeAsync()
    //{
    //    int i = 0;
    //    var containerStatus = "";
    //    do
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(1));

    //        var seedContainer = _compositeService.Containers.FirstOrDefault(c => c.Name == "database-seed-integration-test");
    //        if (seedContainer is null)
    //        {
    //            throw new Exception("No seed container");
    //        }

    //        var container = seedContainer.GetConfiguration(true);
    //        containerStatus = container.State.Status;
    //    } while (i++ < 55
    //            && !containerStatus.Equals("exited", StringComparison.OrdinalIgnoreCase) 
    //            //&& seedContainerState != ServiceRunningState.Stopped
    //            );
    //}

    //public new void Dispose() => _compositeService.Dispose();
    //Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var db = GetDatabaseIntegrationTestDb();
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>(
                    "ConnectionString",
                    $"Server=tcp:{db/*_databaseIntegrationTestPort*/};Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"
                    ),
            });
        });
    }

    private static string GetDatabaseIntegrationTestDb()
    {
        var dbEnvVar = Environment.GetEnvironmentVariable("DOCKERCOURSEAPI_DATABASE_INTEGRATION_TEST_DB");
        var db = (!string.IsNullOrEmpty(dbEnvVar) ? dbEnvVar : "localhost,1434");
        return db;
    }

    //private static string GetDirectoryPath(string path, string searchPattern)
    //{
    //    return GetDirectoryPath(Directory.Exists(path) ? new DirectoryInfo(path) : null, searchPattern);
    //}

    //private static string GetDirectoryPath(DirectoryInfo path, string searchPattern)
    //{
    //    if (path != null)
    //    {
    //        return path.EnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly).Any() ? path.FullName : GetDirectoryPath(path.Parent, searchPattern);
    //    }

    //    var message = $"Cannot find '{searchPattern}' and resolve the base directory in the directory tree. '{path?.FullName}'";
    //    throw new DirectoryNotFoundException(message);
    //}
}
