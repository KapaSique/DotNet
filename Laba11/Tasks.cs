using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DotNet.Laba11;

// === Задание 1: Чтение и запись XML (XmlDocument / DOM) ===
public class XmlDomDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: Чтение и запись XML (DOM) ===");

        string xmlPath = "Laba11/students.xml";
        Directory.CreateDirectory("Laba11");

        // Создание XML через XmlDocument
        XmlDocument doc = new XmlDocument();
        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(decl);

        XmlElement root = doc.CreateElement("students");
        doc.AppendChild(root);

        AddStudent(doc, root, "1", "Иванов", "Петр", "2500", "3");
        AddStudent(doc, root, "2", "Смирнова", "Анна", "3000", "2");
        AddStudent(doc, root, "3", "Козлов", "Дмитрий", "1800", "4");

        doc.Save(xmlPath);
        Console.WriteLine($"XML создан: {xmlPath}");

        // Чтение XML
        Console.WriteLine("\nЧтение XML:");
        XmlDocument readDoc = new XmlDocument();
        readDoc.Load(xmlPath);

        XmlNodeList studentNodes = readDoc.SelectNodes("/students/student")!;
        foreach (XmlNode node in studentNodes)
        {
            string surname = node["surname"]?.InnerText ?? "";
            string name = node["name"]?.InnerText ?? "";
            string stipend = node["stipend"]?.InnerText ?? "";
            string kurs = node["kurs"]?.InnerText ?? "";
            Console.WriteLine($"  {surname} {name} — стипендия {stipend}, курс {kurs}");
        }
    }

    private static void AddStudent(XmlDocument doc, XmlElement root,
        string id, string surname, string name, string stipend, string kurs)
    {
        XmlElement student = doc.CreateElement("student");
        student.SetAttribute("id", id);

        XmlElement surnameEl = doc.CreateElement("surname");
        surnameEl.InnerText = surname;
        student.AppendChild(surnameEl);

        XmlElement nameEl = doc.CreateElement("name");
        nameEl.InnerText = name;
        student.AppendChild(nameEl);

        XmlElement stipendEl = doc.CreateElement("stipend");
        stipendEl.InnerText = stipend;
        student.AppendChild(stipendEl);

        XmlElement kursEl = doc.CreateElement("kurs");
        kursEl.InnerText = kurs;
        student.AppendChild(kursEl);

        root.AppendChild(student);
    }
}

// === Задание 2: Редактирование XML ===
public class XmlEditDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Редактирование XML ===");

        string xmlPath = "Laba11/students.xml";
        if (!File.Exists(xmlPath))
            XmlDomDemo.Run();

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);

        // Добавление нового студента
        XmlElement root = doc.DocumentElement!;
        XmlElement newStudent = doc.CreateElement("student");
        newStudent.SetAttribute("id", "4");
        AddElement(doc, newStudent, "surname", "Петров");
        AddElement(doc, newStudent, "name", "Сергей");
        AddElement(doc, newStudent, "stipend", "2200");
        AddElement(doc, newStudent, "kurs", "5");
        root.AppendChild(newStudent);

        // Изменение существующего (повышение стипендии Иванову)
        XmlNode? ivanov = root.SelectSingleNode("student[surname='Иванов']");
        if (ivanov != null)
        {
            XmlNode? stipendNode = ivanov["stipend"];
            if (stipendNode != null)
            {
                decimal old = decimal.Parse(stipendNode.InnerText);
                stipendNode.InnerText = (old + 500).ToString();
                Console.WriteLine($"Стипендия Иванова повышена: {old} -> {stipendNode.InnerText}");
            }
        }

        doc.Save(xmlPath);
        Console.WriteLine($"XML обновлён: {xmlPath}");

        // Вывод обновлённого содержимого
        Console.WriteLine("\nОбновлённый список:");
        XmlNodeList students = root.SelectNodes("student")!;
        foreach (XmlNode node in students)
        {
            Console.WriteLine($"  [{node.Attributes?["id"]?.Value}] {node["surname"]?.InnerText} {node["name"]?.InnerText} — {node["stipend"]?.InnerText} руб.");
        }
    }

    private static void AddElement(XmlDocument doc, XmlElement parent,
        string tag, string text)
    {
        XmlElement el = doc.CreateElement(tag);
        el.InnerText = text;
        parent.AppendChild(el);
    }
}

