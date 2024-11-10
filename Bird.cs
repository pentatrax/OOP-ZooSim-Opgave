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

        public bool IsFlying { get => isFlying; }

        public void Fly()
        {
            isFlying = true;
        }

        public override void Generate()
        {
            this.name = petNames[rng.Next(0, petNames.Length)];
            this.diet = Diet.Omnivore;
            this.health = 100;
            this.hunger = 0;
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
