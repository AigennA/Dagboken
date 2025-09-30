using System;
using System.Collections.Generic;

namespace Dagboken
{
    public class MenuStub
    {
        private readonly DiaryService _diaryService;
        private readonly FileHandler _fileHandler;

        public MenuStub(DiaryService diaryService, FileHandler fileHandler)
        {
            _diaryService = diaryService;
            _fileHandler = fileHandler;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                Console.Write("Val: ");
                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": WriteEntry(); break;
                    case "2": ListEntries(); break;
                    case "3": SearchEntry(); break;
                    case "4": UpdateEntry(); break;
                    case "5": RemoveEntry(); break;
                    case "6": SaveToFile(); break;
                    case "7": LoadFromFile(); break;
                    case "8":
                        SaveToFile();
                        running = false;
                        break;
                    default:
                        break;
                }

                if (running)
                {
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Min Dagbok ===");
            Console.WriteLine("1. Skriv ny anteckning");
            Console.WriteLine("2. Lista alla anteckningar");
            Console.WriteLine("3. Sök anteckning på datum");
            Console.WriteLine("4. Uppdatera anteckning");
            Console.WriteLine("5. Ta bort anteckning");
            Console.WriteLine("6. Spara till fil");
            Console.WriteLine("7. Läs från fil");
            Console.WriteLine("8. Avsluta");
            Console.WriteLine("==================");
        }
        private void WriteEntry()
        {
        }

        private void ListEntries()
        {
        }

        private void SearchEntry()
        {
        }

        private void UpdateEntry()
        {
        }

        private void RemoveEntry()
        {
        }

        private void SaveToFile()
        {
        }

        private void LoadFromFile()
        {
        }

        private DateTime PromptForDate(string prompt)
        {
            return DateTime.MinValue;
        }
    }
}
