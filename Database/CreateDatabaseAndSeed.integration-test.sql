CREATE DATABASE podcasts
GO
    USE podcasts
GO
    CREATE TABLE Podcasts (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Title NVARCHAR(MAX) NOT NULL
    )
GO
INSERT INTO
    Podcasts (Title)
VALUES
    ('Unhandled Exception Podcast'),
    ('Developer Weekly Podcast'),
    ('The Stack Overflow Podcast'),
    ('The Hanselminutes Podcast'),
    ('The .NET Rocks Podcast'),
    ('The Azure Podcast'),
    ('The AWS Podcast'),
    ('The Rabbit Hole Podcast2'),
    ('The .NET Core Podcast2'),
    ('The Azure Podcast2'),
    ('The AWS Podcast2'),
    ('The Rabbit Hole Podcast3'),
    ('The .NET Core Podcast3'),
    ('The Azure Podcast3'),
    ('The AWS Podcast3'),
    ('The Rabbit Hole Podcast3'),
    ('The .NET Core Podcast3');

GO