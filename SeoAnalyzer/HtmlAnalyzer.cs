using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace SeoAnalyzer
{
    public class HtmlAnalyzer
    {
        public static int GetCount(HtmlDocument htmlDoc)
        {
            int link_count = 0;
            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                link_count++;
            }
            return link_count;
        }

        public static Dictionary<string, int> GetMetaWords(Dictionary<string, int> words, HtmlDocument htmlDoc)
        {
            HtmlNode hnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='keywords']");
            Dictionary<string, int> meta_words = new Dictionary<string, int>();
            if (hnode != null)
            {
                HtmlAttribute desc;
                desc = hnode.Attributes["content"];
                string keywords = desc.Value;
                meta_words = Filter.GetMetaWords(words, keywords);
                return meta_words;
            }
            else
            {
                return meta_words;
            }

        }

        public static string GetTextFromHtml(HtmlDocument htmlDoc)
        {
            string input_text = "";
            foreach (var script in htmlDoc.DocumentNode.Descendants("script").ToArray())
                script.Remove();
            foreach (var style in htmlDoc.DocumentNode.Descendants("style").ToArray())
                style.Remove();
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//text()"))
            {
                input_text = input_text + node.InnerText + " ";
            }
            return input_text;
        }

    }
}