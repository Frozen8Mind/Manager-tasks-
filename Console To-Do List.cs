using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    //class Taskitems
    //{
    //    public string Title { get; set; }
    //    public bool isCompleted { get; set; }

    //    public Taskitems (string title, bool Iscompleted = false) {
    //        Title = title;
    //        isCompleted = Iscompleted;
    //    } //конструктор должен иметь в точности такое же название, как и класс, т.е. - TaskItems

    //    public override string ToString()
    //    {
    //        return $"{(isCompleted ? "[X] " : "[ ] ")}{Title}";
    //    }
    //}
    internal class Console_To_Do_List
    {
        static List<string> LoadTasks(string filename){

            List<string> tasks = new List<string>(); //объект
            if (File.Exists(filename))
            {
                string[] lInes = File.ReadAllLines(filename);//переменная для чтения содержимого файла

                foreach (string line in lInes) 
                {
                    if (!line.StartsWith("[ ] ") && !line.StartsWith("[X] ")) //проверка задач: не начинается ли строка с префикса
                        tasks.Add("[ ] " + line);//добавление префикса; обновление строчки
                    else
                        tasks.Add(line);//если строка имеет префикс, она не обрабатывается и добавляется автоматически 
                }
            }
            return tasks;
        }

        static void SaveTasks(List<string> Tasks, string filename) 
        {
            Console.WriteLine($"Количество задач: {Tasks.Count}");
            File.WriteAllLines(filename, Tasks);
            Console.WriteLine("Файл сохранен" + Directory.GetCurrentDirectory());
        }

        static void CompletedTasks(List<string> TasKs, string Filename) 
        {
            if (TasKs.Count == 0) {
                Console.WriteLine("Задач нет");
                return;
            }

            Console.WriteLine("Выберите номер задачи");

            for (int i = 0; i < TasKs.Count; i++) 
            {
                Console.WriteLine($"{i+1}. {TasKs [i]}");
            } //перебор элементов и выдача сохраненного списка задачи

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= TasKs.Count)
            {
                string Task = TasKs[index - 1];
                if (Task.StartsWith("[ ] "))
                {
                    TasKs[index - 1] = "[X] " + Task.Substring(4);
                    SaveTasks(TasKs, Filename);
                    Console.WriteLine("Задача выполнена!");
                }
                else
                {
                    Console.WriteLine("Задача уже выполнена" +
                        "");
                }
            }
            else 
            {
                Console.WriteLine("Неверный номер команды");
            }
        }

        static void Main(string[] args) //static привязывает метод под названием Main к классу Console_To_Do_List, делая его статическим методом. А void - тип возвращаемого значения, который не возвращает конкретный тип данных
        //void только выполняет действие, и ничего не возвращает
        //другие типы возращаемых данных, в отличие от void, сначала проверяются другой программой ОС, и только потом выдают код для завершения (0 - данные соответствуют, программа завершается успешно; другое число - ложь). 
        //void завершает программу через код 0 по умолчанию
        // string - текстовый тип данных. string[] - массив строк. args - имя переменной, которая присваивает себе массив строк
        {
            //List<string> Tasks = new List<string>(); //здесь создается объект-список для хранения строк. это временный список задач. данные после завершения программы буду очищаться
            //под названием Tasks, под который выделяяется память через new, как под список из-за List<string>
            //в скобках не указаны начальные параметры, т.к. они будут добавляться в процессе
            string filename = args.Length > 0 ? args[0] : "tasks.txt"; //тернарный условный оператор. после "=" пишется условие, "?" - истинное условие, ":" - ложное условие   
            List<string> tasks = LoadTasks(filename);

            while (true) 
            {
                Console.WriteLine("\n=== Менеджер задач ===");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать задачи");
                Console.WriteLine("3. Удалить задачу");
                Console.WriteLine("4. Отметить выполненую задачу");
                Console.WriteLine("5. Выйти из менеджера");
                Console.Write("Введите номер команды: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Напишите задачу: ");
                    string task = Console.ReadLine();
                    string newTasks = "[ ] " + task;
                    tasks.Add(newTasks);
                    SaveTasks(tasks, filename);
                    Console.WriteLine("Задача добавлена");
                    Console.WriteLine($"Количество существующих задач: {tasks.Count}");
                }

                else if (choice == "2")
                {
                    Console.WriteLine("Список задач: ");
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {tasks[i]}");
                    }
                }

                else if (choice == "3")
                {
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("Задач для удаления нет");
                    }

                    else
                    {
                        Console.WriteLine("Список задач: ");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i]}");
                        }

                        Console.WriteLine("Введите номер задачи для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int num) && num >= 1 && num <= tasks.Count) //&& - это логический оператор И
                        {
                            tasks.RemoveAt(num - 1);
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
                    CompletedTasks(tasks,filename);
                }

                else if (choice == "5") 
                {
                    SaveTasks(tasks, filename);
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
