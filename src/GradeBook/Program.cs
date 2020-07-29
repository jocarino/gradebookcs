using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Joao's Grade Book");
            book.GradeAdded += OnGradeAdded; //subscribing to the event

            EnterGrades(book);

            var stats = book.GetStatistics();

            System.Console.WriteLine($"For the book named {book.Name}");
            System.Console.WriteLine($"The average grade is {stats.Average:N1}.");
            System.Console.WriteLine($"The highest grade is {stats.High:N1}.");
            System.Console.WriteLine($"The lowest grade is {stats.Low:N1}.");
            System.Console.WriteLine($"The Letter grade is {stats.Letter}");

        }

        private static void EnterGrades(IBook book)
        {
            do
            {
                System.Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q") { break; } //quit command

                try
                {
                    var grade = double.Parse(input);
                    book.addGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    System.Console.WriteLine("**"); //run after exceptions (like closing a file)
                }

            } while (true);
        }

        static void OnGradeAdded(object sender, EventArgs e) //static method that handles the event
        {
            System.Console.WriteLine("A grade was added");
        }

    }
}
