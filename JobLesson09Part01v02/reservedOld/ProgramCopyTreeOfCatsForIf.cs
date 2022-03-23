using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLesson09Part01v02
{
    internal class Program
    {
        //Требуется создать консольный файловый менеджер начального уровня,
        //который покрывает минимальный набор функционала по работе с файлами.
        //1. Просмотр файловой структуры
        //2. Поддержка копирование файлов, каталогов
        //3. Поддержка удаление файлов, каталогов
        //4. Получение информации о размерах, системных атрибутов файла, каталога
        //5. Вывод файловой структуры должен быть постраничным
        //6. В конфигурационном файле должна быть настройка вывода количества
        //элементов на страницу
        //7. При выходе должно сохраняться, последнее состояние
        //8. Должны быть комментарии в коде
        //9. Должна быть документация к проекту в формате md
        //10. Приложение должно обрабатывать непредвиденные ситуации(не падать)
        //11. При успешном выполнение предыдущих пунктов – реализовать сохранение ошибки
        //в текстовом файле в каталоге errors/random_name_exception.txt
        //12. При успешном выполнение предыдущих пунктов – реализовать движение по
        //истории команд(стрелочки вверх, вниз)
        static void Main()
        {
            string structDirName = @"F:\CategoryForTree";           
            int currentIndex = 0;
            TreeOfCategory(0);
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (currentIndex >= 0)
                            {
                                currentIndex--;
                            }
                            TreeOfCategory(currentIndex);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            currentIndex++;
                            TreeOfCategory(currentIndex);
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            string files = Directory.GetFileSystemEntries(structDirName)[currentIndex];
                            Process.Start(new ProcessStartInfo() { FileName = files, UseShellExecute = true });
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }            
        }
        static string TreeOfCategory(int currenPositon)
        {
            //1. Просмотр файловой структуры
            //Включает:
            //Отображение дерева категорий и дерева файлов
            Console.Clear(); 
            string structDirName = @"F:\CategoryForTree";
            DirectoryInfo dirInfo = new DirectoryInfo(structDirName);
            string[] dirs = Directory.GetDirectories(structDirName);
            Console.WriteLine("═════════════════════════════════════════════");
            Console.WriteLine("Структура категорий:" + "\n"+ dirInfo.FullName);
            File.AppendAllText("Structure.txt", Environment.NewLine + "════════════════════════════════════════════════════"+ Environment.NewLine);
            File.AppendAllText("Structure.txt", "Структура категорий:" + Environment.NewLine + structDirName);
            if (Directory.Exists(structDirName))
            {
                
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (currenPositon == i)
                    {
                        ConsoleColor current = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        InfoFile(dirs[i]);
                        Console.BackgroundColor = current;
                        continue;
                    }
                    DirectoryInfo dirsInfo = new DirectoryInfo(dirs[i]);
                    Console.WriteLine("├" + dirsInfo.Name + " ║ " + dirsInfo.Exists + " ║ " + dirsInfo.Attributes);
                    File.AppendAllText("Structure.txt", Environment.NewLine + "├" + dirsInfo.Name + " ║ " + dirsInfo.Exists + " ║ " + dirsInfo.Attributes);
                }
            }
            Console.WriteLine("═════════════════════════════════════════════");
            File.AppendAllText("Structure.txt", Environment.NewLine + "════════════════════════════════════════════════════");
            Console.WriteLine("Файлы категории:" + dirInfo.FullName);
            File.AppendAllText("Structure.txt", Environment.NewLine + "Файлы категории:" + dirInfo.FullName);
            Console.WriteLine("╔════════════════════╦═══════════╦═══════════════════╦═══════════════════╗");
            Console.WriteLine("║ Имя файла:         ║Расширение:║Размер файла:      ║Дата изменения:    ║");
            Console.WriteLine("╠════════════════════╬═══════════╬═══════════════════╬═══════════════════╣");
            File.AppendAllText("Structure.txt", Environment.NewLine + "╔════════════════════╦═══════════╦═══════════════════╦═══════════════════╗");
            File.AppendAllText("Structure.txt", Environment.NewLine + "║ Имя файла:         ║Расширение:║Размер файла:      ║Дата изменения:    ║");
            File.AppendAllText("Structure.txt", Environment.NewLine + "╠════════════════════╬═══════════╬═══════════════════╬═══════════════════╣");
            if (Directory.Exists(structDirName))
            {
                string[] files = Directory.GetFiles(structDirName);
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo filesInfo = new FileInfo(files[i]);
                    string filesInfoName = filesInfo.Name;
                    string filesInfoNameResize = filesInfoName.Remove(filesInfoName.LastIndexOf('.')).PadRight(40,' ').Substring(0, 19);
                    string fileExtension = filesInfo.Extension;
                    string fileExtensionResize = fileExtension.PadRight(20, ' ').Substring(0, 11);
                    string fileSize = Convert.ToString(filesInfo.Length);
                    string fileSizeResize = fileSize.PadRight(20, ' ').Substring(0, 13);
                    Console.WriteLine("║├" + filesInfoNameResize + "║" + fileExtensionResize + "║" + fileSizeResize + " bytes"+  "║" + filesInfo.LastWriteTime + "║");
                    File.AppendAllText("Structure.txt", Environment.NewLine + "║├" + filesInfoNameResize + "║" + fileExtensionResize + "║" + fileSizeResize + " bytes" + "║" + filesInfo.LastWriteTime + "║");
                }
                
            }
            Console.WriteLine("╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");
            File.AppendAllText("Structure.txt", Environment.NewLine + "╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");
            File.AppendAllText("Structure.txt", Environment.NewLine);
            return structDirName;
        }
        public static void InfoFile(string info)
        {
            DirectoryInfo infoToDir = new DirectoryInfo(info);
            Console.WriteLine($"{infoToDir.Name} {infoToDir.Exists} {infoToDir.Attributes}");
        }
    }
}
