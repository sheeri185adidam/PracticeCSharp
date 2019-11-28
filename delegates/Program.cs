using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{
    delegate int CustomDel(string s);

    public abstract class Animal
    {
	    public abstract string Name { get; } 
    }

    public class Dog : Animal
    {
	    public override string Name => @"Dog";
    }

    delegate Animal AnimalGetter(string s);

    class Program
    {
        static int Hello(string s)
        {
            System.Console.WriteLine("  Hello, {0}!", s);
            return 99;
        }

        static int Goodbye(string s)
        {
            System.Console.WriteLine("  Goodbye, {0}!", s);
            return 100;
        }

        static Animal GetAnimal(string s)
        {
			return new Dog();
        }

        static void Main(string[] args)
        {
            CustomDel multiDel = new CustomDel(Hello);
            multiDel += Goodbye;
            Console.WriteLine(multiDel("ngp"));

			AnimalGetter animalGetter = new AnimalGetter(GetAnimal);
			animalGetter += GetAnimal;
			Console.WriteLine(animalGetter(""));
        }
    }
}
