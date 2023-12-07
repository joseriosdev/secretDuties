using DeacomDutiesExercise.DB;
using System.Data;
using System.Data.SqlClient;
using DeacomDutiesExercise.Models.DTOs;
using DeacomDutiesExercise.Utils;
using System.Xml;
using DeacomDutiesExercise.CoreLogic;
using DeacomDutiesExercise.Models;
using Microsoft.Extensions.Configuration;
using DeacomDutiesExercise;
using System.Reflection;

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