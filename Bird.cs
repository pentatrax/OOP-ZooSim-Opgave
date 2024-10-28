using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Bird : Animal, IFlyable
    {
        private bool isFlying;
        public Bird(string name, Diet diet) : base(name, diet)
        {
        }

        public bool IsFlying { get => isFlying; }

        public void Fly()
        {
            isFlying = true;
        }

        public override Animal Generate(Random rng)
        {
            return new Bird(petNames[rng.Next(0, petNames.Length - 1)], Diet.Herbivore);
        }

        public void Land()
        {
            isFlying = false;
        }
        public void Chirp()
        {
            //chirp
        }
    }
}
