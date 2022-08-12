using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustinTestProject
{
    class PlayGame
    {
        public static void myMethod()
        {

           
            string highScore = @"C:\Users\jjgueri1\Desktop\MyRepository\Database.txt";
           // string str = File.ReadAllText(highScore);

            TimeSpan currentRecord = TimeSpan.Zero;

            bool PlayAgain = true;
            while (PlayAgain)
            {

                Console.Clear();
                Console.WriteLine("Here is your letter....\n\n\n");

                Random rnd = new Random();
                char randomChar = (char)rnd.Next('a', 'z');
                int randomInt = (int)rnd.Next(1, 110);
                Console.SetCursorPosition(randomInt, randomInt);
                Console.WriteLine(randomChar);

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                char guess = Console.ReadKey().KeyChar;
                while (guess != randomChar)
                {
                    Console.Clear();
                    Console.WriteLine("NOPE!  Try again...\n\n\n");
                    randomChar = (char)rnd.Next('a', 'z');
                    randomInt = (int)rnd.Next(1, 10);
                    Console.SetCursorPosition(randomInt, randomInt);
                    Console.WriteLine(randomChar);
                    guess = Console.ReadKey().KeyChar;
                }

                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{00}.{0:00}", ts.Seconds,
                ts.Milliseconds / 10);

                if (currentRecord == TimeSpan.Zero)
                {
                    currentRecord = ts;
                    Console.Clear();
                    Console.WriteLine("That took you " + elapsedTime + " seconds!"); 
                }

                if (currentRecord > ts)
                {
                    Console.Clear();
                    Console.WriteLine("You've got a new record!!");
                    Console.WriteLine("\n\nThat took you " + elapsedTime + " seconds!");
                    currentRecord = ts;
                    string score = elapsedTime;
                    File.WriteAllText(highScore, score);
                    string str = File.ReadAllText(highScore);
                    Console.WriteLine("\nThe new best time is " + str + " seconds!");
                }

                if (currentRecord < ts)
                {
                    Console.Clear();
                    Console.WriteLine("That took you " + elapsedTime + " seconds!");               
                }
                
                Console.WriteLine("\n\n\nPlay Again?? \n\nPress Enter if YES or any key to get the hell out of this and move on with your life...");

                ConsoleKeyInfo Again;
                Again = Console.ReadKey();

                if (Again.Key != ConsoleKey.Enter)
                {
                    PlayAgain = false;
                }

            }
        }
    }
}
