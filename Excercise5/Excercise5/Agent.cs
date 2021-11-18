using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excercise5
{
    public enum SecurityClearance
    {
        Confidential = 0,
        Secret = 1,
        TopSecret = 2,
    };

    class Agent
    {
        public Facility Facility { get; internal set; }
        public string Name { get; }
        public SecurityClearance SecurityClearance { get; }

        Random _rand;
        Floor? _floor;

        public Agent(string name, SecurityClearance securityClearance)
        {
            Name = name;
            SecurityClearance = securityClearance;
            _rand = new Random();
            _floor = null;
        }

        public Agent(string name)
        {
            Name = name;
            _rand = new Random();
            SecurityClearance = (SecurityClearance)_rand.Next(0, 2);
            _floor = null;
        }

        public bool IsInsideElevator()
        {
            return Facility?.Elevator.Agent == this;
        }

        public bool TriesToGoToFloor(Floor targetFloor)
        {
            if (Facility == null)
            {
                return false;
            }
            while(_floor != targetFloor)
            {
                if (!IsInsideElevator())
                {
                    if (_floor == null)
                    {
                        throw new Exception($"Agent {Name} is not inside the elevator but it's floor is null");
                    }
                    while (Facility.Elevator.Floor != _floor)
                    {
                        //Console.WriteLine($"Agent {Name} is calling the elevator to floor {floor}");
                        Facility.Elevator.ElevatorIsCalledOrButtonClicked(_floor.Value);
                        Thread.Sleep(_rand.Next(250, 10000));
                    }
                    Facility.Elevator.AgentTriesToEnter(this);
                }
                else
                {
                    _floor = null;
                    while (Facility.Elevator.Floor != targetFloor)
                    {
                        Facility.Elevator.ElevatorIsCalledOrButtonClicked(targetFloor);
                    }
                    var isSuccess = Facility.Elevator.AgentTriesToLeave();
                    if (isSuccess)
                    {
                        _floor = targetFloor;
                        Console.WriteLine($"Agent {Name} left the elevator and is now at floor {_floor}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Agent {Name} was not let out of the elevator");
                        return false;
                    }
                }
            }
            return false;
        }

        public void TriesToLeave()
        {
            if (Facility == null)
            {
                return;
            }
            while (true)
            {
                if (TriesToGoToFloor(Floor.G))
                {
                    Facility = null;
                    break;
                }
                Thread.Sleep(5000);
            }
        }

        public void Tick(object facility)
        {
            while (Facility == null)
            {
                if (_rand.Next(0, 100) < 5)
                {
                    Console.WriteLine($"Agent {Name} decided to enter the facility");
                    Facility = (Facility)facility;
                    _floor = Floor.G;
                }
            }
            while (Facility != null)
            {
                if (_rand.Next(0, 100) < 5)
                {
                    Console.WriteLine($"Agent {Name} decided to leave the facility");
                    TriesToLeave();
                }
                else
                {
                    if (_rand.Next(0, 100) < 50 && _floor != null)
                    {
                        Console.WriteLine($"Agent {Name} decided to meander at floor {_floor}");
                        Thread.Sleep(_rand.Next(10000, 30000));
                    }
                    else
                    {
                        var targetFloor = (Floor)_rand.Next(-3, 0);
                        while (_floor == targetFloor)
                        {
                            targetFloor = (Floor)_rand.Next(-3, 0);
                        }
                        Console.WriteLine($"Agent {Name} decided to try going to floor {targetFloor}");
                        TriesToGoToFloor(targetFloor);
                    }
                }
            }
        }
    }
}
