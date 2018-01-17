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

        List<string> _words; // Master list of words
        Thread _thread = null;
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

        private void UI_BTN_Palindromes_Click(object sender, EventArgs e)
        {
            // Check if a thread is currently running
            if (_thread != null)
            {
                UI_TB_Output.Text = "Program is running, please wait";
                return;
            }
            else
            {
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
        }

        private void UI_BTN_ASCII_Click(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                UI_TB_Output.Text = "Program is running, please wait";
                return;
            }
            else
            {
                UI_TB_Output.Text = "ASCII search is in progress...";
                UI_BTN_Palindromes.Enabled = false;
                UI_BTN_Substring.Enabled = false;

                _thread = new Thread(ASCII);
                _thread.IsBackground = true;
                _thread.Start(_words);
                _sw.Restart();
            }
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

        // Support Method
        private List<string> DistinctLowerCase(List<string> wordList)
        {
            List<string> distinct = new List<string>();
            wordList.ForEach(o => distinct.Add(o.ToLower()));
            return distinct.Distinct().ToList();
        }

        // Palindromes Threads
        private void Palindromes(object words)
        {
            List<string> wordList = (List<string>)words;
            List<string> palList = new List<string>();

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
            List<string> words = (List<string>)wordList;
            List<string> AsciiOutput = new List<string>();
            List<int> AsciiCount = new List<int>();
            int asciiSum;
            
            foreach(string word in words)
            {
                asciiSum = 0;
                // find the ascii count
                foreach (char c in word)
                    asciiSum += (int)c;           
                // asciiSum = word.Sum(o => o);

                // check if the count exist in AsciiCount list - if not add to both list
                if (!AsciiCount.Contains(asciiSum))
                {
                    AsciiCount.Add(asciiSum);
                    AsciiOutput.Add(string.Format("{0:d4} - {1}", asciiSum, word));
                }
            }

            AsciiOutput.Sort();

            WriteToOutputFile(AsciiOutput, "same.txt");
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
            List<string> words = (List<string>)distinctList;
            List<string> subList = new List<string>();

            string sub = "";

            try
            {
                sub = (string)Invoke(new delStringVoid(PullString));
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }

            words.ForEach(o => { if (o.Contains(sub)) subList.Add(o); });

            WriteToOutputFile(subList, "substring.txt");

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
            File.WriteAllLines(filename, outputList);
        }

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

        private string PullString()
        {
            return UI_TB_Substring.Text.Trim().ToLower();
        }
    }
}
