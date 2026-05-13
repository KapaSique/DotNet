using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNet.Laba10;

internal static class PlatformHelper
{
    public static bool IsDrawingSupported =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static void WarnIfNotSupported()
    {
        if (!IsDrawingSupported)
            Console.WriteLine("[!] System.Drawing требует Windows. На macOS/Linux — демонстрация API.");
    }
}

// === Задание 1: Загрузка и информация об изображении ===
public class ImageInfo
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 1: Загрузка и информация об изображении ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Демонстрация API (без создания файла):");
            Console.WriteLine("  using Bitmap bmp = new Bitmap(path);");
            Console.WriteLine("  bmp.Width, bmp.Height, bmp.PixelFormat");
            Console.WriteLine("  bmp.HorizontalResolution, bmp.VerticalResolution");
            Console.WriteLine("[OK] API продемонстрирован. Рендеринг только на Windows.");
            return;
        }

        string inputPath = "Laba10/input.png";
        Directory.CreateDirectory("Laba10");
        CreateTestImage(inputPath);

        using Bitmap bmp = new Bitmap(inputPath);
        Console.WriteLine($"Файл: {inputPath}");
        Console.WriteLine($"Размер: {bmp.Width}x{bmp.Height}");
        Console.WriteLine($"Формат пикселей: {bmp.PixelFormat}");
        Console.WriteLine($"Горизонтальное разрешение: {bmp.HorizontalResolution} DPI");
        Console.WriteLine($"Вертикальное разрешение: {bmp.VerticalResolution} DPI");
        Console.WriteLine($"Размер изображения (байты): {new FileInfo(inputPath).Length}");
    }

    private static void CreateTestImage(string path)
    {
        using Bitmap bmp = new Bitmap(400, 300);
        using Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.SkyBlue);

        // Солнце
        g.FillEllipse(Brushes.Gold, 300, 20, 70, 70);
        // Трава
        g.FillRectangle(Brushes.ForestGreen, 0, 220, 400, 80);
        // Дом
        g.FillRectangle(Brushes.SaddleBrown, 100, 150, 80, 70);
        Point[] roof = { new(80, 150), new(140, 100), new(200, 150) };
        g.FillPolygon(Brushes.DarkRed, roof);
        // Текст
        using Font font = new Font("Arial", 14, FontStyle.Bold);
        g.DrawString("Original Image", font, Brushes.White, 10, 10);

        bmp.Save(path, ImageFormat.Png);
    }
}

