using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excercise1
{
    class WordAnalyzer
    {
        private readonly string _text;

        public WordAnalyzer(string text)
        {
            _text = text;
        }

        private IEnumerable<string> ExtractWords()
        {
            var matches = Regex.Matches(_text, @"([а-яА-Я]+)", RegexOptions.Multiline);
            return matches.Select(m => m.Value);
        }

        private IEnumerable<string> ExtractLowercaseWords()
        {
            return ExtractWords().Select(m => m.ToLowerInvariant());
        }

        private Dictionary<string, int> GetWordsOccurenceMap()
        {
            var occurenceMap = new Dictionary<string, int>();

            var words = ExtractLowercaseWords();
            foreach (var word in words)
            {
                if (!occurenceMap.ContainsKey(word))
                {
                    occurenceMap.Add(word, 1);
                } else
                {
                    occurenceMap[word] += 1;
                }
            }

            return occurenceMap;
        }

        public int GetNumberOfWords()
        {
            return ExtractWords().Count();
        }

        public string GetShortestWord()
        {
            var shortestWord = "";
            var shortestLength = int.MaxValue;

            var words = ExtractLowercaseWords();
            foreach (var word in words)
            {
                if (shortestLength > word.Length)
                {
                    shortestWord = word;
                    shortestLength = word.Length;
                }
            }

            return shortestWord;
        }

        public string GetLongestWord()
        {
            var longestWord = "";
            var longestLength = 0;

            var words = ExtractLowercaseWords();
            foreach (var word in words)
            {
                if (longestLength < word.Length)
                {
                    longestWord = word;
                    longestLength = word.Length;
                }
            }

            return longestWord;
        }

        public decimal GetAverageWordLength()
        {
            var totalWords = 0;
            var totalLength = 0;

            var words = ExtractLowercaseWords();
            foreach (var word in words)
            {
                totalWords += 1;
                totalLength += word.Length;
            }

            return totalLength / (decimal)totalWords;
        }

        public List<string> GetFiveMostCommonWords()
        {
            var occurenceMap = GetWordsOccurenceMap();
            return occurenceMap
                .OrderByDescending(kv => kv.Value)
                .Take(5)
                .Select(kv => kv.Key)
                .ToList();
        }

        public List<string> GetFiveLeastCommonWords()
        {
            var occurenceMap = GetWordsOccurenceMap();
            return occurenceMap
                .OrderBy(kv => kv.Value)
                .Take(5)
                .Select(kv => kv.Key)
                .ToList();
        }
    }
}
