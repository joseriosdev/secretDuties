namespace DeacomDutiesExercise.Utils
{
    public static class ExtensionMethodsString
    {
        private static Dictionary<char, int> _alphabet = new()
        {
            {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4},
            {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8},
            {'i', 9}, {'j', 10}, {'k', 11}, {'l', 12},
            {'m', 13}, {'n', 14}, {'o', 15}, {'p', 16},
            {'q', 17}, {'r', 18}, {'s', 19}, {'t', 20},
            {'u', 21}, {'v', 22}, {'w', 23}, {'x', 24},
            {'y', 25}, {'z', 26}
        };

        public static string AlphabetPosition(this string text)
        {
            string result = "";

            foreach (char c in text.ToLower())
            {
                if (_alphabet.ContainsKey(c))
                {
                    if (result == "")
                        result += _alphabet[c].ToString();
                    else
                        result += " " + _alphabet[c].ToString();
                }
            }
            return result;
        }

        public static string ToCamelCase(this string str)
        {
            List<char> result = new();
            bool shouldBeUpper = false;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '-' || str[i] == '_')
                {
                    shouldBeUpper = true;
                    continue;
                }
                else
                {
                    if (shouldBeUpper)
                    {
                        result.Add(str[i].ToString().ToUpper()[0]);
                        shouldBeUpper = false;
                    }
                    else
                    {
                        result.Add(str[i]);
                    }
                }
            }
            return new string(result.ToArray());
        }

        public static int DuplicateCount(this string str)
        {
            str = str.ToLower();
            int result = str
              .GroupBy(letter => letter)
              .Where(grupo => grupo.Count() > 1)
              .ToArray()
              .Length;
            return result;
        }

        public static int LengthOfLongestSubstring(this string s)
        {
            if (s.Length == 1) return 1;
            if (String.IsNullOrEmpty(s)) return 0;
            List<List<char>> substrings = new();
            int result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                substrings.Add(new List<char>());

                for (int j = i; j < s.Length; j++)
                {
                    if (!substrings[i].Contains(s[j]))
                        substrings[i].Add(s[j]);
                    else
                        break;
                }
                if (result < substrings[i].Count)
                    result = substrings[i].Count;
            }

            return result;
        }

        public static string Uncensor(this string txt, string vowels)
        {
            int vowelsCounter = 0;
            var newTxt = new char[txt.Length];

            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == '*')
                {
                    newTxt[i] = vowels[vowelsCounter];
                    vowelsCounter++;
                }
                else
                {
                    newTxt[i] = txt[i];
                }
            }
            return new string(newTxt);
        }

        public static bool AlmostPalindrome(this string str)
        {
            str = str.Trim().ToLower();
            int counter = 0;

            for (int i = 0, j = str.Length - 1; i < str.Length / 2; i++, j--)
            {
                if (str[i] != str[j])
                    counter++;
            }
            return counter == 1;
        }
    }
}
