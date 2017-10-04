using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;

namespace ThreeLetterSequences
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            String query = @"\w(?=\w\w)";
            string text = ReadFromFile();
            text = text.ToLower();
            int freqTRA = RegExApproach(text, "tra");
            int freqTSL = RegExApproach(text, query);
            Dictionary<string, int> results = getDictionary(text,query);

            Console.WriteLine("The string 'tra' appears {0} times, there are a total of {1} TLSs!",freqTRA,freqTSL);
            Console.WriteLine("There are a total of {0} unique TSLs.",results.Count);
            Console.WriteLine("There are {0} unique TLSs that appear exactly {1} time(s).",getFreqFreq(results,freqTRA),freqTRA);
            while (true) { 
                showMenu();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        mostFrequent(results);
                        break;
                    case "2":
                        specificTLS(results);
                        break;
                    case "3":
                        text = getWebPageText();
                        text = text.ToLower();
                        results = getDictionary(text, query);
                        break;
                }
                Console.ReadLine();
            }
        }


        static void showMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine(" 1. View most frequent TLSs");
            Console.WriteLine(" 2. View frequency of a specific TLS");
            Console.WriteLine(" 3. Load text from webpage");
        }
        static string getWebPageText()
        {
            string url = "";
            Boolean validInput = false;
            while (!validInput)
            {
                Console.Write("    URL to load: ");
                try
                {
                    url = Console.ReadLine();
                    if(Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid url");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    validInput = false;
                }
            }
            WebClient webClient = new WebClient();
            return webClient.DownloadString(url);
        }
        static void specificTLS(Dictionary<string, int> results)
        {
            string input = "";
            Boolean validInput = false;
            while (!validInput)
            {
                Console.Write("    Enter TLS: ");
                try
                {
                    input = Console.ReadLine();
                    if(input.Length == 3)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("That is not the right length, please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    validInput = false;
                }
            }
            int freq = results[input];
            Console.WriteLine();
            Console.WriteLine("The string {0} appears {1} times!", input, freq);
        }

        static void mostFrequent(Dictionary<string, int> results)
        {
            int n = 0;
            Boolean validInput = false;
            while (!validInput)
            {
                Console.Write("    Number of values to display: ");
                try
                {
                    validInput = true;
                    n = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    validInput = false;
                }
            }
            string[] output = new string[n];
            Console.WriteLine();
            Console.WriteLine("The {0} most common TLSs are: ",n);
            for (int i = 0; i < n; i++)
            {
                output[i] = results.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                results.Remove(output[i]);
                Console.WriteLine("   " + output[i]);
            }
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
