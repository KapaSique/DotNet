using System.Xml;
using System.Xml.Linq;
using DotNet.Laba11;

namespace DotNet.Tests;

public class BookInfoTests
{
    [Fact]
    public void Properties_GetSet()
    {
        var b = new BookInfo { Title = "War and Peace", Author = "Tolstoy", Year = "1869" };
        Assert.Equal("War and Peace", b.Title);
        Assert.Equal("Tolstoy", b.Author);
        Assert.Equal("1869", b.Year);
    }
}

public class LinqToXmlDemoTests
{
    [Fact]
    public void CreateLibraryDocument_ReturnsXDocument()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        Assert.NotNull(doc);
        Assert.NotNull(doc.Root);
    }

    [Fact]
    public void CreateLibraryDocument_HasThreeBooks()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        Assert.Equal(3, doc.Descendants("book").Count());
    }

    [Fact]
    public void GetAllBooks_ReturnsThreeBooks()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        var books = LinqToXmlDemo.GetAllBooks(doc);
        Assert.Equal(3, books.Count);
    }

    [Fact]
    public void GetAllBooks_FirstBookIsCorrect()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        var books = LinqToXmlDemo.GetAllBooks(doc);
        Assert.Equal("Война и мир", books[0].Title);
        Assert.Equal("Толстой Л.Н.", books[0].Author);
        Assert.Equal("1869", books[0].Year);
    }

    [Fact]
    public void GetOldBooks_ReturnsBooksBefore1900()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        var oldBooks = LinqToXmlDemo.GetOldBooks(doc);
        Assert.Equal(2, oldBooks.Count);
        Assert.Contains(oldBooks, b => b.Title == "Война и мир");
        Assert.Contains(oldBooks, b => b.Title == "Преступление и наказание");
    }

    [Fact]
    public void GetOldBooks_ExcludesModernBooks()
    {
        var doc = LinqToXmlDemo.CreateLibraryDocument();
        var oldBooks = LinqToXmlDemo.GetOldBooks(doc);
        Assert.DoesNotContain(oldBooks, b => b.Title == "Мастер и Маргарита");
    }
}

public class XmlDomDemoTests
{
    [Fact]
    public void AddStudent_AddsCorrectElements()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("students");
        doc.AppendChild(root);

        XmlDomDemo.AddStudent(doc, root, "1", "Иванов", "Петр", "2500", "3");

        Assert.Equal(1, root.ChildNodes.Count);
        var student = root.ChildNodes[0]!;
        Assert.Equal("student", student.Name);
        Assert.Equal("1", student.Attributes!["id"]!.Value);
        Assert.Equal("Иванов", student["surname"]!.InnerText);
        Assert.Equal("Петр", student["name"]!.InnerText);
    }

    [Fact]
    public void AddStudent_MultipleStudents_AddedCorrectly()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("students");
        doc.AppendChild(root);

        XmlDomDemo.AddStudent(doc, root, "1", "A", "B", "100", "1");
        XmlDomDemo.AddStudent(doc, root, "2", "C", "D", "200", "2");

        Assert.Equal(2, root.ChildNodes.Count);
    }
}

public class XmlEditDemoTests
{
    [Fact]
    public void AddElement_CreatesAndAppendsElement()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("root");
        doc.AppendChild(root);

        var editDemo = typeof(XmlEditDemo);
        var addElementMethod = editDemo.GetMethod("AddElement",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        if (addElementMethod != null)
        {
            addElementMethod.Invoke(null, new object[] { doc, root, "tag", "value" });
            Assert.Equal(1, root.ChildNodes.Count);
            Assert.Equal("tag", root.ChildNodes[0]!.Name);
            Assert.Equal("value", root.ChildNodes[0]!.InnerText);
        }
    }
}
