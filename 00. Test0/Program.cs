using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _00.Test0
{
    class Program
    {
        static void Main(string[] args)
        {
            var dog = new Dog("Sharo");
            
            Type typeOfDog = typeof(Dog);
            var typeOfAnimal = typeof(Animal);
            Type typeOfMops = typeof(Mops);
            Type typeOfDogII = dog.GetType();
            var unknown = typeOfDogII.ReflectedType;
            var unknownII = typeOfDog.IsVisible;


            Console.WriteLine(typeOfDog);
            Console.WriteLine(unknownII);
            Console.WriteLine($"IS Animal visible -> {typeOfAnimal.IsVisible}");
            Console.WriteLine($"IS Animal abstract -> {typeOfAnimal.IsAbstract}");
            Console.WriteLine($"IS dog instance of animal class -> {typeOfAnimal.IsInstanceOfType(dog)}");
            Console.WriteLine($"IS Animal not public -> {typeOfAnimal.IsNotPublic}");
            Console.WriteLine($"IS Dog not public -> {typeOfDogII.IsNotPublic}");
            Console.WriteLine($"IS Animal Serializable -> {typeOfAnimal.IsSerializable}");
            Console.WriteLine($"IS Dog Serializable -> {typeOfDogII.IsSerializable}");
            Console.WriteLine($"IS Dog subclass of Animal -> {typeOfDogII.IsSubclassOf(typeOfAnimal)}");
            Console.WriteLine($"Which is the base class of Dog -> {typeOfDog.BaseType.Name}");
            Console.WriteLine("Printing the properties of class Animal");

            foreach (var prop in typeOfAnimal.GetProperties())
            {
                Console.WriteLine(prop.Name + " " + prop.PropertyType.Name + " " + prop.SetMethod + " " + prop.CanWrite);
            }


            Console.WriteLine("printing class hierarchy:");

            var baseType = typeOfMops.BaseType;

            while (baseType!=typeof(object))
            {
                Console.WriteLine(baseType.Name);
                baseType = baseType.BaseType;
            }

            // how to get all classes
            Console.WriteLine("Getting all classes");
            var classes = Assembly.GetEntryAssembly().GetTypes();
            foreach (var item in classes)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            var constructors = Assembly.GetEntryAssembly().Modules;
            //Console.WriteLine(constructors);
            foreach (var item in constructors)
            {
                Console.WriteLine(item.Name);
            }


        }
    }

    
}
