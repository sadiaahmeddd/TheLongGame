using System;
using System.IO; // reading and writing files

namespace TheLongGame
{
    internal class Program
    {
        /// <summary>
        /// Entry point for the program.
        /// Plays "The Long Game": user presses keys to gain points,
        /// presses Enter to end the game, and the username + score are
        /// saved to and read from a file.
        /// </summary>
        /// 

        static void Main(string[] args)
        {
            // --- 1. the username from the user and store in a variable ---
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            // Make sure we hav s a username
            if (string.IsNullOrWhiteSpace(username))
            {
                username = "UnknownPlayer";
            }

            // --- 2. Initialize score variable ---
            int score = 0;

            Console.WriteLine();
            Console.WriteLine($"Welcome, {username}!");
            Console.WriteLine("The Long Game has begun...");
            Console.WriteLine("Press ANY key to earn points.");
            Console.WriteLine("Press ENTER to end the game.");
            Console.WriteLine();

            // --- 3. Game loop: increase points on keypress & show updated score ---
            while (true)
            {
                // key press 
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                // If the user presses Enter, we end the game
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Console.WriteLine("You pressed ENTER. Ending the game...");
                    break;
                }

                // For any other key, increase score and display it
                score++; // Increase points when a keypress is made
                Console.WriteLine($"Current score: {score}");
            }

            Console.WriteLine();
            Console.WriteLine($"Game over, {username}! Final score this round: {score}");
            Console.WriteLine();

            // --- 4. Save username and score to a file (file I/O - write) ---
            string filePath = "scores.txt"; // File will live next to the .exe

            try




            {
                // Append a new line with "username,score"
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"{username},{score}");
                }

                Console.WriteLine("Saved your username and score to scores.txt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while saving to file: " + ex.Message);
            }





            // --- 5. Read from the file and store data into local variables ---
            try
            {
                string lastLine = null;

                // Read the file line by line and remember the last line we see
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;


                    }
                }

                if (!string.IsNullOrEmpty(lastLine))
                {
                    // lastLine should look like "username,score"
                    string[] parts = lastLine.Split(',');

                    if (parts.Length == 2 && int.TryParse(parts[1], out int fileScore))
                    {
                        // Store into local variables as the rubric requires
                        string fileUsername = parts[0];

                        Console.WriteLine();

                        Console.WriteLine("Most recent entry loaded FROM FILE:");
                        Console.WriteLine($"Username: {fileUsername}");

                        Console.WriteLine($"Score: {fileScore}");
                    }
                    else
                    {
                        Console.WriteLine("File format was not what was expected.");
                    }
                }
                else
                {
                    Console.WriteLine("scores.txt was empty after saving.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading from file: " + ex.Message);
            }



            Console.WriteLine();

            Console.WriteLine("Press any key to exit...");




            Console.ReadKey();
        }
    }
}
