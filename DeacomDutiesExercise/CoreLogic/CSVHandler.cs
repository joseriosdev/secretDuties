using CsvHelper;
using DeacomDutiesExercise.Models.Interfaces;
using DeacomDutiesExercise.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DeacomDutiesExercise.CoreLogic
{
    public class CSVHandler<T> where T : ISecretDTO, new()
    {
        private string _path;
        private LogBook _log;

        public CSVHandler(string path)
        {
            _path = path;
            _log = new LogBook();
        }

        public List<T> ReadCsvFile([NotNull] string fileName)
        {
            string filePath = $"{_path}{fileName}.csv";
            List<T> secrets = new ();
            

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    secrets = csv.GetRecords<T>().ToList();
                }

            } catch(Exception e)
            {
                _log.AddError($"Error ocurred when importing file: {filePath}", e);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong importing the file: '{fileName}'");
                Console.ResetColor();
            }
            
            return secrets;
        }
    }
}
