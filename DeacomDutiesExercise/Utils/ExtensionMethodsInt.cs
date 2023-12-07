namespace DeacomDutiesExercise.Utils
{
    public static class ExtensionMethodsInt
    {
        public static string GetReadableTime(this int seconds)
        {
            const int SECS_IN_HOUR = 3600;
            const int SIXTY = 60;

            int hours = (int)Math.Floor(Convert.ToDecimal(seconds / SECS_IN_HOUR));
            seconds = seconds - (hours * SECS_IN_HOUR);
            int minutes = (int)Math.Floor(Convert.ToDecimal(seconds / SIXTY));
            seconds = seconds - (minutes * SIXTY);

            string hoursStr = hours < 10 ? $"0{hours}" : hours.ToString();
            string minutesStr = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string secondsStr = seconds < 10 ? $"0{seconds}" : seconds.ToString();

            return $"{hoursStr}:{minutesStr}:{secondsStr}";
        }

        public static int Calculate_3OR5(this int val)
        {
            if (val <= 0) return 0;
            int result = 0;

            while (val > 0)
            {
                val--;
                if (val % 3 == 0 || val % 5 == 0)
                    result += val;
            }
            return result;
        }
    }
}
