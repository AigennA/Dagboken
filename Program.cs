namespace Dagboken
{
  
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello from Dagboken");
            DiaryService dagbok = new DiaryService();
            FileHandler filhanterare = new FileHandler("diary.json");
          
        }
    }
}

