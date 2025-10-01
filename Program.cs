using Dagboken;

class Program
{
    static void Main()
    {
        var diaryService = new DiaryService();
        var fileHandler = new FileHandler("diary.json");
        var loadedEntries = fileHandler.LoadEntries();
        diaryService.LoadFromFile(loadedEntries);

        var menuStub = new MenuStub(diaryService, fileHandler);
        menuStub.Run();
    }
}
