using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Tiger : Animal
    {
        public Tiger(string name, Diet diet) : base(name, diet)
        {
        }

        public override Animal Generate(Random rng)
        {
            return new Tiger(petNames[rng.Next(0, petNames.Length - 1)], Diet.Carnivore);
        }

        public void Roar()
        {
            // roar
        }
    }
}
