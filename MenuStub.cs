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
                    case "4": SearchEntryByText(); break;
                    case "5": UpdateEntry(); break;
                    case "6": DeleteEntry(); break;
                    case "7": SaveToFile(); break;
                    case "8": ReadFromFile(); break;
                    case "9":
                        SaveToFile();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("╔════════════════════════════════════════════╗");
                        Console.WriteLine("║                                            ║");
                        Console.WriteLine("║   ✨ Tack för att du använde Dagboken ✨   ║");
                        Console.WriteLine("║                                            ║");
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        Console.ResetColor();

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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                        ║");
            Console.WriteLine("║                  📖 DAGBOKEN – MENY 📖                 ║");
            Console.WriteLine("║                                                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Skriv ny anteckning                               ║");
            Console.WriteLine("║  2. Lista alla anteckningar                           ║");
            Console.WriteLine("║  3. Sök anteckning på datum                           ║");
            Console.WriteLine("║  4. Sök anteckning med text                           ║");
            Console.WriteLine("║  5. Uppdatera anteckning                              ║");
            Console.WriteLine("║  6. Ta bort anteckning                                ║");
            Console.WriteLine("║  7. Spara till fil                                    ║");
            Console.WriteLine("║  8. Läs från fil                                      ║");
            Console.WriteLine("║  9. Avsluta                                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }




        private void WriteEntry()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         NY DAGBOKSANTECKNING       ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();

            DateTime datum = PromptForDate("Datum (yyyy-MM-dd): ");
            if (datum == DateTime.MinValue) return;

            Console.Write("Text: ");
            string? text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Texten får inte vara tom.");
                return;
            }

            bool success = _diaryService.AddEntry(datum, text);
            if (success)
            {
                Console.WriteLine("Anteckning tillagd.");
                SaveToFile();
            }
            else
            {
                Console.WriteLine("Kunde inte lägga till anteckning.");
            }
        }

        private void ListEntries()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║         ALLA ANTECKNINGAR          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();


            Console.Write("Läser från minnet");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400); // Väntar 400 ms per punkt
                Console.Write(".");
            }
            Console.WriteLine("\n");

            var entries = _diaryService.GetAllEntries();

            if (entries.Count == 0)
            {
                Console.WriteLine("Inga anteckningar hittades.");
                return;
            }

            foreach (var entry in entries.OrderBy(e => e.Date))
                Console.WriteLine(entry.ToString());
        }


        private void SearchEntry()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║   SÖK ANTECKNING PÅ DATUM  ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.ResetColor();

            Console.Write("Ange datum (åååå-mm-dd) eller x för att avbryta: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "x")
            {
                Console.WriteLine("Sökning avbruten.");
                return;
            }

            if (!DateTime.TryParse(input, out DateTime date))
            {
                Console.WriteLine("Ogiltigt datumformat.");
                return;
            }

            var entry = _diaryService.GetEntryByDate(date);

            if (entry != null)
                Console.WriteLine(entry.ToString());
            else
                Console.WriteLine("Ingen anteckning hittades.");
        }

        private void SearchEntryByText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║   SÖK ANTECKNING MED TEXT  ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.ResetColor();


            Console.Write("Ange sökord eller bokstav (eller x för att avbryta): ");
            string? query = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(query) || query.ToLower() == "x")
            {
                Console.WriteLine("Sökningen avbröts.");
                return;
            }

            var results = _diaryService.GetAllEntries()
                .Where(e => e.Text.Contains(query, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.Date)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Ingen anteckning innehåller den texten.");
                return;
            }

            Console.WriteLine($"Hittade {results.Count} anteckningar:");
            foreach (var entry in results)
                Console.WriteLine(entry.ToString());
        }

        private void UpdateEntry()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║   UPPDATERA ANTECKNING     ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.ResetColor();



            DateTime datum = PromptForDate("Ange datum att uppdatera (åååå-mm-dd eller x): ");
            if (datum == DateTime.MinValue)
            {
                Console.WriteLine("Uppdatering avbruten. Återgår till menyn.");
                return;
            }

            Console.Write("Ny text: ");
            string? text = Console.ReadLine();

            bool lyckades = _diaryService.UpdateEntry(datum, text ?? "");
            if (lyckades)
                Console.WriteLine("Anteckningen har uppdaterats.");
            else
                Console.WriteLine("Ingen anteckning hittades eller texten var tom.");
        }

        // Här är metoden för att ta bort eller uppdatera en anteckning med bara datum, men kan sökas också med text.
        // Kan ändras senare om det behövs.

        private void DeleteEntry()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              TA BORT ANTECKNING           ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();

            DateTime date = PromptForDate("Datum att ta bort (åååå-mm-dd eller x): ").Date;
            if (date == DateTime.MinValue)
            {
                Console.WriteLine("Borttagning avbruten.");
                return;
            }

            if (_diaryService.RemoveEntry(date))
            {
                Console.WriteLine("Anteckning borttagen.");
                _fileHandler.SaveEntries(_diaryService.GetAllEntries().ToList());
            }
            else
            {
                Console.WriteLine("Ingen anteckning hittades.");
            }
        }


        private void SaveToFile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║      SPARAR TILL FIL       ║");
            Console.WriteLine("╚════════════════════════════╝");

            Console.ResetColor();
            var entries = _diaryService.GetAllEntries();
            _fileHandler.SaveEntries(entries.ToList());

            Console.WriteLine($"{entries.Count} anteckningar har sparats till filen.");
        }


        private void ReadFromFile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║      LÄSER FRÅN FIL        ║");
            Console.WriteLine("╚════════════════════════════╝");

            Console.ResetColor();
            var loaded = _fileHandler.LoadEntries();
            _diaryService.LoadFromFile(loaded);

            Console.WriteLine($"{loaded.Count} anteckningar har lästs in från filen.\n");

            if (loaded.Count > 0)
            {
                Console.WriteLine("Inlästa anteckningar:");
                foreach (var entry in loaded.OrderBy(e => e.Date))
                    Console.WriteLine(entry.ToString());
            }
            else
            {
                Console.WriteLine("Filen var tom eller inga giltiga anteckningar.");
            }
        }

        // Här är metoden för att exportera anteckningar till CSV-fil.
       /* private void ExportToCsv()
        {
            var entries = _diaryService.GetAllEntries();

            if (entries.Count == 0)
            {
                Console.WriteLine("Inga anteckningar att exportera.");
                return;
            }

            _fileHandler.ExportToCsv(entries.ToList(), "dagbok.csv");
            Console.WriteLine($"{entries.Count} anteckningar har exporterats till dagbok.csv.");
        }*/

        // En hjälpfunktion för att hantera datuminmatning med felhantering och avbrytning.
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
    }
}
