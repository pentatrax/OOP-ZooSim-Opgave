using System;

namespace OOP_ZooSim_Opgave
{
    internal abstract class Animal
    {
        protected static Random rng = new Random();
        protected string name;
        protected int health;
        protected int hunger;
        protected Mood mood;
        protected Diet diet;
        static protected string[] petNames = {
            "Bella", "Max", "Charlie", "Luna", "Rocky", "Milo", "Lucy", "Daisy", "Bailey", "Sadie",
            "Coco", "Buddy", "Ruby", "Zoe", "Loki", "Buster", "Sasha", "Maggie", "Oliver", "Jack",
            "Oscar", "Chloe", "Duke", "Simba", "Rex", "Piper", "Leo", "Toby", "Finn", "Molly",
            "Koda", "Lily", "Sammy", "Winston", "Oreo", "Zeus", "Benny", "Scout", "Teddy", "Rosie",
            "Tucker", "Lola", "Roxy", "Jake", "Gizmo", "Baxter", "Nala", "Jasper", "Bentley", "Ginger",
            "Hazel", "Moose", "Peanut", "Blue", "Ellie", "Harley", "Shadow", "Minnie", "Frankie", "Belle",
            "Milo", "Hank", "Rusty", "Ranger", "Pepper", "Simba", "Hunter", "Athena", "Ace", "Sugar",
            "Lilly", "Tank", "Cali", "Marley", "Boomer", "Benji", "Diesel", "Raven", "Princess", "Riley",
            "Louie", "Penny", "Dakota", "Copper", "Phoebe", "Casper", "Honey", "Sheba", "Romeo", "Sadie",
            "Mimi", "Chief", "Zelda", "Thor", "Waffles", "Blaze", "Bruno", "Mochi", "Chester", "Sassy"
        };

        public string Name { get => name; }
        public bool IsAlive { get => (health > 0); }
        public int Hunger { get => hunger; private set => hunger = (value <= 100 && value >= 0) ? value : (value > 100) ? 100 : 0; } // Set limited to 0-100
        public int Health { get => health; private set => health = (value <= 100 && value >= 0) ? value : (value > 100) ? 100 : 0; } // Set limited to 0-100
        public Diet Diet { get => diet; }
        public Mood Mood { get => mood; private set => mood = (Mood)(((int)value <= 3 && (int)value >= 0) ? value : (Mood)(((int)value > 3) ? 3 : 0)); } // Set limited to 4 values 0-3

        public Animal(string name, Diet diet)
        {
            this.name = name;
            this.diet = diet;
            this.health = 100;
            this.hunger = 0;
            this.mood = Mood.Relaxed;
        }
        public Animal()
        {
            Generate();
        }

        public virtual bool Eat(Food food)
        {
            bool ate = false;
            if ((food.Diet == diet || diet == Diet.Omnivore) && hunger > 0)
            {
                Hunger -= food.Saturation;
                ate = true;
            }
            return ate;
        }
        public bool Eat(Food food, int amount)
        {
            bool ate = false;
            for (int i = 0; i < amount; i++)
            {
                ate = Eat(food);
            }
            return ate;
        }
        public void Sleep()
        {
            health -= 5;
            if (Hunger < 100)
            {
                Hunger += 100 - Health;
                Health += Hunger;
            }
            else
            {
                Health -= 10;

            }
            mood = (Mood)(Hunger / 25f);
        }
        public abstract void Generate();

    }
}
