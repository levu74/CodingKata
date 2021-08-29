using System;
using System.Collections.Generic;

namespace CodingKata.Exercise.CodeWars.MorseCodeDecoder
{

    public class Kata
    {
        public string Decode(string morseCode)
        {
            string[] morseCodeWords = morseCode.Trim().Split("   ");
            Dictionary<string, string> specials = new Dictionary<string, string>()
            {
                {"···−−−···", "SOS"}
            };

            string sentence = string.Empty;
            for (int i = 0; i < morseCodeWords.Length; i++)
            {
                var textWord = string.Empty;
                foreach (var morseLetter in morseCodeWords[i].Split(' '))
                {
                    string letter = MorseCode.Get(morseLetter);
                    if (letter != string.Empty || specials.TryGetValue(morseLetter,out letter))
                    {
                        textWord+= letter;
                    }
                }

                if (textWord != string.Empty)
                {
                    sentence += textWord + " ";
                }
            }

            return sentence.Trim();
        }
    }

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
