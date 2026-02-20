namespace DotNet.Laba2;
public class Radio
{
    private bool _isOn = false;

    public void On()
    {
        _isOn = true;
        Console.WriteLine("Радио включено");
    }

    public void Off()
    {
        _isOn = false;
        Console.WriteLine("Радио выключено");
    }

    public bool IsOn => _isOn;
}
public abstract class Car
{
    protected string _name;
    protected int _speed = 0;
    protected bool _isStarted = false;
    protected Radio _radio;
    protected int _maxSpeed;
    public Car(string name, int maxSpeed)
    {
        _name = name;
        _maxSpeed = maxSpeed;
        _radio = new Radio();
    }

    public void Start()
    {
        _isStarted = true;
        Console.WriteLine($"{_name}: двигатель запущен");
    }

    public void Stop()
    {
        _speed = 0;
        _isStarted = false;
        Console.WriteLine($"{_name}: автомобиль остановлен");
    }
    public virtual void Speedup(int delta)
    {
        if (!_isStarted)
        {
            Console.WriteLine($"{_name}: сначала заведите автомобиль!");
            return;
        }

        _speed += delta;
        if (_speed > _maxSpeed)
            _speed = _maxSpeed;
        if (_speed < 0)
            _speed = 0;

        Console.WriteLine($"{_name}: скорость = {_speed} км/ч");
    }

    public void SlowDown(int delta)
    {
        Speedup(-delta);
    }

    public void RadioOn() => _radio.On();
    public void RadioOff() => _radio.Off();

    public void ShowInfo()
    {
        string status = _isStarted ? "заведён" : "заглушен";
        string radio = _radio.IsOn ? "вкл" : "выкл";
        Console.WriteLine($"{_name}: скорость={_speed} км/ч, {status}, радио {radio}, макс.скорость={_maxSpeed} км/ч");
    }
}
public class Toyota : Car
{
    public Toyota(string name, int maxSpeed) : base(name, maxSpeed) {}

    public override void Speedup(int delta)
    {
        if (!_isStarted)
        {
            Console.WriteLine($"{_name}: сначала заведите автомобиль!");
            return;
        }

        _speed += delta;

        if (_speed > _maxSpeed)
        {
            _speed = _maxSpeed;
            Console.WriteLine($"{_name}: достигнута максимальная скорость {_maxSpeed} км/ч!");
        }
        else
        {
            if (_speed < 0) _speed = 0;
            Console.WriteLine($"{_name}: скорость = {_speed} км/ч");
        }
    }
}

public class Ferrari : Car
{
    public Ferrari(string name, int maxSpeed) : base(name, maxSpeed) { }
}

public class Bugatti : Car
{
    public Bugatti(string name, int maxSpeed) : base(name, maxSpeed) { }
}
public class CarsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Лабораторная работа 2: Автомобили ===\n");

        Console.Write("Введите количество автомобилей: ");
        int count = int.Parse(Console.ReadLine() ?? "0");

        List<Car> cars = new List<Car>();

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\n--- Автомобиль {i + 1} ---");
            Console.WriteLine("Выберите тип автомобиля:");
            Console.WriteLine("1. Toyota");
            Console.WriteLine("2. Ferrari");
            Console.WriteLine("3. Bugatti");

            int type = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Введите название автомобиля: ");
            string name = Console.ReadLine() ?? "Без названия";

            Console.Write("Введите максимальную скорость (км/ч): ");
            int maxSpeed = int.Parse(Console.ReadLine() ?? "200");

            Car car = type switch
            {
                1 => new Toyota(name, maxSpeed),
                2 => new Ferrari(name, maxSpeed),
                3 => new Bugatti(name, maxSpeed),
                _ => new Toyota(name, maxSpeed)
            };
            cars.Add(car);
        }

        Console.WriteLine("\n=== Демонстрация автомобилей ===\n");

        foreach (var car in cars)
        {
            car.Start();
            car.RadioOn();

            Console.Write($"Введите ускорение для текущего авто: ");
            int accel = int.Parse(Console.ReadLine() ?? "50");
            car.Speedup(accel);

            Console.Write($"Введите замедление: ");
            int decel = int.Parse(Console.ReadLine() ?? "20");
            car.SlowDown(decel);

            car.ShowInfo();
            car.Stop();
            car.RadioOff();
            Console.WriteLine();
        }
    }
}
