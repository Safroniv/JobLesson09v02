using System;
using System.IO;

namespace JobLesson09Part01v02
{
    internal class Program
    {
        static void Main()
        {
            //Основной медот который определяет есть ли сохранённые данные Properties
            while (true)
            {
                TreeOfCategory(Properties.Settings.Default.SavedAddress);
                UserInformation();
                StructureOfCommand();
            }
        }
        public static string[] CommandParser(params string[] massiveCommands)
        {           
            //Парсер строки введенной пользователем
            string stringParser = Console.ReadLine();
            bool keyIndex = true;
            for (int i = 0; i < stringParser.Length; i++)
            {                
                if (i < 2)
                {massiveCommands[0] += stringParser[i];}
                if (stringParser[i] == ' ' && stringParser[i + 1] == '?')
                {keyIndex = !keyIndex;i+=3;}
                if (i > 2&& keyIndex)
                { massiveCommands[1] += stringParser[i];}
                if (i > 2 && !keyIndex)
                { massiveCommands[2] += stringParser[i];}
            }
            return massiveCommands;
        }
        static void StructureOfCommand()
        {
            //метод отвечающий за функционал файлового менеджера
            string command = "";
            string address = "";
            string addressCreate = "";
            string[] commands = CommandParser(command, address, addressCreate);
            command = commands[0];
            address = commands[1];
            addressCreate = commands[2];
            //переключатель команд
            switch (command)
            {
                case "vd": { CommandDirAddress(address, addressCreate); } break;
                case "vf": { CommandFilesOfDIrView(address); } break;
                case "cd": { CopyDirs(address, addressCreate); } break;
                case "cf": { CopyFiles(address, addressCreate); } break;               
                case "rm": { Delete(address); } break;
                case "ex": { Environment.Exit(0); } break;
                default:   { Console.WriteLine("Введена неверная команда"); } break;
            }
        }
        static string TreeOfCategory(string structDirName)
        {
            //1. Просмотр файловой структуры. Отображает дерево категорий и дерево файлов
            Console.Clear();
            try { string[] dirs = Directory.GetDirectories(structDirName); }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибочная команда! Введите еще раз.");
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
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
                if (Properties.Settings.Default.SavedNumPageCat < 0 || Properties.Settings.Default.SavedNumPageCat > 5)
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
                "1 - Переход в категорию: d (выбранная папка) -p (выбранная страница папок)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageCategory} категорий." + "\n" +
                "Примечание 2: если указана несущесвующая категория возвращает в C:\\." + "\n" +
                "Примечание 3: если указана несущесвующая страница возвращает первую страницу." + "\n" +
                "Пример: vd C:\\Docs ? 1" + "\n" +
                "2 - Отобразить страницу с файлами в текущей категории: df (выбранная страница файлов)" + "\n" +
                $"Примечание: вывод производиться по {Properties.Settings.Default.SavedSizePageFiles} категорий)." + "\n" +
                $"Примечание 2: если указана несущесвующая страница возвращает - 0." + "\n" +
                "Пример: vf 2" + "\n" +
                "3 - Копирование папки: cd (выбранная папка) (скопированная папка)" + "\n" +
                "Пример: cd C:\\Directory ? F:\\DirectoryCopy" + "\n" +
                "4 - Копирование файла: cf (выбранный файл) (скопированный файл)" + "\n" +
                "Пример: cf C:\\Doc.txt ? F:\\DocCopy.txt" + "\n" +
                "5 - Удаление каталога: rmd (выбранная папка)" + "\n" +
                "Пример: rm F:\\DirectoryCopy" + "\n" +
                "6 - Удаление файла: rmf (выбранный файл)" + "\n" +
                "Пример: rm F:\\DocCopy.txt" + "\n" +
                "7 - Для выхода из приложения введите - ex" + "\n" +
                "╔════════════════════════════════════════════════════════════════════════╗" + "\n" +
                "║Введите команду:                                                        ║" + "\n" +
                "╚════════════════════════════════════════════════════════════════════════╝");
        }

