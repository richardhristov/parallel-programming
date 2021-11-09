using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace DBarSimulator
{
    class Bar
    {
        List<Student> students = new List<Student>();
        Semaphore semaphore = new Semaphore(10, 10);

        Random random = new Random();
        string[] drinkNames =
        {
            "coca-cola", "water", "milk", "orange juice"
        };
        Dictionary<Drink, int> drinks = new Dictionary<Drink, int>();

        bool IsClosed = false;

        public Bar()
        {
            foreach (var name in drinkNames)
            {
                drinks.Add(new Drink(name, random.Next(1, 200) * 100), random.Next(5, 1000));
            }
        }

        public Drink GetRandomDrink()
        {
            var drinksList = drinks.ToList();
            return drinksList[random.Next(0, drinksList.Count - 1)].Key;
        }

        public void RandomlyClose()
        {
            if (random.Next(0, 10000) == 4200)
            {
                CloseBar();
            }
        }

        public bool TryBuyingDrink(Drink drink)
        {
            if (IsClosed)
            {
                return false;
            }
            if (!drinks.ContainsKey(drink) || drinks[drink] < 1)
            {
                return false;
            }
            lock (drinks)
            {
                drinks[drink] -= 1;
                Console.WriteLine($"Sold {drink.Name}");
            }
            return true;
        }

        public bool Enter(Student student)
        {
            if (IsClosed)
            {
                return false;
            }
            if (random.Next(0, 100) == 5)
            {
                return false;
            }
            semaphore.WaitOne();
            if (student.Age < 18)
            {
                return false;
            }
            lock (students)
            {
                students.Add(student);
            }
            student.OnEnterBar();
            return true;
        }

        public void Leave(Student student)
        {
            lock (students)
            {
                students.Remove(student);
            }
            student.OnLeaveBar();
            semaphore.Release();
        }

        public void CloseBar()
        {
            Console.WriteLine("Closing bar");
            IsClosed = true;
            lock (students)
            {
                foreach (var student in students)
                {
                    students.Remove(student);
                    student.OnLeaveBar();
                }
            }
        }

        public void PrintStock()
        {
            Console.WriteLine("Bar's stock:");
            foreach (var drinkKv in drinks)
            {
                Console.WriteLine($"{drinkKv.Key.Name}: {drinkKv.Value}");
            }
        }
    }
}
