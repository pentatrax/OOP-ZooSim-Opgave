using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal static class ZooManager
    {
        static private Zoo zoo;

        static public void GenerateZoo()
        {
            zoo = new Zoo(5);
            
        }
    }
}
