using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLesson09Part01v02
{
    internal class TreeOfCats
    {
        //public TreeOfCats(int currenPositon)
        //{
        //    //1. Просмотр файловой структуры
        //    //Включает:
        //    //Отображение дерева категорий и дерева файлов
        //    Console.Clear();
        //    string structDirName = @"F:\CategoryForTree";
        //    DirectoryInfo dirInfo = new DirectoryInfo(structDirName);
        //    string[] dirs = Directory.GetDirectories(structDirName);
        //    Console.WriteLine("═════════════════════════════════════════════");
        //    Console.WriteLine("Структура категорий:" + "\n" + dirInfo.FullName);
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "════════════════════════════════════════════════════" + Environment.NewLine);
        //    File.AppendAllText("Structure.txt", "Структура категорий:" + Environment.NewLine + structDirName);
        //    if (Directory.Exists(structDirName))
        //    {

        //        for (int i = 0; i < dirs.Length; i++)
        //        {
        //            if (currenPositon == i)
        //            {
        //                ConsoleColor current = Console.BackgroundColor;
        //                Console.BackgroundColor = ConsoleColor.Blue;
        //                InfoFile(dirs[i]);
        //                Console.BackgroundColor = current;
        //                continue;
        //            }
        //            DirectoryInfo dirsInfo = new DirectoryInfo(dirs[i]);
        //            Console.WriteLine("├" + dirsInfo.Name + " ║ " + dirsInfo.Exists + " ║ " + dirsInfo.Attributes);
        //            File.AppendAllText("Structure.txt", Environment.NewLine + "├" + dirsInfo.Name + " ║ " + dirsInfo.Exists + " ║ " + dirsInfo.Attributes);
        //        }
        //    }
        //    Console.WriteLine("═════════════════════════════════════════════");
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "════════════════════════════════════════════════════");
        //    Console.WriteLine("Файлы категории:" + dirInfo.FullName);
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "Файлы категории:" + dirInfo.FullName);
        //    Console.WriteLine("╔════════════════════╦═══════════╦═══════════════════╦═══════════════════╗");
        //    Console.WriteLine("║ Имя файла:         ║Расширение:║Размер файла:      ║Дата изменения:    ║");
        //    Console.WriteLine("╠════════════════════╬═══════════╬═══════════════════╬═══════════════════╣");
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "╔════════════════════╦═══════════╦═══════════════════╦═══════════════════╗");
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "║ Имя файла:         ║Расширение:║Размер файла:      ║Дата изменения:    ║");
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "╠════════════════════╬═══════════╬═══════════════════╬═══════════════════╣");
        //    if (Directory.Exists(structDirName))
        //    {
        //        string[] files = Directory.GetFiles(structDirName);
        //        for (int i = 0; i < files.Length; i++)
        //        {
        //            FileInfo filesInfo = new FileInfo(files[i]);
        //            string filesInfoName = filesInfo.Name;
        //            string filesInfoNameResize = filesInfoName.Remove(filesInfoName.LastIndexOf('.')).PadRight(40, ' ').Substring(0, 19);
        //            string fileExtension = filesInfo.Extension;
        //            string fileExtensionResize = fileExtension.PadRight(20, ' ').Substring(0, 11);
        //            string fileSize = Convert.ToString(filesInfo.Length);
        //            string fileSizeResize = fileSize.PadRight(20, ' ').Substring(0, 13);
        //            Console.WriteLine("║├" + filesInfoNameResize + "║" + fileExtensionResize + "║" + fileSizeResize + " bytes" + "║" + filesInfo.LastWriteTime + "║");
        //            File.AppendAllText("Structure.txt", Environment.NewLine + "║├" + filesInfoNameResize + "║" + fileExtensionResize + "║" + fileSizeResize + " bytes" + "║" + filesInfo.LastWriteTime + "║");
        //        }

        //    }
        //    Console.WriteLine("╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");
        //    File.AppendAllText("Structure.txt", Environment.NewLine + "╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");
        //    File.AppendAllText("Structure.txt", Environment.NewLine);
        //    return structDirName;
        //}
        //public static void InfoFile(string info)
        //{
        //    DirectoryInfo infoToDir = new DirectoryInfo(info);
        //    Console.WriteLine($"{infoToDir.Name} {infoToDir.Exists} {infoToDir.Attributes}");
        //}
    }
}