// === Задание 2: Фильтры обработки изображений ===
public class ImageFilters
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 2: Фильтры обработки изображений ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Доступные фильтры (как в Accord.NET):");
            Console.WriteLine("  - Grayscale (оттенки серого)");
            Console.WriteLine("  - Invert (инвертирование)");
            Console.WriteLine("  - Brightness Correction (яркость)");
            Console.WriteLine("  - Contrast Correction (контрастность)");
            Console.WriteLine("  - Box Blur / Gaussian Blur (размытие)");
            Console.WriteLine("  - Sepia (сепия)");
            Console.WriteLine("  - Sharpen (повышение резкости)");
            Console.WriteLine("  - Edge Detection / Sobel (выделение границ)");
            Console.WriteLine("[OK] Все фильтры описаны. Рендеринг только на Windows.");
            return;
        }

        string inputPath = "Laba10/input.png";
        if (!File.Exists(inputPath))
            ImageInfo.Run();

        using Bitmap original = new Bitmap(inputPath);
        Console.WriteLine($"Обрабатывается изображение {original.Width}x{original.Height}");

        // 1. Grayscale (оттенки серого)
        using Bitmap grayscale = ApplyGrayscale(original);
        grayscale.Save("Laba10/output_grayscale.png", ImageFormat.Png);
        Console.WriteLine("[OK] Grayscale — Laba10/output_grayscale.png");

        // 2. Invert (инвертирование)
        using Bitmap inverted = ApplyInvert(original);
        inverted.Save("Laba10/output_invert.png", ImageFormat.Png);
        Console.WriteLine("[OK] Invert — Laba10/output_invert.png");

        // 3. Brightness (яркость +50)
        using Bitmap bright = ApplyBrightness(original, 50);
        bright.Save("Laba10/output_brightness.png", ImageFormat.Png);
        Console.WriteLine("[OK] Brightness +50 — Laba10/output_brightness.png");

        // 4. Blur (размытие — box blur)
        using Bitmap blurred = ApplyBoxBlur(original, 5);
        blurred.Save("Laba10/output_blur.png", ImageFormat.Png);
        Console.WriteLine("[OK] Box Blur (kernel=5) — Laba10/output_blur.png");

        // 5. Contrast (контрастность -15)
        using Bitmap contrast = ApplyContrast(original, -15);
        contrast.Save("Laba10/output_contrast.png", ImageFormat.Png);
        Console.WriteLine("[OK] Contrast -15 — Laba10/output_contrast.png");

        // 6. Sepia (эффект сепии)
        using Bitmap sepia = ApplySepia(original);
        sepia.Save("Laba10/output_sepia.png", ImageFormat.Png);
        Console.WriteLine("[OK] Sepia — Laba10/output_sepia.png");

        Console.WriteLine("\nВсе фильтры применены. Проверьте папку Laba10/.");
    }

    public static Bitmap ApplyGrayscale(Bitmap src)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color c = src.GetPixel(x, y);
                int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                dst.SetPixel(x, y, Color.FromArgb(c.A, gray, gray, gray));
            }
        }
        return dst;
    }

    public static Bitmap ApplyInvert(Bitmap src)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color c = src.GetPixel(x, y);
                dst.SetPixel(x, y, Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B));
            }
        }
        return dst;
    }

    public static Bitmap ApplyBrightness(Bitmap src, int adjustment)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color c = src.GetPixel(x, y);
                int r = Clamp(c.R + adjustment, 0, 255);
                int g = Clamp(c.G + adjustment, 0, 255);
                int b = Clamp(c.B + adjustment, 0, 255);
                dst.SetPixel(x, y, Color.FromArgb(c.A, r, g, b));
            }
        }
        return dst;
    }

    public static Bitmap ApplyContrast(Bitmap src, int adjustment)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        double factor = (259.0 * (adjustment + 255)) / (255.0 * (259 - adjustment));
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color c = src.GetPixel(x, y);
                int r = Clamp((int)(factor * (c.R - 128) + 128), 0, 255);
                int g = Clamp((int)(factor * (c.G - 128) + 128), 0, 255);
                int b = Clamp((int)(factor * (c.B - 128) + 128), 0, 255);
                dst.SetPixel(x, y, Color.FromArgb(c.A, r, g, b));
            }
        }
        return dst;
    }

    public static Bitmap ApplyBoxBlur(Bitmap src, int kernelSize)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        int half = kernelSize / 2;

        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                int rSum = 0, gSum = 0, bSum = 0, count = 0;
                for (int ky = -half; ky <= half; ky++)
                {
                    for (int kx = -half; kx <= half; kx++)
                    {
                        int px = Clamp(x + kx, 0, src.Width - 1);
                        int py = Clamp(y + ky, 0, src.Height - 1);
                        Color c = src.GetPixel(px, py);
                        rSum += c.R; gSum += c.G; bSum += c.B;
                        count++;
                    }
                }
                dst.SetPixel(x, y, Color.FromArgb(
                    src.GetPixel(x, y).A,
                    rSum / count, gSum / count, bSum / count));
            }
        }
        return dst;
    }

    public static Bitmap ApplySepia(Bitmap src)
    {
        Bitmap gray = ApplyGrayscale(src);
        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                Color c = gray.GetPixel(x, y);
                int r = Clamp((int)(c.R * 1.0), 0, 255);
                int g = Clamp((int)(c.G * 0.8), 0, 255);
                int b = Clamp((int)(c.B * 0.6), 0, 255);
                dst.SetPixel(x, y, Color.FromArgb(c.A, r, g, b));
            }
        }
        gray.Dispose();
        return dst;
    }

    private static int Clamp(int value, int min, int max) =>
        value < min ? min : value > max ? max : value;
}

