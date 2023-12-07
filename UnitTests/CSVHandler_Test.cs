using DeacomDutiesExercise;
using DeacomDutiesExercise.CoreLogic;
using DeacomDutiesExercise.Models.DTOs;
using Microsoft.Extensions.Configuration;

namespace UnitTests
{
    public class CSVHandler_Test
    {
        private IConfiguration _config;
        private string? _path;
        public CSVHandler_Test()
        {
            _config = AppConfig.GetConfiguration();
            _path = _config.GetSection(nameof(CSVHandler_Test)).Value;
        }

        [Fact]
        public void ReadCsvFile_PrimarySecret_Error()
        {
            var path = _config.GetSection("FilePaths:Import").Value;
            var handler = new CSVHandler<PrimarySecretDTO>(path!);

            var secrets = handler.ReadCsvFile("invalidName_cvñlkj");

            Assert.NotNull(secrets);
            Assert.IsType<List<PrimarySecretDTO>>(secrets);
            Assert.Empty(secrets);
        }
    }
}
