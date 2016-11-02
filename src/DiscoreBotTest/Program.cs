﻿using Discore;
using System;
using System.IO;

namespace DiscoreBotTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!File.Exists("token.txt"))
                throw new FileNotFoundException("Missing token.txt file!", "token.txt");

            string token = File.ReadAllText("token.txt").Trim();

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Token from token.txt is invalid!");

            DiscoreLogger.OnLog += DiscoreLogger_OnLog;

            DiscordApplication app = new DiscordApplication(token);

            app.Shards.CreateShards(1);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void DiscoreLogger_OnLog(object sender, DiscoreLogEventArgs e)
        {
            Console.WriteLine($"[{e.Line.Type}] {e.Line.Message}");
        }
    }
}
