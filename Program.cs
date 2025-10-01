using Dagboken;


class Program
{
    static void Main()
    {
        var diaryService = new DiaryService();
        var fileHandler = new FileHandler("diary.json");
        diaryService.LoadFromFile(fileHandler.LoadEntries());


        var menuStub = new MenuStub(diaryService, fileHandler);
        menuStub.Run();
    }
}