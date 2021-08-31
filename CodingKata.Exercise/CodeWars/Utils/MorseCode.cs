using System.Collections.Generic;

namespace CodingKata.Exercise.CodeWars.Utils
{
    public class MorseCode
    {
        private static Dictionary<char, string> _textToMorse = new Dictionary<char, string>()
        {
            {' ', "/"},
            {'A', ".-"},
            {'B', "-..."},
            {'C', "-.-."},
            {'D', "-.."},
            {'E', "."},
            {'F', "..-."},
            {'G', "--."},
            {'H', "...."},
            {'I', ".."},
            {'J', ".---"},
            {'K', "-.-"},
            {'L', ".-.."},
            {'M', "--"},
            {'N', "-."},
            {'O', "---"},
            {'P', ".--."},
            {'Q', "--.-"},
            {'R', ".-."},
            {'S', "..."},
            {'T', "-"},
            {'U', "..-"},
            {'V', "...-"},
            {'W', ".--"},
            {'X', "-..-"},
            {'Y', "-.--"},
            {'Z', "--.."},
            {'1', ".----"},
            {'2', "..---"},
            {'3', "...--"},
            {'4', "....-"},
            {'5', "....."},
            {'6', "-...."},
            {'7', "--..."},
            {'8', "---.."},
            {'9', "----."},
            {'0', "-----"},
            {'.', ".-.-.-" }
        };

        public static string Get(string morseCode)
        {
            foreach (var kvp in _textToMorse)
            {
                if (kvp.Value == morseCode)
                    return kvp.Key.ToString();
            }

            return string.Empty;
        }
    }
}
