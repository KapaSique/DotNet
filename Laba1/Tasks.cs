namespace DotNet.Laba1;

public class LinearEquation
{
    public static void Run()
    {
        Console.WriteLine("=== Решение линейного уравнения ax + b = 0 ===");

        Console.Write("a: ");
        double a = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("b: ");
        double b = double.Parse(Console.ReadLine() ?? "0");

        if (a == 0)
        {
            Console.WriteLine(b == 0 ? "Комплексное число" : "Нет решений");
        }
        else
        {
            double x = -b / a;
            Console.WriteLine($"x = {x}");
        }
    }
}

public class MaxOfTwo
{
    public static void Run()
    {
        Console.WriteLine("=== Максимум из двух чисел ===");

        Console.Write("x: ");
        double x = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("y: ");
        double y = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine($"Максимум: {(x > y ? x : y)}");
    }
}

public class Mushrooms
{
    public static void Run()
    {
        Console.WriteLine("=== Склонение слова 'гриб' ===");

        Console.Write("Количество грибов: ");
        int k = int.Parse(Console.ReadLine() ?? "0");

        int lastTwo = k % 100;
        string word;

        if (lastTwo >= 11 && lastTwo <= 14)
        {
            word = "грибов";
        }
        else
        {
            int last = k % 10;

            if (last == 1)
                word = "гриб";
            else if (last >= 2 && last <= 4)
                word = "гриба";
            else
                word = "грибов";
        }

        Console.WriteLine($"Мы нашли {k} {word} в лесу!");
    }
}

public class CircleAndSquare
{
    public static void Run()
    {
        Console.WriteLine("=== Круг и квадрат ===");

        Console.Write("Площадь окружности: ");
        double circleArea = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Площадь квадрата: ");
        double squareArea = double.Parse(Console.ReadLine() ?? "0");

        double r = Math.Sqrt(circleArea / Math.PI);
        double a = Math.Sqrt(squareArea);

        bool squareInCircle = r >= a / Math.Sqrt(2);
        bool circleInSquare = a >= 2 * r;

        Console.WriteLine(squareInCircle ? "Квадрат можно вписать в окружность" : "Квадрат не помещается в окружность");
        Console.WriteLine(circleInSquare ? "Окружность можно вписать в квадрат" : "Окружность не помещается в квадрат");
    }
}

public class SpeedComparison
{
    public static void Run()
    {
        Console.WriteLine("=== Сравнение скоростей км/ч и м/с ===");

        Console.Write("Скорость в км/ч: ");
        double kmh = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Скорость в м/с: ");
        double ms = double.Parse(Console.ReadLine() ?? "0");

        double kmhToMs = kmh / 3.6;

        if (kmhToMs > ms)
            Console.WriteLine("Скорость в км/ч больше");
        else if (kmhToMs < ms)
            Console.WriteLine("Скорость в м/с больше");
        else
            Console.WriteLine("Скорости равны");
    }
}

public class AgeCalculator
{
    public static void Run()
    {
        Console.WriteLine("=== Определение возраста ===");

        DateTime currentDate = DateTime.Today;

        Console.Write("Год рождения: ");
        int by = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Месяц рождения: ");
        int bm = int.Parse(Console.ReadLine() ?? "0");

        DateTime birthDate = new DateTime(by, bm, 1);

        int age = currentDate.Year - birthDate.Year;
        if (currentDate < birthDate.AddYears(age)) age--;

        Console.WriteLine($"Возраст: {age} лет");
    }
}

public class NameGreeting
{
    public static void Run()
    {
        Console.WriteLine("=== Приветствие по фамилии ===");

        Console.Write("Фамилия: ");
        string s = (Console.ReadLine() ?? "").Trim();

        if (s.EndsWith("ова", StringComparison.OrdinalIgnoreCase))
            Console.WriteLine($"Здравствуйте, госпожа {s}!");
        else if (s.EndsWith("ов", StringComparison.OrdinalIgnoreCase))
            Console.WriteLine($"Здравствуйте, господин {s}!");
        else
            Console.WriteLine($"Здравствуйте, {s}!");
    }
}

public class Schedule
{
    public static void Run()
    {
        Console.WriteLine("=== Расписание на неделю ===");

        Console.Write("Номер дня (1-7): ");
        int day = int.Parse(Console.ReadLine() ?? "0");

        string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        string[] plans = { "Математика", "Физика", "Программирование", "История", "Английский", "Химия", "Выходной" };

        if (day >= 1 && day <= 7)
            Console.WriteLine($"{days[day - 1]}: {plans[day - 1]}");
        else
            Console.WriteLine("Неверный номер дня");
    }
}
