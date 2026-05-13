using DotNet.Laba8;

namespace DotNet.Tests;

public class MovieTests
{
    [Fact]
    public void Properties_GetSet()
    {
        var movie = new Movie { Id = 1, Title = "Test", Description = "Desc", Year = 2020 };
        Assert.Equal(1, movie.Id);
        Assert.Equal("Test", movie.Title);
        Assert.Equal("Desc", movie.Description);
        Assert.Equal(2020, movie.Year);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var movie = new Movie { Id = 5, Title = "Matrix", Description = "Sci-Fi", Year = 1999 };
        Assert.Contains("[5]", movie.ToString());
        Assert.Contains("Matrix", movie.ToString());
        Assert.Contains("1999", movie.ToString());
        Assert.Contains("Sci-Fi", movie.ToString());
    }
}

public class ActorTests
{
    [Fact]
    public void Properties_GetSet()
    {
        var actor = new Actor { Id = 1, Name = "Keanu Reeves", MovieId = 5 };
        Assert.Equal(1, actor.Id);
        Assert.Equal("Keanu Reeves", actor.Name);
        Assert.Equal(5, actor.MovieId);
    }
}

public class AppContextTests
{
    [Fact]
    public void Constructor_SeedsMovies()
    {
        var db = new DotNet.Laba8.AppContext();
        Assert.Equal(3, db.Movies.Count);
    }

    [Fact]
    public void Constructor_SeedsActors()
    {
        var db = new DotNet.Laba8.AppContext();
        Assert.Equal(4, db.Actors.Count);
    }

    [Fact]
    public void Movies_ContainExpected()
    {
        var db = new DotNet.Laba8.AppContext();
        Assert.Contains(db.Movies, m => m.Title == "Интерстеллар");
        Assert.Contains(db.Movies, m => m.Id == 1 && m.Year == 2014);
    }

    [Fact]
    public void AddMovie_IncreasesCount()
    {
        var db = new DotNet.Laba8.AppContext();
        db.Movies.Add(new Movie { Id = 99, Title = "New", Description = "X", Year = 2025 });
        Assert.Equal(4, db.Movies.Count);
    }

    [Fact]
    public void RemoveMovie_DecreasesCount()
    {
        var db = new DotNet.Laba8.AppContext();
        db.Movies.RemoveAll(m => m.Id == 1);
        Assert.Equal(2, db.Movies.Count);
    }
}

public class JsonDemoTests
{
    [Fact]
    public void SerializeMovies_ReturnsNonEmptyJson()
    {
        var movies = new List<Movie> { new() { Id = 1, Title = "Test", Description = "D", Year = 2020 } };
        string json = JsonDemo.SerializeMovies(movies);
        Assert.NotNull(json);
        Assert.NotEmpty(json);
        Assert.Contains("Test", json);
    }

    [Fact]
    public void SerializeMovies_ContainsYear()
    {
        var movies = new List<Movie> { new() { Id = 1, Title = "X", Description = "X", Year = 2015 } };
        string json = JsonDemo.SerializeMovies(movies);
        Assert.Contains("2015", json);
    }

    [Fact]
    public void Roundtrip_PreservesData()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "Интерстеллар", Description = "Sci-Fi", Year = 2014 },
            new() { Id = 2, Title = "Начало", Description = "Thriller", Year = 2010 },
        };
        string json = JsonDemo.SerializeMovies(movies);
        var deserialized = JsonDemo.DeserializeMovies(json);
        Assert.NotNull(deserialized);
        Assert.Equal(2, deserialized!.Count);
        Assert.Equal("Интерстеллар", deserialized[0].Title);
        Assert.Equal(2014, deserialized[0].Year);
    }
}
