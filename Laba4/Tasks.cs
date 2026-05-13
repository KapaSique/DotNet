using System.Text;

namespace DotNet.Laba4;

// === Задание 1: Работа со строками ===
// Замена каждого второго вхождения слова (запись в обратном порядке)
public class StringReplace
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Задание 1: Замена каждого второго вхождения слова ===");
        Console.WriteLine("Введите предложение, состоящее только из слов и предлогов:");
        string input = Console.ReadLine() ?? "";

        string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> wordCounts = new();

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            string wordLower = word.ToLower();

            if (!wordCounts.ContainsKey(wordLower))
                wordCounts[wordLower] = 0;

            wordCounts[wordLower]++;

            if (wordCounts[wordLower] % 2 == 0)
            {
                char[] reversed = word.ToCharArray();
                Array.Reverse(reversed);
                words[i] = new string(reversed);
            }
        }

        string result = string.Join(" ", words);
        Console.WriteLine("Результат:");
        Console.WriteLine(result);
    }
}

// === Задание 2: Квадратная матрица, максимумы строк ===
public class SquareMatrix
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Random rnd = new Random();

        Console.WriteLine("=== Задание 2: Квадратная матрица и максимумы строк ===");
        Console.Write("Введите размер матрицы N: ");
        int n = int.Parse(Console.ReadLine() ?? "4");

        int[,] matrix = new int[n, n];

        Console.WriteLine("\nИсходная матрица:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(0, 100);
                Console.Write($"{matrix[i, j],4}");
            }
            Console.WriteLine();
        }

        Console.Write("\nМаксимальные числа в каждой строке: ");
        int[] maxInRows = new int[n];
        for (int i = 0; i < n; i++)
        {
            int max = matrix[i, 0];
            for (int j = 1; j < n; j++)
            {
                if (matrix[i, j] > max)
                    max = matrix[i, j];
            }
            maxInRows[i] = max;
        }
        Console.WriteLine(string.Join(", ", maxInRows));
    }
}

// === Задание 3: Невыровненная матрица букв a-k, замена, транспонирование ===
public class JaggedCharMatrix
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Random rnd = new Random();

        Console.WriteLine("=== Задание 3: Невыровненная матрица букв a-k ===");
        char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k' };

        int rows = rnd.Next(3, 7);
        char[][] matrix = new char[rows][];

        Console.WriteLine("\nИсходная матрица:");
        for (int i = 0; i < rows; i++)
        {
            int cols = rnd.Next(4, 10);
            matrix[i] = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                matrix[i][j] = letters[rnd.Next(0, letters.Length)];
            }
            Console.WriteLine(new string(matrix[i]));
        }

        // Замена 'a' на 'e'
        char[][] replaced = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            replaced[i] = new char[matrix[i].Length];
            for (int j = 0; j < matrix[i].Length; j++)
            {
                replaced[i][j] = matrix[i][j] == 'a' ? 'e' : matrix[i][j];
            }
        }

        Console.WriteLine("\nМатрица после замены 'a' -> 'e':");
        for (int i = 0; i < rows; i++)
            Console.WriteLine(new string(replaced[i]));

        // Транспонирование
        int maxCols = 0;
        for (int i = 0; i < rows; i++)
            if (replaced[i].Length > maxCols)
                maxCols = replaced[i].Length;

        char[][] transposed = new char[maxCols][];
        for (int j = 0; j < maxCols; j++)
        {
            transposed[j] = new char[rows];
            for (int i = 0; i < rows; i++)
            {
                transposed[j][i] = j < replaced[i].Length ? replaced[i][j] : ' ';
            }
        }

        Console.WriteLine("\nТранспонированная матрица:");
        for (int i = 0; i < maxCols; i++)
            Console.WriteLine(new string(transposed[i]));
    }
}

// === Задание 4: Демонстрация констант (из Лекции4) ===
public class ConstantsDemo
{
    public const double Pi = 3.14159;
    public const string AppName = "DotNet Labs";

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Константы ===");
        Console.WriteLine($"Константа Pi = {Pi}");
        Console.WriteLine($"Константа AppName = {AppName}");

        Console.Write("Введите радиус круга: ");
        double r = double.Parse(Console.ReadLine() ?? "1");
        Console.WriteLine($"Площадь круга = {Pi * r * r:F4}");
    }
}

// === Задание 5: Статические методы и переменные (из Лекции4) ===
public class StaticDemo
{
    private static int _objectCount = 0;
    public string Name { get; }

    public StaticDemo(string name)
    {
        Name = name;
        _objectCount++;
    }

    public static int ObjectCount => _objectCount;

    public static double Average(double[] values)
    {
        if (values.Length == 0) return 0;
        double sum = 0;
        foreach (double v in values) sum += v;
        return sum / values.Length;
    }

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 5: Статические методы и переменные ===");

        var obj1 = new StaticDemo("Объект1");
        var obj2 = new StaticDemo("Объект2");
        var obj3 = new StaticDemo("Объект3");

        Console.WriteLine($"Создано объектов: {StaticDemo.ObjectCount}");

        double[] values = { 3.5, 7.2, 1.8, 9.1, 4.6 };
        Console.WriteLine($"Среднее арифметическое [{string.Join(", ", values)}] = {StaticDemo.Average(values):F2}");
    }
}

// === Задание 6: Пользовательский индексатор (из Лекции4) ===
public class StudentGroup
{
    private string[] _students;

    public StudentGroup(int size)
    {
        _students = new string[size];
        for (int i = 0; i < size; i++)
            _students[i] = $"Студент_{i + 1}";
    }

    public string this[int index]
    {
        get
        {
            if (index >= 0 && index < _students.Length)
                return _students[index];
            return "Индекс вне диапазона";
        }
        set
        {
            if (index >= 0 && index < _students.Length)
                _students[index] = value;
        }
    }

    public int Count => _students.Length;

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 6: Пользовательский индексатор ===");

        StudentGroup group = new StudentGroup(5);
        Console.WriteLine($"Группа из {group.Count} студентов:");

        for (int i = 0; i < group.Count; i++)
            Console.WriteLine($"  [{i}]: {group[i]}");

        group[2] = "Иванов Иван";
        Console.WriteLine("\nПосле изменения студента [2]:");
        Console.WriteLine($"  [2]: {group[2]}");
    }
}

// === Задание 7: Перегрузка методов (из Лекции4) ===
public class MathOperations
{
    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b) => a + b;
    public static string Add(string a, string b) => a + b;
    public static int Add(int a, int b, int c) => a + b + c;

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 7: Перегрузка методов ===");

        Console.WriteLine($"Add(5, 10) = {Add(5, 10)}");
        Console.WriteLine($"Add(3.14, 2.86) = {Add(3.14, 2.86):F2}");
        Console.WriteLine($"Add(\"Hello\", \" World\") = {Add("Hello", " World")}");
        Console.WriteLine($"Add(1, 2, 3) = {Add(1, 2, 3)}");
    }
}

// === Задание 8: Перегрузка операторов (из Лекции4) ===
public class Vector2D
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vector2D operator +(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2D operator -(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2D operator *(Vector2D v, double scalar)
    {
        return new Vector2D(v.X * scalar, v.Y * scalar);
    }

    public override string ToString() => $"({X}, {Y})";

    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 8: Перегрузка операторов ===");

        Vector2D v1 = new Vector2D(3, 4);
        Vector2D v2 = new Vector2D(1, 2);

        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 - v2 = {v1 - v2}");
        Console.WriteLine($"v1 * 2.5 = {v1 * 2.5}");
    }
}
