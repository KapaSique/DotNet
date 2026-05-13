namespace DotNet.Laba1;

public class LinearEquation
{
    public static string Solve(double a, double b)
    {
        if (a == 0)
            return b == 0 ? "Комплексное число" : "Нет решений";
        double x = -b / a;
        return $"x = {x}";
    }

    public static void Run()
    {
        Console.WriteLine("=== Решение линейного уравнения ax + b = 0 ===");

        Console.Write("a: ");
        double a = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("b: ");
        double b = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine(Solve(a, b));
    }
}

public class MaxOfTwo
{
    public static double GetMax(double x, double y) => x > y ? x : y;

    public static void Run()
    {
        Console.WriteLine("=== Максимум из двух чисел ===");

        Console.Write("x: ");
        double x = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("y: ");
        double y = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine($"Максимум: {GetMax(x, y)}");
    }
}

public class Mushrooms
{
    public static string GetDeclension(int k)
    {
        int lastTwo = k % 100;
        if (lastTwo >= 11 && lastTwo <= 14)
            return "грибов";
        int last = k % 10;
        if (last == 1) return "гриб";
        if (last >= 2 && last <= 4) return "гриба";
        return "грибов";
    }

    public static void Run()
    {
        Console.WriteLine("=== Склонение слова 'гриб' ===");

        Console.Write("Количество грибов: ");
        int k = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine($"Мы нашли {k} {GetDeclension(k)} в лесу!");
    }
}

public class CircleAndSquare
{
    public static bool CanSquareFitInCircle(double circleArea, double squareArea)
    {
        double r = Math.Sqrt(circleArea / Math.PI);
        double a = Math.Sqrt(squareArea);
        return r >= a / Math.Sqrt(2);
    }

    public static bool CanCircleFitInSquare(double circleArea, double squareArea)
    {
        double r = Math.Sqrt(circleArea / Math.PI);
        double a = Math.Sqrt(squareArea);
        return a >= 2 * r;
    }

    public static void Run()
    {
        Console.WriteLine("=== Круг и квадрат ===");

        Console.Write("Площадь окружности: ");
        double circleArea = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Площадь квадрата: ");
        double squareArea = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine(CanSquareFitInCircle(circleArea, squareArea) ? "Квадрат можно вписать в окружность" : "Квадрат не помещается в окружность");
        Console.WriteLine(CanCircleFitInSquare(circleArea, squareArea) ? "Окружность можно вписать в квадрат" : "Окружность не помещается в квадрат");
    }
}

public class SpeedComparison
{
    public static int Compare(double kmh, double ms)
    {
        double kmhToMs = kmh / 3.6;
        if (kmhToMs > ms) return 1;
        if (kmhToMs < ms) return -1;
        return 0;
    }

    public static void Run()
    {
        Console.WriteLine("=== Сравнение скоростей км/ч и м/с ===");

        Console.Write("Скорость в км/ч: ");
        double kmh = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Скорость в м/с: ");
        double ms = double.Parse(Console.ReadLine() ?? "0");

        int result = Compare(kmh, ms);
        if (result > 0)
            Console.WriteLine("Скорость в км/ч больше");
        else if (result < 0)
            Console.WriteLine("Скорость в м/с больше");
        else
            Console.WriteLine("Скорости равны");
    }
}

public class AgeCalculator
{
    public static int CalculateAge(int birthYear, int birthMonth, DateTime? referenceDate = null)
    {
        DateTime currentDate = referenceDate ?? DateTime.Today;
        DateTime birthDate = new DateTime(birthYear, birthMonth, 1);
        int age = currentDate.Year - birthDate.Year;
        if (currentDate < birthDate.AddYears(age)) age--;
        return age;
    }

    public static void Run()
    {
        Console.WriteLine("=== Определение возраста ===");

        Console.Write("Год рождения: ");
        int by = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Месяц рождения: ");
        int bm = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine($"Возраст: {CalculateAge(by, bm)} лет");
    }
}

public class NameGreeting
{
    public static string GetGreeting(string surname)
    {
        if (surname.EndsWith("ова", StringComparison.OrdinalIgnoreCase))
            return $"Здравствуйте, госпожа {surname}!";
        if (surname.EndsWith("ов", StringComparison.OrdinalIgnoreCase))
            return $"Здравствуйте, господин {surname}!";
        return $"Здравствуйте, {surname}!";
    }

    public static void Run()
    {
        Console.WriteLine("=== Приветствие по фамилии ===");

        Console.Write("Фамилия: ");
        string s = (Console.ReadLine() ?? "").Trim();

        Console.WriteLine(GetGreeting(s));
    }
}

public class Schedule
{
    public static string GetScheduleForDay(int day)
    {
        string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        string[] plans = { "Математика", "Физика", "Программирование", "История", "Английский", "Химия", "Выходной" };
        if (day >= 1 && day <= 7)
            return $"{days[day - 1]}: {plans[day - 1]}";
        return "Неверный номер дня";
    }

    public static void Run()
    {
        Console.WriteLine("=== Расписание на неделю ===");

        Console.Write("Номер дня (1-7): ");
        int day = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine(GetScheduleForDay(day));
    }
}
