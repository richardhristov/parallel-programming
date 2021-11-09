using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DBarSimulator
{
    class Student
    {
        enum NightlifeActivities { Walk, VisitBar, GoHome };
        enum BarActivities { Drink, Dance, Leave };

        Random random = new Random();

        public string Name { get; set; }
        public Bar Bar { get; set; }

        public int Age { get; set; }

        int BudgetCents { get; set; }

        bool IsInBar = false;

        private NightlifeActivities GetRandomNightlifeActivity()
        {
            int n = random.Next(10);
            if (n < 3) return NightlifeActivities.Walk;
            if (n < 8) return NightlifeActivities.VisitBar;
            return NightlifeActivities.GoHome;
        }

        private BarActivities GetRandomBarActivity()
        {
            int n = random.Next(10);
            if (n < 4) return BarActivities.Dance;
            if (n < 9) return BarActivities.Drink;
            return BarActivities.Leave;
        }

        private void WalkOut()
        {
            Console.WriteLine($"{Name} is walking in the streets.");
            Thread.Sleep(100);
        }

        private void VisitBar()
        {
            Console.WriteLine($"{Name} is getting in the line to enter the bar.");
            var success = Bar.Enter(this);
            if (!success)
            {
                Console.WriteLine($"{Name} failed to get into the bar!");
                return;
            }
            Console.WriteLine($"{Name} entered the bar!");
            while (IsInBar)
            {
                var nextActivity = GetRandomBarActivity();
                switch (nextActivity)
                {
                    case BarActivities.Dance:
                        Console.WriteLine($"{Name} is dancing.");
                        Thread.Sleep(100);
                        break;
                    case BarActivities.Drink:
                        var randomDrink = Bar.GetRandomDrink();
                        if (randomDrink.PriceCents > BudgetCents)
                        {
                            Console.WriteLine($"{Name} tried to buy a random drink but it was too expensive.");
                        }
                        else if(!Bar.TryBuyingDrink(randomDrink))
                        {
                            Console.WriteLine($"{Name} failed to buy a drink.");
                        }
                        else
                        {
                            BudgetCents -= randomDrink.PriceCents;
                            Console.WriteLine($"{Name} is drinking {randomDrink.Name}.");
                        }
                        Thread.Sleep(100);
                        break;
                    case BarActivities.Leave:
                        Console.WriteLine($"{Name} is leaving the bar.");
                        Bar.Leave(this);
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public void PaintTheTownRed()
        {
            WalkOut();
            bool staysOut = true;
            while (staysOut)
            {
                var nextActivity = GetRandomNightlifeActivity();
                switch (nextActivity)
                {
                    case NightlifeActivities.Walk:
                        WalkOut();
                        break;
                    case NightlifeActivities.VisitBar:
                        VisitBar();
                        staysOut = false;
                        break;
                    case NightlifeActivities.GoHome:
                        staysOut = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }

        public void OnEnterBar()
        {
            IsInBar = true;
        }

        public void OnLeaveBar()
        {
            IsInBar = false;
        }

        public Student(string name, Bar bar)
        {
            Name = name;
            Bar = bar;
            Age = random.Next(14, 60);
            BudgetCents = random.Next(10, 200) * 100;
        }
    }
}
