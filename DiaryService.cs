using Dagboken;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Dagboken
{
    public class DiaryService
    {
        private readonly List<DiaryEntry> _entries = new();
        private readonly Dictionary<DateTime, DiaryEntry> _entryDict = new();

        public IReadOnlyList<DiaryEntry> GetAllEntries() => _entries;

        public bool AddEntry(DateTime date, string text)
        {
            date = date.Date; // Normalisera datum
            if (string.IsNullOrWhiteSpace(text)) return false;
            if (_entryDict.ContainsKey(date)) return false; 

            var entry = new DiaryEntry { Date = date, Text = text };
            _entries.Add(entry);
            _entryDict[date] = entry;
            return true;
        }

        public DiaryEntry? GetEntryByDate(DateTime date)
        {
            date = date.Date; // Normalisera datum
            return _entryDict.TryGetValue(date, out var entry) ? entry : null;
        }

        public List<DiaryEntry> SearchEntriesByText(string keyword)
        {
            return _entries
                .Where(e => e.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool UpdateEntry(DateTime date, string newText)
        {
            date = date.Date; // Normalisera datum
            if (_entryDict.ContainsKey(date) && !string.IsNullOrWhiteSpace(newText))
            {
                _entryDict[date].Text = newText;
                return true;
            }
            return false;
        }

        public bool RemoveEntry(DateTime date)
        {
            date = date.Date; // Normalisera datum
            if (_entryDict.ContainsKey(date))
            {
                var entry = _entryDict[date];
                _entries.Remove(entry);
                _entryDict.Remove(date);
                return true;
            }
            return false;
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
        public void SaveToFile(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var entry in _entries)
                writer.WriteLine($"{entry.Date:yyyy-MM-dd}||{entry.Text}");
        }

    }
}