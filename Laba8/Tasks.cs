using System.Text;
using System.Text.Json;

namespace DotNet.Laba8;

// === Задание 1: Database First — классы-проекции таблиц ===
// Модели данных (аналогично классам-проекциям в EF Database First)
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Year { get; set; }

    public override string ToString() => $"[{Id}] {Title} ({Year}) — {Description}";
}

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int MovieId { get; set; }

    public override string ToString() => $"[{Id}] {Name} (фильм #{MovieId})";
}

// === Задание 2: Имитация контекста данных (DbContext) ===
public class AppContext
{
    public List<Movie> Movies { get; set; } = new();
    public List<Actor> Actors { get; set; } = new();

    public AppContext()
    {
        // Database.EnsureCreated() — гарантия, что данные существуют
        SeedData();
    }

    private void SeedData()
    {
        Movies.AddRange(new[]
        {
            new Movie { Id = 1, Title = "Интерстеллар", Description = "Научная фантастика", Year = 2014 },
            new Movie { Id = 2, Title = "Начало", Description = "Триллер", Year = 2010 },
            new Movie { Id = 3, Title = "Матрица", Description = "Киберпанк", Year = 1999 },
        });

        Actors.AddRange(new[]
        {
            new Actor { Id = 1, Name = "Мэттью МакКонахи", MovieId = 1 },
            new Actor { Id = 2, Name = "Энн Хэтэуэй", MovieId = 1 },
            new Actor { Id = 3, Name = "Леонардо ДиКаприо", MovieId = 2 },
            new Actor { Id = 4, Name = "Киану Ривз", MovieId = 3 },
        });
    }

    // LINQ-запросы к данным (аналог работы с DbSet)
    public void ShowAllMovies()
    {
        Console.WriteLine("\nВсе фильмы:");
        foreach (var m in Movies)
            Console.WriteLine($"  {m}");
    }

    public void ShowMoviesWithActors()
    {
        Console.WriteLine("\nФильмы с актёрами (аналог JOIN через LINQ):");
        var query = from m in Movies
                    join a in Actors on m.Id equals a.MovieId into actors
                    select new { Movie = m.Title, Actors = actors };

        foreach (var item in query)
        {
            Console.WriteLine($"  {item.Movie}:");
            foreach (var a in item.Actors)
                Console.WriteLine($"    - {a.Name}");
        }
    }
}

// === Задание 3: JSON сериализация и десериализация ===
public class JsonDemo
{
    public static string SerializeMovies(List<Movie> movies)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        return JsonSerializer.Serialize(movies, options);
    }

    public static List<Movie>? DeserializeMovies(string json)
    {
        return JsonSerializer.Deserialize<List<Movie>>(json);
    }

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: JSON сериализация ===");

        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "Интерстеллар", Description = "Научная фантастика", Year = 2014 },
            new() { Id = 2, Title = "Начало", Description = "Триллер", Year = 2010 },
        };

        string json = SerializeMovies(movies);
        Console.WriteLine("Сериализованный JSON:");
        Console.WriteLine(json);

        string path = "Laba8/movies.json";
        Directory.CreateDirectory("Laba8");
        File.WriteAllText(path, json, Encoding.UTF8);
        Console.WriteLine($"\nJSON сохранён в файл: {path}");

        string loadedJson = File.ReadAllText(path, Encoding.UTF8);
        var loadedMovies = DeserializeMovies(loadedJson);
        Console.WriteLine("\nДесериализованные данные:");
        foreach (var m in loadedMovies!)
            Console.WriteLine($"  {m}");
    }
}

// === Задание 4: Контекст данных и ORM - полный пример ===
public class OrmDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1-2: Контекст данных (ORM-подобный подход) ===");

        AppContext db = new();

        db.ShowAllMovies();
        db.ShowMoviesWithActors();

        Console.WriteLine("\n=== Задание 4: CRUD-операции ===");

        // CREATE
        var newMovie = new Movie { Id = 4, Title = "Дюна", Description = "Эпическая фантастика", Year = 2021 };
        db.Movies.Add(newMovie);
        Console.WriteLine($"\nДобавлен фильм: {newMovie.Title}");

        // READ
        var found = db.Movies.FirstOrDefault(m => m.Title.Contains("Интерстеллар"));
        Console.WriteLine($"Найден фильм: {found?.Title ?? "не найден"}");

        // UPDATE
        var toUpdate = db.Movies.FirstOrDefault(m => m.Id == 2);
        if (toUpdate != null)
        {
            toUpdate.Description = "Психологический триллер (обновлено)";
            Console.WriteLine($"Обновлён фильм: {toUpdate.Title} — {toUpdate.Description}");
        }

        // DELETE
        db.Movies.RemoveAll(m => m.Id == 3);
        Console.WriteLine("Удалён фильм с Id=3");

        db.ShowAllMovies();
    }
}
