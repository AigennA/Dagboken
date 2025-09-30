using System;
using System.Collections.Generic;
using Dagboken;

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
                    case "3": SearchMenu(); break;
                    case "4": UpdateMenu(); break;
                    case "5": DeleteMenu(); break;
                    case "6": SaveToFile(); break;
                    case "7": ReadFromFile(); break;
                    case "8":
                        SaveToFile();
                        Console.WriteLine("Tack för att du använde Dagboken.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║        DAGBOKEN          ║");
            Console.WriteLine("╠════════════════════════════╣");
            Console.WriteLine("║ 1. Skriv ny anteckning     ║");
            Console.WriteLine("║ 2. Lista alla anteckningar ║");
            Console.WriteLine("║ 3. Sök anteckning på datum ║");
            Console.WriteLine("║ 4. Uppdatera anteckning    ║");
            Console.WriteLine("║ 5. Ta bort anteckning      ║");
            Console.WriteLine("║ 6. Spara till fil          ║");
            Console.WriteLine("║ 7. Läs från fil            ║");
            Console.WriteLine("║ 8. Avsluta                 ║");
            Console.WriteLine("╚════════════════════════════╝");
        }

        private void WriteEntry()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         NY DAGBOKSANTECKNING       ║");
            Console.WriteLine("╠════════════════════════════════════╣");
            Console.WriteLine("║ Ange datum och text nedan          ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            // Stub
        }

        private void ListEntries()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         ALLA ANTECKNINGAR          ║");
            Console.WriteLine("╠════════════════════════════════════╣");

            // Stub

            Console.WriteLine("╚════════════════════════════════════╝");
        }

        private void SearchMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║     SÖK ANTECKNING         ║");
            Console.WriteLine("╠════════════════════════════╣");
            Console.WriteLine("║ 1. Sök exakt datum         ║");
            Console.WriteLine("║ 2. Gå tillbaka             ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.Write("Val: ");
            string? val = Console.ReadLine();
            Console.WriteLine();

            switch (val)
            {
                case "1":
                    // Stub
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        private void UpdateMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║   UPPDATERA ANTECKNING     ║");
            Console.WriteLine("╠════════════════════════════╣");
            Console.WriteLine("║ 1. Uppdatera text          ║");
            Console.WriteLine("║ 2. Gå tillbaka             ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.Write("Val: ");
            string? val = Console.ReadLine();
            Console.WriteLine();

            switch (val)
            {
                case "1":
                    // Stub
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        private void DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║    TA BORT ANTECKNING      ║");
            Console.WriteLine("╠════════════════════════════╣");
            Console.WriteLine("║ 1. Ta bort på datum        ║");
            Console.WriteLine("║ 2. Gå tillbaka             ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.Write("Val: ");
            string? val = Console.ReadLine();
            Console.WriteLine();

            switch (val)
            {
                case "1":
                    // Stub
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        private void SaveToFile()
        {
            // Stub
        }

        private void ReadFromFile()
        {
            // Stub
        }

        private DateTime PromptForDate(string prompt)
        {
            // Stub
            return DateTime.MinValue;
        }
    }
}
