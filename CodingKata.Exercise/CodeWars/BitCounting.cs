using System;
using System.Linq;

namespace CodingKata.Exercise.CodeWars.BitCounting
{

    public class Kata
    {
        public int CountBits(int n)
        {
            int count = 0;
            int remain = n;
            int maxBits = 0;

            while (Math.Pow(2, maxBits) <= n)
            {
                maxBits = maxBits + 1;
            }

            for (int i = maxBits; i >= 0; i--)
            {
                var exponent = (int)Math.Pow(2, i);
                if (exponent == remain)
                {
                    count++;
                    break;
                } 
                else if (exponent < remain)
                {
                    count++;
                    remain = remain - exponent;
                }
                else
                {
                    continue;
                }
            }

            return count;
        }
    }

    public class KataBestVote
    {
        public int CountBits(int n)
        {
            return Convert.ToString(n, 2).Count(x => x == '1');
        }
    }
}
