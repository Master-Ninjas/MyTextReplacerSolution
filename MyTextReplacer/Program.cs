using System;
using System.Diagnostics;
using System.Reflection;

namespace MyTextReplacer
{
    class Program
    {
        static void DisplayHeader()
        {
            string appName = System.AppDomain.CurrentDomain.FriendlyName;
            string appVersion = typeof(Program).Assembly.GetName().Version.ToString();
            string appCopyright = "Hewlett-Packard Inc. (C) All Rights Reserved";
            Console.WriteLine(new String('*', 50));
            Console.WriteLine("* {0} v{1}{2} *", appName, appVersion, new String(' ', 46 - appName.Length - appVersion.Length - " v".Length));
            Console.WriteLine("* {0}{1} *", appCopyright, new String(' ', 46 - appCopyright.Length));
            Console.WriteLine(new String('*', 50));
        }
        static void DisplayArgumentsPassed()
        {
            // To be implemented
        }

        static void DisplayExerciseInstructions()
        {
            string[] instructions = 
            { 
                "Hello ninjas.",
                "",
                "For this exercise add code to this skeleton program to accept and handle the following arguments from the command line:",
                "",
                "  -Source <source text filename>",
                "  -Target <target text filename>",
                "  -Replace <text to find in source text file>",
                "  -With <text to replace with>",
                "",
                "The program should be able to fulfill the following requirements:",
                "  * handle phrases, not just words",
                "  * should read and write the text files from any path and not just the current folder",
                "  * should display a syntax help screen if the arguments passed are incorrect in some way",
                "  * should display the arguments passed",
            };

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (string line in instructions)
            { 
                Console.WriteLine(line.PadRight(Console.WindowWidth - 1));
            }
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            DisplayHeader();
            DisplayArgumentsPassed();
            // Comment out the next line 
            DisplayExerciseInstructions();
        }
    }
}
