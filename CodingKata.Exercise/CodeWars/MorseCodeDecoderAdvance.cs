using CodingKata.Exercise.CodeWars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.MorseCodeDecoderAdvance
{
    public interface IBinaryMorseCodeDecoder
    {
        string DecodeBits(string bits);
        string DecodeMorse(string morseCode);
    }

    public class Kata : IBinaryMorseCodeDecoder
    {
        public string DecodeBits(string bits)
        {
            string decodeBits = string.Empty;

            // Trim 0.  Note that some extra 0's may naturally occur at the beginning and the end of a message, make sure to ignore them
            string inputBits = bits.Trim('0');
            int lastPosition = inputBits.Length - 1;
            var firstState = '1';
            var state = firstState;
            int bitLenghtForTimeUnit = CaclulateTransmissionRate(inputBits);

            // "Dot" – is 1 time unit long.
            // "Dash" – is 3 time units long.
            // Pause between dots and dashes in a character – is 1 time unit long.
            // Pause between characters inside a word – is 3 time units long.
            // Pause between words – is 7 time units long.

            int cursor = 0;
            int timeUnitCount = 0;
            const string WordPause = " ";
            const string WordsPause = "   ";
            const string Dot = ".";
            const string Dash = "-";
            bool canNext = true;
            do
            {
                canNext = cursor <= lastPosition;
                // Same state
                if (canNext && inputBits[cursor] == state)
                {
                    timeUnitCount++;
                }

                if (canNext == false || inputBits[cursor] != state)
                {
                    string morse = string.Empty;
                    if (state == '1')
                    {
                        switch (timeUnitCount)
                        {
                            case 1:
                                morse = Dot;
                                break;
                            case 3:
                                morse = Dash;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (timeUnitCount)
                        {
                            case 3:
                                morse = WordPause;
                                break;
                            case 7:
                                morse = WordsPause;
                                break;
                            case 1:
                            default:
                                break;
                        }
                    }
                    decodeBits += morse;
                    // reset
                    if (cursor <= lastPosition)
                    {
                        timeUnitCount = 1;
                        state = inputBits[cursor];
                    }
                }


                cursor += bitLenghtForTimeUnit;

            } while (canNext);

            return decodeBits;
        }

        public int CaclulateTransmissionRate(string inputBits)
        {
            char firstState = inputBits[0];
            char state = firstState;
            int lastPosition = inputBits.Length - 1;
            List<int> sequenceBits = new List<int>();
            int bitLenghtForTimeUnit = 0;
            for (int i = 0; i <= lastPosition; i++)
            {
                if (inputBits[i] == state)
                {
                    bitLenghtForTimeUnit++;
                }

                if (i == lastPosition || inputBits[i] != state)
                {
                    if (bitLenghtForTimeUnit != 0)
                    {
                        sequenceBits.Add(bitLenghtForTimeUnit);
                    }
                    bitLenghtForTimeUnit = 1;
                    state = inputBits[i];
                }
            }

            return sequenceBits.Min();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="morseCode"></param>
        /// <see cref="MorseCodeDecoder.Kata"></see>
        /// <returns></returns>
        public string DecodeMorse(string morseCode)
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
                    if (letter != string.Empty || specials.TryGetValue(morseLetter, out letter))
                    {
                        textWord += letter;
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

    public class KataBestVote : IBinaryMorseCodeDecoder
    {
        public string DecodeBits(string bits)
        {
            var cleanedBits = bits.Trim('0');
            var rate = GetRate();
            return cleanedBits
              .Replace(GetDelimiter(7, "0"), "   ")
              .Replace(GetDelimiter(3, "0"), " ")
              .Replace(GetDelimiter(3, "1"), "-")
              .Replace(GetDelimiter(1, "1"), ".")
              .Replace(GetDelimiter(1, "0"), "");

            string GetDelimiter(int len, string c) => Enumerable.Range(0, len * rate).Aggregate("", (acc, _) => acc + c);
            int GetRate() => GetLengths("0").Union(GetLengths("1")).Min();
            IEnumerable<int> GetLengths(string del) => cleanedBits.Split(del, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Length);
        }

        public string DecodeMorse(string morseCode)
        {
            return morseCode
              .Split("   ")
              .Aggregate("", (res, word) => $"{res}{ConvertWord(word)} ")
              .Trim();

            string ConvertWord(string word) => word.Split(' ').Aggregate("", (wordRes, c) => wordRes + MorseCode.Get(c));
        }
    }
}

