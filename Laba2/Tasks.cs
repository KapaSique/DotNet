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
    public Toyota() : base("Toyota", 300) {}

    public override void Speedup(int delta)
    {
        if (!_isStarted)
        {
            Console.WriteLine($"{_name}: сначала заведите автомобиль!");
            return;
        }

        _speed += delta;

        if (_speed > 300)
        {
            _speed = 300;
            Console.WriteLine($"{_name}: достигнута максимальная скорость 300 км/ч!");
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
    public Ferrari() : base("Ferrari", 350) { }
}

public class Bugatti : Car
{
    public Bugatti() : base("Bugatti", 420) { }
}
public class CarsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Лабораторная работа 2: Автомобили ===\n");

        Car[] cars = { new Toyota(), new Ferrari(), new Bugatti() };

        foreach (var car in cars)
        {
            car.Start();
            car.RadioOn();
            car.Speedup(100);
            car.Speedup(150);
            car.Speedup(100);  
            car.SlowDown(50);
            car.ShowInfo();
            car.Stop();
            car.RadioOff();
            Console.WriteLine();
        }
    }
}
