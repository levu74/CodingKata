using CodingKata.Exercise.CodeWars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingKata.Exercise.CodeWars.MorseCodeDecoder
{

    public class Kata
    {
        public string Decode(string morseCode)
        {
            string[] morseCodeWords = morseCode.Trim().Split("   ");
            Dictionary<string, string> specials = new Dictionary<string, string>()
            {
                {"...−−−...", "SOS"}
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

    public class KataBestVote
    {
        public string Decode(string morseCode)
        {
            var words = morseCode.Trim().Split(new[] { "   " }, StringSplitOptions.None);
            var translatedWords = words.Select(word => word.Split(' ')).Select(letters => string.Join("", letters.Select(MorseCode.Get))).ToList();
            return string.Join(" ", translatedWords);
        }
    }
}
