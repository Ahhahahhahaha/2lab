using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выполняется лабораторная работа 2:\n");
        Console.WriteLine("Введите текст, который нужно сохранить:");
        string textInFile = Console.ReadLine();

        string putkPapke;
        string fileName;
        bool putProverka = false;
        bool fileNameProverka = false;

        do
        {
            Console.WriteLine("Введите путь к папке для сохранения файла:");
            putkPapke = Console.ReadLine();

            if (!ProverkaPut(putkPapke))
            {
                Console.WriteLine("Некорректный путь к папке. Попробуйте снова.");
            }
            else
            {
                putProverka = true;
            }
        } while (!putProverka);

        if (!Directory.Exists(putkPapke))
        {
            try
            {

                Directory.CreateDirectory(putkPapke);
                Console.WriteLine("Папка успешно создана!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
                return;
            }
        }

        do
        {
            Console.WriteLine("Введите название файла (без расширения):");
            fileName = Console.ReadLine();

            if (!FileNameProverka(fileName))
            {
                Console.WriteLine("Некорректное название файла. Попробуйте снова.");
            }
            else
            {
                fileNameProverka = true;
            }
        } while (!fileNameProverka);

        Console.WriteLine("Выберите кодировку (1 - UTF-8, 2 - Win1251, 3 - DOC 866):");
        int encodingChoice = int.Parse(Console.ReadLine());
        Encoding encoding;
        switch (encodingChoice)
        {
            case 1:
                encoding = Encoding.UTF8;
                break;
            case 2:
                encoding = Encoding.GetEncoding("windows-1251");
                break;
            case 3:
                encoding = Encoding.GetEncoding("ibm866");
                break;
            default:
                Console.WriteLine("Неверный выбор кодировки. Используется кодировка UTF-8.");
                encoding = Encoding.UTF8;
                break;
        }

        string fileNameAndRashirenie = Path.ChangeExtension(fileName, "txt");
        string fullPutFile = Path.Combine(putkPapke, fileNameAndRashirenie);
        File.WriteAllText(fullPutFile, textInFile, encoding);
        Console.WriteLine("Файл успешно сохранен!");
        Console.WriteLine("Для закрытия программы нажмите любую кнопку.");
        Console.ReadLine();
    }
    static bool ProverkaPut(string put)
    {
        try
        {
            _ = new DirectoryInfo(put);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    static bool FileNameProverka(string fileName)
    {
        char[] nevernieSimvoly = Path.GetInvalidFileNameChars();
        return !fileName.Any(nevernieSimvoly.Contains);
    }

}

