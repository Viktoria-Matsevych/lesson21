using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("--Працюємо із двійковими файлами--");
/*
// Хочу скачати фото у папку з інтернета
string urlImage = "https://miro.medium.com/v2/resize:fit:720/format:webp/0*MVwoyzdP8rLWVnh9";

try
{
    // Для закачування файлів з мережі використовуємо клас HttpClient
    HttpClient client = new HttpClient();
    // Читаю байти із файлу, який знаходиться в мережі
    byte[] imageBytes = client.GetByteArrayAsync(urlImage).Result;
    //Записуємо масив байт у файл img.webp
    File.WriteAllBytes("img.webp", imageBytes);

}
catch (Exception ex)
{
    Console.WriteLine("Сталася помилка при завантаженні файлу: " + ex.Message);
}
*/

//Користувач вводить назву папки 
//Потім користувач послідовно вводить адреси файлів у інтернеті 
//Ввід завершується коли ввід постий рядок 

//Усі ввелені файл зберігаються у папці.
//Після цього програма виводить вміст папки 

Console.WriteLine("Вкажіть назву папки:");
string folderName = Console.ReadLine();
Directory.CreateDirectory(folderName);
do
{
    try
    {
        Console.WriteLine("Вкажіть адресу url:");
        string fileUrl = Console.ReadLine();
        if (string.IsNullOrEmpty(fileUrl))
            break;
        HttpClient client = new HttpClient();
        byte[] buffer = client.GetByteArrayAsync(fileUrl).Result;
        //Як зробити унікальну назву для файлу
        string fileName = Guid.NewGuid().ToString()+".webp";
        string path = Path.Combine(folderName, fileName);
        File.WriteAllBytes(path, buffer);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка читання файлу: " + ex.Message);
    }
} while (true);