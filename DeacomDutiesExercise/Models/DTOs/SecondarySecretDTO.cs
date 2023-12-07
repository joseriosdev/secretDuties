using CsvHelper.Configuration.Attributes;
using DeacomDutiesExercise.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DeacomDutiesExercise.Models.DTOs
{
    public class SecondarySecretDTO : ISecretDTO
    {
        [Required]
        [Name("secret")]
        public string Secret { get; set; }

        [Required]
        [Name("name")]
        public string Name { get; set; }

        [Required]
        [Name("m35")]
        public int Calculated_m35 { get; set; }

        [Required]
        [Name("time")]
        public int Time { get; set; }
    }
}
