// See https://aka.ms/new-console-template for more information
using AngleSharp;
using System;
using AngleSharp.Dom;
using System.Net;
using HtmlAgilityPack;
using System.Text;
using System.Collections.Specialized;
using Leaf.xNet; // dll
using HttpStatusCode = Leaf.xNet.HttpStatusCode; // debug request
using System.Net; // cookie
using hap = HtmlAgilityPack;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using AngleSharp.Common;
using HashTestConsole.SQLliteScript;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        
        public class WikiInfo
        {
            public List<string> Tags { get; set; } = new List<string>();
        }


        class Parser
        {
            List<string> removeList = new List<string>{
        "ИменительныйКто? Что?",
        "РодительныйКого? Чего?",
        "ДательныйКому? Чему?",
            "Винительный (неод.)Кого? Что?",
            "Винительный (одуш.)Кого? Что?",
            "ТворительныйКем? Чем?",
            "ТворительныйКем? Чем?",
            "ПредложныйО ком? О чём?"};
            public async Task<WikiInfo> GetWiki(string site, string str)
            {
                // конфигурация
                var config = Configuration.Default.WithDefaultLoader();
                // 
                //    var address = "https://ru.wikipedia.org/wiki/%D0%A6%D0%B0%D1%80%D1%81%D1%82%D0%B2%D0%BE_(%D0%B1%D0%B8%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D1%8F)";
                //    // асинхронно загружаем страницу
                //    address = string.Format(
                //"https://www.tagsfinder.com/ru-ru/ajax/?hashtag={0}&limit={1}&country={2}&fs={3}&fp={4}&fg={5}custom={7}&type={6}",
                //Uri.EscapeDataString("психология"),
                //Uri.EscapeDataString("300"),
                //Uri.EscapeDataString("ru"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("live"),
                //Uri.EscapeDataString(""));
                var address = string.Format(site,
                            Uri.EscapeDataString(str));

                var document = await BrowsingContext.New(config).OpenAsync(address);


                WikiInfo result = new WikiInfo();


                //--Картинки

                //--Параграфы
                //var cellSelector = @"table[class='table table-condensed table-bordered']";
                ////cellSelector = @"td[class='tHeader']";
                //var cells = document.QuerySelectorAll(cellSelector);
                //cellSelector = @"td[class='']";
                //var pars = cells.Select(m => m.TextContent);
                //result.Tags = new List<string>(pars);



                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(address);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-condensed table-bordered']")
                                    .Descendants("tr")
                                    .Skip(1)
                                    .Where((tr => tr.Elements("td").Count() > 1))
                                    .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                    .ToList();
                List<string>tempResult =  new List<string>();
                foreach (var row in table)
                {
                    List<string> result22 = row.Except(removeList).ToList();
                    foreach (var cell in result22)
                    {
                        //Console.WriteLine((cell).Replace("&#769;", ""));
                        tempResult.Add((cell).Replace("&#769;", ""));
                    }

                }
                result.Tags = new List<string>(tempResult);


                return result;
            }

            public async Task<WikiInfo> GetWiki2(string site, string str)
            {
                // конфигурация
                var config = Configuration.Default.WithDefaultLoader();
                // 
                //    var address = "https://ru.wikipedia.org/wiki/%D0%A6%D0%B0%D1%80%D1%81%D1%82%D0%B2%D0%BE_(%D0%B1%D0%B8%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D1%8F)";
                //    // асинхронно загружаем страницу
                //    address = string.Format(
                //"https://www.tagsfinder.com/ru-ru/ajax/?hashtag={0}&limit={1}&country={2}&fs={3}&fp={4}&fg={5}custom={7}&type={6}",
                //Uri.EscapeDataString("психология"),
                //Uri.EscapeDataString("300"),
                //Uri.EscapeDataString("ru"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("live"),
                //Uri.EscapeDataString(""));
                var address = string.Format(site,
            Uri.EscapeDataString(str));

                var document = await BrowsingContext.New(config).OpenAsync(address);


                WikiInfo result = new WikiInfo();


                //--Картинки

                //--Параграфы
                var cellSelector = @"td[class='nach']";
                //cellSelector = @"td[class='tHeader']";
                var cells = document.QuerySelectorAll(cellSelector);
                var pars = cells.Select(m => m.TextContent);
                result.Tags = new List<string>(pars);

                return result;
            }


            public async Task<WikiInfo> GetWiki3(string str)
            {
                // конфигурация
                var config = Configuration.Default.WithDefaultLoader();
                // 
                //    var address = "https://ru.wikipedia.org/wiki/%D0%A6%D0%B0%D1%80%D1%81%D1%82%D0%B2%D0%BE_(%D0%B1%D0%B8%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D1%8F)";
                //    // асинхронно загружаем страницу
                //    address = string.Format(
                //"https://www.tagsfinder.com/ru-ru/ajax/?hashtag={0}&limit={1}&country={2}&fs={3}&fp={4}&fg={5}custom={7}&type={6}",
                //Uri.EscapeDataString("психология"),
                //Uri.EscapeDataString("300"),
                //Uri.EscapeDataString("ru"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("off"),
                //Uri.EscapeDataString("live"),
                //Uri.EscapeDataString(""));
                var address = string.Format("https://wordassociations.net/ru/ассоциации-к-слову/{0}?button=Найти",
            Uri.EscapeDataString(str));

                var document = await BrowsingContext.New(config).OpenAsync(address);


                WikiInfo result = new WikiInfo();


                //--Картинки

                //--Параграфы
                var cellSelector = @"div[class='section ADJECTIVE-SECTION']";
                //cellSelector = @"td[class='tHeader']";
                var cells = document.QuerySelectorAll(cellSelector);
                var pars = cells.Select(m => m.TextContent);
                result.Tags = new List<string>(pars);

                return result;
            }

            public async Task<WikiInfo> GetAbjective(string str)
            {

                var config = Configuration.Default.WithDefaultLoader();
                var address = string.Format("https://wordassociations.net/ru/ассоциации-к-слову/{0}?button=Найти",
             Uri.EscapeDataString(str));

                var document = await BrowsingContext.New(config).OpenAsync(address);


                WikiInfo result = new WikiInfo();
                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(address);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//div[@class='section ADJECTIVE-SECTION']")
                                    .Descendants("ul")
                                    .Skip(0)
                                    .Where((tr => tr.Elements("li").Count() > 0))
                                    .Select(tr => tr.Elements("li").Select(li => li.InnerText.Trim()).ToList())
                                    .ToList();
                List<string> tempResult = new List<string>();
                foreach (var row in table)
                {
                    List<string> result22 = row.Except(removeList).ToList();
                    foreach (var cell in result22)
                    {
                        //Console.WriteLine((cell).Replace("&#769;", ""));
                        tempResult.Add((cell).Replace("&#769;", ""));
                    }

                }
                result.Tags = new List<string>(tempResult);

                return result;
            }

        }



        



        static async Task Main()
        {
            while (true)
            {
                string currentPath = Directory.GetCurrentDirectory();
                //if (!File.Exists(System.IO.Path.Combine(currentPath, "GenerationTags.txt")))
                //{
                //    File.Create(System.IO.Path.Combine(currentPath, "GenerationTags.txt"));
                //}


                Console.WriteLine("Введите слово для поиска:");
                string search = Console.ReadLine();
                var AllParser = new WikiInfo();

                
                var parser = new Parser();
                var ress = parser.GetWiki("https://kartaslov.ru/просклонять-существительное/{0}", search).Result;

                
                //AllParser.Tags = new List<string>(ress.Tags);

                //Console.WriteLine("Теги статьи производных:");
                foreach (var item in ress.Tags)
                {
                    //Console.WriteLine(RemoveDiacritics(item.Substring(0, item.Length)));
                    if (!item.Equals(""))
                    {
                        // Console.WriteLine(RemoveDiacritics(item));
                        //string[] words = item.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string Temp = item.ToString().TrimStart(' ', '\t', '\n').TrimEnd();
                        AllParser.Tags.Add(new string((from c in Temp
                                                       where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                                       select c
                                                         ).ToArray()));
                        //Console.WriteLine(words[0]);
                    }
                }



                


                //Console.WriteLine("Далее идут синонимы");

                parser = new Parser();
                ress = parser.GetWiki2("https://sinonim.org/s/{0}", search).Result;

                //Console.WriteLine("Теги статьи синонимов:");
                foreach (var item in ress.Tags)
                {
                    if (!item.Equals(""))
                    {
                        // Console.WriteLine(RemoveDiacritics(item));
                        string[] words = item.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        AllParser.Tags.Add(words[0]);
                        //Console.WriteLine(words[0]);
                    }
                }


                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(currentPath, "GenerationTags.txt"), false))
                {
                    foreach (var item in AllParser.Tags)
                    {
                        writer.WriteLine(item);
                    }
                    writer.Close();
                }


                Connection connection = new Connection();
                List<string> basicWords = new List<string>();
                connection.FromTxtToDB();
                connection.DeleteAllFromHashTagsTable();
                basicWords = new List<string>(connection.GenerationNewHashTags());

                parser = new Parser();
                ress = parser.GetAbjective(search).Result;

                //Console.WriteLine("Теги статьи синонимов:");
                foreach (var item in ress.Tags)
                {
                    if (!item.Equals(""))
                    {
                        Console.WriteLine((item));

                    }
                }

                connection.FillAbkectiveTable(ress.Tags);

                foreach (var word in AllParser.Tags)
                {
                    connection.FillHashTagsTable(String.Concat($"#{word}"));
                }

                foreach (var item in ress.Tags)
                {
                    foreach (var word in AllParser.Tags)
                    {
                        //Console.WriteLine($"#{word}{item}");
                        //Console.WriteLine($"#{item}{word}");
                        connection.FillHashTagsTable(String.Concat($"#{word}{item}"));
                        connection.FillHashTagsTable(String.Concat($"#{item}{word}"));
                    }
                }

                //AllParser.Tags.AddRange(ress.Tags);

                foreach (var item in AllParser.Tags)
                {
                    //Console.WriteLine(RemoveDiacritics(item));
                }

                foreach (var item in basicWords)
                {
                    foreach (var word in AllParser.Tags)
                    {
                        //Console.WriteLine($"#{word}{item}");
                        //Console.WriteLine($"#{item}{word}");
                        connection.FillHashTagsTable(String.Concat($"#{word}{item}"));
                        connection.FillHashTagsTable(String.Concat($"#{item}{word}"));
                    }
                }
                Console.WriteLine("Генерация Завершена");
            }
            
        }

        public static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }       
    }
}









