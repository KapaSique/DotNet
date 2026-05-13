using System.Text;
using DotNet.Laba1;
using DotNet.Laba2;
using DotNet.Laba3;
using DotNet.Laba4;
using DotNet.Laba5;
using DotNet.Laba6;
using DotNet.Laba7;
using DotNet.Laba8;
using DotNet.Laba9;
using DotNet.Laba10;
using DotNet.Laba11;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine("\n=== Лабораторные работы ===");
    Console.WriteLine("1.  Лабораторная 1  — Базовые конструкции C#");
    Console.WriteLine("2.  Лабораторная 2  — ООП: классы, наследование");
    Console.WriteLine("3.  Лабораторная 3  — Файлы, массивы, кодировки");
    Console.WriteLine("4.  Лабораторная 4  — Строки, матрицы, константы, перегрузка");
    Console.WriteLine("5.  Лабораторная 5  — Делегаты и события");
    Console.WriteLine("6.  Лабораторная 6  — ADO.NET, DataSet, DataTable");
    Console.WriteLine("7.  Лабораторная 7  — LINQ, лямбда-выражения");
    Console.WriteLine("8.  Лабораторная 8  — ORM, Entity Framework, JSON");
    Console.WriteLine("9.  Лабораторная 9  — GDI+, графика, System.Drawing");
    Console.WriteLine("10. Лабораторная 10 — Обработка изображений, фильтры");
    Console.WriteLine("11. Лабораторная 11 — XML: чтение, запись, редактирование");
    Console.WriteLine("0.  Выход");
    Console.Write("\nВыберите лабораторную: ");

    string? lab = Console.ReadLine();

    switch (lab)
    {
        case "1": RunLaba1(); break;
        case "2": CarsDemo.Run(); break;
        case "3": RunLaba3(); break;
        case "4": RunLaba4(); break;
        case "5": RunLaba5(); break;
        case "6": RunLaba6(); break;
        case "7": RunLaba7(); break;
        case "8": RunLaba8(); break;
        case "9": RunLaba9(); break;
        case "10": RunLaba10(); break;
        case "11": RunLaba11(); break;
        case "0": return;
    }
}

