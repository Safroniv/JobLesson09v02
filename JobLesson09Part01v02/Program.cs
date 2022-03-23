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
        //1. Просмотр файловой структуры+++++
        //----2. Поддержка копирование файлов, каталогов
        //----3. Поддержка удаление файлов, каталогов
        //4. Получение информации о размерах, системных атрибутов файла, каталога+
        //5. Вывод файловой структуры должен быть постраничным+++++
        //6. В конфигурационном файле должна быть настройка вывода количества
        //элементов на страницу+++++
        //7. При выходе должно сохраняться, последнее состояние+++++
        //8. Должны быть комментарии в коде+++++
        //----9. Должна быть документация к проекту в формате md
        //10. Приложение должно обрабатывать непредвиденные ситуации(не падать)+++++
        //11. При успешном выполнение предыдущих пунктов – реализовать сохранение ошибки
        //в текстовом файле в каталоге errors/random_name_exception.txt+++++
        //12. При успешном выполнение предыдущих пунктов – реализовать движение по
        //истории команд(стрелочки вверх, вниз)+
        static void Main()
        {


            while (true)
            {
                Console.Clear(); 
                string structDirName = $@"C:\";
                //string infoManual = "";
                //string userCommandSring = "";
                //string parseAddress = "";
                if (Properties.Settings.Default.SavedAddress == structDirName)
                {
                    TreeOfCategory(structDirName);
                    StructureOfCommand();

                }
                else
                {
                    TreeOfCategory(Properties.Settings.Default.SavedAddress);
                    StructureOfCommand();
                }
            }
        }
        static void StructureOfCommand()
        {
            CommandLine();
            string userCommandSring = Console.ReadLine();
            CommandFilesOfDIrView(userCommandSring);
            CommandDirCopy(userCommandSring);
            CommandDirAddress(userCommandSring);
            CommandDirPartView(userCommandSring);
            Properties.Settings.Default.Save();
            Quit(userCommandSring);

        }
        static string TreeOfCategory(string structDirName)
        {
            //1. Просмотр файловой структуры
            //Включает:
            //Отображение дерева категорий и дерева файлов
            Console.Clear();
            try { string[] dirs = Directory.GetDirectories(structDirName); }
            catch (Exception ex)
            {
                if (Directory.Exists(@"C:\errors"))
                {
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
                structDirName = $@"C:\";
                ConsoleColor current = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Неверно введен адрес каталога. Введите команду еще раз");
                Console.BackgroundColor = current;

            }
            finally
            {
                if (Properties.Settings.Default.SavedNumPageCat<0|| Properties.Settings.Default.SavedNumPageCat>5)
                {
                    Console.WriteLine("Указан некорректный номер страницы. Принято значение по умолчанию =0.");
                    Properties.Settings.Default.SavedNumPageCat = 0;
                    Properties.Settings.Default.Save();
                }
                DirectoryInfo dirInfo = new DirectoryInfo(structDirName);
                string[] dirs = Directory.GetDirectories(structDirName);
                Console.WriteLine(
                    "╔═════════════════════════════════════════════════════════════════════════" + "\n" +
                    "║Структура категорий:" + "\n" +
                    "║" + dirInfo.FullName);

                if (Directory.Exists(structDirName))
                {
                    int indexViewPage = Properties.Settings.Default.SavedNumPageCat;
                    int numElementsPage = Properties.Settings.Default.SavedSizePageCategory;
                    int FirstElementView = indexViewPage * numElementsPage;
                    int LastElementView = FirstElementView + numElementsPage;
                    for (int i = FirstElementView; i < LastElementView; i++)
                    {
                        if (dirs.Length <= i)
                        {
                            break;
                        }
                        DirectoryInfo dirsInfo = new DirectoryInfo(dirs[i]);
                        Console.WriteLine(
                            "║├" + dirsInfo.Name + " │ " + dirsInfo.Exists + " │ " + dirsInfo.Attributes + " │ " + dirsInfo.CreationTime + " │ ");
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
                    int indexViewFiles = Properties.Settings.Default.SavedNumPageFiles;
                    int numElementsPage = Properties.Settings.Default.SavedSizePageFiles;
                    int FirstElementView = indexViewFiles * numElementsPage;
                    int LastElementView = FirstElementView + numElementsPage;
                    string[] files = Directory.GetFiles(structDirName);

                    for (int i = FirstElementView; i < LastElementView; i++)
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
                            "║├" + filesInfoNameResize + "║" + fileExtensionResize + "║" + fileSizeResize + " bytes" + "║" + fileLastWriteTimeResize + "║");
                    }
                }
                Console.WriteLine(
                    "╚════════════════════╩═══════════╩═══════════════════╩═══════════════════╝");                
            }
            return structDirName;
        }
        public static void CommandLine()
        {
            Console.WriteLine(
                "Команды строки:" + "\n" +
                "1 - Переход в категорию: dir (выбранная папка) -p (выбранная страница папок)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageCategory} категорий." + "\n" +
                "Примечание 2: если указана несущесвующая категория возвращает в C:\\." + "\n" +
                "Примечание 3: если указана несущесвующая страница возвращает первую страницу." + "\n" +
                "Пример: dir C:\\Docs -p 1" + "\n" +
                "2 - Отобразить страницу с файлами в текущей категории: dirF (выбранная страница файлов)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageFiles} категорий)." + "\n" +
                $"Примечание 2: если указана несущесвующая страница возвращает - 0." + "\n" +
                "Пример: dirF 2" + "\n" +
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
                "8 - Для выхода из приложения введите - quit" + "\n" +
                "╔════════════════════════════════════════════════════════════════════════╗" + "\n" +
                "║Введите команду:                                                        ║" + "\n" +
                "╚════════════════════════════════════════════════════════════════════════╝") ;
            return;
        }

        public static string CommandDirAddress(string userCommandSring)
        {
            //Запрос адреса и сохранение адреса в свойства пользователя.
            string[] subs = userCommandSring.Split();
            string parseAddress = @"C:\";
            for (int i =0; i<subs.Length;i++)
            {                
                if (subs[0]=="dir")
                {
                    parseAddress = subs[1];
                    try { parseAddress = subs[1]; }
                    catch (Exception ex)
                    {
                        if (Directory.Exists(@"C:\errors"))
                        {
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }
                        else
                        {
                            Directory.CreateDirectory(@"C:\errors");
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }
                        ConsoleColor current = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Неверно введен адрес каталога. Введите команду еще раз");
                        Console.BackgroundColor = current;
                        parseAddress = @"C:\";
                        CommandDirAddress(Console.ReadLine());
                        break;
                    }
                    finally
                    {
                        if (Directory.Exists(parseAddress) == true)
                        {
                            Properties.Settings.Default.SavedAddress = parseAddress;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            parseAddress = @"C:\";
                            Properties.Settings.Default.SavedAddress = parseAddress;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
            }
            return parseAddress;
        }
        public static string CommandDirPartView(string userCommandSring)
        {
            //Запрос страницы вывода категорий и сохранение страницы вывода в свойства пользователя.            
            string[] subs = userCommandSring.Split();
            int dirsAddress = 0;
            string parseAddress = "";
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "dir")
                {
                    try { dirsAddress = Int32.Parse(subs[3]); }
                    catch (Exception ex)
                    {
                        if (Directory.Exists(@"C:\errors"))
                        {
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }
                        else
                        {
                            Directory.CreateDirectory(@"C:\errors");
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }
                        ConsoleColor current = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Страница не найдена, Введите команду еще раз, с указанием существующей страницы от 0 до 2");
                        Console.BackgroundColor = current;
                    }
                    finally
                    {
                        if (dirsAddress<=0||dirsAddress>=5)
                        {
                            dirsAddress = 0;
                        }
                        Properties.Settings.Default.SavedNumPageCat = dirsAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(dirsAddress);
                    }
                }
            }
            return parseAddress;
        }
        public static string CommandFilesOfDIrView(string userCommandSring)
        {
            //Запрос страницы вывода файлов и сохранение страницы вывода в свойства пользователя.   
            string[] subs = userCommandSring.Split();
            int filesAddress = 0;
            string parseAddress = "";
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "dirF")
                {
                    try { filesAddress = Int32.Parse(subs[1]); }
                    catch (Exception ex)
                    {
                        if (Directory.Exists(@"C:\errors"))
                        {
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }
                        else
                        {
                            Directory.CreateDirectory(@"C:\errors");
                            File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                        }

                        ConsoleColor current = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Неверная команда");
                        Console.BackgroundColor = current;
                        break;
                    }
                    finally
                    {
                        if (filesAddress <= 0 || filesAddress >= 3)
                        {
                            filesAddress = 0;
                        }
                        Properties.Settings.Default.SavedNumPageFiles = filesAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(filesAddress);
                    }
                }
            }            
            return parseAddress;
        }
        public static string CommandDirCopy(string userCommandSring)
        {

            //Запрос копируемой директории и расположения копирования.   
            string[] subs = userCommandSring.Split();

            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "copyD")
                {
                    if (Directory.Exists($@"{subs[1]}"))
                    {
                        Directory.CreateDirectory($@"{subs[2]}");
                        if (Directory.Exists($@"{subs[2]}"))
                        {
                            Directory.CreateDirectory($@"{subs[2]}");
                            Console.WriteLine("директрия в которую производиться копирование уже существует, всё равно выполнить копирование y - да или n - нет.");
                            //if (Console.ReadLine()=="y")
                            //{

                            //}
                            //if(Console.ReadLine()=="n")
                            //{
                            //    Console.WriteLine("введите команду ещё раз");
                            //    return userCommandSring;
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Вы ввели неверную команду, введите команду ещё раз");
                            //    return userCommandSring;
                            //}
                        }

                    }
                    else
                    {
                        Console.WriteLine("Указана не существующая категория для копирования");
                        return Console.ReadLine();                           
                    }
                }
            }
            return userCommandSring;
        }

        public static void Quit(string userCommandSring)
        {
            if(userCommandSring =="quit")
            {
                Environment.Exit(0);                
            }
        }
                
        //public static void KeyCommant()
        //{
        //    ConsoleKeyInfo info = Console.ReadKey();
        //    switch (info.Key)
        //    {
        //        case ConsoleKey.Escape:
        //            {
        //                Environment.Exit(0);
        //            }
        //            break;
        //    }
        //}
    }
}
