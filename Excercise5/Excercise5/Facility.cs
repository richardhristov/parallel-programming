using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise5
{
    enum Floor
    {
        G = 0,
        S = -1,
        T1 = -2,
        T2 = -3,
    };

    class Facility
    {
        public Elevator Elevator { get; }

        public Facility()
        {
            Elevator = new Elevator();
        }
    }
}
