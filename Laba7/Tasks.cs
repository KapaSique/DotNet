using System.Text;

namespace DotNet.Laba7;

public class Student
{
    public int Id { get; set; }
    public string Surname { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Stipend { get; set; }
    public int Kurs { get; set; }
    public string City { get; set; } = "";

    public override string ToString() =>
        $"{Surname} {Name}, стипендия={Stipend}, курс={Kurs}, город={City}";
}

public class Course
{
    public int StudentId { get; set; }
    public string Subject { get; set; } = "";
    public int Grade { get; set; }
}

// === Задание 1: Базовые LINQ запросы (интегрированный синтаксис) ===
public class LinqQuerySyntax
{
    public static List<Student> GetStudents() => new()
    {
        new() { Id = 1, Surname = "Иванов", Name = "Петр", Stipend = 2500, Kurs = 3, City = "Москва" },
        new() { Id = 2, Surname = "Васильев", Name = "Иван", Stipend = 2000, Kurs = 4, City = "Москва" },
        new() { Id = 3, Surname = "Смирнова", Name = "Анна", Stipend = 3000, Kurs = 2, City = "Казань" },
        new() { Id = 4, Surname = "Козлов", Name = "Дмитрий", Stipend = 1800, Kurs = 3, City = "Москва" },
        new() { Id = 5, Surname = "Петров", Name = "Сергей", Stipend = 2800, Kurs = 5, City = "Казань" },
        new() { Id = 6, Surname = "Федорова", Name = "Мария", Stipend = 2200, Kurs = 3, City = "Москва" },
    };

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: LINQ запросы (интегрированный синтаксис) ===");

        var students = GetStudents();
        Console.WriteLine("Исходная коллекция студентов:");
        foreach (var s in students)
            Console.WriteLine($"  {s}");

        // Where + Select — студенты из Москвы
        Console.WriteLine("\n--- Студенты из Москвы (стипендия > 2000) ---");
        var moscowRich = from s in students
                         where s.City == "Москва" && s.Stipend > 2000
                         select new { s.Surname, s.Name, s.Stipend };

        foreach (var s in moscowRich)
            Console.WriteLine($"  {s.Surname} {s.Name} - {s.Stipend} руб.");

        // OrderBy — сортировка по стипендии
        Console.WriteLine("\n--- Сортировка по стипендии (убывание) ---");
        var sorted = from s in students
                     orderby s.Stipend descending
                     select s;

        foreach (var s in sorted)
            Console.WriteLine($"  {s.Surname} {s.Name} - {s.Stipend} руб.");
    }
}

// === Задание 2: Лямбда-выражения и методы расширения LINQ ===
public class LinqLambdaSyntax
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Лямбда-выражения и методы расширения ===");

        var students = LinqQuerySyntax.GetStudents();

        // Where с лямбдой
        Console.WriteLine("\nСтуденты 3-го курса:");
        var kurs3 = students.Where(s => s.Kurs == 3);
        foreach (var s in kurs3)
            Console.WriteLine($"  {s}");

        // Select с анонимным типом
        Console.WriteLine("\nПроекция: только фамилия и стипендия:");
        var projection = students.Select(s => new { s.Surname, s.Stipend, Bonus = s.Stipend * 0.1m });
        foreach (var s in projection)
            Console.WriteLine($"  {s.Surname} - стипендия: {s.Stipend}, надбавка 10%: {s.Bonus}");

        // Aggregate — средняя стипендия
        var avgStipend = students.Average(s => s.Stipend);
        Console.WriteLine($"\nСредняя стипендия: {avgStipend:F2} руб.");

        // Count с условием
        var moscowCount = students.Count(s => s.City == "Москва");
        Console.WriteLine($"Студентов из Москвы: {moscowCount}");
    }
}

// === Задание 3: Группировка (GroupBy) ===
public class LinqGrouping
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: Группировка (GroupBy) ===");

        var students = LinqQuerySyntax.GetStudents();

        // Группировка по городу
        Console.WriteLine("\nГруппировка по городу:");
        var byCity = from s in students
                     group s by s.City into g
                     select new { City = g.Key, Count = g.Count(), AvgStipend = g.Average(s => s.Stipend) };

        foreach (var g in byCity)
            Console.WriteLine($"  {g.City}: {g.Count} студ., средняя стипендия {g.AvgStipend:F2} руб.");

        // Группировка по курсу (лямбда)
        Console.WriteLine("\nГруппировка по курсу:");
        var byKurs = students.GroupBy(s => s.Kurs)
                             .Select(g => new { Kurs = g.Key, Students = g.ToList() });

        foreach (var g in byKurs)
        {
            Console.WriteLine($"  Курс {g.Kurs}:");
            foreach (var s in g.Students)
                Console.WriteLine($"    - {s.Surname} {s.Name}");
        }
    }
}

// === Задание 4: Join (соединение коллекций) ===
public class LinqJoin
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Join (соединение коллекций) ===");

        var students = LinqQuerySyntax.GetStudents();
        var courses = new List<Course>
        {
            new() { StudentId = 1, Subject = "Математика", Grade = 5 },
            new() { StudentId = 1, Subject = "Физика", Grade = 4 },
            new() { StudentId = 2, Subject = "Математика", Grade = 3 },
            new() { StudentId = 3, Subject = "Математика", Grade = 5 },
            new() { StudentId = 3, Subject = "Программирование", Grade = 5 },
            new() { StudentId = 5, Subject = "Физика", Grade = 4 },
        };

        Console.WriteLine("\nСоединение студентов и оценок:");
        var joined = from s in students
                     join c in courses on s.Id equals c.StudentId
                     select new { s.Surname, s.Name, c.Subject, c.Grade };

        foreach (var item in joined)
            Console.WriteLine($"  {item.Surname} {item.Name} — {item.Subject}: {item.Grade}");

        // GroupJoin — студенты с их оценками
        Console.WriteLine("\nGroupJoin — все оценки каждого студента:");
        var groupJoined = from s in students
                          join c in courses on s.Id equals c.StudentId into studentCourses
                          select new { s.Surname, s.Name, Courses = studentCourses };

        foreach (var item in groupJoined)
        {
            Console.Write($"  {item.Surname} {item.Name}: ");
            if (item.Courses.Any())
                Console.WriteLine(string.Join(", ", item.Courses.Select(c => $"{c.Subject}({c.Grade})")));
            else
                Console.WriteLine("нет оценок");
        }
    }
}

// === Задание 5: LINQ to XML ===
public class LinqToXmlDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 5: LINQ к коллекциям — комплексный запрос ===");

        var students = LinqQuerySyntax.GetStudents();

        // Комплексный запрос: топ-3 студента по стипендии среди не-москвичей
        Console.WriteLine("\nТоп-3 по стипендии (немосквичи):");
        var top3 = students.Where(s => s.City != "Москва")
                           .OrderByDescending(s => s.Stipend)
                           .Take(3)
                           .Select((s, index) => new { Rank = index + 1, s.Surname, s.Name, s.Stipend, s.City });

        foreach (var s in top3)
            Console.WriteLine($"  #{s.Rank}: {s.Surname} {s.Name} — {s.Stipend} руб. ({s.City})");
    }
}
