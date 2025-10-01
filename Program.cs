using Dagboken;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var diaryService = new DiaryService();
        var fileHandler = new FileHandler("diary.json");
        var loadedEntries = fileHandler.LoadEntries();
        diaryService.LoadFromFile(loadedEntries);

        var menuStub = new MenuStub(diaryService, fileHandler);
        menuStub.Run();
    }
}