static void RunLaba1()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 1 ===");
        Console.WriteLine("1. Линейное уравнение (ax + b = 0)");
        Console.WriteLine("2. Максимум из двух чисел");
        Console.WriteLine("3. Склонение слова 'гриб'");
        Console.WriteLine("4. Круг и квадрат");
        Console.WriteLine("5. Сравнение скоростей");
        Console.WriteLine("6. Определение возраста");
        Console.WriteLine("7. Приветствие по фамилии");
        Console.WriteLine("8. Расписание");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": LinearEquation.Run(); break;
            case "2": MaxOfTwo.Run(); break;
            case "3": Mushrooms.Run(); break;
            case "4": CircleAndSquare.Run(); break;
            case "5": SpeedComparison.Run(); break;
            case "6": AgeCalculator.Run(); break;
            case "7": NameGreeting.Run(); break;
            case "8": Schedule.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba3()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 3 ===");
        Console.WriteLine("1. Задание 1 (Зубчатый массив)");
        Console.WriteLine("2. Задание 2 (Подсчет символов 's')");
        Console.WriteLine("3. Задание 3 (Перекодировка)");
        Console.WriteLine("4. Задание 4 (Замена слов)");
        Console.WriteLine("5. Задание 5 (Вывод директории)");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba3.Task1.Run(); break;
            case "2": DotNet.Laba3.Task2.Run(); break;
            case "3": DotNet.Laba3.Task3.Run(); break;
            case "4": DotNet.Laba3.Task4.Run(); break;
            case "5": DotNet.Laba3.Task5.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba4()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 4 ===");
        Console.WriteLine("1. Замена каждого второго вхождения слова");
        Console.WriteLine("2. Квадратная матрица и максимумы строк");
        Console.WriteLine("3. Невыровненная матрица букв a-k");
        Console.WriteLine("4. Константы");
        Console.WriteLine("5. Статические методы и переменные");
        Console.WriteLine("6. Пользовательский индексатор");
        Console.WriteLine("7. Перегрузка методов");
        Console.WriteLine("8. Перегрузка операторов");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba4.StringReplace.Run(); break;
            case "2": DotNet.Laba4.SquareMatrix.Run(); break;
            case "3": DotNet.Laba4.JaggedCharMatrix.Run(); break;
            case "4": DotNet.Laba4.ConstantsDemo.Run(); break;
            case "5": DotNet.Laba4.StaticDemo.Run(); break;
            case "6": DotNet.Laba4.StudentGroup.Run(); break;
            case "7": DotNet.Laba4.MathOperations.Run(); break;
            case "8": DotNet.Laba4.Vector2D.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba5()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 5: Делегаты и события ===");
        Console.WriteLine("1. Базовые делегаты");
        Console.WriteLine("2. Многоадресные делегаты");
        Console.WriteLine("3. События (температурный сенсор)");
        Console.WriteLine("4. Встроенные делегаты Action, Func, Predicate");
        Console.WriteLine("5. Асинхронный вызов через делегат");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba5.DelegateBasics.Run(); break;
            case "2": DotNet.Laba5.MulticastDelegateDemo.Run(); break;
            case "3": DotNet.Laba5.EventsDemo.Run(); break;
            case "4": DotNet.Laba5.BuiltInDelegates.Run(); break;
            case "5": DotNet.Laba5.AsyncDelegateDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba6()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 6: ADO.NET ===");
        Console.WriteLine("1. DataSet и DataTable");
        Console.WriteLine("2. Имитация работы с БД (Npgsql)");
        Console.WriteLine("3. XML-представление DataSet");
        Console.WriteLine("4. Отношения между таблицами (DataRelation)");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba6.DataSetDemo.Run(); break;
            case "2": DotNet.Laba6.DatabaseSimulation.Run(); break;
            case "3": DotNet.Laba6.DataSetXmlDemo.Run(); break;
            case "4": DotNet.Laba6.DataRelationDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba7()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 7: LINQ ===");
        Console.WriteLine("1. LINQ запросы (интегрированный синтаксис)");
        Console.WriteLine("2. Лямбда-выражения и методы расширения");
        Console.WriteLine("3. Группировка (GroupBy)");
        Console.WriteLine("4. Join (соединение коллекций)");
        Console.WriteLine("5. Комплексный запрос (топ-3)");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba7.LinqQuerySyntax.Run(); break;
            case "2": DotNet.Laba7.LinqLambdaSyntax.Run(); break;
            case "3": DotNet.Laba7.LinqGrouping.Run(); break;
            case "4": DotNet.Laba7.LinqJoin.Run(); break;
            case "5": DotNet.Laba7.LinqToXmlDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba8()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 8: ORM, EF, JSON ===");
        Console.WriteLine("1. Контекст данных и CRUD (ORM-подход)");
        Console.WriteLine("2. JSON сериализация/десериализация");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba8.OrmDemo.Run(); break;
            case "2": DotNet.Laba8.JsonDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba9()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 9: GDI+, графика ===");
        Console.WriteLine("1. Основные типы System.Drawing");
        Console.WriteLine("2. Рисование на Graphics (Bitmap)");
        Console.WriteLine("3. Работа с цветом");
        Console.WriteLine("4. Перья и кисти");
        Console.WriteLine("5. Вывод изображений");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba9.DrawingBasics.Run(); break;
            case "2": DotNet.Laba9.GraphicsDemo.Run(); break;
            case "3": DotNet.Laba9.ColorDemo.Run(); break;
            case "4": DotNet.Laba9.PensAndBrushesDemo.Run(); break;
            case "5": DotNet.Laba9.ImageOutputDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba10()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 10: Обработка изображений ===");
        Console.WriteLine("1. Информация об изображении");
        Console.WriteLine("2. Фильтры (grayscale, invert, blur, sepia...)");
        Console.WriteLine("3. Гауссово размытие и повышение резкости");
        Console.WriteLine("4. Обзор фильтров (сводная таблица)");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba10.ImageInfo.Run(); break;
            case "2": DotNet.Laba10.ImageFilters.Run(); break;
            case "3": DotNet.Laba10.AdvancedFilters.Run(); break;
            case "4": DotNet.Laba10.FilterComparison.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}

static void RunLaba11()
{
    while (true)
    {
        Console.WriteLine("\n=== Лабораторная 11: XML ===");
        Console.WriteLine("1. Чтение и запись XML (DOM)");
        Console.WriteLine("2. Редактирование XML");
        Console.WriteLine("3. LINQ to XML");
        Console.WriteLine("4. XmlReader / XmlWriter (SAX)");
        Console.WriteLine("5. Структура XML и стандарты");
        Console.WriteLine("0. Назад");
        Console.Write("\nВыберите задание: ");

        string? choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": DotNet.Laba11.XmlDomDemo.Run(); break;
            case "2": DotNet.Laba11.XmlEditDemo.Run(); break;
            case "3": DotNet.Laba11.LinqToXmlDemo.Run(); break;
            case "4": DotNet.Laba11.XmlReaderWriterDemo.Run(); break;
            case "5": DotNet.Laba11.XmlValidationDemo.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}
