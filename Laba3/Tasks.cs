namespace DotNet.Laba3
{
    // ===== Задание 1: Конструкторы =====

    public class Person
    {
        private string _name;
        private int _age;
        private string _city;

        // Конструктор по умолчанию
        public Person()
        {
            _name = "Неизвестно";
            _age = 0;
            _city = "Не указан";
            Console.WriteLine("[Конструктор по умолчанию] Создан объект Person");
        }

        // Параметризованный конструктор
        public Person(string name, int age)
        {
            _name = name;
            _age = age;
            _city = "Не указан";
            Console.WriteLine($"[Параметризованный конструктор] Создан: {_name}, {_age} лет");
        }

        // Конструктор с цепочкой вызовов (this)
        public Person(string name, int age, string city) : this(name, age)
        {
            _city = city;
            Console.WriteLine($"[Конструктор с цепочкой] Город: {_city}");
        }

        // Конструктор копирования
        public Person(Person other)
        {
            _name = other._name;
            _age = other._age;
            _city = other._city;
            Console.WriteLine($"[Конструктор копирования] Скопирован: {_name}");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"  Имя: {_name}, Возраст: {_age}, Город: {_city}");
        }
    }

    public class Counter
    {
        private static int _totalCount;
        private int _id;

        // Статический конструктор
        static Counter()
        {
            _totalCount = 0;
            Console.WriteLine("[Статический конструктор] Counter инициализирован");
        }

        public Counter()
        {
            _totalCount++;
            _id = _totalCount;
            Console.WriteLine($"[Конструктор] Создан Counter #{_id}, всего: {_totalCount}");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"  Counter #{_id}, всего создано: {_totalCount}");
        }
    }

    public class ConstructorsDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Задание 1: Конструкторы ===\n");

            Console.WriteLine("--- 1. Конструктор по умолчанию ---");
            Person p1 = new Person();
            p1.ShowInfo();

            Console.WriteLine("\n--- 2. Параметризованный конструктор ---");
            Console.Write("Введите имя: ");
            string name = Console.ReadLine() ?? "Иван";
            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine() ?? "25");
            Person p2 = new Person(name, age);
            p2.ShowInfo();

            Console.WriteLine("\n--- 3. Конструктор с цепочкой вызовов (this) ---");
            Console.Write("Введите город: ");
            string city = Console.ReadLine() ?? "Москва";
            Person p3 = new Person(name, age, city);
            p3.ShowInfo();

            Console.WriteLine("\n--- 4. Конструктор копирования ---");
            Person p4 = new Person(p3);
            p4.ShowInfo();

            Console.WriteLine("\n--- 5. Статический конструктор ---");
            Counter c1 = new Counter();
            c1.ShowInfo();
            Counter c2 = new Counter();
            c2.ShowInfo();
            Counter c3 = new Counter();
            c3.ShowInfo();
        }
    }

    // ===== Задание 2: Пространства имен =====

    public class NamespacesDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Задание 2: Пространства имен ===\n");

            Console.WriteLine("--- Демонстрация иерархии пространств имен ---\n");

            Console.WriteLine("Создаём объекты из разных пространств имен:\n");

            var manager = new Company.Staff.Manager("Иванов Пётр", "IT-отдел");
            var developer = new Company.Staff.Developer("Сидоров Алексей", "C#");
            var project = new Company.Projects.Project("Сайт компании", manager);
            var task = new Company.Projects.Tasks.Task("Разработать главную страницу", developer);

            Console.WriteLine("--- Информация об объектах ---\n");
            manager.ShowInfo();
            developer.ShowInfo();
            project.ShowInfo();
            task.ShowInfo();

            Console.WriteLine("\n--- Иерархия пространств имен ---");
            Console.WriteLine("Company");
            Console.WriteLine("├── Staff");
            Console.WriteLine("│   ├── Manager");
            Console.WriteLine("│   └── Developer");
            Console.WriteLine("└── Projects");
            Console.WriteLine("    ├── Project");
            Console.WriteLine("    └── Tasks");
            Console.WriteLine("        └── Task");
        }
    }
}

// Вложенные пространства имен для демонстрации иерархии

namespace Company.Staff
{
    public class Manager
    {
        private string _name;
        private string _department;

        public Manager(string name, string department)
        {
            _name = name;
            _department = department;
        }

        public string Name => _name;

        public void ShowInfo()
        {
            Console.WriteLine($"  [Company.Staff.Manager] {_name}, отдел: {_department}");
        }
    }

    public class Developer
    {
        private string _name;
        private string _language;

        public Developer(string name, string language)
        {
            _name = name;
            _language = language;
        }

        public string Name => _name;

        public void ShowInfo()
        {
            Console.WriteLine($"  [Company.Staff.Developer] {_name}, язык: {_language}");
        }
    }
}

namespace Company.Projects
{
    public class Project
    {
        private string _title;
        private Company.Staff.Manager _manager;

        public Project(string title, Company.Staff.Manager manager)
        {
            _title = title;
            _manager = manager;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"  [Company.Projects.Project] \"{_title}\", менеджер: {_manager.Name}");
        }
    }
}

namespace Company.Projects.Tasks
{
    public class Task
    {
        private string _description;
        private Company.Staff.Developer _assignee;

        public Task(string description, Company.Staff.Developer assignee)
        {
            _description = description;
            _assignee = assignee;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"  [Company.Projects.Tasks.Task] \"{_description}\", исполнитель: {_assignee.Name}");
        }
    }
}