// === Задание 3: Гауссово размытие и повышение резкости ===
public class AdvancedFilters
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 3: Гауссово размытие и повышение резкости ===");
        PlatformHelper.WarnIfNotSupported();

        if (!PlatformHelper.IsDrawingSupported)
        {
            Console.WriteLine("Продвинутые фильтры:");
            Console.WriteLine("  - Gaussian Blur (sigma, kernel size)");
            Console.WriteLine("  - Sharpen (матрица 3x3)");
            Console.WriteLine("  - Edge Detection / Sobel (градиенты Gx, Gy)");
            Console.WriteLine("[OK] Фильтры описаны. Рендеринг только на Windows.");
            return;
        }

        string inputPath = "Laba10/input.png";
        if (!File.Exists(inputPath))
            ImageInfo.Run();

        using Bitmap original = new Bitmap(inputPath);

        // Gaussian Blur
        using Bitmap gaussianBlur = ApplyGaussianBlur(original, 2.0);
        gaussianBlur.Save("Laba10/output_gaussian_blur.png", ImageFormat.Png);
        Console.WriteLine("[OK] Gaussian Blur (sigma=2.0) — Laba10/output_gaussian_blur.png");

        // Sharpen
        using Bitmap sharpen = ApplySharpen(original);
        sharpen.Save("Laba10/output_sharpen.png", ImageFormat.Png);
        Console.WriteLine("[OK] Sharpen — Laba10/output_sharpen.png");

        // Edge Detection (Sobel)
        using Bitmap edges = ApplyEdgeDetection(original);
        edges.Save("Laba10/output_edges.png", ImageFormat.Png);
        Console.WriteLine("[OK] Edge Detection (Sobel) — Laba10/output_edges.png");

        Console.WriteLine("\nДополнительные фильтры применены. Проверьте папку Laba10/.");
    }

    public static Bitmap ApplyGaussianBlur(Bitmap src, double sigma)
    {
        int kernelSize = (int)Math.Ceiling(sigma * 6);
        if (kernelSize % 2 == 0) kernelSize++;
        int half = kernelSize / 2;

        double[,] kernel = new double[kernelSize, kernelSize];
        double sum = 0;
        for (int y = -half; y <= half; y++)
        {
            for (int x = -half; x <= half; x++)
            {
                double value = Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                kernel[y + half, x + half] = value;
                sum += value;
            }
        }

        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 0; y < src.Height; y++)
        {
            for (int x = 0; x < src.Width; x++)
            {
                double rSum = 0, gSum = 0, bSum = 0;
                for (int ky = -half; ky <= half; ky++)
                {
                    for (int kx = -half; kx <= half; kx++)
                    {
                        int px = Clamp(x + kx, 0, src.Width - 1);
                        int py = Clamp(y + ky, 0, src.Height - 1);
                        Color c = src.GetPixel(px, py);
                        double k = kernel[ky + half, kx + half] / sum;
                        rSum += c.R * k;
                        gSum += c.G * k;
                        bSum += c.B * k;
                    }
                }
                dst.SetPixel(x, y, Color.FromArgb(src.GetPixel(x, y).A,
                    Clamp((int)rSum, 0, 255), Clamp((int)gSum, 0, 255), Clamp((int)bSum, 0, 255)));
            }
        }
        return dst;
    }

    public static Bitmap ApplySharpen(Bitmap src)
    {
        double[,] kernel = {
            {  0, -1,  0 },
            { -1,  5, -1 },
            {  0, -1,  0 }
        };
        return ApplyConvolution(src, kernel);
    }

    public static Bitmap ApplyEdgeDetection(Bitmap src)
    {
        double[,] kernelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        double[,] kernelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        Bitmap dst = new Bitmap(src.Width, src.Height);
        for (int y = 1; y < src.Height - 1; y++)
        {
            for (int x = 1; x < src.Width - 1; x++)
            {
                double gxR = 0, gxG = 0, gxB = 0;
                double gyR = 0, gyG = 0, gyB = 0;
                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color c = src.GetPixel(x + kx, y + ky);
                        gxR += c.R * kernelX[ky + 1, kx + 1];
                        gxG += c.G * kernelX[ky + 1, kx + 1];
                        gxB += c.B * kernelX[ky + 1, kx + 1];
                        gyR += c.R * kernelY[ky + 1, kx + 1];
                        gyG += c.G * kernelY[ky + 1, kx + 1];
                        gyB += c.B * kernelY[ky + 1, kx + 1];
                    }
                }
                int r = Clamp((int)Math.Sqrt(gxR * gxR + gyR * gyR), 0, 255);
                int g = Clamp((int)Math.Sqrt(gxG * gxG + gyG * gyG), 0, 255);
                int b = Clamp((int)Math.Sqrt(gxB * gxB + gyB * gyB), 0, 255);
                dst.SetPixel(x, y, Color.FromArgb(src.GetPixel(x, y).A, r, g, b));
            }
        }
        return dst;
    }

    private static Bitmap ApplyConvolution(Bitmap src, double[,] kernel)
    {
        Bitmap dst = new Bitmap(src.Width, src.Height);
        int half = kernel.GetLength(0) / 2;
        for (int y = half; y < src.Height - half; y++)
        {
            for (int x = half; x < src.Width - half; x++)
            {
                double rSum = 0, gSum = 0, bSum = 0;
                for (int ky = -half; ky <= half; ky++)
                {
                    for (int kx = -half; kx <= half; kx++)
                    {
                        Color c = src.GetPixel(x + kx, y + ky);
                        double k = kernel[ky + half, kx + half];
                        rSum += c.R * k;
                        gSum += c.G * k;
                        bSum += c.B * k;
                    }
                }
                dst.SetPixel(x, y, Color.FromArgb(src.GetPixel(x, y).A,
                    Clamp((int)rSum, 0, 255), Clamp((int)gSum, 0, 255), Clamp((int)bSum, 0, 255)));
            }
        }
        return dst;
    }

    private static int Clamp(int value, int min, int max) =>
        value < min ? min : value > max ? max : value;
}

