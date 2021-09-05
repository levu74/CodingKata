using CodingKata.Exercise.CodeWars.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingKata.Exercise.CodeWars.MorseCodeDecoderRealLife
{
    public interface IMorseCodeDecoderRealLife
    {
        string DecodeBitsAdvanced(string bits);
        string DecodeMorse(string morseCode);
    }
    public class Kata : IMorseCodeDecoderRealLife
    {
        const string ShortPause = "";
        const string LetterPause = " ";
        const string WordPause = "   ";
        const string Dot = ".";
        const string Dash = "-";
        const char Zero = '0';
        const char One = '1';

        public string DecodeBitsAdvanced(string bits)
        {
            StringBuilder decodedMorseFromBits = new StringBuilder();

            // Trim 0 at start and end bits chain
            string inputBits = bits.Trim(Zero);

            if (inputBits.Length == 0)
            {
                return string.Empty;
            }

            if (inputBits.IndexOf(Zero) == -1)
            {
                return Dot;
            }

            // Convert to number list: 1110011111 => 3[1],2[0],5[1]
            CustomList numberProjecion = ToWeightableProjection(inputBits);

            // Clustering
            var clusteredLabels = new KMeansClustering(numberProjecion.Distinct());

            // Decode
            foreach (var currentValue in numberProjecion)
            {
                string morse = clusteredLabels.GetLabel(currentValue); ;
                decodedMorseFromBits.Append(morse);
            }


            return decodedMorseFromBits.ToString();
        }


        public string DecodeMorse(string morseCode)
        {
            if (string.IsNullOrEmpty(morseCode))
                return string.Empty;
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
                    string letter = Preloaded.MORSE_CODE[morseLetter];
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

        /// <summary>
        /// Convert bit elements to number. e.g. 110110100111 -> 2,-1,2,-2,3
        /// </summary>
        /// <param name="inputBits"></param>
        /// <returns></returns>
        private static CustomList ToWeightableProjection(string inputBits)
        {
            CustomList numberProjecion = new CustomList();
            char state = '1';
            int number = 0;

            for (int i = 0; i < inputBits.Length; i++)
            {
                int coutingValue = 1;
                if (state == inputBits[i])
                {
                    number += coutingValue;
                }
                else
                {
                    numberProjecion.Add(new MorseBits(state, number));
                    // reset
                    state = inputBits[i];
                    number = coutingValue;
                }

                if (i == inputBits.Length - 1)
                {
                    numberProjecion.Add(new MorseBits(state, number));
                }
            }

            return numberProjecion;
        }

        interface IWeightable
        {
            int Weight { get; }
        }

        struct MorseBits : IWeightable, IEquatable<MorseBits>
        {
            public int Weight { get; }
            public int Bit { get; }

            public char State { get; }

            public MorseBits(char state, int weight)
            {
                this.State = state;
                this.Bit = Convert.ToInt32(state.ToString());
                this.Weight = weight;
            }

            public bool Equals(MorseBits other)
            {
                return this.Weight == other.Weight && this.Bit == other.Bit;
            }

            public override bool Equals(object obj)
            {
                if (obj is MorseBits)
                {
                    return Equals((MorseBits)obj);
                }

                return false;
            }

            public override int GetHashCode()
            {
                return this.State.GetHashCode() + this.Weight.GetHashCode();
            }

            public override string ToString()
            {
                return $"{{{this.State},{this.Weight}}}";
            }
        }

        class CustomList : ICollection<MorseBits>
        {
            private readonly List<MorseBits> list = new List<MorseBits>();


            #region ICollection
            public int Count => list.Count;

            public bool IsReadOnly => true;

            public void Add(MorseBits item)
            {
                list.Add(item);
            }

            public void Clear()
            {
                list.Clear();
            }

            public bool Contains(MorseBits item)
            {
                return list.Contains(item);
            }

            public void CopyTo(MorseBits[] array, int arrayIndex)
            {
                list.CopyTo(array, arrayIndex);
            }

            public IEnumerator<MorseBits> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            public bool Remove(MorseBits item)
            {
                return list.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return list.GetEnumerator();
            }
            #endregion

            public override string ToString()
            {
                return list.Select(m => m.ToString()).Aggregate((m, n) => $"{m}-{n}");
            }
        }


        class KMeansClustering
        {
            private readonly IEnumerable<MorseBits> samples;
            private Dictionary<MorseBits, string> labels = new Dictionary<MorseBits, string>();
            private MorseBitsCentroid[] Centroids { get; set; }

            public KMeansClustering(IEnumerable<MorseBits> samples)
            {
                this.samples = samples;
                DoWork();
            }

            private void DoWork()
            {
                IInitCentroidsStratery centroidsInitStrategy = new InitCentroidsByMiniumAggregation();
                IUpdateCentroidsStrategy centroidsUpdateStrategy = new UpdateCentroidsByBaseOnFirstCentroid();
                // Set centroids
                Centroids = centroidsInitStrategy.Initialize(samples);

                do
                {
                    foreach (MorseBitsCentroid centroid in Centroids)
                    {
                        centroid.ClusterItems.Clear();
                    }
                    foreach (MorseBits sample in samples)
                    {
                        MorseBitsCentroid nearest = Centroids[0];
                        double currentDistance = nearest.CalculateDistance(sample);
                        for (int i = 1; i < Centroids.Length; i++)
                        {
                            double distance = Centroids[i].CalculateDistance(sample);
                            if (distance < currentDistance)
                            {
                                currentDistance = distance;
                                nearest = Centroids[i];
                            }
                        }

                        nearest.ClusterItems.Add(sample);
                    }

                    // Re-calculate centroids
                    centroidsUpdateStrategy.Update(Centroids);

                }
                while (Centroids.Any(centroid => !centroid.IsSameWithPrevious()));
            }

            public string GetLabel(MorseBits morseBits)
            {
                foreach (MorseBitsCentroid centroid in Centroids)
                {
                    if (centroid.Contains(morseBits))
                    {
                        return morseBits.State == One ? centroid.LabelOne : centroid.LabelZero;
                    }
                }

                throw new ArgumentException("Sample not found");
            }
        }

        interface IInitCentroidsStratery
        {
            MorseBitsCentroid[] Initialize(IEnumerable<MorseBits> samples);
        }

        interface IUpdateCentroidsStrategy
        {
            void Update(MorseBitsCentroid[] centroids);
        }

        class InitCentroidsByMiniumAggregation : IInitCentroidsStratery
        {
            public MorseBitsCentroid[] Initialize(IEnumerable<MorseBits> samples)
            {
                double min1 = samples.Select(x => x.Weight).Min();
                double min = samples.Where(x => x.Weight < min1 * 3).Average(x => x.Weight);
                
                // For short data
                if (samples.Count() <= 2)
                {
                    min = min1 / 2;
                }

                // centroids
                return new MorseBitsCentroid[]
                {
                    new MorseBitsCentroid(min, Dot, ShortPause),
                    new MorseBitsCentroid(min * 3, Dash, LetterPause),
                    new MorseBitsCentroid(min * 7, Dash, WordPause)
                };
            }
        }

        class UpdateCentroidsByDefault : IUpdateCentroidsStrategy
        {
            public void Update(MorseBitsCentroid[] centroids)
            {
                foreach (var centroid in centroids)
                {
                    // We don't change state of centroid
                    centroid.SetValue(centroid.GetWeightMean());
                }
            }
        }

        class UpdateCentroidsByBaseOnFirstCentroid : IUpdateCentroidsStrategy
        {
            public void Update(MorseBitsCentroid[] centroids)
            {
                centroids[0].SetValue(centroids[0].GetWeightMean());
                centroids[1].SetValue((centroids[1].GetWeightMean() + centroids[0].Current * 3) / 2);
                centroids[2].SetValue((centroids[2].GetWeightMean() + centroids[0].Current * 7) / 2);
            }
        }

        class MorseBitsCentroid
        {
            private double MiddleBitPoint { get; }

            public double Current { get; private set; }
            public string LabelOne { get; }
            public string LabelZero { get; }
            public double Previous { get; private set; }
            public List<MorseBits> ClusterItems { get; private set; } = new List<MorseBits>();
            public MorseBitsCentroid(double value, string labelOne, string labelZero)
            {
                Current = value;
                LabelOne = labelOne;
                LabelZero = labelZero;
                MiddleBitPoint = 0.5;
            }

            public void SetValue(double newValue)
            {
                Previous = Current;
                Current = newValue;
            }

            public bool IsSameWithPrevious()
            {
                return Current.Equals(Previous);
            }

            public double GetWeightMean()
            {
                if (ClusterItems.Count == 0)
                {
                    return Current;
                }

                return ClusterItems.Average(x => x.Weight);
            }

            public double CalculateDistance(MorseBits sample)
            {
                return Math.Sqrt(Math.Pow(MiddleBitPoint - sample.Bit, 2) + Math.Pow(Current - sample.Weight, 2));
            }

            public bool Contains(MorseBits sample)
            {
                return ClusterItems.Contains(sample);
            }

            public override string ToString()
            {
                return $"{{{MiddleBitPoint},{Current}}} - [{string.Join(", ", ClusterItems.Select(x => x.ToString()))}]";
            }
        }

    }

    /// <summary>
    /// Author: https://www.codewars.com/users/jacobmott
    /// </summary>
    public class KataStandardDeviation : IMorseCodeDecoderRealLife
    {
        public string DecodeBitsAdvanced(string bits)
        {
            // Back out if string has no content. 
            if (bits.Contains('1') == false) return "";

            // Removes 0s from front and back. 
            bits = bits.Trim('0');

            // Create char array of string characters.  
            char[] Characters = new char[bits.Length];
            for (int i = 0; i < bits.Length; i++) { Characters[i] = bits[i]; }

            // Create int array containg sequential counts of 0s and 1s.
            // Create string array containing partitioned 0s and 1s. 
            int[] CountArray = new int[bits.Length];
            string[] BitArray = new string[bits.Length];
            string bitString = "";
            int ArrayCounter = 0;
            char Check = '1';
            int counter = 0;
            foreach (char c in Characters)
            {
                if (c != Check)
                {
                    BitArray[ArrayCounter] = bitString;
                    CountArray[ArrayCounter] = counter;
                    ArrayCounter++;
                    if (Check == '1') Check = '0';
                    else Check = '1';
                    counter = 1;
                    bitString = "";
                }
                else
                {
                    counter++;
                }
                bitString = bitString + c;
            }
            CountArray[ArrayCounter] = counter;
            BitArray[ArrayCounter] = bitString;
            ArrayCounter++;
            Array.Resize(ref CountArray, ArrayCounter);
            Array.Resize(ref BitArray, ArrayCounter);

            // Calculate sum, average, and standard deviation of transmission data. 
            int sum = CountArray.Sum();
            double average = CountArray.Average();
            double sumOfSquaresOfDifferences = CountArray.Select(val => (val - average) * (val - average)).Sum();
            double StandardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / CountArray.Length);

            // If large text, round average to prevent too much deviation. 
            if (BitArray.Length > 500)
            {
                average = Math.Ceiling(average);
            }

            // Parse bits for morse symbols.
            StringBuilder TranslationOne = new StringBuilder();
            foreach (string str in BitArray)
            {
                if (str.Length <= average && str.Contains('1'))
                    TranslationOne.Append(".");
                else if (str.Length > average && str.Contains('1'))
                    TranslationOne.Append("-");
                else if ((str.Length <= average && str.Contains('0')))
                    TranslationOne.Append("");
                else if (str.Length >= (average + (StandardDeviation * 2)) && str.Contains('0'))
                    TranslationOne.Append("   ");
                else if (str.Contains('0') && BitArray.Length <= 3 && str.Length > 5)
                    TranslationOne.Append("   ");
                else if (str.Length > average && str.Contains('0'))
                    TranslationOne.Append(" ");
            }

            // Return morse code string. 
            return TranslationOne.ToString();
        }

        public string DecodeMorse(string morseCode)
        {
            // Return blank string if morse code is empty. 
            if (morseCode == "") return "";

            // Use preloaded dictionary to parse morse code for translation. 
            StringBuilder sentence = new StringBuilder();
            string[] words = morseCode.Split("   ");
            foreach (string word in words)
            {
                string[] letters = word.Split(" ");
                foreach (string letter in letters)
                {
                    sentence.Append(Preloaded.MORSE_CODE[letter]);
                }
                sentence.Append(" ");
            }

            // Return completed sentences. 
            return sentence.ToString().Trim(' ');
        }
    }

    public static class Preloaded
    {
        public static readonly MorseCodeMap MORSE_CODE = new MorseCodeMap();

        public class MorseCodeMap
        {
            public string this[string morseCode]
            {
                get { return MorseCode.Get(morseCode); }
            }
        }
    }
}
