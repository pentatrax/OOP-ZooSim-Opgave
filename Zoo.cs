using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_ZooSim_Opgave
{
    internal static class Zoo
    {
        private static HashSet<Zookeeper> zookeepers = new HashSet<Zookeeper>();
        private static HashSet<AnimalPen> animalPens = new HashSet<AnimalPen>();
        private static HashSet<Animal> animals = new HashSet<Animal>();
        private static Dictionary<string, Food> fodder = new Dictionary<string, Food>();
        private static int sleepProgress = 0;

        public static HashSet<Zookeeper> Zookeepers { get => zookeepers; }
        public static HashSet<AnimalPen> AnimalPens { get => animalPens; }
        public static HashSet<Animal> Animals { get => animals; }
        public static Dictionary<string, Food> Fodder { get => fodder; }

        public static bool BuyAnimal(Animal animal)
        {
            if (animal != null)
            {
                animals.Add(animal);
            } else
            {
                ThrowInputError();
                return false;
            }
            return true;
        }
        public static bool SellAnimal(Animal animal)
        {
            if (animal != null)
            {
                foreach (AnimalPen pen in animalPens)
                {
                    if (pen.GetAnimals().Contains(animal)) pen.RemoveAnimal(animal);
                }
                animals.Remove(animal);
            } else
            {
                ThrowInputError();
                return false;
            }
            return true;
        }
        public static void FireZookeeper(Zookeeper keeper)
        {
            if (keeper != null)
            {
                zookeepers.Remove(keeper);
            }
            else
            {
                ThrowInputError();
            }
        }
        public static void HireZookeeper(Zookeeper keeper)
        {
            if (keeper != null)
            {
                zookeepers.Add(keeper);
            } else
            {
                ThrowInputError();
            }
        }
        public static bool BuyFodder(Food food)
        {
            bool success = true;
            if (food != null)
            {
                if (fodder.ContainsKey(food.Name))
                {
                    // increment amount instead
                }
                else
                {
                    fodder.Add(food.Name, food);
                }
            } else
            {
                ThrowInputError();
                success = false;
            }
            return success;
        }
        public static void TellKeeperToFeedAnimal(Zookeeper keeper, Animal animal, Food food)
        {
            if (keeper != null && animal != null && food != null)
            {
                keeper.TryFeed(animal, food);
            } else
            {
                ThrowInputError();
            }
        }
        public static bool MoveAnimalToPen(Animal animal, AnimalPen animalPen)
        {
            if (animal != null && animalPen != null)
            {
                if (animalPen.Animals.Count <= 0 || animalPen.GetAnimals().First().Diet == animal.Diet)
                {
                    foreach (AnimalPen pen in AnimalPens)
                    {
                        pen.RemoveAnimal(animal);
                    }
                    animalPen.AddAnimal(animal);
                }
                else
                {
                    MessageBox.Show("It's a bad idea to put animals of different types in the same pen!", "IQ Drop Detected", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            } else
            {
                ThrowInputError();
                return false;
            }
            return true;
        }
        public static void Generate()
        {
            zookeepers.Add(Zookeeper.Generate());
            zookeepers.Add(Zookeeper.Generate());
            animalPens.Add(new AnimalPen());
            animalPens.Add(new AnimalPen());
            animalPens.Add(new AnimalPen());
            animals.Add(new Tiger());
            animals.Add(new Tiger());
            animals.Add(new Bird());
            animals.Add(new Bird());
            animals.Add(new Zebra());
            animals.Add(new Zebra());
            Food[] foods = Food.Generate(20);
            foreach (Food food in foods)
            {
                BuyFodder(food);
            }

        }

        public static Zookeeper GetZookeeperByName(string name)
        {
            foreach (Zookeeper zookeeper in zookeepers)
            {
                if (zookeeper.Name == name) return zookeeper;
            }
            return null;
        }
        public static Zookeeper GetZookeeperByName(ICollection<Zookeeper> collection, string name)
        {
            foreach (Zookeeper zookeeper in collection)
            {
                if (zookeeper.Name == name) return zookeeper;
            }
            return null;
        }

        public static AnimalPen GetPenByHash(int hash)
        {
            foreach (AnimalPen pen in animalPens)
            {
                if (pen.GetHashCode() == hash) return pen;
            }
            return null;
        }
        public static Animal GetAnimalByName(string name)
        {
            foreach (Animal animal in animals)
            {
                if (animal.Name == name) return animal;
            }
            return null;
        }
        public static Animal GetAnimalByName(ICollection<Animal> collection, string name)
        {
            foreach (Animal animal in collection)
            {
                if (animal.Name == name) return animal;
            }
            return null;
        }
        public static Food GetFoodByName(string name)
        {
            if (fodder.ContainsKey(name))
            {
                return fodder[name];
            } else
            {
                MessageBox.Show("One or more attributes have not been set!", "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            return null;
        }
        public static void ProgressSleep()
        {
            sleepProgress++;
            if (sleepProgress >= 5)
            {
                foreach (Animal animal in animals)
                {
                    animal.Sleep();
                }
                sleepProgress = 0;
            }
        }

        private static void ThrowInputError()
        {
            MessageBox.Show("One or more attributes have not been set!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
