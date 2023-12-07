using CsvHelper.Configuration.Attributes;
using DeacomDutiesExercise.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DeacomDutiesExercise.Models.DTOs
{
    public class PrimarySecretDTO : ISecretDTO
    {
        [Required]
        [Name("secret1")]
        public string Secret { get; set; }

        [Required]
        [Name("secret2")]
        public string Secret2 { get; set; }
    }
}
