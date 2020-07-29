using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Joao's Grade Book");
            book.addGrade(89.1);
            book.addGrade(90.5);
            book.addGrade(77.5);
            
            var stats = book.GetStatistics();

            System.Console.WriteLine($"The average grade is {stats.Average:N1}, the highest is {stats.High:N1} and the lowest is {stats.Low:N1}.");

        }


    }
}
