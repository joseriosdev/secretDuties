using DeacomDutiesExercise.CoreLogic.Abstracts;
using DeacomDutiesExercise.Models;
using DeacomDutiesExercise.Models.DTOs;
using DeacomDutiesExercise.Models.Interfaces;
using DeacomDutiesExercise.Utils;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DeacomDutiesExercise.CoreLogic
{
    public class PrimarySecretImporter :
        SecretsImporter<PrimarySecretDTO, PrimarySecret>
    {
        public override List<PrimarySecret> MapSecretsToDBEntity(List<PrimarySecretDTO> secrets)
        {
            List<PrimarySecret> primarySecrets = new ();
            foreach (PrimarySecretDTO s in secrets)
                primarySecrets.Add(s.MapToPrimarySecretEntity());

            return primarySecrets;
        }

        public override void BulkInsertSecrets(List<PrimarySecret> secrets)
        {
            IConfiguration config = AppConfig.GetConfiguration();
            string? connectionString = config.GetConnectionString("SqlServer");
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("Connection String is null or empty");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "secreto";
                    DataTable table = new DataTable();
                    table.Columns.Add("secret", typeof(string));
                    table.Columns.Add("encrypted", typeof(string));
                    table.Columns.Add("longest_substring", typeof(int));
                    table.Columns.Add("duplicates_count", typeof(int));
                    table.Columns.Add("almost_palindrome", typeof(bool));

                    foreach (var s in secrets)
                    {
                        table.Rows.Add(
                            s.Secret,
                            s.Encrypted,
                            s.LongestSubstring,
                            s.DuplicatesCount,
                            s.AlmostPalindrome
                            );
                    }
                    bulkCopy.WriteToServer(table);
                }
                connection.Close();
            }
        }
    }
}