// === Задание 3: LINQ to XML (XDocument) ===
public class LinqToXmlDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: LINQ to XML ===");

        // Создание XML через LINQ to XML
        XDocument doc = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("library",
                new XElement("book",
                    new XAttribute("id", "1"),
                    new XElement("title", "Война и мир"),
                    new XElement("author", "Толстой Л.Н."),
                    new XElement("year", "1869")
                ),
                new XElement("book",
                    new XAttribute("id", "2"),
                    new XElement("title", "Преступление и наказание"),
                    new XElement("author", "Достоевский Ф.М."),
                    new XElement("year", "1866")
                ),
                new XElement("book",
                    new XAttribute("id", "3"),
                    new XElement("title", "Мастер и Маргарита"),
                    new XElement("author", "Булгаков М.А."),
                    new XElement("year", "1967")
                )
            )
        );

        string xmlPath = "Laba11/library.xml";
        doc.Save(xmlPath);
        Console.WriteLine($"XML создан: {xmlPath}");

        // LINQ-запросы к XML
        XDocument loaded = XDocument.Load(xmlPath);
        Console.WriteLine("\nВсе книги:");
        var allBooks = from book in loaded.Descendants("book")
                       select new
                       {
                           Title = book.Element("title")?.Value,
                           Author = book.Element("author")?.Value,
                           Year = book.Element("year")?.Value
                       };

        foreach (var b in allBooks)
            Console.WriteLine($"  {b.Author} — \"{b.Title}\" ({b.Year})");

        // Запрос с фильтрацией
        Console.WriteLine("\nКниги, изданные до 1900 года:");
        var oldBooks = loaded.Descendants("book")
                             .Where(b => int.Parse(b.Element("year")?.Value ?? "0") < 1900);

        foreach (var b in oldBooks)
            Console.WriteLine($"  {b.Element("title")?.Value} ({b.Element("year")?.Value})");
    }
}

// === Задание 4: XmlReader / XmlWriter (SAX-подобный подход) ===
public class XmlReaderWriterDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: XmlReader / XmlWriter (SAX-подход) ===");

        string sourcePath = "Laba11/students.xml";
        if (!File.Exists(sourcePath))
            XmlDomDemo.Run();

        string targetPath = "Laba11/students_copy.xml";

        Console.WriteLine("Чтение через XmlReader (потоковое, без загрузки в память):");
        Console.WriteLine(new string('-', 50));

        using XmlReader reader = XmlReader.Create(sourcePath);
        using XmlWriter writer = XmlWriter.Create(targetPath, new XmlWriterSettings
        {
            Indent = true,
            Encoding = Encoding.UTF8
        });

        writer.WriteStartDocument();
        writer.WriteStartElement("students");

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "student")
            {
                string? id = reader.GetAttribute("id");
                writer.WriteStartElement("student");
                writer.WriteAttributeString("id", id);
            }
            else if (reader.NodeType == XmlNodeType.Element &&
                     (reader.Name == "surname" || reader.Name == "name" ||
                      reader.Name == "stipend" || reader.Name == "kurs"))
            {
                string tag = reader.Name;
                reader.Read();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine($"  {tag}: {reader.Value}");
                    writer.WriteElementString(tag, reader.Value);
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "student")
            {
                Console.WriteLine("  ---");
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
        reader.Close();

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"Файл скопирован через XmlReader/Writer: {targetPath}");
    }
}

// === Задание 5: XML-схема и валидация ===
public class XmlValidationDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 5: Структура XML и стандарты ===");

        Console.WriteLine("Основные сведения об XML:");
        Console.WriteLine("  XML (Extensible Markup Language) — язык разметки документов");
        Console.WriteLine("  Использование XML:");
        Console.WriteLine("    - Универсальный формат для обмена информацией");
        Console.WriteLine("    - Базовый стандарт для RDF");
        Console.WriteLine("    - Дополнение к HTML (XHTML)");
        Console.WriteLine("    - Промежуточный формат данных в трёхзвенных системах");

        Console.WriteLine("\nСтандарты XML:");
        Console.WriteLine("  DTD — описание грамматики XML");
        Console.WriteLine("  XSLT — преобразование XML-документов");
        Console.WriteLine("  SAX — событийный парсер (нетребователен к ресурсам)");
        Console.WriteLine("  DOM — древовидная модель (загружает весь документ в память)");
        Console.WriteLine("  XAPI — нейтральный интерфейс доступа к XML БД");

        // Создание XML с DTD-подобным комментарием
        string xmlPath = "Laba11/with_schema.xml";
        StringBuilder sb = new();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<!--DOCTYPE students SYSTEM \"students.dtd\"-->");
        sb.AppendLine("<students>");
        sb.AppendLine("  <student id=\"1\">");
        sb.AppendLine("    <surname>Иванов</surname>");
        sb.AppendLine("    <name>Петр</name>");
        sb.AppendLine("    <stipend>3000</stipend>");
        sb.AppendLine("    <kurs>3</kurs>");
        sb.AppendLine("  </student>");
        sb.AppendLine("</students>");

        File.WriteAllText(xmlPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"\nСоздан XML с DOCTYPE-комментарием: {xmlPath}");
    }
}
