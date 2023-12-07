using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeacomDutiesExercise.Utils
{
    public static class InputValidator
    {
        private static LogBook _log = new();
        private static IConfiguration _config = AppConfig.GetConfiguration();

        public static void ValidateInput(ref string? input, string[]? validValues = null)
        {
            const int MAX_ATTEMPTS = 5;
            int attempts = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            _log.AddInfo($"Validating input, max attempts: {MAX_ATTEMPTS} | validValuesIsNull {validValues is null}");

            if (validValues is not null)
            {
                string validValuesStr = string.Join(", ", validValues);

                while (attempts < MAX_ATTEMPTS && !validValues.Contains(input) || input is null)
                {
                    Console.WriteLine($"Please, enter a valid value:\n[{validValuesStr}]");
                    input = Console.ReadLine();
                    attempts++;

                    if (attempts == MAX_ATTEMPTS - 1)
                    {
                        _log.AddInfo("There is one attempt left for a valid input");
                        Console.WriteLine("You got one more chance to put a right value");
                    }
                }
            }
            else
            {
                while (attempts < MAX_ATTEMPTS || input is null)
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

        public static (string, string) ValidateImportPathFileName()
        {
            string? path = _config.GetSection("FilesPaths:Import").Value;
            if (path is null)
            {
                Exception ex = new NullReferenceException("Double check import path");
                _log.AddError("There is something wrong with the import path", ex);
                throw ex;
            }

            string? fileName = Console.ReadLine();
            ValidateInput(ref fileName);
            return (path, fileName!);
        }
    }
}
