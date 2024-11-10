using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OOP_ZooSim_Opgave
{
    public class ZooManager
    {
        private MainWindow window;
        private string[] selections;
        private List<Zookeeper> tempZookeepers = new List<Zookeeper>();
        private List<Animal> tempAnimals = new List<Animal>();

        public ZooManager(MainWindow window)
        {
            this.window = window;
            this.selections = new string[5];
            for (int i = 0; i < selections.Length; i++)
            {
                selections[i] = "None";
            }
            Zoo.Generate();
            PopulateWindow();
            window.Zookeepers.SelectionChanged += Zookeepers_SelectionChanged;
            window.Animals.SelectionChanged += Animals_SelectionChanged;
            window.Action.SelectionChanged += Action_SelectionChanged;
            window.AnimalPens.SelectionChanged += AnimalPens_SelectionChanged;
            window.Fodder.SelectionChanged += Fodder_SelectionChanged;
            window.CommitButton.Click += CommitButton_Click;
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            Zoo.ProgressSleep();
            UpdateAnimalInfo();
            UpdataPenInfo();
            switch (selections[0])
            {
                case "Move":
                    int hash = -1;
                    int.TryParse(selections[2], out hash);
                    Zoo.MoveAnimalToPen(Zoo.GetAnimalByName(selections[3]), Zoo.GetPenByHash(hash));
                    window.Animals.SelectedItem = "";
                    window.Animals.Items.Clear();
                    AnimalPen pen = Zoo.GetPenByHash(hash);
                    if (pen != null)
                        foreach (Animal item in Zoo.GetPenByHash(hash).GetAnimals())
                        {
                            window.Animals.Items.Add(item.Name);
                        }
                    break;
                case "Feed":
                    Zoo.TellKeeperToFeedAnimal(Zoo.GetZookeeperByName(selections[1]), Zoo.GetAnimalByName(selections[3]), Zoo.GetFoodByName(selections[4]));
                    break;
                case "Buy":
                    Animal animal = null;
                    foreach (Animal animal1 in tempAnimals)
                    {
                        if (animal1.Name == selections[3]) animal = animal1;
                    }
                    Zoo.BuyAnimal(animal);
                    break;
                case "Sell":
                    Zoo.SellAnimal(Zoo.GetAnimalByName(selections[3]));
                    resetAnimals();
                    break;
                case "Fire":
                    Zoo.FireZookeeper(Zoo.GetZookeeperByName(selections[1]));
                    resetZookeepers();
                    break;
                case "Hire":
                    Zookeeper keeper = null;
                    foreach (Zookeeper item in tempZookeepers)
                    {
                        if (item.Name == selections[1]) keeper = item;
                    }
                    Zoo.HireZookeeper(keeper);
                    break;

                default:
                    //PopulateWindow();
                    break;
            }
            window.Action.SelectedValue = "None";
        }

        private void Fodder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections[4] = ((sender as ComboBox).SelectedValue != null) ? (sender as ComboBox).SelectedValue.ToString() : "";
            Food fodder = (selections[4] != "None") ? Zoo.GetFoodByName(selections[4]) : null;
            string msg = "Food Info";
            if (fodder != null)
            {
                msg = $"-- Fodder --\nName: {fodder.Name}\nSaturation: {fodder.Saturation}\nDiet: {fodder.Diet}";
            }
            window.Con5Label.Text = msg;
        }

        private void AnimalPens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!window.Animals.Items.Contains("None") && selections[0] != "Move")
            {
                resetAnimals();
            }
            selections[2] = ((sender as ComboBox).SelectedValue != null) ? (sender as ComboBox).SelectedValue.ToString() : "";
            int hash = 0;
            int.TryParse(selections[2], out hash);
            AnimalPen animalPen = Zoo.GetPenByHash(hash);
            if (animalPen != null)
            {
                if (selections[0] != "Move")
                {
                    window.Animals.SelectedItem = "";
                    window.Animals.Items.Clear();
                    foreach (Animal item in animalPen.GetAnimals())
                    {
                        window.Animals.Items.Add(item.Name);
                    }
                }
            }
            UpdataPenInfo();
        }

        private void Animals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections[3] = ((sender as ComboBox).SelectedValue != null) ? (sender as ComboBox).SelectedValue.ToString() : "";
            UpdateAnimalInfo();
        }

        private void Zookeepers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections[1] = ((sender as ComboBox).SelectedValue != null) ? (sender as ComboBox).SelectedValue.ToString() : "";
            Zookeeper zookeeper = Zoo.GetZookeeperByName(selections[1]);
            if (selections[0] == "Hire") zookeeper = Zoo.GetZookeeperByName(tempZookeepers, selections[1]);
            string msg = "Zookeeper Info";
            if (zookeeper != null)
                msg = $"-- Zookeeper --\nName: {zookeeper.Name}\nAge: {zookeeper.Age}";
            window.Con1Label.Text = msg;
        }
        private void Action_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections[0] = ((sender as ComboBox).SelectedValue != null) ? (sender as ComboBox).SelectedValue.ToString() : "";
            if (!window.Animals.Items.Contains("None") && (window.AnimalPens.SelectedValue.ToString() == "None" || selections[0] == "Move")) resetAnimals();
            if (!window.Zookeepers.Items.Contains("None")) resetZookeepers();
            string msg = "";
            switch (selections[0])
            {
                case "Move":
                    msg = $"-- Move --\nTakes a Animal and a Animal Pen. \nAnimal will be put into the Animal Pen if the pen meets the criteria.";
                    break;
                case "Feed":
                    msg = $"-- Feed --\nTakes a Zookeeper, Animal and Fodder. \nA animal will be fed if the chosen fodder matches the animals diet.";
                    resetAnimals();
                    break;
                case "Buy":
                    msg = $"-- Buy --\nTakes an Animal. \nThe chosesn animal will be added to the list of animals you can interact with.";
                    window.Animals.SelectedItem = "";
                    window.Animals.Items.Clear();
                    tempAnimals.Clear();
                    tempAnimals.Add(new Tiger());
                    tempAnimals.Add(new Tiger());
                    tempAnimals.Add(new Bird());
                    tempAnimals.Add(new Bird());
                    tempAnimals.Add(new Zebra());
                    tempAnimals.Add(new Zebra());
                    foreach (Animal item in tempAnimals)
                    {
                        window.Animals.Items.Add(item.Name);
                    }
                    break;
                case "Sell":
                    msg = $"-- Sell --\nTakes an Animal. \nThe chosen animal will be sold and removed from the list you can interact with.";
                    resetAnimals();
                    break;
                case "Fire":
                    msg = $"-- Fire --\nTakes a Zookeeper. \nThe chosen zookeeper will be fired and no longer work in this Zoo.";
                    resetZookeepers();
                    break;
                case "Hire":
                    msg = $"-- Hire --\nTakes a Zookeeper. \nThe chosen zookeeper will start working at the Zoo.";
                    window.Zookeepers.SelectedItem = "";
                    window.Zookeepers.Items.Clear();
                    tempZookeepers.Clear();
                    tempZookeepers.Add(Zookeeper.Generate());
                    tempZookeepers.Add(Zookeeper.Generate());
                    tempZookeepers.Add(Zookeeper.Generate());
                    tempZookeepers.Add(Zookeeper.Generate());
                    tempZookeepers.Add(Zookeeper.Generate());
                    foreach (Zookeeper item in tempZookeepers)
                    {
                        window.Zookeepers.Items.Add(item.Name);
                    }
                    break;

                default:
                    msg = "Action Info";
                    break;
            }
            window.Con3Label.Text = msg;
        }
        private void FoodSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections[0] = (sender as ComboBox).SelectedValue.ToString();
        }
        private void PopulateWindow()
        {
            window.Zookeepers.Items.Clear();
            window.AnimalPens.Items.Clear();
            window.Animals.Items.Clear();
            window.Action.Items.Clear();
            window.Fodder.Items.Clear();

            window.Zookeepers.Items.Add("None");
            window.AnimalPens.Items.Add("None");
            window.Animals.Items.Add("None");
            window.Action.Items.Add("None");
            window.Fodder.Items.Add("None");

            window.Zookeepers.SelectedItem = "None";
            window.AnimalPens.SelectedItem = "None";
            window.Animals.SelectedItem = "None";
            window.Action.SelectedItem = "None";
            window.Fodder.SelectedItem = "None";

            window.LabelBox1.Content = "Zookeepers";
            foreach (Zookeeper zookeeper in Zoo.Zookeepers)
            {
                window.Zookeepers.Items.Add(zookeeper.Name);
            }
            window.Action.Items.Add("Move");
            window.Action.Items.Add("Feed");
            window.Action.Items.Add("Sell");
            window.Action.Items.Add("Buy");
            window.Action.Items.Add("Fire");
            window.Action.Items.Add("Hire");
            foreach (AnimalPen pen in Zoo.AnimalPens)
            {
                window.AnimalPens.Items.Add(pen.GetHashCode());
            }
            foreach (Animal item in Zoo.Animals)
            {
                window.Animals.Items.Add(item.Name);
            }
            foreach (KeyValuePair<string, Food> item in Zoo.Fodder)
            {
                window.Fodder.Items.Add(item.Value.Name);
            }
        }
        private void resetAnimals()
        {
            window.Animals.Items.Clear();
            window.Animals.Items.Add("None");
            foreach (Animal item in Zoo.Animals)
            {
                window.Animals.Items.Add(item.Name);
            }
            window.Animals.SelectedValue = "None";
        }
        private void resetZookeepers()
        {
            window.Zookeepers.Items.Clear();
            window.Zookeepers.Items.Add("None");
            foreach (Zookeeper item in Zoo.Zookeepers)
            {
                window.Zookeepers.Items.Add(item.Name);
            }
            window.Zookeepers.SelectedValue = "None";
        }
        private void UpdateAnimalInfo()
        {
            Animal animal = Zoo.GetAnimalByName(selections[3]);
            if (selections[0] == "Buy") animal = Zoo.GetAnimalByName(tempAnimals, selections[3]);
            string msg = "Animal Info";
            if (animal != null)
            {
                msg = $"-- Animal --\nName: {animal.Name}\nHealth: {animal.Health}\nHunger: {animal.Hunger}\nMood: {animal.Mood}\nDiet: {animal.Diet}\nAnimal: {animal.GetType().ToString().Split('.').Last()}";
            }
            window.Con2Label.Text = msg;
        }
        private void UpdataPenInfo()
        {
            int hash = 0;
            int.TryParse(selections[2], out hash);
            AnimalPen animalPen = Zoo.GetPenByHash(hash);
            string animal = "N/A";
            string msg = "Animal Pen Info";
            if (animalPen != null)
            {
                if (animalPen.GetAnimals().Length > 0)
                    animal = animalPen.GetAnimals()[0].GetType().ToString().Split('.').Last();
                msg = $"-- Animal Pen --\nInhabited by: {animal}(s)";
            }
            window.Con4Label.Text = msg;
        }
    }
}
