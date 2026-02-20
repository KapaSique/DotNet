using System.Text;
using DotNet.Laba1;
using DotNet.Laba2;
using DotNet.Laba3;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine("\n=== Лабораторные работы ===");
    Console.WriteLine("1. Лабораторная 1");
    Console.WriteLine("2. Лабораторная 2");
    Console.WriteLine("3. Лабораторная 3");
    Console.WriteLine("0. Выход");
    Console.Write("\nВыберите лабораторную: ");

    string lab = Console.ReadLine();

    switch (lab)
    {
        case "1": RunLaba1(); break;
        case "2": CarsDemo.Run(); break;
        case "3": RunLaba3(); break;
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

        string choice = Console.ReadLine() ?? "0";
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

        string choice = Console.ReadLine() ?? "0";
        Console.WriteLine();

        switch (choice)
        {
            case "1": Task1.Run(); break;
            case "2": Task2.Run(); break;
            case "3": Task3.Run(); break;
            case "4": Task4.Run(); break;
            case "5": Task5.Run(); break;
            case "0": return;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }
}
