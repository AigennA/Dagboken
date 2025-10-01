using System;
using System.Collections.Generic;
using System.Linq;
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
                    case "3": SearchEntry(); break;
                    case "4": UpdateEntry(); break;
                    case "5": DeleteEntry(); break;
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
            Console.WriteLine("║        DAGBOKEN            ║");
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
            Console.ResetColor();
        }

        private void WriteEntry()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         NY DAGBOKSANTECKNING       ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            DateTime date = PromptForDate("Datum (åååå-mm-dd): ");
            Console.Write("Text: ");
            string? text = Console.ReadLine();

            if (_diaryService.AddEntry(date, text ?? ""))
                Console.WriteLine("Anteckning sparad.");
            else
                Console.WriteLine("Texten får inte vara tom.");
        }

        private void ListEntries()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         ALLA ANTECKNINGAR          ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            var entries = _diaryService.GetAllEntries();

            if (entries.Count == 0)
                Console.WriteLine("Inga anteckningar hittades.");
            else
                foreach (var entry in entries.OrderBy(e => e.Date))
                    Console.WriteLine(entry.ToString());
        }

        private void SearchEntry()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║     SÖK ANTECKNING         ║");
            Console.WriteLine("╚════════════════════════════╝");

            DateTime date = PromptForDate("Datum att söka (åååå-mm-dd eller x): ");
            if (date == DateTime.MinValue)
            {
                Console.WriteLine("Sökning avbruten.");
                return;
            }

            var entry = _diaryService.GetEntryByDate(date);

            if (entry != null)
                Console.WriteLine(entry.ToString());
            else
                Console.WriteLine("Ingen anteckning hittades.");
        }


        private void UpdateEntry()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║   UPPDATERA ANTECKNING     ║");
            Console.WriteLine("╚════════════════════════════╝");

            DateTime date = PromptForDate("Datum att uppdatera: ");
            Console.Write("Ny text: ");
            string? newText = Console.ReadLine();

            if (_diaryService.UpdateEntry(date, newText ?? ""))
                Console.WriteLine("Anteckning uppdaterad.");
            else
                Console.WriteLine("Ingen anteckning hittades eller texten var tom.");
        }

        private void DeleteEntry()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║    TA BORT ANTECKNING      ║");
            Console.WriteLine("╚════════════════════════════╝");

            DateTime date = PromptForDate("Datum att ta bort: ");

            if (_diaryService.RemoveEntry(date))
                Console.WriteLine("Anteckning borttagen.");
            else
                Console.WriteLine("Ingen anteckning hittades.");
        }

        private void SaveToFile()
        {
            var entries = _diaryService.GetAllEntries();
            _fileHandler.SaveEntries(entries.ToList());
            Console.WriteLine("Sparat till fil.");
        }

        private void ReadFromFile()
        {
            var loaded = _fileHandler.LoadEntries();
            _diaryService.LoadFromFile(loaded);
            Console.WriteLine("Läst från fil.");
        }

        private DateTime PromptForDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (input?.ToLower() == "x")
                    return DateTime.MinValue;

                if (DateTime.TryParse(input, out DateTime date))
                    return date;

                Console.WriteLine("Ogiltigt datumformat. Skriv igen eller 'x' för att avbryta.");
            }
        }


        private void ExportToCsv()
        {
            var entries = _diaryService.GetAllEntries();
            _fileHandler.ExportToCsv(entries.ToList(), "dagbok.csv");
        }
    }
}