// === Задание 4: Сравнение фильтров — сводная таблица ===
public class FilterComparison
{
    public static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Задание 4: Обзор фильтров обработки изображений ===");

        Console.WriteLine("\nФильтры (аналог Accord.NET):");
        Console.WriteLine("┌──────────────────────────┬─────────────────────────────────┐");
        Console.WriteLine("│ Фильтр                   │ Описание                        │");
        Console.WriteLine("├──────────────────────────┼─────────────────────────────────┤");
        Console.WriteLine("│ Grayscale                │ Оттенки серого                  │");
        Console.WriteLine("│ Invert                   │ Инвертирование цветов           │");
        Console.WriteLine("│ BrightnessCorrection     │ Коррекция яркости               │");
        Console.WriteLine("│ ContrastCorrection       │ Коррекция контрастности         │");
        Console.WriteLine("│ BoxBlur / GaussianBlur   │ Размытие (простое / по Гауссу) │");
        Console.WriteLine("│ Sharpen / GaussianSharpen│ Повышение резкости              │");
        Console.WriteLine("│ Sepia                    │ Эффект старины (сепия)         │");
        Console.WriteLine("│ Edge Detection (Sobel)   │ Выделение границ                │");
        Console.WriteLine("│ Median                   │ Медианный фильтр (подавление   │");
        Console.WriteLine("│                          │     шума «соль и перец»)       │");
        Console.WriteLine("└──────────────────────────┴─────────────────────────────────┘");

        Console.WriteLine("\nПримеры использования (как в Accord.NET):");
        Console.WriteLine("  // Размытие по Гауссу:");
        Console.WriteLine("  IFilter gaussian = new GaussianBlur(sigma: 5, kernel: 7);");
        Console.WriteLine("  pictureBox2.Image = gaussian.Apply(bmp);");
        Console.WriteLine();
        Console.WriteLine("  // Изменение яркости и контрастности:");
        Console.WriteLine("  IFilter brightness = new BrightnessCorrection(50);");
        Console.WriteLine("  IFilter contrast = new ContrastCorrection(-15);");
    }
}
