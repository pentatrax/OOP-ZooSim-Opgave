using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Zookeeper
    {
        private string name;
        private int age;
        private Diet diet;
        private static Random rng = new Random();

        public string Name { get => name; }
        public int Age { get => age; }

        public Zookeeper(string name, int age, Diet diet)
        {
            this.name = name;
            this.age = age;
            this.diet = diet;
        }
        
        public bool TryFeed(Animal animal, Food food)
        {
            return animal.Eat(food);
        }
        public static Zookeeper Generate()
        {
            string[] zooKeeperNames = {
                "Alex Thompson", "Jamie Patel", "Morgan Lee", "Chris Rivera", "Taylor Kim",
                "Jordan Parker", "Sam Martinez", "Riley Johnson", "Casey Nguyen", "Drew Singh",
                "Quinn Adams", "Avery Smith", "Harper Brown", "Logan Clark", "Cameron Lewis",
                "Peyton Garcia", "Skylar Robinson", "Rowan White", "Sydney Young", "Emerson Bell"
            };
            string name = zooKeeperNames[rng.Next(0, zooKeeperNames.Length)].Split(' ')[rng.Next(0, 2)];
            name += " "+zooKeeperNames[rng.Next(0, zooKeeperNames.Length)].Split(' ')[rng.Next(0, 2)];
            return new Zookeeper(name, rng.Next(16, 100), Diet.Omnivore);
        }
        public static Zookeeper[] Generate(int amount)
        {
            Zookeeper[] zookeepers = new Zookeeper[amount];
            for (int i = 0; i < amount; i++)
            {
                zookeepers[i] = Generate();
            }
            return zookeepers;
        }
    }
}
