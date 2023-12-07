using System.ComponentModel.DataAnnotations;

namespace DeacomDutiesExercise.Models
{
    public record SecondarySecret 
    {
        [Required]
        public string Secret { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Calculated_m35 { get; set; }

        [Required]
        public string Time { get; set; }
    }
}
