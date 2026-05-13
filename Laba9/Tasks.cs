using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNet.Laba9;

internal static class PlatformHelper
{
    public static bool IsDrawingSupported =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static void WarnIfNotSupported()
    {
        if (!IsDrawingSupported)
            Console.WriteLine("[!] System.Drawing требует Windows. На macOS/Linux показаны концепты.");
    }
}

// === Задание 1: Основные типы System.Drawing ===
public class DrawingBasics
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: Основные типы System.Drawing ===");
        PlatformHelper.WarnIfNotSupported();

        // Point и PointF
        Point p1 = new Point(10, 20);
        PointF pf1 = new PointF(10.5f, 20.3f);
        Console.WriteLine($"Point: {p1}");
        Console.WriteLine($"PointF: {pf1}");

        // Rectangle и RectangleF
        Rectangle rect = new Rectangle(10, 10, 100, 50);
        RectangleF rectF = new RectangleF(10.5f, 10.5f, 100.2f, 50.3f);
        Console.WriteLine($"Rectangle: {rect}");
        Console.WriteLine($"RectangleF: {rectF}");

        // Color
        Color customColor = Color.FromArgb(255, 100, 50, 200);
        Console.WriteLine($"\nЦвета:");
        Console.WriteLine($"  Color.Red = {Color.Red}");
        Console.WriteLine($"  Color.Blue = {Color.Blue}");
        Console.WriteLine($"  Custom (100,50,200) = {customColor}");

        // Pen и Brush (только на Windows)
        if (PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine($"\nПерья (Pen):");
            using Pen pen1 = new Pen(Color.Red, 2.0f);
            using Pen pen2 = new Pen(Color.Blue, 3.5f);
            Console.WriteLine($"  Pen1: цвет={pen1.Color}, ширина={pen1.Width}");
            Console.WriteLine($"  Pen2: цвет={pen2.Color}, ширина={pen2.Width}");

            Console.WriteLine($"\nКисти (Brush):");
            Console.WriteLine($"  Brushes.Red, Brushes.Blue, Brushes.Green...");
            Console.WriteLine($"  SystemBrushes.ActiveCaption, SystemBrushes.Control...");
        }
        else
        {
            Console.WriteLine($"\nПерья (Pen): Pen(Color, width) — API описан");
            Console.WriteLine($"  Pen pen = new Pen(Color.Red, 2.0f);");
            Console.WriteLine($"Кисти (Brush): Brushes.Red, SolidBrush, HatchBrush — API описан");
        }
    }
}

// === Задание 2: Рисование на Graphics (Bitmap) ===
public class GraphicsDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Рисование на Graphics ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Демонстрация концептов (без рендеринга):");
            Console.WriteLine("  using Bitmap bmp = new Bitmap(800, 600);");
            Console.WriteLine("  using Graphics g = Graphics.FromImage(bmp);");
            Console.WriteLine("  g.SmoothingMode = SmoothingMode.AntiAlias;");
            Console.WriteLine("  g.DrawLine(pen, 50, 50, 200, 50);");
            Console.WriteLine("  g.DrawRectangle(dashPen, 70, 70, 160, 120);");
            Console.WriteLine("  g.FillEllipse(brush, 250, 50, 150, 150);");
            Console.WriteLine("  g.DrawString(\"Text\", font, brush, 50, 300);");
            Console.WriteLine("  bmp.Save(\"output.png\", ImageFormat.Png);");
            Console.WriteLine("[OK] Код корректен, рендеринг только на Windows.");
            return;
        }

        string outputPath = "Laba9/drawing_output.png";
        Directory.CreateDirectory("Laba9");

        using Bitmap bmp = new Bitmap(800, 600);
        using Graphics g = Graphics.FromImage(bmp);

        // Сглаживание
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        // Pen — рисуем линии
        using Pen redPen = new Pen(Color.Red, 3);
        g.DrawLine(redPen, 50, 50, 200, 50);
        g.DrawLine(redPen, 50, 50, 50, 200);

        // Pen с DashStyle
        using Pen dashPen = new Pen(Color.Blue, 3);
        dashPen.DashStyle = DashStyle.Dash;
        g.DrawRectangle(dashPen, 70, 70, 160, 120);

        // Brush — заливка
        using SolidBrush greenBrush = new SolidBrush(Color.FromArgb(128, 0, 255, 0));
        g.FillEllipse(greenBrush, 250, 50, 150, 150);

        // Градиентная кисть
        using LinearGradientBrush gradientBrush = new LinearGradientBrush(
            new Point(420, 50), new Point(570, 200),
            Color.Yellow, Color.Red);
        g.FillRectangle(gradientBrush, 420, 50, 150, 150);

        // Эллипс
        using Pen purplePen = new Pen(Color.Purple, 4);
        g.DrawEllipse(purplePen, 420, 50, 150, 150);

        // Текст
        using Font font = new Font("Arial", 20, FontStyle.Bold);
        using SolidBrush textBrush = new SolidBrush(Color.Black);
        g.DrawString("GDI+ Graphics Demo", font, textBrush, 50, 300);

        // Полигон (треугольник)
        Point[] triangle = {
            new(50, 450), new(200, 550), new(100, 400)
        };
        using Pen greenPen = new Pen(Color.Green, 2);
        g.DrawPolygon(greenPen, triangle);
        using SolidBrush fillBrush = new SolidBrush(Color.FromArgb(80, 0, 255, 0));
        g.FillPolygon(fillBrush, triangle);

        // Pie (сектор)
        using Pen orangePen = new Pen(Color.Orange, 2);
        g.DrawPie(orangePen, 250, 400, 150, 150, 30, 120);

        // Сохраняем
        bmp.Save(outputPath, ImageFormat.Png);
        Console.WriteLine($"Изображение сохранено: {outputPath}");
        Console.WriteLine("(800x600, линии, прямоугольник, эллипс, градиент, текст, полигон, сектор)");
    }
}

