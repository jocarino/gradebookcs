using System.Collections.Generic;
using System;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void addGrade(double grade);
        Statistics GetStatistics();
        string Name {get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
            Name = name;
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void addGrade(double grade); // the exact implementation idk yet

        public abstract Statistics GetStatistics(); // later this method can be overrided

    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            Name = name;
        }

        public override void  addGrade(double grade)
        {
            string path = $"{this.Name}.txt";
            using (StreamWriter writer = File.AppendText(path))//after using this disposes
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }

        public override event GradeAddedDelegate GradeAdded;

    }
    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void addGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    addGrade(90);
                    break;
                case 'B':
                    addGrade(80);
                    break;
                case 'C':
                    addGrade(70);
                    break;
                case 'D':
                    addGrade(60);
                    break;
                default:
                    addGrade(0);
                    break;
            }
        }
        public override void addGrade(double grade)
        {
            if(grade <= 100 && grade >=0)
            {
                grades.Add(grade);
                if(GradeAdded != null) //check if someone is listening
                {
                    GradeAdded(this, new EventArgs()); //raising the event
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            foreach(var grade in grades)
            {
                result.Add(grade);
            }
        
            return result;
        }

        public override event GradeAddedDelegate GradeAdded; // fiel in the book class



        private List<double> grades;
        
        //automatic readonly propriety, enherits from namedobject class
        // public string Name
        // {
        //     get;
        //     set;
        // }
        /*
        // proprety
        public string  Name 
        {
            get
            {
                return name; //gets the private type
            }
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    name = value;
                }
                
            }
        }
        private string name; // private backing field */

        //readonly string category = "science";
        public const string CATEGORY = "science";
    }


}