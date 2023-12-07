using DeacomDutiesExercise.CoreLogic.Abstracts;
using DeacomDutiesExercise.Models.DTOs;
using DeacomDutiesExercise.Models;
using System.Data.SqlClient;
using System.Data;
using DeacomDutiesExercise.Utils;
using Microsoft.Extensions.Configuration;

namespace DeacomDutiesExercise.CoreLogic
{
    public class SecondarySecretImporter :
        SecretsImporter<SecondarySecretDTO, SecondarySecret>
    {
        public override List<SecondarySecret> MapSecretsToDBEntity(List<SecondarySecretDTO> secrets)
        {
            List<SecondarySecret> secondarySecrets = new();
            foreach (SecondarySecretDTO s in secrets)
                secondarySecrets.Add(s.MapToSecondarySecretEntity());

            return secondarySecrets;
        }

        public override void BulkInsertSecrets(List<SecondarySecret> secrets)
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
                    bulkCopy.DestinationTableName = "secreto2";
                    DataTable table = new DataTable();
                    table.Columns.Add("name", typeof(string));
                    table.Columns.Add("calculated", typeof(int));
                    table.Columns.Add("time", typeof(string));
                    table.Columns.Add("secret", typeof(string));

                    foreach (var s in secrets)
                    {
                        table.Rows.Add(
                            s.Name,
                            s.Calculated_m35,
                            s.Time,
                            s.Secret
                            );
                    }
                    bulkCopy.WriteToServer(table);
                }
                connection.Close();
            }
        }
    }
}
