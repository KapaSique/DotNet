using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8; 

//x + b = 0,a!= 0

Console.Write("а: ");

double a = double.Parse(Console.ReadLine() ?? "0");

Console.Write("b: ");
double b = double.Parse(Console.ReadLine() ?? "0");

double x = -b / a;
Console.WriteLine(x);

//max среди двух дабл чисел

// Console.Write("x: ");
// double x = double.Parse(Console.ReadLine() ?? "0");

// Console.Write("y: ");
// double y = double.Parse(Console.ReadLine() ?? "0");

// Console.WriteLine(x > y ? x : y);


//грибы (мухомор)

// Console.Write("k: ");
// int k = int.Parse(Console.ReadLine() ?? "0");

// int lastTwo = k % 100;
// string word;

// if (lastTwo >= 11 && lastTwo <= 14){
//     word = "грибов";
// }
// else{
//     int last = k % 10;

//     if (last == 1)
//         word = "гриб";
//     else if (last >= 2 && last <= 4)
//         word = "гриба";
//     else
//         word = "грибов";
// }

// Console.WriteLine($"Мы нашли {k} {word} в лесу!");


//крух и квадрат

// Console.Write("Площадь окружности: "); //первый случай, пусть 100, второй случай 50, третий случай никто никуда не влезает, пусть 25
// double circleArea = double.Parse(Console.ReadLine() ?? "0");

// Console.Write("Площадь квадрата: "); //пусть 25, пусть 64,  пусть 25
// double squareArea = double.Parse(Console.ReadLine() ?? "0");

// double r = Math.Sqrt(circleArea / Math.PI);
// double a = Math.Sqrt(squareArea);

// bool squareInCircle = r >= a / Math.Sqrt(2);
// bool circleInSquare = a >= 2 * r;

// Console.WriteLine(squareInCircle ? "Вписанный в окружность квадрат" : "Квадрат не в окружности");
// Console.WriteLine(circleInSquare ? "Описанная окружность в квадрате" : "Окружность не в квадрате");


//сравнение кмч и мс

// Console.Write("км/ч: "); //первый случай, пусть 36, второй случай 18, третий случай 72
// double kmh = double.Parse(Console.ReadLine() ?? "0");

// Console.Write("м/с: "); //пусть 5, пусть 10, 20
// double ms = double.Parse(Console.ReadLine() ?? "0");

// double kmhMs = kmh / 3.6;

// if (kmhMs > ms){
//     Console.WriteLine("в км/ч быстрее");
// } 
// else if (kmhMs < ms){
//     Console.WriteLine("в м/с быстрее");
// }
// else Console.WriteLine("они равны");


//определение возраста

// DateTime currentDate = DateTime.Today;

// Console.Write("Год рождения: ");
// int by = int.Parse(Console.ReadLine() ?? "0");

// Console.Write("Месяц рождения: ");
// int bm = int.Parse(Console.ReadLine() ?? "0");

// DateTime birthDate = new DateTime(by, bm, 1);

// int age = currentDate.Year - birthDate.Year;
// if (currentDate < birthDate.AddYears(age)) age--;

// Console.WriteLine(age);


//фамилии

// Console.Write("фамилия: ");
// string s = (Console.ReadLine() ?? "").Trim();

// if (s.EndsWith("oва", StringComparison.OrdinalIgnoreCase))
//     Console.WriteLine($"Здравствуйте госпока {s}!");
// else if (s.EndsWith("ов", StringComparison.OrdinalIgnoreCase))
//     Console.WriteLine($"Здравствуйте господин {s}!");
// else
//     Console.WriteLine($"Здравствуйте господин(госпожа) {s}!");


//расписание

// Console.Write("день: ");
// int day = int.Parse(Console.ReadLine() ?? "0");

// string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
// string[] plans = { "Математика", "Физика", "Программирование", "История", "Английский", "Химия", "Выходной" };

// Console.WriteLine($"{days[day - 1]}: {plans[day - 1]}");

