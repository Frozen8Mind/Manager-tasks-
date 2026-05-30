using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    //class Taskitems 
    //{
    //    public string Title { get; set; }
    //    public bool isCompleted { get; set; }

    //    public override string ToString()
    //    {
    //        return(isCompleted ? "[X] " : "[ ] ") + Title;
    //    }
    //}
    internal class Console_To_Do_List
    {
        static List<string> LoadTasks(string filename){
            List<string> tasks = new List<string>();
            if (File.Exists(filename))
                tasks.AddRange(File.ReadAllLines(filename));
            return tasks;
        }

        static void SaveTasks(List<string> Tasks, string filename) 
        {
            Console.WriteLine($"Количество задач: {Tasks.Count}");
            File.WriteAllLines(filename, Tasks);
            Console.WriteLine("Файл сохранен" + Directory.GetCurrentDirectory());
        }

        static void Main(string[] args) //static привязывает метод под названием Main к классу Console_To_Do_List, делая его статическим методом. А void - тип возвращаемого значения, который не возвращает конкретный тип данных
        //void только выполняет действие, и ничего не возвращает
        //другие типы возращаемых данных, в отличие от void, сначала проверяются другой программой ОС, и только потом выдают код для завершения (0 - данные соответствуют, программа завершается успешно; другое число - ложь). 
        //void завершает программу через код 0 по умолчанию
        // string - текстовый тип данных. string[] - массив строк. args - имя переменной, которая присваивает себе массив строк
        {
            List<string> Tasks = new List<string>(); //здесь создается объект-список для хранения строк
                                                     //под названием Tasks, под который выделяяется память, как под список
                                                     //в скобках ничего не указаны начальные параметры, т.к. они будут добавляться в процессе
                                                     //временный список задач. данные после завершения программы буду очищаться
            string filename = "tasks.txt";
            List<string> tasks = LoadTasks(filename);

            while (true) 
            {
                Console.WriteLine("\n=== Менеджер задач ===");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать задачи");
                Console.WriteLine("3. Удалить задачу");
                Console.WriteLine("4. Выйти из менеджера");
                Console.Write("Введите номер команды: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Напишите задачу: ");
                    string task = Console.ReadLine();
                    Tasks.Add(task);
                    Console.WriteLine($"Количество существующих задач: {Tasks.Count}");
                    SaveTasks(Tasks, filename);
                    Console.WriteLine("Задача добавлена");
                }

                else if (choice == "2")
                {
                    Console.WriteLine("Список задач: ");
                    for (int i = 0; i < Tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Tasks[i]}");
                    }
                }

                else if (choice == "3")
                {
                    if (Tasks.Count == 0)
                    {
                        Console.WriteLine("Задач для удаления нет");
                    }

                    else
                    {
                        Console.WriteLine("Список задач: ");
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {Tasks[i]}");
                        }

                        Console.WriteLine("Введите номер задачи для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int num) && num >= 1 && num <= Tasks.Count) //&& - это логический оператор И
                        {
                            Tasks.RemoveAt(num - 1);
                            Console.WriteLine("Задача удалена");
                        }
                        else
                        {
                            Console.WriteLine("Неверный номер");
                        }
                    }
                }

                else if (choice == "4")
                {
                    SaveTasks(Tasks, filename);
                    Console.WriteLine("До встречи");
                    break;
                }

                else 
                {
                    Console.WriteLine("Введен неверный номер команды");
                }
            }
                
        }
    }
}
