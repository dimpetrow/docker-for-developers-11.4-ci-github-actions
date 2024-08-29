using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace DockerCourseApi.Tests;

public class ApiTests
{
    [Fact]
    public async Task GivenGetRequestToPodcastsEndpoint_ShouldReturnOkay()
    {
        // Arrange
        var httpClient = new CustomWebApplicationFactory().CreateClient();

        // Act
        var response = await httpClient.GetAsync("/podcasts");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var expectedPodcasts = new[]
        {
            "Unhandled Exception Podcast",
            "Developer Weekly Podcast",
            "The Stack Overflow Podcast",
            "The Hanselminutes Podcast",
            "The .NET Rocks Podcast",
            "The Azure Podcast",
            "The AWS Podcast",
            "The Rabbit Hole Podcast",
            "The .NET Core Podcast"
        };

        var expectedPodcastsAnotherOrdering = new[]
        {
            "Unhandled Exception Podcast",
            "The Stack Overflow Podcast",
            "The Azure Podcast",
            "The Hanselminutes Podcast",
            "The .NET Rocks Podcast",
            "Developer Weekly Podcast",
            "The AWS Podcast",
            "The .NET Core Podcast",
            "The Rabbit Hole Podcast",
        };
        var actualpodcasts = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        // Ordering does not matter for .BeEquivalentTo
        actualpodcasts.Should().BeEquivalentTo(expectedPodcasts);
        actualpodcasts.Should().BeEquivalentTo(expectedPodcastsAnotherOrdering);
    }

    [Fact]
    public async Task GivenGetRequestToPodcastsFullEndpoint_ShouldReturnOkay()
    {
        // Arrange
        var httpClient = new CustomWebApplicationFactory().CreateClient();

        // Act
        var response = await httpClient.GetAsync("/podcastsFull");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var expectedPodcasts = new[]
        {
            new Podcast(Guid.Empty, "Unhandled Exception Podcast"),
            new Podcast(Guid.Empty, "Developer Weekly Podcast"),
            new Podcast(Guid.Empty, "The Stack Overflow Podcast"),
            new Podcast(Guid.Empty, "The Hanselminutes Podcast"),
            new Podcast(Guid.Empty, "The .NET Rocks Podcast"),
            new Podcast(Guid.Empty, "The Azure Podcast"),
            new Podcast(Guid.Empty, "The AWS Podcast"),
            new Podcast(Guid.Empty, "The Rabbit Hole Podcast"),
            new Podcast(Guid.Empty, "The .NET Core Podcast"),
        };

        var expectedPodcastsAnotherOrdering = new[]
        {
            new Podcast(Guid.Empty, "Unhandled Exception Podcast"),
            new Podcast(Guid.Empty, "The Stack Overflow Podcast"),
            new Podcast(Guid.Empty, "The Azure Podcast"),
            new Podcast(Guid.Empty, "The Hanselminutes Podcast"),
            new Podcast(Guid.Empty, "The .NET Rocks Podcast"),
            new Podcast(Guid.Empty, "Developer Weekly Podcast"),
            new Podcast(Guid.Empty, "The AWS Podcast"),
            new Podcast(Guid.Empty, "The .NET Core Podcast"),
            new Podcast(Guid.Empty, "The Rabbit Hole Podcast"),
        };
        var actualpodcasts = await response.Content.ReadFromJsonAsync<IEnumerable<Podcast>>();

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