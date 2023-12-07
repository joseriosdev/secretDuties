using Microsoft.Extensions.Configuration;

namespace DeacomDutiesExercise.Utils
{
    public class LogBook
    {
        private readonly IConfiguration _config;
        private readonly string _path;

        public LogBook()
        {
            _config = AppConfig.GetConfiguration();
            _path = _config.GetSection("FilePaths:Logs").Value!;
        }

        public void AddInfo(string message)
        {
            CreateDirectory();
            string name = GetNameFile();
            string str =
                $"[INFO] {DateTime.Now.ToString("yyyy-MM-dd_HH:MM:ss")} - {message}{Environment.NewLine}";

            StreamWriter sw = new StreamWriter($"{_path}/{name}", true);
            sw.Write(str);
            sw.Close();
        }

        public void AddError(string message, Exception ex)
        {
            CreateDirectory();
            string name = GetNameFile();
            string str =
                $"[ERROR] {DateTime.Now.ToString("yyyy-MM-dd_HH:MM:ss")} - {message}" +
                $"{Environment.NewLine} Execption: {ex.Message} - {ex}";

            StreamWriter sw = new StreamWriter($"{_path}/{name}", true);
            sw.Write(str);
            sw.Close();
        }


        private string GetNameFile() => $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
            }
            catch (DirectoryNotFoundException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
