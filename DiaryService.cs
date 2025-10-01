using Dagboken;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Dagboken
{
    public class DiaryService
    {
        // lista för att lagra alla anteckningar
        private readonly List<DiaryEntry> _entries = new();

        // Dictionary för datum-baserad snabb åtkomst
        private readonly Dictionary<DateTime, DiaryEntry> _entryDict = new();

        public IReadOnlyList<DiaryEntry> GetAllEntries() => _entries;

        public bool AddEntry(DateTime date, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            var entry = new DiaryEntry { Date = date, Text = text };
            _entries.Add(entry);
            _entryDict[date] = entry;
            return true;
        }

        public DiaryEntry? GetEntryByDate(DateTime date)
        {
            return _entryDict.TryGetValue(date, out var entry) ? entry : null;
        }

        public bool UpdateEntry(DateTime date, string newText)
        {
            if (_entryDict.ContainsKey(date) && !string.IsNullOrWhiteSpace(newText))
            {
                _entryDict[date].Text = newText;
                return true;
            }
            return false;
        }

        public bool RemoveEntry(DateTime date)
        {
            if (_entryDict.ContainsKey(date))
            {
                var entry = _entryDict[date];
                _entries.Remove(entry);
                _entryDict.Remove(date);
                return true;
            }
            return false;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (DiaryEntry entry in _entries)
                    {
                        string line = $"{entry.Date:yyyy-MM-dd}|{entry.Text}";
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid sparning: " + ex.Message);
            }
        }

        public void LoadFromFile(string filePath)
        {
            List<DiaryEntry> loadedEntries = new List<DiaryEntry>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length != 2) continue;

                        if (DateTime.TryParse(parts[0], out DateTime date))
                        {
                            string text = parts[1];
                            loadedEntries.Add(new DiaryEntry { Date = date, Text = text });
                        }
                    }
                }

                LoadFromFile(loadedEntries); // uppdaterar 
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Filen hittades inte.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid inläsning: " + ex.Message);
            }
        }

        public void LoadFromFile(List<DiaryEntry> loadedEntries)
        {
            _entries.Clear();
            _entryDict.Clear();
            foreach (var entry in loadedEntries)
            {
                _entries.Add(entry);
                _entryDict[entry.Date] = entry;
            }
        }
    }
}