// === Задание 3: Цвета и диалог цвета ===
public class ColorDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: Работа с цветом ===");

        Console.WriteLine("Именованные цвета (первые 12):");
        Color[] colors = {
            Color.Red, Color.Green, Color.Blue, Color.Yellow,
            Color.Cyan, Color.Magenta, Color.Orange, Color.Purple,
            Color.Brown, Color.Pink, Color.Gray, Color.Teal
        };

        int index = 0;
        foreach (var c in colors)
        {
            Console.Write($"  {c.Name,-12}");
            index++;
            if (index % 4 == 0) Console.WriteLine();
        }
        Console.WriteLine();

        // FromArgb
        Console.WriteLine("\nСоздание цветов через FromArgb:");
        Color semiRed = Color.FromArgb(128, 255, 0, 0);
        Color custom = Color.FromArgb(100, 150, 200);
        Console.WriteLine($"  Полупрозрачный красный: {semiRed} (A={semiRed.A})");
        Console.WriteLine($"  RGB(100,150,200): {custom}");

        // Преобразования HSB
        Console.WriteLine("\nПреобразования цвета:");
        float hue = Color.Red.GetHue();
        float saturation = Color.Red.GetSaturation();
        float brightness = Color.Red.GetBrightness();
        Console.WriteLine($"  Red: H={hue:F1}°, S={saturation:F2}, B={brightness:F2}");
    }
}

// === Задание 4: Перья (Pen) и кисти (Brush) ===
public class PensAndBrushesDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Перья и кисти — полный обзор ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Демонстрация Pen/Brush API:");
            Console.WriteLine("  DashStyle: Solid, Dash, Dot, DashDot, DashDotDot");
            Console.WriteLine("  HatchStyle: Cross, DiagonalCross, Vertical, Horizontal");
            Console.WriteLine("  LineCap: Round, ArrowAnchor, Square, DiamondAnchor");
            Console.WriteLine("  Brushes: Red, Blue, Green, Black...");
            Console.WriteLine("[OK] API продемонстрирован. Рендеринг только на Windows.");
            return;
        }

        using Bitmap bmp = new Bitmap(700, 500);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        int y = 30;
        using Font font = new Font("Arial", 10);

        // Разные стили Pen
        g.DrawString("Стили Pen (DashStyle):", font, Brushes.Black, 10, y);
        y += 25;

        DashStyle[] styles = { DashStyle.Solid, DashStyle.Dash, DashStyle.Dot,
                               DashStyle.DashDot, DashStyle.DashDotDot };
        foreach (var style in styles)
        {
            using Pen pen = new Pen(Color.Blue, 2);
            pen.DashStyle = style;
            g.DrawString(style.ToString(), font, Brushes.Black, 10, y + 5);
            g.DrawLine(pen, 120, y + 10, 350, y + 10);
            y += 30;
        }

        // HatchBrush styles
        y = 30;
        g.DrawString("Типы штриховых кистей (HatchStyle):", font, Brushes.Black, 380, y);
        y += 25;

        HatchStyle[] hatchStyles = { HatchStyle.Cross, HatchStyle.DiagonalCross,
                                      HatchStyle.Vertical, HatchStyle.Horizontal,
                                      HatchStyle.ForwardDiagonal };

        foreach (var hs in hatchStyles)
        {
            using HatchBrush hb = new HatchBrush(hs, Color.Black, Color.White);
            g.FillRectangle(hb, 380, y, 150, 20);
            g.DrawString(hs.ToString(), font, Brushes.Black, 540, y + 3);
            y += 30;
        }

        // Caps and joins
        y = 300;
        g.DrawString("Pen Caps и Line Joins:", font, Brushes.Black, 10, y);
        y += 30;

        using (Pen capPen = new Pen(Color.Red, 8))
        {
            capPen.StartCap = LineCap.Round;
            capPen.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(capPen, 10, y, 200, y);
            g.DrawString("Round/ArrowAnchor", font, Brushes.Black, 220, y - 5);
        }

        y += 40;
        using (Pen capPen = new Pen(Color.Green, 8))
        {
            capPen.StartCap = LineCap.Square;
            capPen.EndCap = LineCap.DiamondAnchor;
            g.DrawLine(capPen, 10, y, 200, y);
            g.DrawString("Square/DiamondAnchor", font, Brushes.Black, 220, y - 5);
        }

        y += 40;
        using (Pen capPen = new Pen(Color.Blue, 8))
        {
            capPen.StartCap = LineCap.Triangle;
            capPen.EndCap = LineCap.RoundAnchor;
            g.DrawLine(capPen, 10, y, 200, y);
            g.DrawString("Triangle/RoundAnchor", font, Brushes.Black, 220, y - 5);
        }

        string outputPath = "Laba9/pens_and_brushes.png";
        bmp.Save(outputPath, ImageFormat.Png);
        Console.WriteLine($"Изображение сохранено: {outputPath}");
    }
}

