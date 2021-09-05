using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.StripComments
{
    public interface IStripCommentsKata
    {
        string StripComments(string text, string[] commentSymbols);
    }

    public class Kata : IStripCommentsKata
    {
        public string StripComments(string text, string[] commentSymbols)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var regex = new Regex($"(?m) *?([{Regex.Escape(string.Join("", commentSymbols))}].*)?$");
            return regex.Replace(text, string.Empty);

        }

    }

    public class KataBestVote : IStripCommentsKata
    {
        public string StripComments(string text, string[] commentSymbols)
        {
            string[] lines = text.Split(new[] { "\n" }, StringSplitOptions.None);
            lines = lines.Select(x => x.Split(commentSymbols, StringSplitOptions.None).First().TrimEnd()).ToArray();
            return string.Join("\n", lines);
        }
    }
}
