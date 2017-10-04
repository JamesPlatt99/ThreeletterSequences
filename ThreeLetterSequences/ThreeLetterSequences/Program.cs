using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ThreeLetterSequences
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = ReadFromFile();
            int freqTRA = RegExApproach(text, "tra");
            int freqTSL = RegExApproach(text, @"\w\w\w");
            Dictionary<string, int> results = getDictionary(text,@"\w\w\w");

            Console.WriteLine("The string 'tra' appears {0} times, there are a total of {1} TLSs!",freqTRA,freqTSL);
            Console.WriteLine("There are a total of {0} unique TSLs.",results.Count);
            Console.WriteLine("There are {0} unique TSLs that appear exactly {1} time(s).",getFreqFreq(results,freqTRA),freqTRA);
            Console.ReadLine();
        }

        static int getFreqFreq(Dictionary<string, int> results,int n)
        {
            int output =0;
            foreach(KeyValuePair<string, int> keyValuePair in results)
            {
                if(keyValuePair.Value == n)
                {
                    output++;
                }
            }
            return output;
        }
        
        static int RegExApproach(string text,string query)
        {
            int count;
            count = Regex.Matches(text.ToLower(), query).Count;
            return count;
        }
        

        static Dictionary<string, int> getDictionary(string text, string query)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();
            MatchCollection matches = Regex.Matches(text.ToLower(), query);           

            foreach (Match result in matches)
            {
                if (results.ContainsKey(result.Value))
                {
                    results[result.Value]++;                    
                }
                else
                {
                    results.Add(result.Value, 1);
                }
            }
            return results;
        }

        static int NaiveApproach(string text)
        {
            int count = 0;
            for(int i = 0; i < text.Length - 3; i++)
            {
                if (text.Substring(i, 3).ToLower() == "tra") {
                    count++;
                }
            }
            return count;
        }
        static string ReadFromFile()
        {
            string text;
            var fileStream = new FileStream("SampleText.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            return text;
        }
    }
}
