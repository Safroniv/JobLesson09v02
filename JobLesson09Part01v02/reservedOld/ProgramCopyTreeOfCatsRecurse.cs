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
            //string structDirName = @"F:\CategoryForTree";
            //int currentIndex = 0;
            Console.WriteLine("Структура категорий и файлов в: F:\\CategoryForTree\n");
            File.AppendAllText("Structure.txt", Environment.NewLine + "Структура категорий и файлов в: F:\\CategoryForTree" + Environment.NewLine);
            string path = @"F:\CategoryForTree";
            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)
            {
                Console.WriteLine("|" + dirs[i]);
                File.AppendAllText("Structure.txt", Environment.NewLine + "|" + dirs[i]);
            }
            TreeOfCategory(path);
            Console.ReadLine();
            //while (true)
            //{
            //    ConsoleKeyInfo info = Console.ReadKey();
            //    switch (info.Key)
            //    {
            //        case ConsoleKey.UpArrow:
            //            {
            //                if (currentIndex >= 0)
            //                {
            //                    currentIndex--;
            //                }
            //                TreeOfCategory(currentIndex);
            //            }
            //            break;
            //        case ConsoleKey.DownArrow:
            //            {
            //                currentIndex++;
            //                TreeOfCategory(currentIndex);
            //            }
            //            break;
            //        case ConsoleKey.Enter:
            //            {
            //                string files = Directory.GetFileSystemEntries(structDirName)[currentIndex];
            //                Process.Start(new ProcessStartInfo() { FileName = files, UseShellExecute = true });
            //            }
            //            break;
            //        case ConsoleKey.Escape:
            //            {
            //                Environment.Exit(0);
            //            }
            //            break;
            //    }
            //}            
        }
        static string TreeOfCategory(string path)
        {
            string structDirName = path;
            string[] dirs = Directory.GetDirectories(structDirName);

            for (int i = 0; i < dirs.Length; i++)
            {
                if (Directory.Exists(dirs[i]))
                {
                    string[] subDirs = Directory.GetDirectories(TreeOfCategory(dirs[i]));
                    for (int j = 0; j < subDirs.Length; j++)
                    {
                        
                        Console.WriteLine("||" + subDirs[j]);
                        File.AppendAllText("Structure.txt", Environment.NewLine + "||" + subDirs[j]);
                    }
                }
            }
            return structDirName;
        }
        //public static void InfoFile(string info)
        //{
        //    DirectoryInfo infoToDir = new DirectoryInfo(info);
        //    Console.WriteLine($"{infoToDir.Name} {infoToDir.Exists} {infoToDir.Attributes}");
        //}
    }
}
