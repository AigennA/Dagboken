using Dagboken;
using System;
using System.Collections.Generic;

namespace Dagboken
{
    public class DiaryService
    {
        private readonly List<DiaryEntry> _entries = new();
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

        public bool UpdateEntry(DateTime date, string newText)
        {
            return false;
        }

        public bool RemoveEntry(DateTime date)
        {
            return false;
        }

        public DiaryEntry GetEntryByDate(DateTime date)
        {
            return null;
        }


        public void LoadFromFile(List<DiaryEntry> lista)
        {
        }
    }
}
