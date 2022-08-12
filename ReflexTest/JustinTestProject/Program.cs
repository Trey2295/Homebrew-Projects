using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlTypes;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

namespace JustinTestProject
{
    class Program 
    {
        static void Main(string[] args)
        {

            
            Console.WriteLine("Hello, and Welcome to this Fantastic Program!");
            Console.WriteLine("\n\nYou will be displayed a letter, and we will time how fast it takes for you to enter it on your keyboard");
            string highScore = @"C:\Users\jjgueri1\Desktop\MyRepository\Database.txt";

            if (File.Exists(highScore))
            {
                // Read all the content in one string 
                // and display the string 
                string str = File.ReadAllText(highScore);
                Console.WriteLine("\nThe best current time is " + str + " seconds!");
            }

            Console.WriteLine("\n\nAre you ready?? \n\n\nPress Enter if so...");

            ConsoleKeyInfo Enter;
            //TimeSpan currentRecord = TimeSpan.Zero;

            Enter = Console.ReadKey();
            while (Enter.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("You must hit Enter!");
                Enter = Console.ReadKey();
            }
            PlayGame.myMethod();
  
        }
    }
}
