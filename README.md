JobLesson09v02
# Программа Файловый менеджер от Safroniv
Программа файловый менеджер написана в качестве контрольного задания при прохождении курса GeekBrains Разработчик C# 

## Оглавление
0. [Задание](#Задание)
1. [Требования](#Требования)
2. [Описание функций программы](#Описание-функций-программы)
3. [Общие характеристики программы](#Общие-характеристики-программы)

## Задание
Требуется создать консольный файловый менеджер начального уровня, который
покрывает минимальный набор функционала по работе с файлами.
Функции и требования
1. Просмотр файловой структуры
2. Поддержка копирование файлов, каталогов
3. Поддержка удаление файлов, каталогов
4. Получение информации о размерах, системных атрибутов файла, каталога
5. Вывод файловой структуры должен быть постраничным
6. В конфигурационном файле должна быть настройка вывода количества
элементов на страницу
7. При выходе должно сохраняться, последнее состояние
8. Должны быть комментарии в коде
9. Должна быть документация к проекту в формате md
10. Приложение должно обрабатывать непредвиденные ситуации (не падать)
11. При успешном выполнение предыдущих пунктов – реализовать сохранение ошибки
в текстовом файле в каталоге errors/random_name_exception.txt
12. При успешном выполнение предыдущих пунктов – реализовать движение по
истории команд (стрелочки вверх, вниз)

## Требования

Функции менеджера
+ Вывод дерева файловой системы с условием “пейджинга” - только два уровня!
+ Копирование каталога
+ Копирование файла
+ Удаление каталога рекурсивно
+ Удаление файла
+ Вывод информации о каталоге
+ Вывод информации о файле


## Описание функций программы

Программа файловый менеджер написана на языке C#.
Основные команды:
+ 1 - Переход в категорию: vd (выбранная папка) ? (выбранная страница папок)
    + Примечание: вывод производиться по n категорий сохранённых в - Properties.Settings.Default.SavedSizePageCategory.
    + Примечание 2: если указана несущесвующая категория возвращает в C:\\.
    + Примечание 3: если указана несущесвующая страница возвращает первую страницу.
    + Пример: vd C:\\Docs ? 1
+ 2 - Отобразить страницу с файлами в текущей категории: vf (выбранная страница файлов)
    + Примечание 1: вывод производиться по n категорий сохранённых в - Properties.Settings.Default.SavedSizePageFiles.
    + Примечание 2: если указана несущесвующая страница возвращает - 0.
    + Пример: vf 2
+ 3 - Копирование папки: cd (выбранная папка) (скопированная папка)
    + Пример: cd C:\\Directory ? F:\\DirectoryCopy
+ 4 - Копирование файла: cf (выбранный файл) (скопированный файл)
    + Пример: cf C:\\Doc.txt ? F:\\DocCopy.txt"
+ 5 - Удаление каталога: rm (выбранная папка)
    + Пример: rm F:\\DirectoryCopy
+ 6 - Удаление файла: rm (выбранный файл)
    + Пример: rm F:\\DocCopy.txt
+ 7 - Для выхода из приложения введите - ex



## Общие характеристики программы
Программа запускается в консоли и имеет вид:

![Вид консольного приложения](https://github.com/Safroniv/JobLesson09v02/blob/JobLesson09Part01v02/JobLesson09Part01v02/scr/proInWork.png )

Программа сохраняет последние действия и при следующем запуске запускается последнее сохранённое состояние.
Все параметры сохраняются в Propertice:

![меню Propertice](https://github.com/Safroniv/JobLesson09v02/blob/JobLesson09Part01v02/JobLesson09Part01v02/scr/Porperties.png )

Все команды отображаются после меню вывода на страницу менеджера и инструкции.

![командная строка](https://github.com/Safroniv/JobLesson09v02/blob/JobLesson09Part01v02/JobLesson09Part01v02/scr/CommandLineinFileManeger.png)
