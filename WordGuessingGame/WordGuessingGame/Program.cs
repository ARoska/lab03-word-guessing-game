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

            do
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
                    Console.Clear();
                    AddWord(path);
                }
                else if (homeInput == "3" || homeInput.ToUpper() == "DELETE" || homeInput.ToUpper() == "DELETE WORD")
                {
                    Console.Clear();
                    DeleteWord(path);
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
            } while (run == true);
        }

        public static void LaunchGame(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";

                if ((s = sr.ReadLine()) != null)
                {
                    createSeededList(path);
                }
            }
        }

        /// <summary>
        /// Displays list of words saved in memory
        /// </summary>
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
        /// Adds a word to the list
        /// </summary>
        public static void AddWord(string path)
        {
            Regex rgx = new Regex(@"/^\S *$/");
            Console.Write("\n" +
                "Please enter a single word to add to the list: ");
            string addWordInput = Console.ReadLine();
            using (StreamWriter sw = new StreamWriter(path))
            {

            }
        }

        /// <summary>
        /// Deletes a word from the list
        /// </summary>
        public static void DeleteWord(string path)
        {

        }

        public static void createSeededList(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                string[] starterWords = { "anime", "game", "coffee", "banana", "red" };

                foreach (string word in starterWords)
                {
                    sw.WriteLine(word);
                }
            }
        }
    }
}
