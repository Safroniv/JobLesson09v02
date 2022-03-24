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
        //1. Просмотр файловой структуры+++++
        //2. Поддержка копирование файлов, каталогов+++++
        //3. Поддержка удаление файлов, каталогов+++++
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
            //создан метод отвечающий за функуионал файлового менеджера
            UserInformation();
            string userCommandString = Console.ReadLine();
            CommandFilesOfDIrView(userCommandString);
            CommandDirCopy(userCommandString);
            CopyFiles(userCommandString);
            CommandDirAddress(userCommandString);
            CommandDirPartView(userCommandString);
            Properties.Settings.Default.Save();
            Delete(userCommandString);
            DeleteFile(userCommandString);
            Quit(userCommandString);
        }
        static string TreeOfCategory(string structDirName)
        {
            //1. Просмотр файловой структуры
            //Включает:
            //Отображение дерева категорий и дерева файлов
            Console.Clear();
            try { string[] dirs = Directory.GetDirectories(structDirName); }
            catch
            {
                Exeptions();
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
        public static void UserInformation()
        {
            //метод с описанием функций файлового менеджера
            Console.WriteLine(
                "Команды строки:" + "\n" +
                "1 - Переход в категорию: dir (выбранная папка) -p (выбранная страница папок)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageCategory} категорий." + "\n" +
                "Примечание 2: если указана несущесвующая категория возвращает в C:\\." + "\n" +
                "Примечание 3: если указана несущесвующая страница возвращает первую страницу." + "\n" +
                "Пример: d C:\\Docs -p 1" + "\n" +
                "2 - Отобразить страницу с файлами в текущей категории: dirF (выбранная страница файлов)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageFiles} категорий)." + "\n" +
                $"Примечание 2: если указана несущесвующая страница возвращает - 0." + "\n" +
                "Пример: df 2" + "\n" +
                "3 - Копирование папки: copyD (выбранная папка) (скопированная папка)" + "\n" +
                "Пример: cd C:\\Directory F:\\DirectoryCopy" + "\n" +
                "4 - Копирование файла: copyF (выбранный файл) (скопированный файл)" + "\n" +
                "Пример: cf C:\\Doc.txt F:\\DocCopy.txt" + "\n" +
                "5 - Удаление каталога: delDir (выбранная папка)" + "\n" +
                "Пример: rmd F:\\DirectoryCopy" + "\n" +
                "6 - Удаление файла: delFile (выбранный файл)" + "\n" +
                "Пример: rmf F:\\DocCopy.txt" + "\n" +
                "7 - Для выхода из приложения введите - quit" + "\n" +
                "╔════════════════════════════════════════════════════════════════════════╗" + "\n" +
                "║Введите команду:                                                        ║" + "\n" +
                "╚════════════════════════════════════════════════════════════════════════╝") ;
            return;
        }
        public static string CommandDirAddress(string userCommandString)
        {
            //Запрос адреса и сохранение адреса в свойства пользователя.
            string[] subs = userCommandString.Split();
            string parseAddress = @"C:\";
            for (int i =0; i<subs.Length;i++)
            {                
                if (subs[0]=="d")
                {
                    parseAddress = subs[1];
                    try { parseAddress = subs[1]; }
                    catch
                    {
                        Exeptions();
                        Console.WriteLine($"Неверно введен адрес каталога. Введите команду еще раз");
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
        public static string CommandDirPartView(string userCommandString)
        {
            //Запрос страницы вывода категорий и сохранение страницы вывода в свойства пользователя.            
            string[] subs = userCommandString.Split();
            int dirsAddress = 0;
            string parseAddress = "";
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "d")
                {
                    try { dirsAddress = Int32.Parse(subs[3]); }
                    catch
                    {
                        Exeptions();
                        Console.WriteLine($"Страница не найдена, Введите команду еще раз, с указанием существующей страницы от 0 до 2");
                    }
                    finally
                    {
                        if (dirsAddress<=0||dirsAddress>=5){dirsAddress = 0;}
                        Properties.Settings.Default.SavedNumPageCat = dirsAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(dirsAddress);
                    }
                }
            }
            return parseAddress;
        }
        public static string CommandFilesOfDIrView(string userCommandString)
        {
            //Запрос страницы вывода файлов и сохранение страницы вывода в свойства пользователя.   
            string[] subs = userCommandString.Split();
            int filesAddress = 0;
            string parseAddress = "";
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "df")
                {
                    try { filesAddress = Int32.Parse(subs[1]); }
                    catch 
                    {
                        Exeptions();
                        Console.WriteLine("Неверная команда");
                        break;
                    }
                    finally
                    {
                        if (filesAddress <= 0 || filesAddress >= 3) { filesAddress = 0; }
                        Properties.Settings.Default.SavedNumPageFiles = filesAddress;
                        Properties.Settings.Default.Save();
                        parseAddress = Convert.ToString(filesAddress);
                    }
                }
            }            
            return parseAddress;
        }
        public static string CommandDirCopy(string userCommandString)
        {

            //Запрос копируемой директории и расположения копирования.   
            string[] subs = userCommandString.Split();

            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "cd")
                {
                    if (Directory.Exists($@"{subs[1]}"))
                    {
                        if (Directory.Exists($@"{subs[2]}")){CopyDirsAndFiles(userCommandString);}
                        else
                        {
                            Directory.CreateDirectory($@"{subs[2]}");
                            CopyDirsAndFiles(userCommandString);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Указана не существующая исходная категория для копирования");
                        return userCommandString;                           
                    }
                }
            }
            return userCommandString;
        }
        public static string CopyDirsAndFiles (string userCommandString)
        {
            string[] subs = userCommandString.Split();
            //Создаётся структура папок исходного каталога
            foreach (string dirPath in Directory.GetDirectories($@"{subs[1]}", "*", SearchOption.AllDirectories))
            {
                try {Directory.CreateDirectory(dirPath.Replace($@"{subs[1]}", $@"{subs[2]}"));}
                catch{Exeptions();}
            }
            //Копирование содержимого
            foreach (string newPath in Directory.GetFiles($@"{subs[1]}", "*.*",
                SearchOption.AllDirectories))
            {
                try{File.Copy(newPath, newPath.Replace($@"{subs[1]}", $@"{subs[2]}"), true);}
                catch{ Exeptions();}
            }
            return userCommandString;
        }
        public static string CopyFiles(string userCommandString)
        {
            string[] subs = userCommandString.Split();

            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "cf")
                {
                    try { File.Copy($@"{subs[1]}", $@"{subs[1]}".Replace($@"{subs[1]}", $@"{subs[2]}"), true); }
                    catch { Exeptions(); }
                }
            }
            return userCommandString;
        }
        public static void Delete(string userCommandString)
        {
            string[] subs = userCommandString.Split();

            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "rmd")
                {
                    try
                    {
                        Directory.Delete(subs[1], true);
                        bool directoryExists = Directory.Exists(subs[1]);
                        Console.WriteLine("top-level directory exists: " + directoryExists);
                    }
                    catch {Exeptions();}
                }
            }
        }
        public static void DeleteFile(string userCommandString)
        {
            string[] subs = userCommandString.Split();
            for (int i = 0; i < subs.Length; i++)
            {
                if (subs[0] == "rmf")
                {
                    try {File.Delete($@"{subs[1]}");}
                    catch {Exeptions();}
                }
            }
        }
        public static void Quit(string userCommandSring)
        {
            if(userCommandSring =="quit"){Environment.Exit(0);}
        }
        public static void Exeptions()
        {
            try { }
            catch (Exception ex)
            {
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
            }
        }
    }
}
