using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace DockerCourseApi.Tests;

public class ApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly string[] ExpectedPodcasts = new[]
    {
        "Unhandled Exception Podcast",
        "Developer Weekly Podcast",
        "The Stack Overflow Podcast",
        "The Hanselminutes Podcast",
        "The .NET Rocks Podcast",
        "The Azure Podcast",
        "The AWS Podcast",
        "The Rabbit Hole Podcast2",
        "The .NET Core Podcast2",
        "The Azure Podcast2",
        "The AWS Podcast2",
        "The Rabbit Hole Podcast3",
        "The .NET Core Podcast3",
        "The Azure Podcast3",
        "The AWS Podcast3",
        "The Rabbit Hole Podcast3",
        "The .NET Core Podcast3",
    };
    private readonly string[] ExpectedPodcastsAnotherOrdering = new[]
    {
        "Unhandled Exception Podcast",
        "The Hanselminutes Podcast",
        "Developer Weekly Podcast",
        "The Stack Overflow Podcast",
        "The AWS Podcast",
        "The .NET Rocks Podcast",
        "The Rabbit Hole Podcast2",
        "The .NET Core Podcast2",
        "The Rabbit Hole Podcast3",
        "The Azure Podcast2",
        "The .NET Core Podcast3",
        "The Rabbit Hole Podcast3",
        "The Azure Podcast",
        "The AWS Podcast2",
        "The .NET Core Podcast3",
        "The AWS Podcast3",
        "The Azure Podcast3",
    };

    private readonly HttpClient _httpClient;

    public ApiTests(CustomWebApplicationFactory _factory)
    {
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task GivenGetRequestToPodcastsEndpoint_ShouldReturnOkay()
    {
        // Act
        var response = await _httpClient.GetAsync("/podcasts");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var actualpodcasts = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        // Ordering does not matter for .BeEquivalentTo
        actualpodcasts.Should().BeEquivalentTo(ExpectedPodcasts);
        actualpodcasts.Should().BeEquivalentTo(ExpectedPodcastsAnotherOrdering);
    }

    [Fact]
    public async Task GivenGetRequestToPodcastsFullEndpoint_ShouldReturnOkay()
    {
        // Act
        var response = await _httpClient.GetAsync("/podcastsFull");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var actualpodcasts = await response.Content.ReadFromJsonAsync<IEnumerable<Podcast>>();

        var expectedPodcasts = ExpectedPodcasts.Select(p => new Podcast(Guid.Empty, p)).ToArray();
        var expectedPodcastsAnotherOrdering = ExpectedPodcastsAnotherOrdering.Select(p => new Podcast(Guid.Empty, p)).ToArray();
        actualpodcasts.Should().BeEquivalentTo(expectedPodcasts, equivalencyAssertionOptions => equivalencyAssertionOptions.Excluding(p => p.Id));
        // All of the bellow variants also work the same as the above one. .Excluding would add member Id to the current object
        //actualpodcasts.Should().BeEquivalentTo(expectedPodcasts, equivalencyAssertionOptions =>
        //{
        //    var latestOptions = equivalencyAssertionOptions.Excluding(p => p.Id); 
        //    return latestOptions;
        //});
        //actualpodcasts.Should().BeEquivalentTo(expectedPodcasts, equivalencyAssertionOptions =>
        //{
        //    var latestOptions = equivalencyAssertionOptions.Excluding(p => p.Id);
        //    return equivalencyAssertionOptions;
        //});
        //actualpodcasts.Should().BeEquivalentTo(expectedPodcasts, equivalencyAssertionOptions =>
        //{
        //    return equivalencyAssertionOptions.Excluding(p => p.Id);
        //});

        // Would fail due to IDs not matching
        //actualpodcasts.Should().BeEquivalentTo(expectedPodcasts);

        // Ordering does not matter for .BeEquivalentTo with option overrides either
        actualpodcasts.Should().BeEquivalentTo(expectedPodcastsAnotherOrdering, equivalencyAssertionOptions => equivalencyAssertionOptions.Excluding(p => p.Id));
    }
}