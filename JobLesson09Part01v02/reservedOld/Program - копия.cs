﻿using System;
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
        //1. Просмотр файловой структуры+
        //2. Поддержка копирование файлов, каталогов
        //3. Поддержка удаление файлов, каталогов
        //4. Получение информации о размерах, системных атрибутов файла, каталога+
        //5. Вывод файловой структуры должен быть постраничным+
        //6. В конфигурационном файле должна быть настройка вывода количества
        //элементов на страницу+
        //7. При выходе должно сохраняться, последнее состояние+
        //8. Должны быть комментарии в коде+
        //9. Должна быть документация к проекту в формате md
        //10. Приложение должно обрабатывать непредвиденные ситуации(не падать)
        //11. При успешном выполнение предыдущих пунктов – реализовать сохранение ошибки
        //в текстовом файле в каталоге errors/random_name_exception.txt
        //12. При успешном выполнение предыдущих пунктов – реализовать движение по
        //истории команд(стрелочки вверх, вниз)
        static void Main()
        {
            while (true)
            {
                Console.Clear(); 
                string structDirName = $@"F:\";
                string infoManual = "";
                //string categoryAddress = structDirName;
                string userCommandSring = "";
                //string parseAddress = "";
                if (Properties.Settings.Default.SavedAddress == structDirName)
                {
                    TreeOfCategory(structDirName);
                    CommandLine(infoManual);
                    userCommandSring = Console.ReadLine();
                    CommandDirAddress(userCommandSring);
                    CommandDirPart(userCommandSring);
                    Properties.Settings.Default.Save();
                    
                }
                else
                {
                    TreeOfCategory(Properties.Settings.Default.SavedAddress);
                    CommandLine(infoManual);
                    userCommandSring = Console.ReadLine();
                    CommandDirAddress(userCommandSring);
                    CommandDirPart(userCommandSring);
                    Properties.Settings.Default.Save();
                }
            }
        }
        static string TreeOfCategory(string structDirName)
        {
            //1. Просмотр файловой структуры
            //Включает:
            //Отображение дерева категорий и дерева файлов
            Console.Clear();                 
            DirectoryInfo dirInfo = new DirectoryInfo(structDirName);
            string[] dirs = Directory.GetDirectories(structDirName);
            Console.WriteLine(
                "╔═════════════════════════════════════════════════════════════════════════" + "\n" +
                "║Структура категорий:" + "\n" +
                "║" + dirInfo.FullName);

            if (Directory.Exists(structDirName))
            {

                for (int i = Properties.Settings.Default.SavedNumPageCat; i < Properties.Settings.Default.SavedSizePageCategory; i++)
                {
                    if (dirs.Length <= i)
                    {
                        break;
                    }
                    DirectoryInfo dirsInfo = new DirectoryInfo(dirs[i]);
                    Console.WriteLine(
                        "├" + dirsInfo.Name + " │ " + dirsInfo.Exists + " │ " + dirsInfo.Attributes + " │ " + dirsInfo.CreationTime + " │ ");
                }
            }
            Console.WriteLine(
                "╚══════════════════════════════════════════════════════════════════════════" + "\n" +
                "Файлы категории:" + dirInfo.FullName + "\n" +
                "╔════════════════════╦═══════════╦═══════════════════╦═══════════════════╗" + "\n" +
                "║ Имя файла:         ║Расширение:║Размер файла:      ║Дата изменения:    ║" + "\n" +
                "╠════════════════════╬═══════════╬═══════════════════╬═══════════════════╣");
            if (Directory.Exists(structDirName))
            {
                string[] files = Directory.GetFiles(structDirName);
                for (int i = 0; i < Properties.Settings.Default.SavedSizePageFiles; i++)
                {
                    if (files.Length <= i)
                    {
                        break;
                    }
                    FileInfo filesInfo = new FileInfo(files[i]);
                    string filesInfoNameResize = filesInfo.Name.Remove(filesInfo.Name.LastIndexOf('.')).PadRight(40, ' ').Substring(0, 19);
                    string fileExtensionResize = filesInfo.Extension.PadRight(20, ' ').Substring(0, 11);
                    string fileSizeResize = Convert.ToString(filesInfo.Length).PadRight(20, ' ').Substring(0, 13);
                    string fileLastWriteTimeResize = Convert.ToString(filesInfo.LastWriteTime).PadRight(20, ' ').Substring(0, 19);
                    Console.WriteLine(
                        "├" + filesInfoNameResize + "│" + fileExtensionResize + "│" + fileSizeResize + " bytes" + "│" + fileLastWriteTimeResize + "│");
                }
            }
            Console.WriteLine(
                "╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");                    
            return structDirName;
        }
        public static string CommandLine(string infoManual)
        {

            Console.WriteLine(
                "Команды строки:" + "\n" +
                "1 - Переход в категорию: dir (выбранная папка) -p (выбранная страница папок)" + "\n" +
                "Примечание: вывод производиться по 5 категорий)." + "\n" +
                "Пример: dir C:\\Docs -p 1" + "\n" +
                "2 - Отобразить сраницу с файлами: dir (выбранная папка) -p (выбранная страница файлов)" + "\n" +
                "Примечание: вывод производиться по 5 категорий)." + "\n" +
                "Пример: dirFiles C:\\Docs -p 2" + "\n" +
                "3 - Копирование папки: copyD (выбранная папка) (скопированная папка)" + "\n" +
                "Пример: copyD C:\\Directory F:\\DirectoryCopy" + "\n" +
                "4 - Копирование файла: copyF (выбранный файл) (скопированный файл)" + "\n" +
                "Пример: copyF C:\\Doc.txt F:\\DocCopy.txt" + "\n" +
                "5 - Удаление каталога: delDir (выбранная папка)" + "\n" +
                "Пример: delDir F:\\DirectoryCopy" + "\n" +
                "6 - Удаление файла: delFile (выбранный файл)" + "\n" +
                "Пример: delFile F:\\DocCopy.txt" + "\n" +
                "7 - Информация о файле: info (выбранный файл)" + "\n" +
                "Пример: info F:\\DocCopy.txt" + "\n" +
                "╔════════════════════════════════════════════════════════════════════════╗" + "\n" +
                "║Введите команду:                                                        ║" +"\n" +
                "╚════════════════════════════════════════════════════════════════════════╝");
            return infoManual;
        }

        public static string CommandDirAddress(string userCommandSring)
        {
            //Запрос адреса и сохранение адреса в свойства пользователя.
            string[] subs = userCommandSring.Split();
            for (int i =0; i<subs.Length;i++)
            {                
                if (subs[0]=="dir")
                {
                    userCommandSring = subs[1];
                    Properties.Settings.Default.SavedAddress = userCommandSring;
                    Properties.Settings.Default.Save();
                }
            }
            return userCommandSring;
        }
        public static string CommandDirPart(string userCommandSring)
        {
            //Запрос страницы вывода и сохранение страницы вывода в свойства пользователя.            
            string[] subs = userCommandSring.Split();
            int dirsAddress = 0;
            string parseAddress = "";
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "dir")
                {

                    dirsAddress = Int32.Parse(subs[3]);
                    if (dirsAddress == 0 || dirsAddress == 1 || dirsAddress == 2)
                    {
                        Properties.Settings.Default.SavedNumPageCat = dirsAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(dirsAddress);
                    }
                    else
                    {
                        Console.WriteLine("Введите страницу отборажения 0 или 1 или 2");
                        dirsAddress = Int32.Parse(Console.ReadLine());
                        Properties.Settings.Default.SavedNumPageCat = dirsAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(dirsAddress);
                    }

                }
            }
            return parseAddress;
        }
    }
}
