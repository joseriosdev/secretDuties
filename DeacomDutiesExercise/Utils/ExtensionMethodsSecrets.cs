using DeacomDutiesExercise.Models;
using DeacomDutiesExercise.Models.DTOs;

namespace DeacomDutiesExercise.Utils
{
    public static class ExtensionMethodsSecrets
    {
        public static PrimarySecret MapToPrimarySecretEntity(this PrimarySecretDTO ps)
        {
            string uncensored = ps.Secret.Uncensor(ps.Secret2);
            return new PrimarySecret()
            {
                Secret = uncensored,
                Encrypted = uncensored.AlphabetPosition(),
                LongestSubstring = uncensored.LengthOfLongestSubstring(),
                DuplicatesCount = uncensored.DuplicateCount(),
                AlmostPalindrome = uncensored.AlmostPalindrome()
            };
        }

        public static SecondarySecret MapToSecondarySecretEntity(this SecondarySecretDTO ss)
        {
            return new SecondarySecret
            {
                Secret = ss.Secret,
                Name = ss.Name.ToCamelCase(),
                Calculated_m35 = ss.Calculated_m35.Calculate_3OR5(),
                Time = ss.Time.GetReadableTime()
            };
        }
    }
}
