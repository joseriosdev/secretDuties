using System.ComponentModel.DataAnnotations;

namespace DeacomDutiesExercise.Models
{
    public class PrimarySecret
    {
        [Required]
        public string Secret { get; set; }

        [Required]
        public string Encrypted { get; set; }

        [Required]
        public int LongestSubstring { get; set; }

        [Required]
        public int DuplicatesCount { get; set; }

        [Required]
        public bool AlmostPalindrome { get; set; }
    }
}
