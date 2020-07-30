using System;
using System.Diagnostics;
using System.IO;
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
        static void DisplayArgumentsPassed(string sourceFile, string targetFile, string textToReplace, string replacementText)
        {
            Console.WriteLine("- Name of file you are reading from: " + sourceFile);
            Console.WriteLine("- Name of file you are writing to: " + targetFile);
            Console.WriteLine("- String of text you are replacing:" + textToReplace);
            Console.WriteLine("- String of text you are replacing old text with:" + replacementText + "\n");
        }

        static void DisplayExerciseInstructions()
        {
            string[] instructions =
            {
                "Hello ninjas.",
                "",
                "For this exercise add code to this skeleton program to accept and handle the following arguments from the command line:",
                "",
                "  -Source <source text filename> (DONE)",
                "  -Target <target text filename> (DONE)",
                "  -Replace <text to find in source text file> (DONE)",
                "  -With <text to replace with> (DONE)",
                "",
                "The program should be able to fulfill the following requirements:",
                "  * handle phrases, not just words (DONE)",
                "  * should read and write the text files from any path and not just the current folder",
                "  * should display a syntax help screen if the arguments passed are incorrect in some way",
                "  * should display the arguments passed (DONE)",
            };

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (string line in instructions)
            {
                Console.WriteLine(line.PadRight(Console.WindowWidth - 1));
            }
            Console.ResetColor();
        }

        //Display syntax help when requested
        static void DisplaySyntaxHelp()
        {
            string[] helpLines =
            {
                "****************************",
                "* MyTextReplacer Solution Syntax Help *",
                "****************************",
                "MyTextReplacer -Source <path> -Target <path> -Replace <text> -With <text you want to replace old text with in new file> [-Help]",
                "\t -Source <path> -- path to file you want to retrieve text from. Be sure to insert the FULL path, not just the text file name, like this --> C:\\Users\\HPInc\\Desktop\\test.txt",
                "\t -Target <path> -- path to file you want to store new text in. Be sure to insert the FULL path, not just the text file name, like this --> C:\\Users\\HPInc\\Desktop\\test.txt",
                "\t -Replace <text> -- text from source file you want to replace. Be sure to begin and end your text with a backslash and double quotes like this --> \\\"Example\\\" ",
                "\t -With <text> -- text you want to replace old text with in new file.  Be sure to begin and end your text with a backslash and double quotes like this --> \\\"Example\\\"",
                "\t -Help        -- will display the syntax help",
                "",
            };

            // Loop through the array of lines in helpLines
            foreach (string line in helpLines)
            {
                // Write each line to the console.
                Console.WriteLine(line);
            }
        }

        //Check if the source path provided in the command line exists
        static bool IsSourcePathValid(string sourceFileName)
        {

            bool fileExists = false;

            //Until the user inputs a valid .txt file name, keep requesting for input
            //do
            //{

            //Console.Write("Enter the name of the text file you would like to read from (Do NOT include .txt extension): ");
            //sourceFileName = Console.ReadLine() + ".txt";

            if (File.Exists(sourceFileName))
            {
                Console.WriteLine("Source file found!\n");
                fileExists = true;
            }
            else
            {
                Console.WriteLine("Sorry, you specified source file/file path does not exist. Please input a different file name/path.\n");
            }

            return fileExists;
            //}
            //while (fileExists == false);

        }

        //Check if the target path provided in the command line exists
        static bool IsTargetPathValid(string targetFileName)
        {
            /*string targetFileName;

            Console.Write("Enter the name of the text file you would like to save your new text into (Do NOT include .txt extension): ");
            targetFileName = Console.ReadLine() + ".txt";*/

            bool isValid = false;

            try
            {
                if (File.Exists(targetFileName))
                {
                    Console.WriteLine("Your specified target text file already exists, so your new text will be appended onto the end of the existing text in the file.\n");
                    isValid = true;
                }
                else
                {
                    File.Create(targetFileName);
                    Console.WriteLine("A new file has been created to store your new text.\n");
                    isValid = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Sorry, unable to open or create target file from the path you've provided. Please try again.\n");
            }

            return isValid;
        }

        //Add spaces to the beginning and end of the string to prevent instances of "a word within a word" from getting replaced (i.e. "in" inside the word "fine")
        static string FormatTextToReplace(string textToReplace)
        {
            //Console.Write("Enter the text you would like to replace from the source file: ");
            //string textToReplace = Console.ReadLine();

            //Add spaces to the beginning and end of the string to prevent instances of "a word within a word" from getting replaced (i.e. "in" inside the word "fine")
            if (!textToReplace[0].Equals(" "))
            {
                textToReplace = " " + textToReplace;
            }

            if (!textToReplace[textToReplace.Length - 1].Equals(" "))
            {
                textToReplace += " ";
            }
            return textToReplace;
        }

        //Add spaces to the beginning and end of string to accomodate for the space being replaced
        static string FormatReplacementText(string replacementText)
        {
            //Console.Write("Enter the text you would like to replace the old text with: ");
            //string replacementText = Console.ReadLine();

            //Add spaces to the beginning and end of string to accomodate for the space being replaced
            if (!replacementText[0].Equals(" "))
            {
                replacementText = " " + replacementText;
            }

            if (!replacementText[replacementText.Length - 1].Equals(" "))
            {
                replacementText += " ";
            }

            return replacementText;
        }

        //Replace the specified old text with specified new text
        static void ReplaceText(string oldFile, string newFile, string oldText, string newText)
        {
            string text = File.ReadAllText(oldFile);
            text = text.Replace(oldText, newText);
            File.WriteAllText(newFile, text);

            Console.WriteLine("");
        }

        static void GetCommandLineArguments(string[] args, out string oldFilePath, out string newFilePath, out string textToReplace, out string replacementText, out bool oldFilePathValid, out bool newFilePathValid, out bool requestedHelp)
        {
            oldFilePath = "";
            newFilePath = "";
            textToReplace = "";
            replacementText = "";
            oldFilePathValid = false;
            newFilePathValid = false;
            requestedHelp = false;

            int i = 0;
            while (i < args.Length)
            {
                if (args[i].Equals("-Help", StringComparison.OrdinalIgnoreCase))
                {
                    requestedHelp = true;
                }

                if (args[i].Equals("-Source", StringComparison.OrdinalIgnoreCase))
                {
                    oldFilePath = args[++i];
                    oldFilePathValid = IsSourcePathValid(oldFilePath);
                }
                else if (args[i].Equals("-Target", StringComparison.OrdinalIgnoreCase))
                {
                    newFilePath = args[++i];
                    newFilePathValid = IsTargetPathValid(newFilePath);
                }
                else if (args[i].Equals("-Replace", StringComparison.OrdinalIgnoreCase))
                {
                    textToReplace = FormatTextToReplace(args[++i]);
                }
                else if (args[i].Equals("-With", StringComparison.OrdinalIgnoreCase))
                {
                    replacementText = FormatReplacementText(args[++i]);
                }
                else
                {
                    requestedHelp = true;
                    break;
                }
                i++;
            }
        }

        static void Main(string[] args)
        {
            DisplayHeader();

            //variables to store args values
            string oldFilePath = "";
            string newFilePath = "";
            string textToReplace = "";
            string replacementText = "";

            //variables to check validity of source and file paths
            bool oldFilePathValid = false;
            bool newFilePathValid = false;
            bool requestedHelp = false;

            //check for command line keywords
            GetCommandLineArguments(args, out oldFilePath, out newFilePath, out textToReplace, out replacementText, out oldFilePathValid, out newFilePathValid, out requestedHelp);
            if (!requestedHelp)
            {

                //Once source and target file paths have been validated, display arguments and start text replacement
                if (oldFilePathValid && newFilePathValid)
                {
                    DisplayArgumentsPassed(oldFilePath, newFilePath, textToReplace, replacementText);
                    ReplaceText(oldFilePath, newFilePath, textToReplace, replacementText);
                    Console.WriteLine("Text replaced successfully! Please check your target file for your new text.\n");
                }
                //Display error message if the source and/or target file path is invalid
                else
                {
                    Console.WriteLine("I'm sorry, because at least one of your inputted file paths was invalid, no changes were made. Please try again.\n");
                }

            }
            else
            {
                DisplaySyntaxHelp();
            }

            /*DisplayHeader();
            // Comment out the next line 
            //DisplayExerciseInstructions()

            //Get user input from console
            string source = isSourcePathValid();
            string target = isTargetPathValid();
            string textToReplace = getTextToReplace();
            string replacement = getReplacementText();

            //Display values inputted by the user
            Console.WriteLine("");
            DisplayArgumentsPassed(source, target, textToReplace, replacement);
            Console.WriteLine("");

            //Copy text from source file, replace requested string, then copy the new text to the target file 
            replaceText(source, target, textToReplace, replacement);

            Console.WriteLine("Your new text with the requested string of text from " + source + " replaced has been copied to " + target + "\n");*/
        }
    }
}
