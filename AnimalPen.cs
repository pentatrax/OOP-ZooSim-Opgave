using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class AnimalPen
    {
        private HashSet<Animal> animals;

        public HashSet<Animal> Animals { get => animals; }

        public AnimalPen()
        {
            this.animals = new HashSet<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }
        public void RemoveAnimal(Animal animal)
        {
            animals.Remove(animal);
        }
        public Animal[] GetAnimals()
        {
            return animals.ToArray();
        }
    }
}
