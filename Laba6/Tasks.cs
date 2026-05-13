using System.Data;
using System.Text;

namespace DotNet.Laba6;

// === Задание 1: DataSet и DataTable (отсоединённый режим) ===
public class DataSetDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: DataSet и DataTable (отсоединённый режим) ===");

        DataSet dataSet = new DataSet("University");

        // Таблица Students
        DataTable students = new DataTable("Students");
        students.Columns.Add("Id", typeof(int));
        students.Columns.Add("Surname", typeof(string));
        students.Columns.Add("Name", typeof(string));
        students.Columns.Add("Stipend", typeof(decimal));
        students.Columns.Add("Kurs", typeof(int));

        students.Rows.Add(1, "Иванов", "Петр", 2500m, 3);
        students.Rows.Add(2, "Васильев", "Иван", 2000m, 4);
        students.Rows.Add(3, "Смирнова", "Анна", 3000m, 2);
        students.Rows.Add(4, "Козлов", "Дмитрий", 1800m, 3);

        // Таблица Courses
        DataTable courses = new DataTable("Courses");
        courses.Columns.Add("Id", typeof(int));
        courses.Columns.Add("Title", typeof(string));
        courses.Columns.Add("Hours", typeof(int));

        courses.Rows.Add(1, "Математика", 120);
        courses.Rows.Add(2, "Программирование", 180);
        courses.Rows.Add(3, "Физика", 90);

        dataSet.Tables.Add(students);
        dataSet.Tables.Add(courses);

        // Вывод данных
        Console.WriteLine("\nТаблица Students:");
        Console.WriteLine($"{"Id",-5}{"Фамилия",-15}{"Имя",-12}{"Стипендия",-12}{"Курс",-5}");
        Console.WriteLine(new string('-', 50));
        foreach (DataRow row in students.Rows)
        {
            Console.WriteLine($"{row["Id"],-5}{row["Surname"],-15}{row["Name"],-12}{row["Stipend"],-12}{row["Kurs"],-5}");
        }

        Console.WriteLine($"\nТаблица Courses:");
        Console.WriteLine($"{"Id",-5}{"Название",-22}{"Часы",-5}");
        Console.WriteLine(new string('-', 35));
        foreach (DataRow row in courses.Rows)
        {
            Console.WriteLine($"{row["Id"],-5}{row["Title"],-22}{row["Hours"],-5}");
        }

        // Фильтрация с DataView
        DataView view = new DataView(students);
        view.RowFilter = "Stipend >= 2500";
        view.Sort = "Surname ASC";

        Console.WriteLine("\nDataView: студенты со стипендией >= 2500 (отсортировано):");
        foreach (DataRowView row in view)
        {
            Console.WriteLine($"  {row["Surname"]} {row["Name"]} - {row["Stipend"]} руб.");
        }
    }
}

// === Задание 2: Имитация работы с БД (Npgsql-подобный API) ===
public class DatabaseSimulation
{
    // Демонстрация строки подключения и основных объектов ADO.NET
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Структура подключения к БД ===");

        Console.WriteLine("Строка подключения к PostgreSQL (Npgsql):");
        Console.WriteLine("  server=localhost; user=postgres; password=***; database=university");

        Console.WriteLine("\nЭтапы работы с БД в ADO.NET:");
        Console.WriteLine("  1. NpgsqlConnection — установка соединения");
        Console.WriteLine("  2. NpgsqlDataAdapter — адаптер для запросов");
        Console.WriteLine("  3. DataSet — хранение данных в памяти");
        Console.WriteLine("  4. DataTable — таблицы внутри DataSet");
        Console.WriteLine("  5. DataView — фильтрация и сортировка");

        Console.WriteLine("\nПример SQL-запроса через адаптер:");
        Console.WriteLine("  SELECT * FROM students WHERE stipend > 2000 AND kurs > 2;");

        // Имитация результата запроса
        Console.WriteLine("\nРезультат выполнения (имитация):");

