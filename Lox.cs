﻿namespace cslox
{
    internal class Lox
    {
        static bool hadError = false;
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: cslox [script]");
                Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                runFile(args[0]);
            }
            else
            {
                runPrompt();
            }
        }


        private static void runFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(Path.GetFullPath(path));
            run(Convert.ToBase64String(bytes));

            // Indicate an error in the exit code
            if (hadError) Environment.Exit(65);
        }

        private static void runPrompt()
        {
            StreamReader reader = new StreamReader(Console.OpenStandardInput());
            for (;;)
            {
                Console.Write("> ");
                string? line = reader.ReadLine();
                if (line is null) break;
                run(line);
                hadError = false;
            }
        }

        private static void run(string source)
        {
            List<string> tokens = new(source.Split(">"));
            for (int i = 0; i < tokens.Count(); i++)
            {
                Console.WriteLine(tokens[i]);
            }
        }

        static void error (int line, string message) 
        {
            report(line, "", message);
        }

        private static void report(int line, string where, string message) 
        {
            Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }
    }
}