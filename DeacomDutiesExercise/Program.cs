using DeacomDutiesExercise.Utils;
using DeacomDutiesExercise.CoreLogic;

public class Program
{
    private static void Main(string[] args)
    {
        var log = new LogBook();
        log.AddInfo("---------------------- Starting the program ----------------------");

        while(true)
            AppController.Run();
    }
}