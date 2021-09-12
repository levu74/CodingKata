using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.RangeExtraction
{
    public interface IRangeExtraction
    {
        string Extract(int[] args);
    }

    public class Kata : IRangeExtraction
    {
        public string Extract(int[] args)
        {
            const char RangeNotation = '-';
            const char NumberDelimeter = ',';
            int[] numbers = args;
            if (numbers == null || numbers.Length == 0)
                return string.Empty;

            if (numbers.Length == 1)
                return args[0].ToString();
            if (numbers.Length == 2) 
                return string.Join(NumberDelimeter, numbers);
            StringBuilder output = new StringBuilder(numbers[0].ToString());
            int counter = 1;
            int previousNumber = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                int currentNumber = numbers[i];
                if (currentNumber == previousNumber + 1)
                {
                    counter++;
                }
                else if(i != numbers.Length - 1)
                {
                    if (counter > 2)
                    {
                        output.Append(RangeNotation);
                        output.Append(previousNumber);
                    }
                    else if (counter > 1)
                    {
                        output.Append(NumberDelimeter);
                        output.Append(previousNumber);
                    }
                    output.Append(NumberDelimeter);
                    output.Append(currentNumber);
                    counter = 1;
                }

                if (i == numbers.Length - 1)
                {
                    if (counter > 2)
                    {
                        if (currentNumber != previousNumber + 1)
                        {
                            output.Append(RangeNotation).Append(previousNumber).Append(NumberDelimeter).Append(currentNumber);
                        } 
                        else
                        {
                            output.Append(RangeNotation);
                            output.Append(currentNumber);
                        }
                    }
                    else 
                    {
                        if (counter == 2 && currentNumber != previousNumber + 1)
                        {
                            output.Append(NumberDelimeter);
                            output.Append(previousNumber);
                        }
                        output.Append(NumberDelimeter);
                        output.Append(currentNumber);  
                    }
                }

                previousNumber = currentNumber;
            }

            return output.ToString();
        }
    }

    public class KataBestVote : IRangeExtraction
    {
        public int Value, Count;
        public int NextValue => Value + Count;
        public KataBestVote()
        {

        }

        public KataBestVote(int value)
        {
            Value = value;
            Count = 1;
        }

        public override string ToString()
            => Count == 1 ? $"{Value}" :
               Count == 2 ? $"{Value},{Value + 1}" :
                            $"{Value}-{NextValue - 1}";

        public string Extract(int[] args)
        {
            var list = new List<KataBestVote>();

            foreach (var n in args)
                if (list.LastOrDefault()?.NextValue == n) list.Last().Count++;
                else list.Add(new KataBestVote(n));

            return string.Join(",", list);
        }
    }
}
