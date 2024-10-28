using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Zoo
    {
        private HashSet<Zookeeper> zookeepers;
        private AnimalPen[] animalPens;
        private HashSet<Animal> animals;
        private Dictionary<string, Food> fodder;

        public HashSet<Zookeeper> Zookeepers { get => zookeepers; }
        private AnimalPen[] AnimalPens { get => animalPens; }
        private HashSet<Animal> Animals { get => animals; }
        private Dictionary<string, Food> Fodder { get => fodder; }

        public Zoo(int amountOfPens)
        {
            animalPens = new AnimalPen[amountOfPens];
            for (int i = 0; i < amountOfPens; i++)
            {
                animalPens[i] = new AnimalPen();
            }
        }

        public bool BuyAnimal(Animal animal)
        {
            bool success = false;

            return success;
        }
    }
}
