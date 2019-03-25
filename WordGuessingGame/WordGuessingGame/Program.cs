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

                while (execute == true)
                {
                    execute = NavigateHome();
                }

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
            string path = "../../../word-list.txt";

            try
            {
                if (File.Exists(path) == false)
                {
                    createSeededList(path);
                }

                while (run == true)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome!\n" +
                        "\n" +
                        "1. Start a new game \n" +
                        "2. Admin Menu \n" +
                        "3. Exit \n" +
                        "");
                    Console.Write("Enter the number of the option you wish to select: ");
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

                }

                return false;
            }
            catch (FileNotFoundException)
            {
                Console.Write("Uh-oh... The word list is no longer accessable.  Please wait while I make a new one.");
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
                System.Threading.Thread.Sleep(500);
                Console.Write(".\n" +
                    "Done!  The app will now restart.");

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
                Console.WriteLine("Admin Menu.\n" +
                    "\n" +
                    "1. View word bank \n" +
                    "2. Add word to bank \n" +
                    "3. Delete word from bank \n" +
                    "4. Return to main menu \n" +
                    "");
                Console.Write("Enter the number of the option you wish to select: ");
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
                    Console.Write("Please enter a single word to add to the list.\n" +
                        "Word must be at least 3 characters long and cannot contain spaces or special characters\n" +
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
                        else if (addWordInput.Length > 2)
                        {
                            valid = AddWord(path, addWordInput);
                            if (valid == false)
                            {
                                Console.Write("\n" +
                                        "Please ONLY enter a SINGLE word to add to the list.\n" +
                                        "Word must be at least 3 characters long and CANNOT contain spaces or special characters\n" +
                                        "Or, enter X to quit without adding a new word: ");
                                addWordInput = Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Write("\n" +
                                "Please ONLY enter a SINGLE word to add to the list.\n" +
                                "Word must be at least 3 characters long and CANNOT contain spaces or special characters\n" +
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
            char[] answerArray = new char[randomWord.Length];
            char[] guessArray = new char[0];

            for (int i = 0; i < answerArray.Length; i++)
            {
                answerArray[i] = '_';
            }

            Console.Clear();


            while (correct == false)
            {
                string guessInput = "";
                Console.WriteLine("Guess the word, one letter at a time.  Good Luck!\n" +
                    "" +
                    $"{string.Join("  ", answerArray)}" +
                    "\n" +
                    "Previous guesses: \n" +
                    $"[{string.Join(" ", guessArray)}]\n" +
                    "");
                Console.Write("Please enter a letter (or type 'exit' to return to the Main Menu): ");
                guessInput = Console.ReadLine();

                if (guessInput.ToLower() == "exit")
                {
                    correct = true;
                    break;
                }
                if (Regex.IsMatch(guessInput, @"^[A-Z]", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    char guessChar = Convert.ToChar(guessInput);

                    // Checks previous gusses and if current guess is new adds it to the list
                    if (Array.IndexOf(guessArray, guessChar) == -1)
                    {
                        char[] tempArray = new char[(guessArray.Length + 1)];
                        for (int i = 0; i < guessArray.Length; i++)
                        {
                            tempArray[i] = guessArray[i];
                        }
                        tempArray[guessArray.Length] = guessChar;
                        guessArray = tempArray;
                    }

                    int[] indexArray = Guess(randomWord, guessChar);

                    if (indexArray.Length == 0)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        for (int i = 0; i < indexArray.Length; i++)
                        {
                            int index = indexArray[i];
                            answerArray[index] = guessChar;
                        }
                    }
                    Console.Clear();

                    // Checks if the user has completely guessed the word.
                    if (Array.IndexOf(answerArray, '_') == -1)
                    {
                        correct = true;
                        Console.WriteLine("You win! Press any key to return to the Main Menu.\n" +
                            "" +
                            $"{string.Join("  ", answerArray)}" +
                            "\n" +
                            "All guesses:\n" +
                            $"[{string.Join(" ", guessArray)}]\n" +
                            "\n");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Write("\n" +
                        "Please guess a letter: ");

                    guessInput = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Checks the user's guessed letter agains the random word currently in play.
        /// If the letter is in the word anywhere it returns an array that has the position of every instance of that letter,
        /// otherwise it returns an empty array.
        /// </summary>
        /// <param name="randomWord">Current random word being played.</param>
        /// <param name="guessChar">User's guess character.</param>
        /// <returns>Int array with all indexes of found character, or empty array if not found.</returns>
        public static int[] Guess(string randomWord, char guessChar)
        {
            int[] indexArray = new int[0];

            for (int i = 0; i < randomWord.Length; i++)
            {
                if (randomWord[i] == guessChar)
                {
                    int[] tempArray = new int[(indexArray.Length + 1)];

                    for (int j = 0; j < indexArray.Length; j++)
                    {
                        tempArray[j] = indexArray[j];
                    }

                    tempArray[(indexArray.Length)] = i;
                    indexArray = tempArray;
                }
            }
            return indexArray;
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
        /// <param name="path">Path to the file storing word list.</param>
        /// <param name="deleteWordInput">Word to be deleted.</param>
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
            string[] starterWords = { "anime", "game", "coffee", "banana", "red",
                "funny", "lucky", "ducky", "faux", "bomb", "mock", "muck", "fuzzy", "jazz", "funny" };

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
