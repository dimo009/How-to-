using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            while (baseType != typeof(object))
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

            Console.WriteLine();
            Console.WriteLine("How to istantiate anything...?");

            // След тайпъф, трябва да се подадат елементите на конструктора
            var dogActivator = (Dog)Activator.CreateInstance(typeof(Dog), "Joro");

            //обаче

            Console.WriteLine(dogActivator.ToString());

            //var dogWithNewInstance = New<Dog>.Instance;
            //dogWithNewInstance.Name = "Kiro";
            //Console.WriteLine(dogWithNewInstance.ToString());


            // Hoow to take info regarding classes, properties etc
            var fields = typeOfAnimal.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                Console.WriteLine(field.Name);
            }



            // How to set value of a field:

            // 1. Create a new instance of a class

            var dogRoro = new Dog("Roro");

            //2. Find the fileds

            var dogFields = typeOfAnimal.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in dogFields)
            {
                Console.WriteLine(field.Name);

                if (field.Name.Contains("name"))
                {
                    Console.WriteLine("First value of the name of the dog");
                    Console.WriteLine(dogRoro.Name);
                    field.SetValue(dogRoro, "Misho");
                    Console.WriteLine("New value of dog's name");
                    Console.WriteLine(dogRoro.Name);
                }
            }
            Console.WriteLine();
            //How to get the proeprties of a class

            //1. 
            PropertyInfo[] properties = typeOfDog.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name);
                if (prop.Name.ToLower().Contains("name"))
                {
                    prop.SetValue(dogRoro, "Spas");
                    Console.WriteLine(dogRoro.Name);
                }

            }

            // How to get info about the constructors

            var typeOfWhale = typeof(Whale);

            ConstructorInfo[] cons = typeOfWhale.GetConstructors();

            foreach (var constructor in cons)
            {
               
                var parametars = constructor.GetParameters();

                foreach (var param in parametars)
                {
                   
                    Console.WriteLine(param.Name);
                    Console.WriteLine(param.ParameterType.Name);
                }
                Console.WriteLine("-------");

            }

            // Calling construktors

            //1. Empty
            var emptyConstructor = typeof(Whale).GetConstructor(Type.EmptyTypes);

            //2. Constructor with parameters

            var constructorWithParametars = typeof(Whale).GetConstructor(new[] { typeof(string), typeof(double) });

            var whale = constructorWithParametars.Invoke(new object[] { "Kosatka", 4.5 });



            //how to get the methods

            MethodInfo []methods = typeof(Whale).GetMethods(BindingFlags.Instance|BindingFlags.NonPublic);
            var whaleInstance = new Whale("Kosatka",7.9);
            var invokedMethod = methods.Where(m => m.Name == "MakeSound").First().Invoke(whaleInstance, new object[0]); //new object[0] защото методът няма параметри
            Console.WriteLine(invokedMethod);

            //Ако методът приема параметри

            var invokedMethodII = methods.Where(m => m.Name == "CalculateWeight").First().Invoke(whaleInstance, new object[] { 8.9 });
            Console.WriteLine(invokedMethodII);
        }




        // How to instantiate new instance of a class
        //Option one - consumes memory and works only with empty constructor
        public class New<T>
        {
            public static T Instance = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile()();
        }

        
       

      

    }

    
}
