using System;
using System.IO;
using System.Collections.Generic;

namespace Tumakovlaboratory_8
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Выберите задачу для выполнения:");
                Console.WriteLine("1. Банковский счет с переводом денег");
                Console.WriteLine("2. Переворот строки");
                Console.WriteLine("3. Чтение файла и запись в новый с преобразованием в верхний регистр");
                Console.WriteLine("4. Проверка интерфейса IFormattable");
                Console.WriteLine("5. Извлечение e-mail из файла");
                Console.WriteLine("6. Работа с песнями");
                Console.WriteLine("0. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleBankTransactions();
                        break;
                    case "2":
                        ReverseStringTask();
                        break;
                    case "3":
                        ProcessFileTask();
                        break;
                    case "4":
                        CheckIFormattable();
                        break;
                    case "5":
                        ExtractEmailsFromFile();
                        break;
                    case "6":
                        HandleSongs();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        // Упражнение 8.1: Реализовать класс банковского счета с возможностью перевода денег с одного счета на другой
        static void HandleBankTransactions()
        {
            BankAccount account1 = new BankAccount("Иван Иванов", 12345);
            BankAccount account2 = new BankAccount("Петр Петров", 7890);

            PrintAccountInfo(account1);
            PrintAccountInfo(account2);
            account1.Transfer(account2, 300);
            PrintAccountInfo(account1);
            PrintAccountInfo(account2);
        }

        static void PrintAccountInfo(BankAccount account)
        {
            Console.WriteLine($"Баланс счета {account.AccountHolder}: {account.Balance}");
        }

        // Упражнение 8.2: Реализовать метод, который переворачивает строку
        static void ReverseStringTask()
        {
            string input = "C# — объектно-ориентированный язык программирования";
            string reversed = ReverseString(input);
            Console.WriteLine("Original String: " + input);
            Console.WriteLine("Reversed String: " + reversed);
        }

        public static string ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // Упражнение 8.3: Прочитать файл, преобразовать текст в верхний регистр и записать в новый файл
        static void ProcessFileTask()
        {
            Console.WriteLine("Введите полный путь к исходному файлу:");
            string inputFileName = Console.ReadLine();

            if (!File.Exists(inputFileName))
            {
                Console.WriteLine("Ошибка: файл не существует.");
                return;
            }

            Console.WriteLine("Введите полный путь для выходного файла:");
            string outputFileName = Console.ReadLine();

            try
            {
                string fileContent = File.ReadAllText(inputFileName);
                string upperCaseContent = fileContent.ToUpper();

                File.WriteAllText(outputFileName, upperCaseContent);
                Console.WriteLine($"Содержимое файла {inputFileName} было записано в файл {outputFileName}.");

                Console.WriteLine("\nСодержимое выходного файла:");
                Console.WriteLine(File.ReadAllText(outputFileName));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        // Упражнение 8.4: Проверка, реализует ли объект интерфейс IFormattable
        static void CheckIFormattable()
        {
            object obj1 = 123;
            object obj2 = "Hello";

            Console.WriteLine(CheckIfIFormattable(obj1));
            Console.WriteLine(CheckIfIFormattable(obj2));
        }

        static bool CheckIfIFormattable(object obj)
        {
            if (obj is IFormattable)
            {
                return true;
            }

            IFormattable formattableObj = obj as IFormattable;
            return formattableObj != null;
        }

        // Домашнее задание 8.1: Извлечь email из файла и сохранить в новый файл
        static void ExtractEmailsFromFile()
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";
            try
            {
                string[] lines = File.ReadAllLines(inputFilePath);
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (string line in lines)
                    {
                        string email = line;
                        SearchMail(ref email);
                        writer.WriteLine(email);
                    }
                }
                Console.WriteLine("Список адресов электронной почты был успешно записан в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static void SearchMail(ref string s)
        {
            int index = s.IndexOf('#');
            if (index != -1)
            {
                s = s.Substring(index + 2).Trim();
            }
        }

        // Домашнее задание 8.2: Работа с песнями, создание списка песен
        static void HandleSongs()
        {
            List<Song> songs = CreateSongsList();
            PrintSongs(songs);

            bool areEqual = songs[0].Equals(songs[1]);
            Console.WriteLine($"Первая и вторая песня одинаковы: {areEqual}");
        }

        static List<Song> CreateSongsList()
        {
            List<Song> songs = new List<Song>();

            Song song1 = new Song();
            song1.FillName("Song1");
            song1.FillAuthor("Author1");
            songs.Add(song1);

            Song song2 = new Song();
            song2.FillName("Song2");
            song2.FillAuthor("Author2");
            song2.FillPrev(song1);
            songs.Add(song2);

            Song song3 = new Song();
            song3.FillName("Song3");
            song3.FillAuthor("Author3");
            song3.FillPrev(song2);
            songs.Add(song3);

            Song song4 = new Song();
            song4.FillName("Song4");
            song4.FillAuthor("Author4");
            song4.FillPrev(song3);
            songs.Add(song4);

            return songs;
        }

        static void PrintSongs(List<Song> songs)
        {
            foreach (var song in songs)
            {
                Console.WriteLine(song.Title());
            }
        }
    }

    class BankAccount
    {
        public string AccountHolder { get; set; }
        public double Balance { get; private set; }

        public BankAccount(string accountHolder, double initialBalance)
        {
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Внесено {amount} на счет {AccountHolder}. Новый баланс: {Balance}");
            }
        }

        public bool Withdraw(double amount)
        {
            if (amount <= Balance && amount > 0)
            {
                Balance -= amount;
                Console.WriteLine($"Снято {amount} с счета {AccountHolder}. Новый баланс: {Balance}");
                return true;
            }
            return false;
        }

        public void Transfer(BankAccount targetAccount, double amount)
        {
            if (this.Withdraw(amount))
            {
                targetAccount.Deposit(amount);
                Console.WriteLine($"Переведено {amount} с счета {this.AccountHolder} на счет {targetAccount.AccountHolder}");
            }
        }
    }

    class Song
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public Song Prev { get; set; }

        public void FillName(string name)
        {
            Name = name;
        }

        public void FillAuthor(string author)
        {
            Author = author;
        }

        public void FillPrev(Song prev)
        {
            Prev = prev;
        }

        public string Title()
        {
            return $"{Name} - {Author}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Song other = (Song)obj;
            return Name == other.Name && Author == other.Author;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Author.GetHashCode();
        }
    }
}
