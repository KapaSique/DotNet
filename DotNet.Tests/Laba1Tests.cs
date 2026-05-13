using DotNet.Laba1;

namespace DotNet.Tests;

public class LinearEquationTests
{
    [Theory]
    [InlineData(0, 0, "Комплексное число")]
    [InlineData(0, 5, "Нет решений")]
    [InlineData(2, -4, "x = 2")]
    [InlineData(5, 10, "x = -2")]
    [InlineData(-3, 6, "x = 2")]
    public void Solve_ReturnsCorrectResult(double a, double b, string expected)
    {
        Assert.Equal(expected, LinearEquation.Solve(a, b));
    }
}

public class MaxOfTwoTests
{
    [Theory]
    [InlineData(5, 3, 5)]
    [InlineData(3, 5, 5)]
    [InlineData(-1, -5, -1)]
    [InlineData(0, 0, 0)]
    [InlineData(3.14, 2.71, 3.14)]
    public void GetMax_ReturnsLargerValue(double x, double y, double expected)
    {
        Assert.Equal(expected, MaxOfTwo.GetMax(x, y));
    }
}

public class MushroomsTests
{
    [Theory]
    [InlineData(1, "гриб")]
    [InlineData(2, "гриба")]
    [InlineData(3, "гриба")]
    [InlineData(4, "гриба")]
    [InlineData(5, "грибов")]
    [InlineData(10, "грибов")]
    [InlineData(11, "грибов")]
    [InlineData(12, "грибов")]
    [InlineData(13, "грибов")]
    [InlineData(14, "грибов")]
    [InlineData(21, "гриб")]
    [InlineData(22, "гриба")]
    [InlineData(25, "грибов")]
    [InlineData(101, "гриб")]
    [InlineData(111, "грибов")]
    [InlineData(112, "грибов")]
    [InlineData(121, "гриб")]
    public void GetDeclension_ReturnsCorrectForm(int k, string expected)
    {
        Assert.Equal(expected, Mushrooms.GetDeclension(k));
    }
}

public class CircleAndSquareTests
{
    [Fact]
    public void CanSquareFitInCircle_LargeCircle_ReturnsTrue()
    {
        Assert.True(CircleAndSquare.CanSquareFitInCircle(314.159, 100));
    }

    [Fact]
    public void CanSquareFitInCircle_SmallCircle_ReturnsFalse()
    {
        Assert.False(CircleAndSquare.CanSquareFitInCircle(10, 100));
    }

    [Fact]
    public void CanCircleFitInSquare_LargeSquare_ReturnsTrue()
    {
        Assert.True(CircleAndSquare.CanCircleFitInSquare(3.14, 100));
    }

    [Fact]
    public void CanCircleFitInSquare_SmallSquare_ReturnsFalse()
    {
        Assert.False(CircleAndSquare.CanCircleFitInSquare(314.159, 10));
    }

    [Fact]
    public void VeryLargeCircle_SquareFits()
    {
        Assert.True(CircleAndSquare.CanSquareFitInCircle(1000, 10));
    }
}

public class SpeedComparisonTests
{
    [Theory]
    [InlineData(36, 10, 0)]
    [InlineData(72, 10, 1)]
    [InlineData(18, 10, -1)]
    [InlineData(3.6, 1, 0)]
    public void Compare_ReturnsCorrectComparison(double kmh, double ms, int expected)
    {
        Assert.Equal(expected, SpeedComparison.Compare(kmh, ms));
    }
}

public class AgeCalculatorTests
{
    [Fact]
    public void CalculateAge_BornThisYear_ReturnsZero()
    {
        var refDate = new DateTime(2025, 6, 15);
        int age = AgeCalculator.CalculateAge(2025, 1, refDate);
        Assert.Equal(0, age);
    }

    [Fact]
    public void CalculateAge_BornLastYear_ReturnsOne()
    {
        var refDate = new DateTime(2025, 6, 15);
        int age = AgeCalculator.CalculateAge(2024, 1, refDate);
        Assert.Equal(1, age);
    }

    [Fact]
    public void CalculateAge_BeforeBirthday_ReturnsCorrect()
    {
        var refDate = new DateTime(2025, 3, 15);
        int age = AgeCalculator.CalculateAge(2024, 6, refDate);
        Assert.Equal(0, age);
    }

    [Fact]
    public void CalculateAge_AfterBirthday_ReturnsCorrect()
    {
        var refDate = new DateTime(2025, 9, 15);
        int age = AgeCalculator.CalculateAge(2024, 6, refDate);
        Assert.Equal(1, age);
    }
}

public class NameGreetingTests
{
    [Theory]
    [InlineData("Иванова", "Здравствуйте, госпожа Иванова!")]
    [InlineData("Петрова", "Здравствуйте, госпожа Петрова!")]
    [InlineData("Иванов", "Здравствуйте, господин Иванов!")]
    [InlineData("Петров", "Здравствуйте, господин Петров!")]
    [InlineData("Сидоров", "Здравствуйте, господин Сидоров!")]
    public void GetGreeting_GenderedSurnames_ReturnsCorrect(string surname, string expected)
    {
        Assert.Equal(expected, NameGreeting.GetGreeting(surname));
    }

    [Theory]
    [InlineData("Ли", "Здравствуйте, Ли!")]
    [InlineData("Шмидт", "Здравствуйте, Шмидт!")]
    public void GetGreeting_NeutralSurnames_ReturnsNeutral(string surname, string expected)
    {
        Assert.Equal(expected, NameGreeting.GetGreeting(surname));
    }

    [Fact]
    public void GetGreeting_CaseInsensitiveOva()
    {
        Assert.Contains("госпожа", NameGreeting.GetGreeting("ИВАНОВА"));
    }
}

public class ScheduleTests
{
    [Theory]
    [InlineData(1, "Понедельник: Математика")]
    [InlineData(2, "Вторник: Физика")]
    [InlineData(3, "Среда: Программирование")]
    [InlineData(4, "Четверг: История")]
    [InlineData(5, "Пятница: Английский")]
    [InlineData(6, "Суббота: Химия")]
    [InlineData(7, "Воскресенье: Выходной")]
    public void GetScheduleForDay_ValidDays_ReturnsCorrectSchedule(int day, string expected)
    {
        Assert.Equal(expected, Schedule.GetScheduleForDay(day));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    [InlineData(-1)]
    [InlineData(100)]
    public void GetScheduleForDay_InvalidDays_ReturnsError(int day)
    {
        Assert.Equal("Неверный номер дня", Schedule.GetScheduleForDay(day));
    }
}
