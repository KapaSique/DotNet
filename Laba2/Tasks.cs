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
        Speedup(-Math.Abs(delta));
    }

    public int Speed => _speed;

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
    public Toyota(string name) : base(name, 200) {}

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

            Car car;
            if (type == 1)
            {
                car = new Toyota(name);
                Console.WriteLine("Toyota: максимальная скорость 200 км/ч");
            }
            else
            {
                Console.Write("Введите максимальную скорость (км/ч): ");
                int maxSpeed = int.Parse(Console.ReadLine() ?? "200");
                car = type switch
                {
                    2 => new Ferrari(name, maxSpeed),
                    3 => new Bugatti(name, maxSpeed),
                    _ => new Ferrari(name, maxSpeed)
                };
            }

            Console.Write("Завести автомобиль? (да/нет): ");
            string start = Console.ReadLine() ?? "нет";
            if (start.Equals("да", StringComparison.OrdinalIgnoreCase))
                car.Start();

            cars.Add(car);
        }

        Console.WriteLine("\n=== Демонстрация автомобилей ===\n");

        foreach (var car in cars)
        {
            car.RadioOn();

            Console.Write("Введите ускорение для текущего авто: ");
            int accel = Math.Abs(int.Parse(Console.ReadLine() ?? "50"));
            car.Speedup(accel);

            while (car.Speed > 0)
            {
                Console.Write($"Текущая скорость: {car.Speed} км/ч. Введите замедление (0 - стоп): ");
                int decel = Math.Abs(int.Parse(Console.ReadLine() ?? "0"));
                if (decel == 0) break;
                if (decel > car.Speed)
                {
                    Console.WriteLine($"Замедление не может быть больше текущей скорости ({car.Speed} км/ч)!");
                    continue;
                }
                car.SlowDown(decel);
            }

            car.ShowInfo();
            car.Stop();
            car.RadioOff();
            Console.WriteLine();
        }
    }
}
