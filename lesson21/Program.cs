using System.Diagnostics;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("-- Працюємо із двійковими файлами :) --");
Console.WriteLine("Вкажіть назву папки:");
string folderName = Console.ReadLine();

Directory.CreateDirectory(folderName);

int successCount = 0;

HttpClient client = new HttpClient();
do
{
    try
    {
        Console.WriteLine("Вкажіть адресу url:");
        string fileUrl = Console.ReadLine();

        if (string.IsNullOrEmpty(fileUrl))
            break;

        // Початок вимірювання часу
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Завантаження файлу
        byte[] buffer = await client.GetByteArrayAsync(fileUrl);

        // Створення унікальної назви файлу
        string fileName = Guid.NewGuid().ToString() + ".webp";
        string path = Path.Combine(folderName, fileName);

        // Запис файлу у папку
        File.WriteAllBytes(path, buffer);

        // Завершення вимірювання часу
        stopwatch.Stop();

        successCount++;

        Console.WriteLine("Файл успішно завантажено!");
        Console.WriteLine("Назва файлу: " + fileName);
        Console.WriteLine("Час завантаження: " +
            stopwatch.Elapsed.TotalSeconds.ToString("F2") + " секунд");
        Console.WriteLine("--------------------------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка читання файлу: " + ex.Message);
    }

} while (true);

// Виведення загальної кількості завантажень
Console.WriteLine("\n-- ЗВІТ ПРО ЗАВАНТАЖЕННЯ --");

Console.WriteLine("Успішно завантажено файлів: " + successCount);

// Виведення вмісту папки
Console.WriteLine("\n-- ВМІСТ ПАПКИ --");

DirectoryInfo directory = new DirectoryInfo(folderName);

FileInfo[] files = directory.GetFiles();

foreach (FileInfo file in files)
{
    double sizeKB = file.Length / 1024.0;

    Console.WriteLine("Назва: " + file.Name);
    Console.WriteLine("Розмір: " + sizeKB.ToString("F2") + " КБ");
    Console.WriteLine("--------------------------------");
}

Console.WriteLine("Усього файлів у папці: " + files.Length);

client.Dispose();