using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal interface IFlyable
    {
        bool IsFlying { get; }
        void Fly();
        void Land();
    }
}
