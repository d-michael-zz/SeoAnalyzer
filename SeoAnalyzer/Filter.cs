using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer
{
    public class Filter
    {
        public static Dictionary<string, int> GetDict(string s)
        {
            string re_s = "";
            re_s = s.ToLower();
            //Add additional chars if needed
            char[] delimiterChars = { ' ', ',', '!', '?', ';', '.', ':', '-', '\t' };
            List<string> words = new List<string>(re_s.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries));
            
            Dictionary<string, int> words_with_count = new Dictionary<string, int>();
            for (int i = 0; i < words.Count(); i++)
            {
                if(words_with_count.ContainsKey(words[i]))
                {
                    words_with_count[words[i]]++;
                }
                else
                {
                    words_with_count.Add(words[i], 1);
                }
            }

            return words_with_count;
        }

        public static Dictionary<string, int> RemoveStopWords(Dictionary<string, int> words)
        {
            Dictionary<string, int> temp_w = new Dictionary<string, int>();
            temp_w = words;

            //list of stop words, can be expanded
            List<string> stop_words = new List<string>() { "a", "an", "and", "are", "no", "yes", "or", "the" };

            foreach (string s in stop_words)
            {
                if (temp_w.ContainsKey(s))
                {
                    temp_w.Remove(s);
                }
            }

            return temp_w;
        }

        public static Dictionary<string, int> GetMetaWords(Dictionary<string, int> words, string meta_words)
        {
            //Dictionary<string, int> meta_words_dict = new Dictionary<string, int>();
            //meta_words_dict = words;
            char[] delimiterChars = { ' ', ',', '!', '?', ';', '.', ':', '-', '\t' };
            List<string> temp_meta_words = new List<string>(meta_words.ToLower().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries));
            bool flag = false;

            foreach (string s in words.Keys.ToArray())
            {
                for (int i = 0; i < temp_meta_words.Count(); i++)
                {
                    if (temp_meta_words[i] == s)
                        flag = true;
                }
                if(flag != true)
                    words.Remove(s);
                flag = false;
            }

            //return meta_words_dict;
            return words;
        }
    }
}