using System;
using System.Collections.Generic;
namespace РаспределениеЗадачПоИерархииСотрудников;
//Написать программу, моделирующую иерархию сотрудников в компании и автоматическое распределение задач.
//Иерархия компании: Генеральный директор — Тимур; Подчиненные Тимура: Финансовый директор — Рашид,
//Директор по автоматизации — Ильхам; В подчинении Рашида — бухгалтерия (главный — Лукас);
//В подчинении Ильхама — отдел информационных технологий,
//состоящий из двух секторов: сектор системщиков (начальник — Оркадий, зам. начальника — Володя,
//главный — Ильшат, зам. главного — Иваныч, сотрудники: Илья, Витя, Женя) и
//сектор разработчиков (начальник — Сергей, зам. начальника — Ляйсан, сотрудники: Марат, Дина, Ильдар, Антон).
//Программа должна распределять задачи среди сотрудников, исходя из их иерархии и
//назначать их на основе категорий: системщикам, разработчикам или начальству.
//Вывод на экране: кто назначает задачу, кому и берёт ли этот человек задачу.


class Program
{
static void Main()
{
    Employee timur = new Employee("Тимур", "Генеральный директор");
    Employee rashid = new Employee("Рашид", "Финансовый директор");
    Employee ilham = new Employee("О Ильхам", "Директор по автоматизации");

    Employee lucas = new Employee("Лукас", "Бухгалтерия");

    Employee orkadiy = new Employee("Оркадий", "Начальник ИТ отдела");
    Employee volodya = new Employee("Володя", "Заместитель начальника ИТ отдела");

    Employee ilshat = new Employee("Ильшат", "Системщики");
    Employee ivanych = new Employee("Иваныч", "Зам. системщиков");
    Employee ilya = new Employee("Илья", "Системщик");
    Employee vitya = new Employee("Витя", "Системщик");
    Employee zhenya = new Employee("Женя", "Системщик");

    Employee sergey = new Employee("Сергей", "Разработчики");
    Employee lyaysan = new Employee("Ляйсан", "Зам. разработчиков");
    Employee marat = new Employee("Марат", "Разработчик");
    Employee dina = new Employee("Дина", "Разработчик");
    Employee ildar = new Employee("Ильдар", "Разработчик");
    Employee anton = new Employee("Антон", "Разработчик");

    timur.AddSubordinate(rashid);
    timur.AddSubordinate(ilham);

    rashid.AddSubordinate(lucas);

    ilham.AddSubordinate(orkadiy);
    ilham.AddSubordinate(volodya);

    orkadiy.AddSubordinate(ilshat);
    orkadiy.AddSubordinate(ivanych);
    orkadiy.AddSubordinate(sergey);

    ivanych.AddSubordinate(ilya);
    ivanych.AddSubordinate(vitya);
    ivanych.AddSubordinate(zhenya);

    lyaysan.AddSubordinate(marat);
    lyaysan.AddSubordinate(dina);
    lyaysan.AddSubordinate(ildar);
    lyaysan.AddSubordinate(anton);

    Task task1 = new Task("Автоматизация бухгалтерии", TaskType.Management);
    Task task2 = new Task("Разработка новой системы", TaskType.Developers);
    Task task3 = new Task("Установка сетевого оборудования", TaskType.Systems);

    task1.AssignToEmployee(rashid);
    task2.AssignToEmployee(sergey);
    task3.AssignToEmployee(ilshat);
}
}

enum TaskType
{
Management,
Developers,
Systems
}

class Task
{
public string Name { get; set; }
public TaskType Type { get; set; }

public Task(string name, TaskType type)
{
    Name = name;
    Type = type;
}

public void AssignToEmployee(Employee employee)
{
    if (employee.CanTakeTask(Type))
    {
        Console.WriteLine($"{employee.Name} берет задачу: {Name}");
    }
    else
    {
        Console.WriteLine($"{employee.Name} не может взять задачу: {Name}");
    }
}
}

class Employee
{
public string Name { get; set; }
public string Position { get; set; }
public List<Employee> Subordinates { get; set; }

public Employee(string name, string position)
{
    Name = name;
    Position = position;
    Subordinates = new List<Employee>();
}

public void AddSubordinate(Employee subordinate)
{
    Subordinates.Add(subordinate);
}

public bool CanTakeTask(TaskType taskType)
{
    if (taskType == TaskType.Management && (Position == "Генеральный директор" || Position == "Финансовый директор" || Position == "Директор по автоматизации"))
    {
        return true;
    }

    if (taskType == TaskType.Developers && (Position == "Разработчик" || Position == "Зам. разработчиков" || Position == "Главный разработчик"))
    {
        return true;
    }

    if (taskType == TaskType.Systems && (Position == "Системщик" || Position == "Зам. системщиков" || Position == "Главный системщик"))
    {
        return true;
    }

    return false;
}
}
