using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excercise5
{
    class Elevator
    {
        public Agent Agent { get; internal set; }
        public Floor Floor { get; internal set; }
        public Floor? TargetFloor { get; internal set; }

        object _lock;

        public Elevator()
        {
            Floor = Floor.G;
            TargetFloor = null;
            _lock = new object();
        }

        public bool AgentTriesToEnter(Agent agent)
        {
            if (Agent != null)
            {
                return false;
            }
            lock (_lock)
            {
                if (Agent != null)
                {
                    Console.WriteLine($"{agent.Name} tried to enter but {Agent.Name} is already in the elevator");
                    return false;
                }
                Agent = agent;
                return true;
            }
        }

        public bool AgentTriesToLeave()
        {
            lock (_lock)
            {
                if (Agent == null)
                {
                    throw new Exception("Agent tried to leave the elevator but there was no one in the elevator");
                }
                if (!DoorCanOpen())
                {
                    return false;
                }
                Agent = null;
                return true;
            }
        }

        public bool IsAtTargetFloor()
        {
            return Floor == TargetFloor || TargetFloor == null;
        }

        public void MoveTowardsFloor()
        {
            if (IsAtTargetFloor())
            {
                return;
            }
            lock (_lock)
            {
                while (!IsAtTargetFloor())
                {
                    Thread.Sleep(1000);
                    if (TargetFloor > Floor)
                    {
                        Floor += 1;
                        Console.WriteLine($"Elevator moved up to {Floor}");
                    } 
                    else
                    {
                        Floor -= 1;
                        Console.WriteLine($"Elevator moved down to {Floor}");
                    }
                }
                Console.WriteLine($"Elevator has arrived at {TargetFloor}");
                TargetFloor = null;
            }
        }

        public void ElevatorIsCalledOrButtonClicked(Floor targetFloor)
        {
            if (Floor == targetFloor || TargetFloor != null)
            {
                return;
            }
            lock (_lock)
            {
                if (Floor == targetFloor || TargetFloor != null)
                {
                    return;
                }
                TargetFloor = targetFloor;
                Console.WriteLine($"Elevator has changed it's target floor to {TargetFloor}");
            }
        }

        public bool DoorCanOpen()
        {
            if (Agent == null)
            {
                return true;
            }
            switch (Agent.SecurityClearance)
            {
                case SecurityClearance.Confidential:
                    return Floor == Floor.G;
                case SecurityClearance.Secret:
                    return Floor == Floor.G || Floor == Floor.S;
                case SecurityClearance.TopSecret:
                    return true;
            }
            return false;
        }

        public void Run()
        {
            while (true)
            {
                MoveTowardsFloor();
            }
        }
    }
}
