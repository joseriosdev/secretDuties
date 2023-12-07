using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeacomDutiesExercise
{
    public class AppConfig
    {
        public static IConfiguration GetConfiguration()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();
            return config;
        }
    }
}
