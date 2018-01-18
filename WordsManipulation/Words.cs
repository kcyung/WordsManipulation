using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WordsManipulation
{
    public partial class WordsThread : Form
    {
        // Delegate declaration
        private delegate void delVoidInt(int n);
        private delegate string delStringVoid();

        List<string> _words;        // Master list of words
        Thread _thread = null;      // Variable to check if a thread is running
        System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch();

        public WordsThread()
        {
            InitializeComponent();
        }

        private void WordsThread_Load(object sender, EventArgs e)
        {
            // Place all lines from text file into master word list
            try
            {
                _words = new List<string>(File.ReadAllLines("words.txt"));
            }
            catch
            {
                UI_TB_Output.Text = "Error reading from file";
            }

            UI_Label.Text = "Loaded " + _words.Count().ToString() + " words!";
        }


        // *************    EVENT HANDLERS     ************* //
        private void UI_BTN_Palindromes_Click(object sender, EventArgs e)
        {
            // Check if a thread is currently running
            if (_thread != null)
            {
                UI_TB_Output.Text = "Program is running, please wait";
                return;
            }

            // update UI
            UI_TB_Output.Text = "Palindrome search in progress...";
            UI_BTN_ASCII.Enabled = false;
            UI_BTN_Substring.Enabled = false;

            // start parametized thread start with distinct word list
            _thread = new Thread(Palindromes);
            _thread.IsBackground = true;
            _thread.Start(DistinctLowerCase(_words));
            _sw.Restart();
        }

        private void UI_BTN_ASCII_Click(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                UI_TB_Output.Text = "Program is running, please wait";
                return;
            }

            UI_TB_Output.Text = "ASCII search is in progress...";
            UI_BTN_Palindromes.Enabled = false;
            UI_BTN_Substring.Enabled = false;

            _thread = new Thread(ASCII);
            _thread.IsBackground = true;
            _thread.Start(_words);
            _sw.Restart();
        }

        private void UI_BTN_Substring_Click(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                UI_TB_Output.Text = "Program is running, please wait";
                return;
            }

            if (UI_TB_Substring.Text.Trim().Length < 3)
            {
                UI_TB_Output.Text = "Substring must be at least three characters long";
                return;
            }

            UI_TB_Output.Text = "Substring search in progress...";
            UI_BTN_Palindromes.Enabled = false;
            UI_BTN_ASCII.Enabled = false;

            _thread = new Thread(Substring);
            _thread.IsBackground = true;
            _thread.Start(DistinctLowerCase(_words));
            _sw.Restart();
        }

        // Support Method - returns a distinct list of all words in lower case
        private List<string> DistinctLowerCase(List<string> wordList)
        {
            List<string> distinct = new List<string>();
            wordList.ForEach(o => distinct.Add(o.ToLower()));
            return distinct.Distinct().ToList();
        }

        // Palindromes Threads
        private void Palindromes(object words)
        {
            // Cast out distinct lowercase list of words
            List<string> wordList = (List<string>)words;

            // Create a list to store all palindromes
            List<string> palList = new List<string>();

            // Palindrome check for words larger than one character
            foreach(string word in wordList)
            {
                if (word.Length > 1 && word == new string(word.ToCharArray().Reverse().ToArray()))
                    palList.Add(word);
            }

            WriteToOutputFile(palList, "Palindromes.txt");

            // Invoke delegate to update the UI
            try
            {
                Invoke(new delVoidInt(UpdateUI), palList.Count);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead.");
            }
        }

        // ASCII Same Thread
        private void ASCII(Object wordList)
        {
            // Cast out word list
            List<string> words = (List<string>)wordList;

            // Create a list to store first unique ASCII sum
            List<string> AsciiOutput = new List<string>();

            // Create a list to store all found ASCII sums so far
            List<int> AsciiCount = new List<int>();

            // The ASCII sum of current word being checked
            int asciiSum;
            
            foreach(string word in words)
            {
                // Reset the sum for each new word
                asciiSum = 0;

                // Find the ASCII sum for the word
                foreach (char c in word)
                    asciiSum += (int)c;           
               
                if (!AsciiCount.Contains(asciiSum))
                {
                    AsciiCount.Add(asciiSum);
                    AsciiOutput.Add(string.Format("{0:d4} - {1}", asciiSum, word));
                }
            }

            AsciiOutput.Sort();

            WriteToOutputFile(AsciiOutput, "same.txt");

            // Update the UI
            try
            {
                Invoke(new delVoidInt(UpdateUI), AsciiOutput.Count);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead.");
            }
        }

        // Substring Thread
        private void Substring(object distinctList)
        {
            // Cast out distinct list of lower case words
            List<string> words = (List<string>)distinctList;

            // Create a list to store all matching substrings
            List<string> subList = new List<string>();

            // current substring
            string sub = "";

            // Get the substring from the UI via a delegate
            try
            {
                sub = (string)Invoke(new delStringVoid(PullString));
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }

            // Iterate through list and word to substring list if it matches
            words.ForEach(o => { if (o.Contains(sub)) subList.Add(o); });

            WriteToOutputFile(subList, "substring.txt");

            // Update the UI
            try
            {
                Invoke(new delVoidInt(UpdateUI), subList.Count);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }
        }

        // Create text file to write Palindromes/Ascii/Substring list to
        private void WriteToOutputFile(List<string> outputList, string filename)
        {
            try
            {
                File.WriteAllLines(filename, outputList);
            }
            catch (Exception err)
            {
                System.Diagnostics.Trace.WriteLine("Error writing to file: " + err);
            }
        }

        // Method used to update the UI upon completion of palindrome/ASCII sum/substring search
        private void UpdateUI(int listCount)
        {
            _sw.Stop();

            string test = "";

            if (UI_BTN_Palindromes.Enabled)
                test = "Palindromes";
            else if (UI_BTN_ASCII.Enabled)
                test = "ASCII";
            else
                test = "Substring";

            UI_BTN_Palindromes.Enabled = true;
            UI_BTN_ASCII.Enabled = true;
            UI_BTN_Substring.Enabled = true;

            UI_TB_Output.Text = string.Format("Completed {2} search, producing {0} words in {1} seconds", 
                listCount, _sw.ElapsedMilliseconds / (float)1000, test);

            _thread = null;
        }

        // Method for the delegate to supply substring thread with the textbox input
        private string PullString()
        {
            return UI_TB_Substring.Text.Trim().ToLower();
        }
    }
}