        DataTable result = new DataTable("QueryResult");
        result.Columns.Add("id", typeof(int));
        result.Columns.Add("surname", typeof(string));
        result.Columns.Add("stipend", typeof(decimal));
        result.Columns.Add("kurs", typeof(int));

        result.Rows.Add(1, "Иванов", 2500m, 3);
        result.Rows.Add(3, "Смирнова", 3000m, 2); // не подходит по kurs > 2
        result.Rows.Add(5, "Петров", 2800m, 4);

        // Фильтруем вручную (как если бы БД вернула это)
        DataView filtered = new DataView(result);
        filtered.RowFilter = "stipend > 2000 AND kurs > 2";

        Console.WriteLine("Студенты (stipend > 2000 AND kurs > 2):");
        foreach (DataRowView row in filtered)
        {
            Console.WriteLine($"  id={row["id"]}, {row["surname"]}, стипендия={row["stipend"]} руб., курс={row["kurs"]}");
        }
    }
}

// === Задание 3: Работа с XML-представлением DataSet ===
public class DataSetXmlDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: XML-представление DataSet ===");

        DataSet ds = new DataSet("Library");

        DataTable books = new DataTable("Books");
        books.Columns.Add("Id", typeof(int));
        books.Columns.Add("Title", typeof(string));
        books.Columns.Add("Author", typeof(string));

        books.Rows.Add(1, "Война и мир", "Толстой Л.Н.");
        books.Rows.Add(2, "Преступление и наказание", "Достоевский Ф.М.");

        ds.Tables.Add(books);

        // Сохранение в XML
        string xmlPath = "Laba6/library.xml";
        Directory.CreateDirectory("Laba6");
        ds.WriteXml(xmlPath);
        Console.WriteLine($"DataSet сохранён в XML: {xmlPath}");

        // Чтение из XML в новый DataSet
        DataSet ds2 = new DataSet();
        ds2.ReadXml(xmlPath);
        Console.WriteLine($"\nDataSet загружен из XML. Таблиц: {ds2.Tables.Count}");
        Console.WriteLine("Содержимое таблицы Books:");
        foreach (DataRow row in ds2.Tables["Books"]!.Rows)
        {
            Console.WriteLine($"  [{row["Id"]}] {row["Author"]} — \"{row["Title"]}\"");
        }
    }
}

// === Задание 4: Отношения между таблицами (DataRelation) ===
public class DataRelationDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Отношения между таблицами (DataRelation) ===");

        DataSet ds = new DataSet("School");

        DataTable groups = new DataTable("Groups");
        groups.Columns.Add("GroupId", typeof(int));
        groups.Columns.Add("GroupName", typeof(string));
        groups.Rows.Add(1, "ИСП-101");
        groups.Rows.Add(2, "ИСП-202");

        DataTable students = new DataTable("Students");
        students.Columns.Add("StudentId", typeof(int));
        students.Columns.Add("FullName", typeof(string));
        students.Columns.Add("GroupId", typeof(int));
        students.Rows.Add(1, "Иванов И.И.", 1);
        students.Rows.Add(2, "Петров П.П.", 1);
        students.Rows.Add(3, "Сидоров С.С.", 2);

        ds.Tables.Add(groups);
        ds.Tables.Add(students);

        DataRelation relation = new DataRelation(
            "GroupStudents",
            groups.Columns["GroupId"]!,
            students.Columns["GroupId"]!
        );
        ds.Relations.Add(relation);

        Console.WriteLine("Студенты по группам (через DataRelation):");
        foreach (DataRow groupRow in groups.Rows)
        {
            Console.WriteLine($"\nГруппа: {groupRow["GroupName"]}");
            DataRow[] childRows = groupRow.GetChildRows(relation);
            foreach (DataRow studentRow in childRows)
            {
                Console.WriteLine($"  - {studentRow["FullName"]}");
            }
        }
    }
}
