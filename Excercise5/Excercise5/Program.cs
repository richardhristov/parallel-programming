using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Excercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Creating facility and elevator thread");
            var facility = new Facility();
            var agents = new List<Agent>();
            for (int i = 0; i < 100; i++)
            {
                agents.Add(new Agent($"Stoyan{i}"));
            }
            Console.WriteLine("Creating elevator thread");
            var threadElevator = new Thread(facility.Elevator.Run);
            threadElevator.Start();
            Console.WriteLine("Creating agent threads");
            var threadsAgents = new List<Thread>();
            foreach (var agent in agents)
            {
                threadsAgents.Add(new Thread(agent.Tick));
            }
            Console.WriteLine("Starting agent threads");
            foreach (var thread in threadsAgents)
            {
                thread.Start(facility);
            }
            Console.WriteLine("Waiting for agent threads to join");
            foreach (var thread in threadsAgents)
            {
                thread.Join();
            }
            Console.WriteLine("Agent threads are done");
            Console.WriteLine("Program complete");
            Environment.Exit(0);
        }
    }
}
