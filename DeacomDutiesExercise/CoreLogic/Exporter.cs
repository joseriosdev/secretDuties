using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Microsoft.Extensions.Configuration;
using DeacomDutiesExercise.Utils;

namespace DeacomDutiesExercise.CoreLogic
{
    public static class Exporter
    {
        private static LogBook _log = new();

        public static void ToXML()
        {
            IConfiguration config = AppConfig.GetConfiguration();
            string? connectionString = config.GetConnectionString("SqlServer");
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("Connection String is null or empty");

            string secretQuery = "SELECT * FROM secreto";
            string secret2Query = "SELECT * FROM secreto2";

            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(secretQuery, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataSet, "Secret");

                    command.CommandText = secret2Query;
                    adapter.Fill(dataSet, "Secret2");
                }
                catch (Exception ex)
                {
                    _log.AddError("Something went wrong during query execution", ex);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong");
                    Console.ResetColor();
                }
            }

            if (dataSet.Tables.Contains("Secret") && dataSet.Tables.Contains("Secret2"))
            {
                _log.AddInfo("Creating new XML document");
                DataTable primarySecretTable = dataSet.Tables["Secret"]!;
                DataTable secondarySecretTable = dataSet.Tables["Secret2"]!;

                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("information");
                xmlDoc.AppendChild(root);

                foreach (DataRow secretRow in primarySecretTable!.Rows)
                {
                    XmlElement secretElement = xmlDoc.CreateElement("secret");

                    string id = secretRow["secret"].ToString()!;
                    int longestSubstring = Convert.ToInt32(secretRow["longest_substring"]);
                    int duplicatesCount = Convert.ToInt32(secretRow["duplicates_count"]);
                    bool almostPalindrome = Convert.ToBoolean(secretRow["almost_palindrome"]);
                    string encrypted = secretRow["encrypted"].ToString()!;

                    XmlAttribute idAttribute = xmlDoc.CreateAttribute("id");
                    idAttribute.Value = id;
                    secretElement.Attributes.Append(idAttribute);

                    XmlElement longestSubstringElement = xmlDoc.CreateElement("longest_substring");
                    longestSubstringElement.InnerText = longestSubstring.ToString();
                    secretElement.AppendChild(longestSubstringElement);

                    XmlElement duplicatesCountElement = xmlDoc.CreateElement("duplicates_count");
                    duplicatesCountElement.InnerText = duplicatesCount.ToString();
                    secretElement.AppendChild(duplicatesCountElement);

                    XmlElement almostPalindromeElement = xmlDoc.CreateElement("almost_palindrome");
                    almostPalindromeElement.InnerText = almostPalindrome.ToString();
                    secretElement.AppendChild(almostPalindromeElement);

                    XmlElement encryptedElement = xmlDoc.CreateElement("encrypted");
                    encryptedElement.InnerText = encrypted!;
                    secretElement.AppendChild(encryptedElement);

                    XmlElement namesElement = xmlDoc.CreateElement("names");

                    DataRow[] relatedRows = secondarySecretTable!.Select($"[secret] = '{id}'");

                    foreach (DataRow relatedRow in relatedRows)
                    {
                        XmlElement nameElement = xmlDoc.CreateElement("name");
                        string nameId = relatedRow["name"].ToString()!;

                        XmlAttribute nameIdAttribute = xmlDoc.CreateAttribute("id");
                        nameIdAttribute.Value = nameId;
                        nameElement.Attributes.Append(nameIdAttribute);

                        string time = relatedRow["time"].ToString()!;
                        int calculated = Convert.ToInt32(relatedRow["calculated"]);

                        XmlElement timeElement = xmlDoc.CreateElement("time");
                        timeElement.InnerText = time;
                        nameElement.AppendChild(timeElement);

                        XmlElement calculatedElement = xmlDoc.CreateElement("calculated_m35");
                        calculatedElement.InnerText = calculated.ToString();
                        nameElement.AppendChild(calculatedElement);

                        namesElement.AppendChild(nameElement);
                    }

                    secretElement.AppendChild(namesElement);
                    root.AppendChild(secretElement);
                }

                string xmlFilePath =
                    $"{config.GetSection("FilePaths:Export").Value}{DateTime.Now.ToString("yyyyMMddHHmmss")}Export.xml";
                xmlDoc.Save(xmlFilePath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data exported to XML successfully at: " + xmlFilePath);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Double check the existence of the Tables in the DB.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
