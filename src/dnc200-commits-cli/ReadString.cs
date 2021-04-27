using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace dnc200_commits_cli
{
    public class ReadString
    {
        string prompt;
        public ReadString(string _prompt)
        {
            prompt = _prompt;
        }

        public string GetInput()
        {
            while (true)
            {
                try
                {
                    string input;
                    Console.WriteLine(prompt);
                    input = Console.ReadLine();
                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a valid number.");
                    Console.WriteLine($"{e}");
                }
            }
        }

    }
}
