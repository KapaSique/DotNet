using DotNet.Laba7;

namespace DotNet.Tests;

public class LinqQuerySyntaxTests
{
    [Fact]
    public void GetStudents_ReturnsSixStudents()
    {
        Assert.Equal(6, LinqQuerySyntax.GetStudents().Count);
    }

    [Fact]
    public void GetStudents_AllHaveIds()
    {
        var students = LinqQuerySyntax.GetStudents();
        Assert.All(students, s => Assert.True(s.Id > 0));
    }

    [Fact]
    public void GetMoscowRichStudents_FiltersCorrectly()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqQuerySyntax.GetMoscowRichStudents(students);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.Surname == "Иванов");
        Assert.Contains(result, s => s.Surname == "Федорова");
    }

    [Fact]
    public void GetMoscowRichStudents_AllHaveStipendAbove2000()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqQuerySyntax.GetMoscowRichStudents(students);
        Assert.All(result, s => Assert.True(s.Stipend > 2000));
    }

    [Fact]
    public void GetStudentsSortedByStipend_FirstIsHighest()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqQuerySyntax.GetStudentsSortedByStipend(students);
        Assert.Equal("Смирнова", result[0].Surname);
        Assert.Equal(3000m, result[0].Stipend);
    }

    [Fact]
    public void GetStudentsSortedByStipend_LastIsLowest()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqQuerySyntax.GetStudentsSortedByStipend(students);
        Assert.Equal("Козлов", result[^1].Surname);
        Assert.Equal(1800m, result[^1].Stipend);
    }
}

public class LinqLambdaSyntaxTests
{
    [Fact]
    public void GetKurs3Students_ReturnsCorrect()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqLambdaSyntax.GetKurs3Students(students);
        Assert.Equal(3, result.Count);
        Assert.All(result, s => Assert.Equal(3, s.Kurs));
    }

    [Fact]
    public void GetAverageStipend_ReturnsCorrect()
    {
        var students = LinqQuerySyntax.GetStudents();
        decimal avg = LinqLambdaSyntax.GetAverageStipend(students);
        decimal expected = (2500m + 2000m + 3000m + 1800m + 2800m + 2200m) / 6;
        Assert.Equal(expected, avg);
    }

    [Fact]
    public void GetMoscowCount_ReturnsFour()
    {
        var students = LinqQuerySyntax.GetStudents();
        Assert.Equal(4, LinqLambdaSyntax.GetMoscowCount(students));
    }
}

public class LinqGroupingTests
{
    [Fact]
    public void GetGroupsByCity_ReturnsTwoGroups()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqGrouping.GetGroupsByCity(students);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetGroupsByCity_MoscowHasFour()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqGrouping.GetGroupsByCity(students);
        var moscowGroup = result.First(g => g.Key == "Москва");
        Assert.Equal(4, moscowGroup.Count());
    }

    [Fact]
    public void GetGroupsByKurs_ReturnsFiveGroups()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqGrouping.GetGroupsByKurs(students);
        Assert.Equal(4, result.Count);
    }
}

public class LinqJoinTests
{
    [Fact]
    public void GetCourses_ReturnsSixCourses()
    {
        Assert.Equal(6, LinqJoin.GetCourses().Count);
    }

    [Fact]
    public void GetJoinedStudentCourses_ReturnsSixRows()
    {
        var students = LinqQuerySyntax.GetStudents();
        var courses = LinqJoin.GetCourses();
        var result = LinqJoin.GetJoinedStudentCourses(students, courses);
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public void GetJoinedStudentCourses_ContainsExpected()
    {
        var students = LinqQuerySyntax.GetStudents();
        var courses = LinqJoin.GetCourses();
        var result = LinqJoin.GetJoinedStudentCourses(students, courses);
        Assert.Contains(result, r => r.Surname == "Иванов" && r.Subject == "Математика" && r.Grade == 5);
        Assert.Contains(result, r => r.Surname == "Смирнова" && r.Subject == "Программирование");
    }
}

public class LinqToXmlDemo_Linq_Tests
{
    [Fact]
    public void GetTop3NonMoscow_ReturnsNonMoscowOnly()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqToXmlDemo.GetTop3NonMoscow(students);
        Assert.All(result, r => Assert.Equal("Казань", r.City));
    }

    [Fact]
    public void GetTop3NonMoscow_SortedByStipendDesc()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqToXmlDemo.GetTop3NonMoscow(students);
        Assert.Equal(2, result.Count);
        Assert.Equal(3000m, result[0].Stipend);
        Assert.Equal(2800m, result[1].Stipend);
    }

    [Fact]
    public void GetTop3NonMoscow_HasRanking()
    {
        var students = LinqQuerySyntax.GetStudents();
        var result = LinqToXmlDemo.GetTop3NonMoscow(students);
        Assert.Equal(1, result[0].Rank);
        Assert.Equal(2, result[1].Rank);
    }
}
