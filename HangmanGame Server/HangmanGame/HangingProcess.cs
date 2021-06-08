using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    class HangingProcess
    {
        private List<New_Word> new_word_list;
        private List<New_Word> word_list;
        public HangingProcess()
        {
            // Get data file that contains the words for the hangman game
            get_data_file();
        }
        public struct New_Word
        {
            public string word;
            public string category;
        }
        public New_Word get_word()
        {
            // Process for selecting random word from the text file
            if (word_list.Count == 0)
            {
                word_list = new_word_list;
            }
            Random rdm = new Random();
            int a = rdm.Next(0, word_list.Count);
            New_Word words = word_list[a];
            return words;
        }
        private New_Word Create_new_word(string word, string category)
        {
            // Creating the word and set it as the new category
            New_Word words = new New_Word();
            words.word = word;
            words.category = category;
            return words;
        }
        private void get_data_file()
        {
            // Reading the data text file
            new_word_list = new List<New_Word>();
            StreamReader read = new StreamReader(@"data.txt", Encoding.Default);
            string data;
            while ((data = read.ReadLine()) != null)
            {
                if (data != "")
                {
                    new_word_list.Add(Create_new_word(data.Split(',')[0], data.Split(',')[1]));
                }
            }
            word_list = new_word_list;
        }
    }
}
