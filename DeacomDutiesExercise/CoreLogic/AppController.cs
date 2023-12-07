using DeacomDutiesExercise.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeacomDutiesExercise.CoreLogic
{
    public static class AppController
    {
        private static LogBook _log = new ();

        public static void Run()
        {
            string[] validValues = new string[] { "1", "2", "3" };
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" _____________");
            Console.WriteLine("| Deacom Task |");
            Console.WriteLine(" -------------");
            Console.ResetColor();
            Console.WriteLine("** what do you want to do **");
            Console.WriteLine("[1] Import a .csv file");
            Console.WriteLine("[2] Export Database to .xml");
            Console.WriteLine("[3] Exit");
            string? input = Console.ReadLine();
            ValidateInput(ref input, validValues);

            switch (input)
            {
                case "1":
                    ImportCSV();  break;
                case "2":
                    ExportDB(); break;
                case "3":
                    _log.AddInfo("Selected Exit program... Closing.");
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ImportCSV()
        {
            _log.AddInfo("Selected Import");
            string[] validValues = new string[] { "1", "2", "3" };
            Console.WriteLine("Please, select the type of Secret you want to import");
            Console.WriteLine("[1] Primary");
            Console.WriteLine("[2] Secondary");
            Console.WriteLine("[3] Show example");
            string? input = Console.ReadLine();
            ValidateInput(ref input, validValues);

            switch(input)
            {
                case "1":
                    break;
                case "2": break;
                case "3": PrintSample(); break;
            }


            void PrintSample()
            {
                Console.WriteLine("dd");
            }
        }

        private static void ExportDB()
        {
            _log.AddInfo("Selected Export");
            IConfiguration config = AppConfig.GetConfiguration();

            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Exported .xml file will be saved here:");
                Console.WriteLine(config.GetSection("FilePaths:Export").Value);
                Console.ResetColor();
                Exporter.ToXML();
            }
            catch( Exception ex )
            {
                _log.AddError("Something wrong while Exporting", ex);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong");
                Console.ResetColor();
            }
        }

        private static void ValidateInput(ref string? input, string[]? validValues = null)
        {
            const int MAX_ATTEMPTS = 5;
            int attempts = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            _log.AddInfo($"Validating input, max attempts: {MAX_ATTEMPTS} | validValuesIsNull {validValues is null}");

            if(validValues is not null)
            {
                string validValuesStr = string.Join(", ", validValues);

                while (attempts < MAX_ATTEMPTS && !validValues.Contains(input) || input is null)
                {
                    Console.WriteLine($"Please, enter a valid value:\n[{validValuesStr}]");
                    input = Console.ReadLine();
                    attempts++;

                    if (attempts == MAX_ATTEMPTS-1)
                    {
                        _log.AddInfo("There is one attempt left for a valid input");
                        Console.WriteLine("You got one more chance to put a right value");
                    }
                }
            }
            else
            {
                while(attempts < MAX_ATTEMPTS || input is null)
                {
                    Console.WriteLine("Please, enter a valid value.");
                    input = Console.ReadLine();
                    attempts++;

                    if (attempts == MAX_ATTEMPTS - 1)
                    {
                        _log.AddInfo("There is one attempt left for a valid input");
                        Console.WriteLine("You got one more chance to put a right value");
                    }
                }
            }
            Console.ResetColor();
        }
    }

}
