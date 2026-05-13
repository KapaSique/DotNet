using DotNet.Laba4;

namespace DotNet.Tests;

public class StringReplaceTests
{
    [Fact]
    public void ProcessString_NoRepeats_ReturnsSame()
    {
        Assert.Equal("Корабли лавировали", StringReplace.ProcessString("Корабли лавировали"));
    }

    [Fact]
    public void ProcessString_OneRepeat_ReversesSecond()
    {
        string result = StringReplace.ProcessString("Корабли лавировали лавировали");
        Assert.Equal("Корабли лавировали илаворивал", result);
    }

    [Fact]
    public void ProcessString_SingleWord_ReturnsSame()
    {
        Assert.Equal("Привет", StringReplace.ProcessString("Привет"));
    }

    [Fact]
    public void ProcessString_EmptyString_ReturnsEmpty()
    {
        Assert.Equal("", StringReplace.ProcessString(""));
    }
}

public class SquareMatrixTests
{
    [Fact]
    public void FindMaxInRows_2x2_ReturnsCorrect()
    {
        int[,] matrix = { { 1, 5 }, { 8, 3 } };
        int[] result = SquareMatrix.FindMaxInRows(matrix);
        Assert.Equal(new[] { 5, 8 }, result);
    }

    [Fact]
    public void FindMaxInRows_3x3_ReturnsCorrect()
    {
        int[,] matrix = { { 10, 20, 15 }, { 7, 9, 12 }, { 33, 22, 11 } };
        int[] result = SquareMatrix.FindMaxInRows(matrix);
        Assert.Equal(new[] { 20, 12, 33 }, result);
    }

    [Fact]
    public void FindMaxInRows_AllEqual_ReturnsSame()
    {
        int[,] matrix = { { 5, 5 }, { 5, 5 } };
        int[] result = SquareMatrix.FindMaxInRows(matrix);
        Assert.Equal(new[] { 5, 5 }, result);
    }

    [Fact]
    public void FindMaxInRows_1x1_ReturnsSingle()
    {
        int[,] matrix = { { 42 } };
        Assert.Equal(new[] { 42 }, SquareMatrix.FindMaxInRows(matrix));
    }
}

public class JaggedCharMatrixTests
{
    [Fact]
    public void ReplaceChar_ReplacesAll()
    {
        char[][] matrix = { new[] { 'a', 'b' }, new[] { 'a', 'a' } };
        char[][] result = JaggedCharMatrix.ReplaceChar(matrix, 'a', 'e');
        Assert.Equal(new[] { 'e', 'b' }, result[0]);
        Assert.Equal(new[] { 'e', 'e' }, result[1]);
    }

    [Fact]
    public void ReplaceChar_NoMatches_ReturnsCopy()
    {
        char[][] matrix = { new[] { 'b', 'c' } };
        char[][] result = JaggedCharMatrix.ReplaceChar(matrix, 'a', 'e');
        Assert.Equal(new[] { 'b', 'c' }, result[0]);
    }

    [Fact]
    public void Transpose_2x3_Becomes_3x2()
    {
        char[][] matrix = { new[] { 'a', 'b', 'c' }, new[] { 'd', 'e', 'f' } };
        char[][] result = JaggedCharMatrix.Transpose(matrix);
        Assert.Equal(3, result.Length);
        Assert.Equal(2, result[0].Length);
        Assert.Equal('a', result[0][0]);
        Assert.Equal('d', result[0][1]);
        Assert.Equal('b', result[1][0]);
        Assert.Equal('e', result[1][1]);
    }

    [Fact]
    public void Transpose_JaggedInput_HandlesGaps()
    {
        char[][] matrix = { new[] { 'a', 'b' }, new[] { 'c' } };
        char[][] result = JaggedCharMatrix.Transpose(matrix);
        Assert.Equal(2, result.Length);
        Assert.Equal('a', result[0][0]);
        Assert.Equal('c', result[0][1]);
        Assert.Equal('b', result[1][0]);
        Assert.Equal(' ', result[1][1]);
    }
}

public class ConstantsDemoTests
{
    [Fact]
    public void Pi_HasCorrectValue()
    {
        Assert.Equal(3.14159, ConstantsDemo.Pi);
    }

    [Fact]
    public void AppName_HasCorrectValue()
    {
        Assert.Equal("DotNet Labs", ConstantsDemo.AppName);
    }
}

