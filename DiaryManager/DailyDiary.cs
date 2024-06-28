using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiaryManager
{
    public class DailyDiary
    {
        public string _filePath;

        public DailyDiary(string filePath)
        {
            _filePath = filePath;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Daily Diary Application");
                Console.WriteLine("1. Read Diary");
                Console.WriteLine("2. Add Entry");
                Console.WriteLine("3. Delete Entry");
                Console.WriteLine("4. Count Lines");
                Console.WriteLine("5. Search Entries");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine(ReadDiary());
                        break;
                    case "2":
                        AddEntry();
                        break;
                    case "3":
                        DeleteEntry();
                        break;
                    case "4":
                        Console.WriteLine($"Total number of lines in the diary: {CountLines()}");
                        break;
                    case "5":
                        SearchEntries();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public string ReadDiary()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    return File.ReadAllText(_filePath);
                }
                else
                {
                    return "No diary entries found.";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred while reading the diary: {ex.Message}";
            }
        }

        public void AddEntry(string entryContent = null)
        {
            try
            {
                string content;
                if (entryContent == null)
                {
                    Console.Write("Enter your diary entry: ");
                    content = Console.ReadLine();
                }
                else
                {
                    content = entryContent;
                }

                Entry entry = new Entry { Date = DateTime.Now, Content = content };
                File.AppendAllText(_filePath, entry.ToString() + Environment.NewLine);
                Console.WriteLine("Entry added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the entry: {ex.Message}");
            }
        }

        private void DeleteEntry()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    Console.Write("Enter the date of the entry to delete (YYYY-MM-DD): ");
                    string date = Console.ReadLine();

                    if (!Regex.IsMatch(date, @"^\d{4}-\d{2}-\d{2}$"))
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        return;
                    }

                    List<string> entries = File.ReadAllLines(_filePath).ToList();
                    bool entryDeleted = false;

                    for (int i = entries.Count - 1; i >= 0; i--)
                    {
                        if (entries[i].StartsWith(date))
                        {
                            entries.RemoveAt(i);

                            if (i < entries.Count)
                            {
                                entries.RemoveAt(i);
                            }

                            entryDeleted = true;
                        }
                    }

                    File.WriteAllLines(_filePath, entries);

                    if (entryDeleted)
                    {
                        Console.WriteLine("Entry deleted.");
                    }
                    else
                    {
                        Console.WriteLine("No entries found for the given date.");
                    }
                }
                else
                {
                    Console.WriteLine("No diary entries found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the entry: {ex.Message}");
            }
        }

        public int CountLines()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    int lineCount = File.ReadAllLines(_filePath).Length;
                    return lineCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while counting the lines: {ex.Message}");
                return -1;
            }
        }

        private void SearchEntries()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    Console.Write("Enter a keyword to search for: ");
                    string keyword = Console.ReadLine();
                    string[] entries = File.ReadAllLines(_filePath);
                    bool found = false;

                    foreach (string entry in entries)
                    {
                        if (entry.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(entry);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No entries found containing the keyword.");
                    }
                }
                else
                {
                    Console.WriteLine("No diary entries found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching the entries: {ex.Message}");
            }
        }
    }
}
