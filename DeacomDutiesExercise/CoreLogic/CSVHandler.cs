using CsvHelper;
using DeacomDutiesExercise.Models.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DeacomDutiesExercise.CoreLogic
{
    public class CSVHandler<T> where T : ISecretDTO, new()
    {
        private string _path;

        public CSVHandler(string path)
        {
            _path = path;
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
                Console.WriteLine(e.Message);
            }
            
            return secrets;
        }
    }
}