public class StaticDemoTests
{
    [Fact]
    public void Average_NormalArray_ReturnsCorrect()
    {
        double[] values = { 1, 2, 3, 4, 5 };
        Assert.Equal(3.0, StaticDemo.Average(values));
    }

    [Fact]
    public void Average_EmptyArray_ReturnsZero()
    {
        Assert.Equal(0.0, StaticDemo.Average(Array.Empty<double>()));
    }

    [Fact]
    public void Average_SingleValue_ReturnsValue()
    {
        Assert.Equal(7.5, StaticDemo.Average(new[] { 7.5 }));
    }

    [Fact]
    public void Average_NegativeValues_ReturnsCorrect()
    {
        Assert.Equal(0.0, StaticDemo.Average(new[] { -1.0, 0.0, 1.0 }));
    }
}

public class StudentGroupTests
{
    [Fact]
    public void Indexer_ValidIndex_ReturnsStudent()
    {
        var group = new StudentGroup(3);
        Assert.Equal("Студент_1", group[0]);
        Assert.Equal("Студент_3", group[2]);
    }

    [Fact]
    public void Indexer_InvalidIndex_ReturnsError()
    {
        var group = new StudentGroup(3);
        Assert.Equal("Индекс вне диапазона", group[5]);
        Assert.Equal("Индекс вне диапазона", group[-1]);
    }

    [Fact]
    public void Indexer_Set_UpdatesValue()
    {
        var group = new StudentGroup(3);
        group[1] = "Иванов";
        Assert.Equal("Иванов", group[1]);
    }

    [Fact]
    public void Count_ReturnsCorrectSize()
    {
        Assert.Equal(5, new StudentGroup(5).Count);
        Assert.Equal(1, new StudentGroup(1).Count);
    }
}

public class MathOperationsTests
{
    [Theory]
    [InlineData(5, 10, 15)]
    [InlineData(-3, 7, 4)]
    [InlineData(0, 0, 0)]
    public void Add_Int_ReturnsSum(int a, int b, int expected)
    {
        Assert.Equal(expected, MathOperations.Add(a, b));
    }

    [Theory]
    [InlineData(3.14, 2.86, 6.0)]
    [InlineData(-1.5, 1.5, 0.0)]
    public void Add_Double_ReturnsSum(double a, double b, double expected)
    {
        Assert.Equal(expected, MathOperations.Add(a, b), 2);
    }

    [Fact]
    public void Add_Strings_ReturnsConcatenation()
    {
        Assert.Equal("Hello World", MathOperations.Add("Hello", " World"));
    }

    [Theory]
    [InlineData(1, 2, 3, 6)]
    [InlineData(-1, 0, 1, 0)]
    public void Add_ThreeInts_ReturnsSum(int a, int b, int c, int expected)
    {
        Assert.Equal(expected, MathOperations.Add(a, b, c));
    }
}

public class Vector2DTests
{
    [Fact]
    public void Add_TwoVectors_ReturnsCorrect()
    {
        var v1 = new Vector2D(3, 4);
        var v2 = new Vector2D(1, 2);
        var result = v1 + v2;
        Assert.Equal(4, result.X);
        Assert.Equal(6, result.Y);
    }

    [Fact]
    public void Subtract_TwoVectors_ReturnsCorrect()
    {
        var v1 = new Vector2D(5, 5);
        var v2 = new Vector2D(2, 3);
        var result = v1 - v2;
        Assert.Equal(3, result.X);
        Assert.Equal(2, result.Y);
    }

    [Fact]
    public void Multiply_Scalar_ReturnsCorrect()
    {
        var v = new Vector2D(3, 4);
        var result = v * 2.5;
        Assert.Equal(7.5, result.X);
        Assert.Equal(10, result.Y);
    }

    [Fact]
    public void Multiply_Zero_ReturnsZeroVector()
    {
        var v = new Vector2D(5, 10);
        var result = v * 0;
        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        Assert.Equal("(3, 4)", new Vector2D(3, 4).ToString());
        Assert.Equal("(0, 0)", new Vector2D(0, 0).ToString());
    }

    [Fact]
    public void ChainedOperations_ReturnCorrect()
    {
        var v1 = new Vector2D(3, 4);
        var v2 = new Vector2D(1, 2);
        var result = (v1 + v2) * 2;
        Assert.Equal(8, result.X);
        Assert.Equal(12, result.Y);
    }
}
