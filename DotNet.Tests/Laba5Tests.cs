using DotNet.Laba5;

namespace DotNet.Tests;

public class DelegateBasicsTests
{
    [Fact]
    public void ToUpperString_ConvertsCorrectly()
    {
        Assert.Equal("ПРИВЕТ", DelegateBasics.ToUpperString("Привет"));
    }

    [Fact]
    public void ToUpperString_AlreadyUpper_StaysSame()
    {
        Assert.Equal("HELLO", DelegateBasics.ToUpperString("HELLO"));
    }

    [Fact]
    public void ToLowerString_ConvertsCorrectly()
    {
        Assert.Equal("привет", DelegateBasics.ToLowerString("ПРИВЕТ"));
    }

    [Fact]
    public void ReverseString_ReversesCorrectly()
    {
        Assert.Equal("!тевирП", DelegateBasics.ReverseString("Привет!"));
    }

    [Fact]
    public void ReverseString_Palindrome_StaysSame()
    {
        Assert.Equal("казак", DelegateBasics.ReverseString("казак"));
    }
}

public class BuiltInDelegatesTests
{
    [Theory]
    [InlineData(3.5, 2.5, 6.0)]
    [InlineData(-1, 5, 4)]
    [InlineData(0, 0, 0)]
    public void AddOperation_ReturnsSum(double a, double b, double expected)
    {
        Assert.Equal(expected, BuiltInDelegates.AddOperation(a, b));
    }

    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(3.5, 2, 7)]
    [InlineData(5, 0, 0)]
    public void MultiplyOperation_ReturnsProduct(double a, double b, double expected)
    {
        Assert.Equal(expected, BuiltInDelegates.MultiplyOperation(a, b));
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    [InlineData(-4, true)]
    public void IsEven_ReturnsCorrect(int n, bool expected)
    {
        Assert.Equal(expected, BuiltInDelegates.IsEven(n));
    }

    [Fact]
    public void GetEvenNumbers_ReturnsOnlyEvens()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        var result = BuiltInDelegates.GetEvenNumbers(numbers);
        Assert.Equal(new List<int> { 2, 4, 6 }, result);
    }

    [Fact]
    public void GetEvenNumbers_AllOdds_ReturnsEmpty()
    {
        var numbers = new List<int> { 1, 3, 5 };
        var result = BuiltInDelegates.GetEvenNumbers(numbers);
        Assert.Empty(result);
    }
}

public class AsyncDelegateDemoTests
{
    [Fact]
    public void LongOperation_ReturnsUppercased()
    {
        string result = AsyncDelegateDemo.LongOperation("hello", 10);
        Assert.Contains("HELLO", result);
    }

    [Fact]
    public void LongOperation_ReturnsNonEmpty()
    {
        string result = AsyncDelegateDemo.LongOperation("test", 10);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}

public class TemperatureSensorTests
{
    [Fact]
    public void SetTemperature_RaisesEvent()
    {
        var sensor = new TemperatureSensor();
        TemperatureEventArgs? args = null;
        sensor.TemperatureChanged += (_, e) => args = e;

        sensor.SetTemperature(25.0);

        Assert.NotNull(args);
        Assert.Equal(25.0, args!.Temperature);
    }

    [Fact]
    public void SetTemperature_MultipleCalls_RaiseEvents()
    {
        var sensor = new TemperatureSensor();
        var temps = new List<double>();
        sensor.TemperatureChanged += (_, e) => temps.Add(e.Temperature);

        sensor.SetTemperature(20);
        sensor.SetTemperature(30);
        sensor.SetTemperature(10);

        Assert.Equal(new[] { 20.0, 30.0, 10.0 }, temps);
    }

    [Fact]
    public void UnsubscribedHandler_NotCalled()
    {
        var sensor = new TemperatureSensor();
        var callCount = 0;
        void handler(object? s, TemperatureEventArgs e) => callCount++;

        sensor.TemperatureChanged += handler;
        sensor.SetTemperature(20);
        sensor.TemperatureChanged -= handler;
        sensor.SetTemperature(30);

        Assert.Equal(1, callCount);
    }
}
