using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace dnc200_commits_cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            string username, repo;
            //hint: this is the base of the github API url "https://api.github.com/"
            ReadString getUserName = new ReadString("Type in your github username:");
            username = getUserName.GetInput();
            ReadString getRepo = new ReadString("Type in Repo name or leave blank to see all repo commits:");
            repo = getRepo.GetInput();

            if(repo != "")
            {
                GetCommits(username, repo);
            } else
            {
                GetCommits(username);
            }

        }

        public static int GetCommits(string username, string repo = "null")
        {
            string url;
            if (repo == "null")
            {
                url = $"https://api.github.com/users/{username}/events";
            } else
            {
                url = $" https://api.github.com/repos/{username}/{repo}/events";
            }

            var response = GitHub(url);

            string[] words = response.Split(" ");
            //Console.WriteLine(response);

            int commits = 0;
            foreach (var word in words)
            {
                //Console.WriteLine($"{word}");
                string regexPattern = "commits";
                Regex reg = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match m = reg.Match(word);
                if(m.Success) {
                    commits++;
                }
            }
            if(commits != 0 && commits % 2 == 0)
            {
                //removing the url matches aka duplicates
                commits /= 2;
            }

            Console.WriteLine($"Recent Commit Count : {commits}");  
            return commits;
        }
        public static string GitHub(string url)
        {           
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var response = client.DownloadString(url);
            return response;
        }
    }
}
