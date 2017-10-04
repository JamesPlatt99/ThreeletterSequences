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
            String query = @"\w(?=\w\w)";
            string text = ReadFromFile();
            text = text.ToLower();
            //string text = "tra ttratra";
            int freqTRA = RegExApproach(text, "tra");
            int freqTSL = RegExApproach(text, query);
            Dictionary<string, int> results = getDictionary(text,query);

            Console.WriteLine("The string 'tra' appears {0} times, there are a total of {1} TLSs!",freqTRA,freqTSL);
            Console.WriteLine("There are a total of {0} unique TSLs.",results.Count);
            Console.WriteLine("There are {0} unique TLSs that appear exactly {1} time(s).",getFreqFreq(results,freqTRA),freqTRA);           

            Console.WriteLine("The 10 most common TLSs are: ");
            foreach(string curTSL in topTen(results))
            {
                Console.WriteLine("   " + curTSL);
            }
            Console.ReadLine();
        }

        static string[] topTen(Dictionary<string, int> results)
        {
            string[] output = new string[10];
            
            for(int i = 0; i < 10; i++)
            {
                output[i] = results.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                results.Remove(output[i]);
            }
            return output;
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
            count = Regex.Matches(text, query).Count;
            return count;
        }        

        static Dictionary<string, int> getDictionary(string text, string query)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();
            MatchCollection matches = Regex.Matches(text, query);
            String curResVal;
            foreach (Match result in matches)
            {
                curResVal = text.Substring(result.Index, 3);
                if (results.ContainsKey(curResVal))
                {
                    results[curResVal]++;                    
                }
                else
                {
                    results.Add(curResVal, 1);
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