        public static void CommandDirAddress(string address, string pagecat)
        {
            //Запрос адреса, сохранение адреса в свойства пользователя с последующим выводом страницы категории с условием пейджинга
            string parseAddress = @"C:\";
            int dirsAddress = 0;

            try { parseAddress = address; }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибочная команда! Введите еще раз.");
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
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
            //Запрос страницы вывода категорий (пейджинг) и сохранение страницы вывода в свойства пользователя.  
            try { dirsAddress = Int32.Parse(pagecat); }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибочная команда! Введите еще раз.");
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
            }
            finally
            {

                if (dirsAddress <= 0 || dirsAddress > (Directory.GetFiles(Properties.Settings.Default.SavedAddress).Length / Properties.Settings.Default.SavedSizePageFiles + 1)) { dirsAddress = 0; }
                Properties.Settings.Default.SavedNumPageCat = dirsAddress;
                Properties.Settings.Default.Save();
            }
        }
        public static void CommandFilesOfDIrView(string address)
        {
            //Запрос страницы вывода файлов (пейджинг) и сохранение страницы вывода в свойства пользователя.   
            int filesAddress = 0;

            try { filesAddress = Int32.Parse(address); }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибочная команда! Введите еще раз.");
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
            }
            finally
            {
                if (filesAddress <= 0 || filesAddress > (Directory.GetFiles(Properties.Settings.Default.SavedAddress).Length / Properties.Settings.Default.SavedSizePageFiles + 1)) { filesAddress = 0; }
                Properties.Settings.Default.SavedNumPageFiles = filesAddress;
                Properties.Settings.Default.Save();
            }
        }
        public static void CopyDirs(string address, string addressCreate)
        {
            //Запрос копируемой директории и расположения копирования.                
            if (Directory.Exists($@"{address}"))
            {
                if (Directory.Exists($@"{addressCreate}")) { CopyDirsAndFiles(address, addressCreate); }
                else
                {
                    Directory.CreateDirectory($@"{addressCreate}");
                    CopyDirsAndFiles(address, addressCreate);
                }
            }
            else { Console.WriteLine("Указана не существующая исходная категория для копирования"); }
        }
        public static void CopyDirsAndFiles(string address, string addressCreate)
        {
            //копирование файлов и директорий
            //Создаётся структура папок исходного каталога
            foreach (string dirPath in Directory.GetDirectories($@"{address}", "*", SearchOption.AllDirectories))
            {
                try { Directory.CreateDirectory(dirPath.Replace($@"{address}", $@"{addressCreate}")); }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибочная команда! Введите еще раз.");
                    if (Directory.Exists(@"C:\errors"))
                    { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                    else
                    {
                        Directory.CreateDirectory(@"C:\errors");
                        File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                    }
                }
            }
            //Копирование содержимого
            foreach (string newPath in Directory.GetFiles($@"{address}", "*.*", SearchOption.AllDirectories))
            {
                try { File.Copy(newPath, newPath.Replace($@"{address}", $@"{addressCreate}"), true); }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибочная команда! Введите еще раз.");
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
        public static void CopyFiles(string address, string addressCreate)
        {
            //копирование отдельных файлов
            try { File.Copy($@"{address}", $@"{address}".Replace($@"{address}", $@"{addressCreate}"), true); }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибочная команда! Введите еще раз.");
                if (Directory.Exists(@"C:\errors"))
                { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                else
                {
                    Directory.CreateDirectory(@"C:\errors");
                    File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                }
            }
        }
        public static void Delete(string address)
        {
            //удаление файлов и категорий            
            if (Directory.Exists(address))
            {
                try
                {
                    Directory.Delete(address, true);
                    bool directoryExists = Directory.Exists(address);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибочная команда! Введите еще раз.");
                    if (Directory.Exists(@"C:\errors"))
                    { File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n"); }
                    else
                    {
                        Directory.CreateDirectory(@"C:\errors");
                        File.AppendAllText(@"C:\errors\random_name_exception.txt", $"{DateTime.Now} {ex.Message}\n");
                    }
                }
            }
            //удаление файлов
            if (File.Exists(address))
            {
                try { File.Delete($@"{address}"); }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибочная команда! Введите еще раз.");
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
}
