using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Zebra : Animal
    {
        public override void Generate()
        {
            this.name = petNames[rng.Next(0, petNames.Length)];
            this.diet = Diet.Herbivore;
            this.health = 100;
            this.hunger = 0;
        }

        public void Bark()
        {
            //bark
        }
    }
}
