using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Dagboken
{
    public class FileHandler
    {
        private readonly string _filePath;
        private readonly string _logFile = "error.log";

        public FileHandler(string filePath)
        {
            _filePath = filePath;
        }

        // Läser anteckningar från JSON-fil
        public List<DiaryEntry> LoadEntries()
        {
            List<DiaryEntry> entries = new();

            try
            {
                if (!File.Exists(_filePath))
                    return entries;

                string content = File.ReadAllText(_filePath);
                entries = JsonSerializer.Deserialize<List<DiaryEntry>>(content) ?? new();
            }
            catch (Exception ex)
            {
                LogError("Fel vid läsning från fil: " + ex.Message);
                Console.WriteLine("Kunde inte läsa filen. Se error.log för detaljer.");
            }

            return entries;
        }

        // Sparar anteckningar till JSON-fil
        public void SaveEntries(List<DiaryEntry> entries)
        {
            try
            {
                string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                LogError("Fel vid skrivning till fil: " + ex.Message);
                Console.WriteLine("Kunde inte spara filen. Se error.log för detaljer.");
            }
        }

        // Exporterar anteckningar till CSV-fil
        public void ExportToCsv(List<DiaryEntry> entries, string csvFile)
        {
            try
            {
                List<string> lines = new();

                foreach (var entry in entries)
                {
                    string line = $"{entry.Date:yyyy-MM-dd};\"{entry.Text.Replace("\"", "\"\"")}\"";
                    lines.Add(line);
                }

                File.WriteAllLines(csvFile, lines);
                Console.WriteLine("Exporterat till " + csvFile);
            }
            catch (Exception ex)
            {
                LogError("Fel vid CSV-export: " + ex.Message);
                Console.WriteLine("Kunde inte exportera till CSV. Se error.log.");
            }
        }

        // Loggar fel till separat textfil
        private void LogError(string message)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(_logFile, line + Environment.NewLine);
        }
    }
}
