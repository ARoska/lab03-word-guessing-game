using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WordGuessingGame
{
    public class Program
    {
        /// <summary>
        /// Main entry point for app.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                bool execute = true;

                do
                {
                    execute = NavigateHome();
                } while (execute == true);

            }
            catch (Exception e)
            {
                Console.WriteLine("Oops!  Something went wrong =(  Press any key to close the app.");
                Console.WriteLine();
                Console.WriteLine(e);
                Console.ReadKey();
            }
            finally
            {
                Console.Clear();
                Console.WriteLine("Bye!  Thanks for playing!");
                System.Threading.Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Displays main nav menu.  If user chooses to exit returns to Main and ends run loop.
        /// </summary>
        /// <returns>Boolean for terminating Main run loop</returns>
        public static bool NavigateHome()
        {
            bool run = true;
            string path = "../../../wordList.txt";

            try
            {
                if (File.Exists(path) == false)
                {
                    createSeededList(path);
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine("Welcome!  Please select an option: \n" +
                        "\n" +
                        "1. Start a new game \n" +
                        "2. Admin Menu \n" +
                        "3. Exit \n" +
                        "");
                    string homeInput = Console.ReadLine();

                    if (homeInput == "1" || homeInput.ToUpper() == "START" || homeInput.ToUpper() == "START GAME" || homeInput.ToUpper() == "PLAY" || homeInput.ToUpper() == "PLAY GAME" || homeInput.ToUpper() == "NEW" || homeInput.ToUpper() == "NEW GAME")
                    {
                        Console.Clear();
                        LaunchGame(path);
                    }
                    else if (homeInput == "2" || homeInput.ToUpper() == "ADMIN" || homeInput.ToUpper() == "ADMIN MENU")
                    {
                        Console.Clear();
                        NavigateAdmin(path);
                    }
                    else if (homeInput == "3" || homeInput.ToUpper() == "EXIT")
                    {
                        run = false;
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                } while (run == true);

                return false;

            }
            catch (FileNotFoundException)
            {
                createSeededList(path);
                return true;
            }
        }

        /// <summary>
        /// Displays admin nav menu
        /// </summary>
        public static void NavigateAdmin(string path)
        {
            bool run = true;

            while (run == true)
            {
                Console.WriteLine("Admin Menu.  Please select an option: \n" +
                    "\n" +
                    "1. View word bank \n" +
                    "2. Add word to bank \n" +
                    "3. Delete word from bank \n" +
                    "4. Return to main menu \n" +
                    "");
                string homeInput = Console.ReadLine();

                if (homeInput == "1" || homeInput.ToUpper() == "VIEW" || homeInput.ToUpper() == "VIEW BANK")
                {
                    Console.Clear();
                    ViewWordList(path);
                }
                else if (homeInput == "2" || homeInput.ToUpper() == "ADD" || homeInput.ToUpper() == "ADD WORD")
                {
                    bool valid = false;

                    Console.Clear();
                    Console.Write("Please enter a single word to add to the list (Cannot contain spaces or special characters)\n" +
                        "Or, enter X to quit without adding a new word: ");
                    string addWordInput = Console.ReadLine();

                    while (valid == false)
                    {
                        if (addWordInput.ToLower() == "x")
                        {
                            Console.Clear();
                            valid = true;
                            break;
                        }

                        valid = AddWord(path, addWordInput);

                        if (valid == false)
                        {
                            Console.Write("\n" +
                                    "Please ONLY enter a single word to add to the list (Cannot contain spaces or special characters)\n" +
                                    "Or, enter X to quit without adding a new word: ");
                            addWordInput = Console.ReadLine();
                        }
                    }
                }
                else if (homeInput == "3" || homeInput.ToUpper() == "DELETE" || homeInput.ToUpper() == "DELETE WORD")
                {
                    Console.Clear();
                    ViewWordList(path);
                    Console.Write("Please enter the word you would like to delete from the list\n" +
                        "Or, enter X to quit without deleting any words: ");

                    string deleteWordInput = Console.ReadLine();

                    if (deleteWordInput.ToLower() != "x")
                    {
                        DeleteWord(path, deleteWordInput);
                        Console.Clear();
                        ViewWordList(path);
                    }
                    Console.Clear();
                }
                else if (homeInput == "4" || homeInput.ToUpper() == "EXIT" || homeInput.ToUpper() == "RETURN" || homeInput.ToUpper() == "MAIN" || homeInput.ToUpper() == "MAIN MENU")
                {
                    run = false;
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Launches the guessin game.
        /// </summary>
        /// <param name="path">Path to the file storing word list.</param>
        public static void LaunchGame(string path)
        {
            bool correct = false;
            string[] words = File.ReadAllLines(path);
            string randomWord = words[GetRandomNumber(words.Length) - 1];
            char[] answer = new char[randomWord.Length];

            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = '_';
            }

            Console.Clear();

            while (correct == false)
            {
                string guessInput = "";
                char[] guesses = new char[0];
                Console.WriteLine("Here is your word:\n" +
                    "");
                Console.Write($"{string.Join("  ", answer)}");
                Console.WriteLine("\n" +
                    "Current guesses:\n" +
                    $"[{string.Join(" ", guesses)}]");

                while (guessInput.Length != 1)
                {
                    Console.Write("\n" +
                        "Please guess a letter: ");

                    guessInput = Console.ReadLine();
                }
                char guessChar = Convert.ToChar(guessInput);
                int[] index = Guess(randomWord, guessChar);
                if (index.Length == 0)
                {
                    Console.Clear();
                }
                else
                {
                    for (int i = 0; i < index.Length; i++)
                    {
                        answer[i] = guessChar;
                    }
                }
                Console.Clear();
            }
        }

        public static int[] Guess(string randomWord, char guessChar)
        {
            int[] index = new int[0];

            for (int i = 0; i < randomWord.Length; i++)
            {
                if (randomWord[i] == guessChar)
                {
                    int[] tempIndex = new int[(index.Length + 1)];

                    for (int j = 0; j < index.Length; j++)
                    {
                        tempIndex[j] = index[j];
                    }

                    tempIndex[(index.Length)] = i;
                    index = tempIndex;
                }
            }

            return index;
        }


        /// <summary>
        /// Displays all words saved in the current word list.
        /// </summary>
        /// <param name="path">Path to the file storing word list.</param>
        public static void ViewWordList(string path)
        {
            string[] words = File.ReadAllLines(path);

            Console.WriteLine("Word List:\n" +
                "");

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Adds a word to the list.  This word cannot contain spaces or special characters, and duplicate words will not be added.
        /// </summary>
        /// <param name="path">Path to the file storing word list.</param>
        /// <param name="addWordInput">Word the user would like to input.</param>
        /// <returns>Returns true or false depending on success.</returns>
        public static bool AddWord(string path, string addWordInput)
        {
            string[] words = File.ReadAllLines(path);

            if (Regex.IsMatch(addWordInput, @"^[A-Z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            {

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == addWordInput.ToLower())
                    {
                        DeleteWord(path, addWordInput);
                        i = words.Length;
                    }
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(addWordInput.ToLower());
                }

                words = File.ReadAllLines(path);

                if (words[words.Length - 1] == addWordInput.ToLower())
                {
                    Console.WriteLine("Success!  Here is the new word list:\n" +
                        "");
                    ViewWordList(path);
                    return true;
                }

                Console.WriteLine("Something went wrong... Please try again:");
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a word from the list
        /// </summary>
        public static void DeleteWord(string path, string deleteWordInput)
        {
            if (Regex.IsMatch(deleteWordInput, @"^[A-Z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            {
                string[] words = File.ReadAllLines(path);

                File.Delete(path);

                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (string word in words)
                    {
                        if (word != deleteWordInput.ToLower())
                        {
                            sw.WriteLine(word);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates a seeded word list.
        /// </summary>
        /// <param name="path">Path to the file storing word list.</param>
        public static void createSeededList(string path)
        {
            string[] starterWords = { "anime", "game", "coffee", "banana", "red" };

            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string word in starterWords)
                {
                    sw.WriteLine(word);
                }
            }
        }

        /// <summary>
        /// Rolls a random number between 1 and the length of the current word list.
        /// </summary>
        /// <param name="listSize">This should be the length of the current word list.</param>
        /// <returns>Random number within range</returns>
        private static int GetRandomNumber(int listSize)
        {
            Random random = new Random();
            int value = random.Next(1, listSize + 1);
            return value;
        }

    }
}
