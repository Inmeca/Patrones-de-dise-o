using System;

namespace Prototype
{
    public class Perro
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Perro ShallowCopy()
        {
            return (Perro)this.MemberwiseClone();
        }

        public Perro DeepCopy()
        {
            Perro clone = (Perro)this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Perro perro1 = new Perro();
            perro1.Age = 4;
            perro1.BirthDate = Convert.ToDateTime("2016-06-06");
            perro1.Name = "Ruki";
            perro1.IdInfo = new IdInfo(111);

            // Perform a shallow copy of p1 and assign it to p2.
            Perro perro2 = perro1.ShallowCopy();
            // Make a deep copy of p1 and assign it to p3.
            Perro perro3 = perro1.DeepCopy();

            // Display values of p1, p2 and p3.
            Console.WriteLine("Original values of perro1, perro2, perro3:");
            Console.WriteLine("   perro1 instance values: ");
            DisplayValues(perro1);
            Console.WriteLine("   perro2 instance values:");
            DisplayValues(perro2);
            Console.WriteLine("   perro3 instance values:");
            DisplayValues(perro3);

            // Change the value of p1 properties and display the values of p1,
            // p2 and p3.
            perro1.Age = 1;
            perro1.BirthDate = Convert.ToDateTime("2020-01-01");
            perro1.Name = "Polo";
            perro1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nValues of perro1, perro2 and perro3 after changes to perro1:");
            Console.WriteLine("   perro1 instance values: ");
            DisplayValues(perro1);
            Console.WriteLine("   perro2 instance values (reference values have changed):");
            DisplayValues(perro2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayValues(perro3);
        }

        public static void DisplayValues(Perro p)
        {
            Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.Name, p.Age, p.BirthDate);
            Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
        }
    }
}