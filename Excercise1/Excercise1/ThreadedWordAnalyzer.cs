using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise1
{
    class ThreadedWordAnalyzer : WordAnalyzer
    {
        public int NumberOfWords { get; set; }
        public string ShortestWord { get; set; }
        public string LongestWord { get; set; }
        public decimal AverageWordLength { get; set; }
        public List<string> FiveMostCommonWords { get; set; }
        public List<string> FiveLeastCommonWords { get; set; }

        public ThreadedWordAnalyzer(string file): base(file)
        {
            NumberOfWords = 0;
            ShortestWord = "";
            LongestWord = "";
            AverageWordLength = 0;
            FiveMostCommonWords = new List<string>();
            FiveLeastCommonWords = new List<string>();
        }

        public void ComputeNumberOfWords()
        {
            NumberOfWords = GetNumberOfWords();
        }

        public void ComputeShortestWord()
        {
            ShortestWord = GetShortestWord();
        }

        public void ComputeLongestWord()
        {
            LongestWord = GetLongestWord();
        }

        public void ComputeAverageWordLength()
        {
            AverageWordLength = GetAverageWordLength();
        }

        public void ComputeFiveMostCommonWords()
        {
            FiveMostCommonWords = GetFiveMostCommonWords();
        }

        public void ComputeFiveLeastCommonWords()
        {
            FiveLeastCommonWords = GetFiveLeastCommonWords();
        }
    }
}
