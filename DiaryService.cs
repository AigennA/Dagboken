using Dagboken;
using System;
using System.Collections.Generic;

namespace Dagboken
{
    public class DiaryService
    {
        private List<DiaryEntry> _entries;
        private Dictionary<DateTime, DiaryEntry> _entryDict;

        public DiaryService()
        {
            _entries = new List<DiaryEntry>();
            _entryDict = new Dictionary<DateTime, DiaryEntry>();
        }

        public bool AddEntry(DateTime date, string text)
        {
            return false;
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

        public List<DiaryEntry> GetAllEntries()
        {
            return new List<DiaryEntry>();
        }

        public void LoadFromFile(List<DiaryEntry> lista)
        {
        }
    }
}