// === Задание 5: Вывод изображений ===
public class ImageOutputDemo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 5: Вывод изображений ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Информация об изображении (концепты):");
            Console.WriteLine("  Bitmap bmp = new Bitmap(600, 400);");
            Console.WriteLine("  g.DrawString(\"Title\", font, Brushes.Navy, 20, 20);");
            Console.WriteLine("  // SizeMode: Normal, StretchImage, AutoSize, CenterImage, Zoom");
            Console.WriteLine("  bmp.Save(\"output.png\", ImageFormat.Png);");
            Console.WriteLine("[OK] Концепты продемонстрированы. Рендеринг только на Windows.");
            return;
        }

        using Bitmap bmp = new Bitmap(600, 400);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        // Рисуем несколько фигур как демонстрацию
        using Font titleFont = new Font("Arial", 16, FontStyle.Bold);
        g.DrawString("Image Output Demo", titleFont, Brushes.Navy, 20, 20);

        // Размеры изображения
        Console.WriteLine($"Размеры изображения: {bmp.Width}x{bmp.Height}");
        Console.WriteLine($"Горизонтальное разрешение: {bmp.HorizontalResolution} DPI");
        Console.WriteLine($"Вертикальное разрешение: {bmp.VerticalResolution} DPI");
        Console.WriteLine($"Формат пикселей: {bmp.PixelFormat}");
        Console.WriteLine($"Глубина цвета: {Image.GetPixelFormatSize(bmp.PixelFormat)} бит");

        // SizeMode (из Лекции9)
        Console.WriteLine("\nРежимы отображения (SizeMode):");
        Console.WriteLine("  Normal — изображение в исходном размере");
        Console.WriteLine("  StretchImage — растянуть по размерам PictureBox");
        Console.WriteLine("  AutoSize — подогнать PictureBox под изображение");
        Console.WriteLine("  CenterImage — по центру");
        Console.WriteLine("  Zoom — пропорционально с сохранением пропорций");

        // Нарисовать какую-то демонстрационную сцену
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Солнце
        using SolidBrush sunBrush = new SolidBrush(Color.Gold);
        g.FillEllipse(sunBrush, 450, 30, 80, 80);

        // Дом
        using Pen housePen = new Pen(Color.Brown, 3);
        g.DrawRectangle(housePen, 50, 200, 150, 120);
        Point[] roof = { new(20, 200), new(125, 120), new(230, 200) };
        g.DrawPolygon(housePen, roof);
        using SolidBrush roofBrush = new SolidBrush(Color.SaddleBrown);
        g.FillPolygon(roofBrush, roof);

        // Дверь
        using SolidBrush doorBrush = new SolidBrush(Color.DarkRed);
        g.FillRectangle(doorBrush, 100, 240, 45, 80);

        string outputPath = "Laba9/image_output.png";
        bmp.Save(outputPath, ImageFormat.Png);
        Console.WriteLine($"\nИзображение сохранено: {outputPath}");
    }
}
