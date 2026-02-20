using System;
using System.IO;
using System.Text;

namespace DotNet.Laba3;

public class Task1
{
    public static void Run()
    {
        Console.WriteLine("=== Задание 1: Зубчатый массив ===");
        Random rnd = new Random();
        int rows = rnd.Next(3, 8);
        string[] arr = new string[rows];
        
        string alph = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < rows; i++)
        {
            int cols = rnd.Next(5, 50);
            char[] arrSymb = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                arrSymb[j] = alph[rnd.Next(0, alph.Length)];
            }
            arr[i] = new string(arrSymb);
            Console.WriteLine($"Строка {i + 1}: {arr[i]}");
        }

        string path = "Laba3/Tasks/ArrayOutput.txt";
        File.WriteAllLines(path, arr);
        Console.WriteLine($"\nМассив сохранен в файл: {path}");
    }
}

public class Task2
{
    public static void Run()
    {
        Console.WriteLine("=== Задание 2: Подсчет символов 's' ===");
        string path = "Laba3/Tasks/ArrayOutput.txt";
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден! Сначала выполните задание 1.");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        string result = "";

        for (int i = 0; i < lines.Length; i++)
        {
            int count = 0;
            string currentLine = lines[i];
            
            for (int j = 0; j < currentLine.Length; j++)
            {
                if (currentLine[j] == 's' || currentLine[j] == 'S')
                {
                    count++;
                }
            }
            
            result += count.ToString();
            if (i < lines.Length - 1)
            {
                result += ", ";
            }
        }

        Console.WriteLine("Количество символов 's' для каждой строки:");
        Console.WriteLine(result);
    }
}

public class Task3
{
    public static void Run()
    {
        Console.WriteLine("=== Задание 3: Перекодировка файла ===");
        string path = "Laba3/Tasks/Task1.txt";
        string newPath = "Laba3/Tasks/Task1_new.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл {path} не найден!");
            return;
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding cp866 = Encoding.GetEncoding(866);

        string text = File.ReadAllText(path, cp866);
        File.WriteAllText(newPath, text, Encoding.UTF8);

        Console.WriteLine("Файл успешно перекодирован и сохранен как Task1_new.txt");
    }
}

public class Task4
{
    public static void Run()
    {
        Console.WriteLine("=== Задание 4: Замена слова и количество слов ===");
        string path = "Laba3/Tasks/Task2.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл {path} не найден!");
            return;
        }

        string text = File.ReadAllText(path);
        
        string newText = text.Replace("объект", "класс");
        newText = newText.Replace("Объект", "Класс");

        Console.WriteLine("Новый текст:");
        Console.WriteLine(newText);
        Console.WriteLine();

        char[] delimiters = new char[] { ' ', '\r', '\n', '\t', '.', ',', ';', ':', '!', '?', '-', '(', ')' };
        string[] words = newText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine($"Количество слов в тексте: {words.Length}");
    }
}

public class Task5
{
    public static void Run()
    {
        Console.WriteLine("=== Задание 5: Содержимое директории ===");
        
        string path = "/Applications";

        if (!Directory.Exists(path))
        {
             Console.WriteLine("Директория не найдена!");
             return;
        }

        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        Console.WriteLine($"Содержимое папки {path}:");
        
        Console.WriteLine("\nПапки:");
        for (int i = 0; i < directories.Length; i++)
        {
            Console.WriteLine(Path.GetFileName(directories[i]));
        }

        Console.WriteLine("\nФайлы:");
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(Path.GetFileName(files[i]));
        }
    }
}
