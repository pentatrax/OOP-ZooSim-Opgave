using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ZooSim_Opgave
{
    internal class Food
    {
        private string name;
        private Diet diet;
        private int saturation;
        private static Random rng = new Random();

        public int Saturation { get => saturation; }
        public Diet Diet { get => diet; }
        public string Name { get => name; }

        public Food(string name, int saturation, Diet diet)
        {
            this.name = name;
            this.saturation = saturation;
            this.diet = diet;
        }
        public static Food Generate()
        {
            string[] fodderItems = new string[]
            {
                "Hay", "Grass", "Alfalfa", "Corn Silage", "Soybean Meal",
                "Wheat Bran", "Barley", "Oats", "Cottonseed Meal", "Sorghum",
                "Chicken Meal", "Fish Meal", "Meat Scraps", "Bone Meal", "Blood Meal",
                "Clover", "Lucerne", "Turnips", "Carrots", "Molasses"
            };

            // Define an array to hold Diet enum values for each fodder item
            Diet[] dietType = new Diet[fodderItems.Length];

            // Populate dietType array based on the type of each fodder item
            for (int i = 0; i < fodderItems.Length; i++)
            {
                if (fodderItems[i] == "Chicken Meal" || fodderItems[i] == "Fish Meal" ||
                    fodderItems[i] == "Meat Scraps" || fodderItems[i] == "Bone Meal" ||
                    fodderItems[i] == "Blood Meal")
                {
                    dietType[i] = Diet.Carnivore;
                }
                else
                {
                    dietType[i] = Diet.Herbivore;
                }
            }
            int index = rng.Next(0, fodderItems.Length - 1);
            return new Food(fodderItems[index], rng.Next(20, 76), dietType[index]);
        }
        public static Food[] Generate(int amount)
        {
            if (amount > 20) amount = 20;
            Food[] foods = new Food[amount];
            for (int i = 0; i < amount; i++)
            {
                Food food = Generate();
                if (!foods.Contains<Food>(food))
                {
                    foods[i] = food;
                } else
                {
                    i -= 1;
                }
            }
            return foods;
        }
    }
}
