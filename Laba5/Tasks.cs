using System.Text;

namespace DotNet.Laba5;

// === Задание 1: Базовые делегаты ===
public delegate void SimpleDelegate(string message);

public class DelegateBasics
{
    public static string ToUpperString(string msg) => msg.ToUpper();
    public static string ToLowerString(string msg) => msg.ToLower();
    public static string ReverseString(string msg)
    {
        char[] chars = msg.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public static void PrintUpper(string msg) =>
        Console.WriteLine($"Верхний регистр: {ToUpperString(msg)}");

    public static void PrintLower(string msg) =>
        Console.WriteLine($"Нижний регистр: {ToLowerString(msg)}");

    public static void PrintReversed(string msg)
    {
        Console.WriteLine($"Реверс: {ReverseString(msg)}");
    }

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: Базовые делегаты ===");

        SimpleDelegate del = PrintUpper;
        del("Привет, Мир!");

        del = PrintLower;
        del("Привет, Мир!");

        del = PrintReversed;
        del("Привет, Мир!");
    }
}

// === Задание 2: Многоадресные делегаты ===
public class MulticastDelegateDemo
{
    public delegate void BookDelegate();

    public static void Book1() => Console.WriteLine("  - Война и мир (Л.Н. Толстой)");
    public static void Book2() => Console.WriteLine("  - Братья Карамазовы (Ф.М. Достоевский)");
    public static void Book3() => Console.WriteLine("  - Мастер и Маргарита (М.А. Булгаков)");
    public static void Book4() => Console.WriteLine("  - Евгений Онегин (А.С. Пушкин)");

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Многоадресные делегаты ===");

        BookDelegate library = Book1;
        library += Book2;
        library += Book3;
        library += Book4;

        Console.WriteLine("Каталог библиотеки:");
        library();

        Console.WriteLine("\nУбираем первую книгу из каталога:");
        library -= Book1;
        library();
    }
}

// === Задание 3: События (температурный монитор) ===
public class TemperatureEventArgs : EventArgs
{
    public double Temperature { get; }
    public DateTime Time { get; }

    public TemperatureEventArgs(double temp)
    {
        Temperature = temp;
        Time = DateTime.Now;
    }
}

public class TemperatureSensor
{
    public delegate void TemperatureChangedHandler(object sender, TemperatureEventArgs e);
    public event TemperatureChangedHandler? TemperatureChanged;

    private double _currentTemp = 20.0;

    public void SetTemperature(double newTemp)
    {
        double oldTemp = _currentTemp;
        _currentTemp = newTemp;

        OnTemperatureChanged(new TemperatureEventArgs(newTemp));

        if (newTemp > 30)
            Console.WriteLine($"  [ПРЕДУПРЕЖДЕНИЕ] Температура превысила 30°C!");
        else if (newTemp < 0)
            Console.WriteLine($"  [ПРЕДУПРЕЖДЕНИЕ] Температура ниже 0°C!");
    }

    protected virtual void OnTemperatureChanged(TemperatureEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
}

public class TemperatureMonitor
{
    public void OnTemperatureChanged(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"  [Монитор] Температура изменилась: {e.Temperature:F1}°C в {e.Time:HH:mm:ss}");
    }
}

public class TemperatureLogger
{
    private List<string> _log = new();

    public void OnTemperatureChanged(object sender, TemperatureEventArgs e)
    {
        string entry = $"[{e.Time:yyyy-MM-dd HH:mm:ss}] Температура: {e.Temperature:F1}°C";
        _log.Add(entry);
        Console.WriteLine($"  [Логгер] Записано: {entry}");
    }
}

public class EventsDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: События (температурный сенсор) ===");

        TemperatureSensor sensor = new();
        TemperatureMonitor monitor = new();
        TemperatureLogger logger = new();

        sensor.TemperatureChanged += monitor.OnTemperatureChanged;
        sensor.TemperatureChanged += logger.OnTemperatureChanged;

        Console.WriteLine("Имитация изменения температуры:\n");
        sensor.SetTemperature(25.5);
        sensor.SetTemperature(32.0);
        sensor.SetTemperature(15.0);
        sensor.SetTemperature(-5.0);

        Console.WriteLine("\nОтписываем логгер от события...");
        sensor.TemperatureChanged -= logger.OnTemperatureChanged;
        sensor.SetTemperature(22.0);
    }
}

// === Задание 4: Делегаты Action, Func, Predicate ===
public class BuiltInDelegates
{
    public static double AddOperation(double a, double b) => a + b;
    public static double MultiplyOperation(double a, double b) => a * b;
    public static bool IsEven(int n) => n % 2 == 0;
    public static List<int> GetEvenNumbers(List<int> numbers) => numbers.FindAll(IsEven);

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Встроенные делегаты Action, Func, Predicate ===");

        Action<string> print = msg => Console.WriteLine($"  Action: {msg}");
        print("Привет из Action!");

        Console.WriteLine($"  Func add(3.5, 2.5) = {AddOperation(3.5, 2.5)}");
        Console.WriteLine($"  Func multiply(4.0, 5.0) = {MultiplyOperation(4.0, 5.0)}");

        List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> evenNumbers = GetEvenNumbers(numbers);
        Console.WriteLine($"  Predicate (чётные числа): [{string.Join(", ", evenNumbers)}]");
    }
}

// === Задание 5: Асинхронный вызов через делегат ===
public class AsyncDelegateDemo
{
    public delegate string LongOperationDelegate(string input, int delayMs);

    public static string LongOperation(string input, int delayMs)
    {
        Thread.Sleep(delayMs);
        return $"Обработано: {input.ToUpper()} (задержка {delayMs} мс)";
    }

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 5: Асинхронный вызов через делегат ===");

        LongOperationDelegate del = LongOperation;

        Console.WriteLine("Запуск длительной операции...");
        Task<string> result = Task.Run(() => del("тестовые данные", 1000));

        Console.WriteLine("Основной поток продолжает работу...");
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(250);
            Console.WriteLine($"  Основной поток: шаг {i + 1}");
        }

        string output = result.Result;
        Console.WriteLine($"Результат: {output}");
    }
}
