﻿using System;
using System.Collections.Generic;

public class Task
{
    public string Name { get; set; }
    public int Priority { get; set; }
    public DateTime Date { get; set; }
}

public class CompletedTaskNode
{
    public Task Task { get; set; }
    public CompletedTaskNode Next { get; set; }
}

class Program
{
    private const int MAX_TASKS = 100;
    private static Task[] tasks = new Task[MAX_TASKS];
    private static int taskCount = 0;
    private static CompletedTaskNode completedHead = null;
    private static Queue<Task> urgentTasks = new Queue<Task>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n1. Add Task\n2. Show Tasks\n3. Delete Task\n4. Sort by Priority\n5. Sort by Date\n6. Complete Task\n7. Show Completed\n8. Add Urgent\n9. Show Urgent\n10. Exit");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ShowTasks();
                    break;
                case "3":
                    DeleteTask();
                    break;
                case "4":
                    SortByPriority();
                    break;
                case "5":
                    SortByDate();
                    break;
                case "6":
                    CompleteTask();
                    break;
                case "7":
                    ShowCompleted();
                    break;
                case "8":
                    AddUrgent();
                    break;
                case "9":
                    ShowUrgent();
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
    }

    static void AddTask()
    {
        if (taskCount >= MAX_TASKS)
        {
            Console.WriteLine("Array full!");
            return;
        }
        Task t = new Task();
        Console.Write("Name: ");
        t.Name = Console.ReadLine();
        Console.Write("Priority (1-3): ");
        t.Priority = int.Parse(Console.ReadLine());
        Console.Write("Date (dd/mm/yyyy): ");
        t.Date = DateTime.Parse(Console.ReadLine());
        tasks[taskCount] = t;
        taskCount++;
        Console.WriteLine("Added!");
    }

    static void ShowTasks()
    {
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Name} | Prio: {tasks[i].Priority} | Date: {tasks[i].Date:d}");
        }
    }

    static void DeleteTask()
    {
        ShowTasks();
        Console.Write("Task number to delete: ");
        int num = int.Parse(Console.ReadLine()) - 1;
        if (num < 0 || num >= taskCount)
        {
            Console.WriteLine("Invalid");
            return;
        }
        for (int i = num; i < taskCount - 1; i++)
        {
            tasks[i] = tasks[i + 1];
        }
        taskCount--;
        Console.WriteLine("Deleted!");
    }

    static void SortByPriority()
    {
        for (int i = 0; i < taskCount - 1; i++)
        {
            for (int j = 0; j < taskCount - i - 1; j++)
            {
                if (tasks[j].Priority > tasks[j + 1].Priority)
                {
                    Task temp = tasks[j];
                    tasks[j] = tasks[j + 1];
                    tasks[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Sorted by priority!");
    }

    static void SortByDate()
    {
        QuickSort(0, taskCount - 1);
        Console.WriteLine("Sorted by date!");
    }

    static void QuickSort(int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(low, high);
            QuickSort(low, pi - 1);
            QuickSort(pi + 1, high);
        }
    }

    static int Partition(int low, int high)
    {
        DateTime pivot = tasks[high].Date;
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (tasks[j].Date < pivot)
            {
                i++;
                Task temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }
        Task temp2 = tasks[i + 1];
        tasks[i + 1] = tasks[high];
        tasks[high] = temp2;
        return i + 1;
    }

    static void CompleteTask()
    {
        ShowTasks();
        Console.Write("Task number to complete: ");
        int num = int.Parse(Console.ReadLine()) - 1;
        if (num < 0 || num >= taskCount)
        {
            Console.WriteLine("Invalid");
            return;
        }
        Task completed = tasks[num];
        for (int i = num; i < taskCount - 1; i++)
        {
            tasks[i] = tasks[i + 1];
        }
        taskCount--;

        CompletedTaskNode newNode = new CompletedTaskNode();
        newNode.Task = completed;
        newNode.Next = completedHead;
        completedHead = newNode;
        Console.WriteLine("Task completed!");
    }

    static void ShowCompleted()
    {
        CompletedTaskNode current = completedHead;
        int i = 1;
        while (current != null)
        {
            Console.WriteLine($"{i}. {current.Task.Name} | Prio: {current.Task.Priority} | Date: {current.Task.Date:d}");
            current = current.Next;
            i++;
        }
    }

    static void AddUrgent()
    {
        Task t = new Task();
        Console.Write("Urgent Task Name: ");
        t.Name = Console.ReadLine();
        Console.Write("Priority (1-3): ");
        t.Priority = int.Parse(Console.ReadLine());
        Console.Write("Date (dd/mm/yyyy): ");
        t.Date = DateTime.Parse(Console.ReadLine());
        urgentTasks.Enqueue(t);
        Console.WriteLine("Urgent task added!");
    }

    static void ShowUrgent()
    {
        int i = 1;
        foreach (Task t in urgentTasks)
        {
            Console.WriteLine($"{i}. {t.Name} | Prio: {t.Priority} | Date: {t.Date:d}");
            i++;
        }
    }
}