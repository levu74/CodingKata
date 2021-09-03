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
                return $"{this.State}|{this.Weight}";
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
                return list.Select(m => m.ToString()).Aggregate((m,n) => $"{m}-{n}" );
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
                var min = samples.Select(x=> x.Weight).Min();
                // Set centroids
                Centroids = new MorseBitsCentroid[]
                {
                    new MorseBitsCentroid(new MorseBits(One, min), Dot),
                    new MorseBitsCentroid(new MorseBits(One, min * 3), Dash),
                    new MorseBitsCentroid(new MorseBits(Zero, min), ShortPause),
                    new MorseBitsCentroid(new MorseBits(Zero, min * 3), LetterPause),
                    new MorseBitsCentroid(new MorseBits(Zero, min * 7), WordPause)
                };

                Centroids[2].Dependence = Centroids[0];
                Centroids[3].Dependence = Centroids[1];

                do
                {
                    foreach (MorseBitsCentroid centroid in Centroids)
                    {
                        centroid.ClusterItems.Clear();
                    }
                    foreach (MorseBits sample in samples)
                    {
                        var centroids = Centroids.Where(c => c.Current.Bit == sample.Bit).ToList();
                        MorseBitsCentroid nearest = centroids[0];
                        double currentDistance = nearest.CalculateDistance(sample);
                        for (int i = 1; i < centroids.Count; i++)
                        {
                            double distance = centroids[i].CalculateDistance(sample);
                            if (distance < currentDistance)
                            {
                                currentDistance = distance;
                                nearest = centroids[i];
                            }
                        }

                        nearest.ClusterItems.Add(sample);
                    }

                    // Re-calculate centroids
                    foreach (var centroid in Centroids)
                    {
                        // We don't change state of centroid
                        centroid.SetValue(new MorseBits(centroid.Current.State, centroid.GetWeightMean()));
                    }
                }
                while (Centroids.Any(centroid => !centroid.IsSameWithPrevious()));
            }

            public string GetLabel(MorseBits morseBits)
            {
                foreach (MorseBitsCentroid centroid in Centroids)
                {
                    if (centroid.Contains(morseBits))
                    {
                        return centroid.Label;
                    }
                }

                throw new ArgumentException("Sample not found");
            }
        }

        class MorseBitsCentroid
        {
            public MorseBits Current { get; private set; }
            public string Label { get; }
            public MorseBits Previous { get; private set; }
            public List<MorseBits> ClusterItems { get; private set; } = new List<MorseBits>();
            public MorseBitsCentroid Dependence { get; set; }
            public MorseBitsCentroid(MorseBits morseBit, string label)
            {
                Current = morseBit;
                Label = label;
            }

            public void SetValue(MorseBits newValue)
            {
                if (Dependence != null && newValue.Weight > Dependence.Current.Weight)
                {
                    newValue = new MorseBits(newValue.State, Dependence.Current.Weight);
                }

                Previous = Current;
                Current = newValue;
            }

            public bool IsSameWithPrevious()
            {
                return Current.Equals(Previous);
            }

            public int GetWeightMean()
            {
                if (ClusterItems.Count == 0)
                {
                    return this.Current.Weight;
                }

                return (int)Math.Round(ClusterItems.Average(x => x.Weight), 0);
            }

            public double CalculateDistance(MorseBits sample)
            {
                return Math.Sqrt(Math.Pow(Current.Bit - sample.Bit, 2)  + Math.Pow(Current.Weight - sample.Weight, 2)); 
            }

            public bool Contains(MorseBits sample)
            {
                return ClusterItems.Contains(sample);
            }
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
