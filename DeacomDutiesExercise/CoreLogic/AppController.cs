using DeacomDutiesExercise.Models.DTOs;
using DeacomDutiesExercise.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DeacomDutiesExercise.CoreLogic
{
    public static class AppController
    {
        private static LogBook _log = new ();

        public static void Run()
        {
            string[] validValues = new string[] { "1", "2", "3" };
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" _____________");
            Console.WriteLine("| Deacom Task |");
            Console.WriteLine(" -------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("** what do you want to do **");
            Console.WriteLine("[1] Import a .csv file");
            Console.WriteLine("[2] Export Database to .xml");
            Console.WriteLine("[3] Exit");
            Console.ResetColor();
            string? input = Console.ReadLine();
            InputValidator.ValidateInput(ref input, validValues);

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
            string[] validValues = new string[] { "1", "2", "3", "4" };
            string? input;
            bool stayOnCurrentMenu = true;
            
            while (stayOnCurrentMenu)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Please, select an option");
                Console.WriteLine("[1] Import PRIMARY Secret File");
                Console.WriteLine("[2] Import SECONDARY Secret File");
                Console.WriteLine("[3] Show sample");
                Console.WriteLine("[4] Back to main menu");
                input = Console.ReadLine();
                InputValidator.ValidateInput(ref input, validValues);

                switch (input)
                {
                    case "1": ImportPrimary();  break;
                    case "2": ImportSecondary(); break;
                    case "3": PrintSample(); break;
                    case "4":
                        stayOnCurrentMenu = false;
                        break;
                }
                Console.ResetColor();
            }

            void ImportPrimary()
            {
                var (path, fileName) = InputValidator.ValidateImportPathFileName();
                var csvCtrl = new CSVHandler<PrimarySecretDTO>(path);
                var importer = new PrimarySecretImporter();
                var dtoArr = csvCtrl.ReadCsvFile(fileName);
                var entityArr = importer.MapSecretsToDBEntity(dtoArr);
                importer.BulkInsertSecrets(entityArr);
            }

            void ImportSecondary()
            {
                var (path, fileName) = InputValidator.ValidateImportPathFileName();
                var csvCtrl = new CSVHandler<SecondarySecretDTO>(path);
                var importer = new SecondarySecretImporter();
                var dtoArr = csvCtrl.ReadCsvFile(fileName);
                var entityArr = importer.MapSecretsToDBEntity(dtoArr);
                importer.BulkInsertSecrets(entityArr);
            }

            void PrintSample()
            {
                Console.WriteLine(">>>> Primary");
                Console.WriteLine("> secret1: *w*s*m*");
                Console.WriteLine("> secret2: aeoe");
                Console.WriteLine("");
                Console.WriteLine(">>>> Secondary");
                Console.WriteLine("> secret: bog");
                Console.WriteLine("> name: the-bog");
                Console.WriteLine("> m35: 3");
                Console.WriteLine("> time: 3004");
                Console.WriteLine("");
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
    }
}
