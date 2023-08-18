using System.Linq.Expressions;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 1,
        AskingPrice = 79.99,
        City = "Nashville",
        ZIP = 37209,
        Sold = true,
        AvailableUntil = new DateTime(2023, 8, 31)
    },
    new Plant()
    {
        Species = "Money Tree",
        LightNeeds = 4,
        AskingPrice = 89.99,
        City = "Nashville",
        ZIP = 37212,
        Sold = false,
        AvailableUntil = new DateTime(2023, 8, 31)
    },
    new Plant()
    {
        Species = "Bird of Paradise",
        LightNeeds = 4,
        AskingPrice = 99.99,
        City = "Nashville",
        ZIP = 37209,
        Sold = false,
        AvailableUntil = new DateTime(2023, 7, 30)
    },
    new Plant()
    {
        Species = "Fiddle Leaf Fig",
        LightNeeds = 5,
        AskingPrice = 119.99,
        City = "Nashville",
        ZIP = 37205,
        Sold = false,
        AvailableUntil = new DateTime(2023, 10, 31)
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 2,
        AskingPrice = 19.99,
        City = "Nashville",
        ZIP = 37211,
        Sold = true,
        AvailableUntil = new DateTime(2023, 8, 31)
    },
};

// menu option 1 - display all plants

void displayAllPlants()
{
    Console.Clear();
    Console.WriteLine(@"~~Current plants in stock~~
    ");

    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for ${plants[i].AskingPrice}.");
    }

    Console.WriteLine(@"
Press any key to return to the menu...");

    Console.ReadKey();
    Console.Clear();
}

// menu option 2 - post a new plant listing

void PostPlant()
{

    // the rest of this needs error handling.

    Console.Clear();
    Plant newPlant = new Plant();
    
    
    while (newPlant.Species == null)
    {
        Console.WriteLine("New plant's species: ");

        string verifiedSpecies = Console.ReadLine().Trim();

        if (int.TryParse(verifiedSpecies, out int parsedNumber))
        {
            Console.WriteLine("Cannot assign number values to a plant species.");
        }
        else
        {
            newPlant.Species = verifiedSpecies;
        }
    }
    

    while (newPlant.LightNeeds < 1 || newPlant.LightNeeds > 5)
    {
        Console.WriteLine("New plant's light needs (1-5): ");
        try
        {
            newPlant.LightNeeds = int.Parse(Console.ReadLine().Trim());
            if (newPlant.LightNeeds < 1 || newPlant.LightNeeds > 5)
            {
                Console.WriteLine("Please enter only 1, 2, 3, 4, or 5.");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid value (a whole number between 1 and 5)");
        }
    }

    while (newPlant.AskingPrice == 0)
    {
        Console.WriteLine("New plant's price: ");
        try
        {
            newPlant.AskingPrice = double.Parse(Console.ReadLine().Trim());
            if (newPlant.AskingPrice == 0)
            {
                Console.WriteLine("price cannot be 0.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please input a valid numerical price (numbers only)");
        }
    }

    while (newPlant.City == null)
    {
        Console.WriteLine("New plant's current city: ");

        string verifiedCity = Console.ReadLine().Trim();

        if (int.TryParse(verifiedCity, out int parsedNumber))
        {
            Console.WriteLine("Cannot assign number values to a city name.");
        }
        else
        {
            newPlant.City = verifiedCity;
        }
    }

    while (newPlant.ZIP == 0)
    {
        Console.WriteLine("New plant's 5-digit ZIP: ");

        try
        {
            newPlant.ZIP = int.Parse(Console.ReadLine().Trim());
            if (newPlant.ZIP > 99999 || newPlant.ZIP < 00001)
            {
                Console.WriteLine("Please enter a valid 5-digit ZIP");
                newPlant.ZIP = 0;
            }       
        }
        catch (FormatException)
        {
            Console.WriteLine("please enter numbers only.");
        }
    }

    while (true)
    {
        Console.WriteLine("Until when will this plant be available?");
        Console.WriteLine("Year (e.g., 2024): ");
        int newPlantYear = int.Parse(Console.ReadLine().Trim());
        Console.WriteLine("Month (e.g., 7): ");
        int newPlantMonth = int.Parse(Console.ReadLine().Trim());
        Console.WriteLine("Day (e.g., 31): ");
        int newPlantDay = int.Parse(Console.ReadLine().Trim());

        try
        {
            newPlant.AvailableUntil = new DateTime(newPlantYear, newPlantMonth, newPlantDay);
            break;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.Clear();
            Console.WriteLine($"Date does not exist. Please enter a valid date.");
        }
    
    }
    
    

    newPlant.Sold = false;

    string confirmation = null;

    while (confirmation == null)
    {
        try
        {
            Console.WriteLine($"");
            Console.WriteLine($"Species: {newPlant.Species}");
            Console.WriteLine($"Light Needs: {newPlant.LightNeeds} / 5");
            Console.WriteLine($"Price: {newPlant.AskingPrice}");
            Console.WriteLine($"City: {newPlant.City}");
            Console.WriteLine($"ZIP: {newPlant.ZIP}");
            Console.WriteLine($"Available until: {newPlant.AvailableUntil.ToString("yyyy-MM-dd")}");
            Console.WriteLine($"");
            Console.WriteLine("Is this correct? (Y/N)");

            confirmation = Console.ReadLine().Trim().ToUpper();
            if (confirmation == "Y")
            {
                plants.Add(newPlant);
                Console.Clear();
                Console.WriteLine($@"{newPlant.Species} added to plant inventory!
                ");
            }
            else if (confirmation == "N")
            {
                PostPlant();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Please answer Y or N");
            confirmation = null;
        }
    }
}


// menu option 3 - adopt a plant

void AdoptPlant()
{
    Console.Clear();
    
    List<Plant> adoptablePlants = new List<Plant>();
    DateTime now = DateTime.Now;

    foreach (Plant plant in plants)
    {
        
        TimeSpan isStillAvailable = plant.AvailableUntil - now;
        
        if (plant.Sold == false && isStillAvailable.TotalSeconds > 0)
        {
            adoptablePlants.Add(plant);
        }
    }

    if (adoptablePlants.Count == 0)
    {
        Console.Clear();
        Console.WriteLine("No plants are currently available for adoption. Go add one!");
    }

    Console.WriteLine(@"Plants available for adoption:");
    Console.WriteLine("    0. Return to menu");

    for (int i = 0; i < adoptablePlants.Count; i++)
    {
        Console.WriteLine($"    {i + 1}. {adoptablePlants[i].Species} (${adoptablePlants[i].AskingPrice})");
    }

    Console.WriteLine("Select which plant you would like to adopt by its number.");

    Plant? adoptedPlant = null;
    int plantChoice;

    while (adoptedPlant == null)
    {
        
        try
        {
            plantChoice = int.Parse(Console.ReadLine().Trim());

            if (plantChoice == 0)
            {
                Console.Clear();
                break;
            }
            else if (plantChoice <= adoptablePlants.Count && plantChoice >= (adoptablePlants.Count - adoptablePlants.Count + 1))
            {
                foreach (Plant plant in plants)
                {
                    if (adoptablePlants[plantChoice - 1].Species == plant.Species)
                    {
                        plant.Sold = true;
                        Console.Clear();
                        Console.WriteLine($"You purchased the {plant.Species}.");
                        break;
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Please select a plant in the list.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Select plant to adopt by its number.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Select plant to adopt by its number.");
        }
    }
}

// menu option 4 - de-list a plant

void DeletePlant()
{
    Console.Clear();
    Console.WriteLine(@"Select a plant by its number to delete it:
    ");

    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City}");
    }

    Console.WriteLine(@"
    Or, type '0' to return to the main menu.
    ");

    string userSelection = null;

    while (userSelection == null)
    {
        try 
        {
            userSelection = Console.ReadLine().Trim();
            if (int.TryParse(userSelection, out int intUserSelection))
            {
                if (intUserSelection <= plants.Count && intUserSelection > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"{plants[intUserSelection - 1].Species} has been de-listed.");
                    plants.RemoveAt(intUserSelection - 1);
                    break;
                }
                else if (intUserSelection == 0)
                {
                    Console.Clear();
                    break;
                }
                else 
                {
                    Console.WriteLine("Please choose a valid menu number.");
                    userSelection = null;
                }
            }
            else
            {
                Console.WriteLine("Please select plant via its menu number.");
                userSelection = null;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Input must match a plant's list number.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    
}

// menu option 5 - display a random plant

void RandomPlant()
{
    Random random = new Random();
    int randomPlantIndex = random.Next(0, plants.Count);
    while (plants[randomPlantIndex].Sold == true)
    {
        // Console.WriteLine($"iterating... random index = {randomPlantIndex}");
        randomPlantIndex = random.Next(0, plants.Count);
    }
    
    // Console.WriteLine($"final index: {randomPlantIndex}");

    Console.Clear();
    Console.WriteLine(@$"Random plant of the day:
    
A lovely {plants[randomPlantIndex].Species}, available for only ${plants[randomPlantIndex].AskingPrice}. Adopt today!");

    Console.WriteLine(@"
Press any key to return to the menu...");

    Console.ReadKey();
    Console.Clear();
}

// menu option 6 - filter plants by light needs

void FilterLightNeeds()
{
    Console.Clear();
    Console.WriteLine(@"Enter the max amount of light you would like (1-5): 
    ");

    string userLight = null;
    while (userLight == null)
    {
        try
        {
            userLight = Console.ReadLine().Trim();
            int userLightInt;
            if (int.TryParse(userLight, out userLightInt))
            {
                if (userLightInt <= 5 && userLightInt >= 1)
                {
                    foreach (Plant plant in plants)
                    {
                        if (plant.LightNeeds <= userLightInt)
                        {
                            Console.WriteLine($"- {plant.Species} -- needs {plant.LightNeeds} / 5 light");
                        }
                    }
                }
                else {
                    Console.WriteLine("Please enter a valid light needs value (1-5)");
                    userLight = null;
                }
            }
            else
            {
                Console.WriteLine("Please enter only integers (1-5)");
                userLight = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    Console.WriteLine(@"
Press any key to return to the menu...");

    Console.ReadKey();
    Console.Clear();
}

// menu option 7 - app statistics
    // lowest price plant's name
    // # of plants not sold + still available date-wise
    // name of plant with highest light needs
    // average light needs
    // % of plants adopted
void AppStats()
{
    // fyi, currently no error handling here.

    // a. find lowest price plant
    Plant lowestPricePlant = plants.OrderBy(plant => plant.AskingPrice).FirstOrDefault();

    // b. find number of plants not sold + still available (date not yet reached)
    List<Plant> adoptablePlants = new List<Plant>();
    DateTime now = DateTime.Now;

    foreach (Plant plant in plants)
    {
        
        TimeSpan isStillAvailable = plant.AvailableUntil - now;
        
        if (plant.Sold == false && isStillAvailable.TotalSeconds > 0)
        {
            adoptablePlants.Add(plant);
        }
    }
    
    // c. find the plant with the highest light needs
    Plant neediestPlant = plants.OrderBy(plant => plant.LightNeeds).Last();
    
    // d. calculate average light needs (add up all light needs, divide by plants.Count; cast value as double)
    int totalLightNeed = 0;
    foreach (Plant plant in plants)
    {
        totalLightNeed += plant.LightNeeds;
        // Console.WriteLine(totalLightNeed);
    }
    int totalPlants = plants.Count;
    double totalLightNeedDbl = (double)totalLightNeed;

    double averageLightNeed = totalLightNeedDbl / totalPlants;

    // e. find percentage of plants adopted

    List<Plant> adoptedPlants = new List<Plant>();
    foreach (Plant plant in plants)
    {
        if (plant.Sold == true)
        {
            adoptedPlants.Add(plant);
        }
    }

    double numAdoptedPlantsDbl = (double)adoptedPlants.Count;

    double percentAdopted = numAdoptedPlantsDbl / totalPlants * 100;


    Console.Clear();
    Console.WriteLine(@$"App stats:
Current lowest-priced plant: {lowestPricePlant.Species} (${lowestPricePlant.AskingPrice})
Number of plants available: {adoptablePlants.Count}
Most light-needy plant: {neediestPlant.Species} ({neediestPlant.LightNeeds} / 5)
Average light needs: {averageLightNeed} / 5
Percentage of plants adopted: {percentAdopted}%
");

    Console.WriteLine(@"
Press any key to return to the menu...");

    Console.ReadKey();
    Console.Clear();
}

// main program function

void Main()
{
    string greeting = "Welcome!";

    string menuChoice = null;

    while (menuChoice != "8")
    {
        Console.WriteLine(@$"{greeting}

    Choose an option:
    1. Display all plants
    2. Post a plant to be adopted
    3. Adopt a plant
    4. De-list a plant
    5. Show a random plant
    6. Filter plants by light needs
    7. View app stats
    8. Exit");

        menuChoice = Console.ReadLine().Trim();

        // switch version
        try
        {
            switch (menuChoice)
            {
                case "1":
                    displayAllPlants();
                    break;
                case "2":
                    PostPlant();
                    break;
                case "3":
                    AdoptPlant();
                    break;
                case "4":
                    // throw new NotImplementedException("feature not yet built");
                    DeletePlant();
                    break;
                case "5":
                    RandomPlant();
                    break;
                case "6":
                    FilterLightNeeds();
                    break;
                case "7":
                    AppStats();
                    break;
                case "8":
                    Console.WriteLine("SEEYUH");
                    break;
                default:
                    throw new Exception();
            }
        }
        catch (NotImplementedException)
        {
            Console.Clear(); 
            Console.WriteLine("menu option not yet set up.");
        }
        catch (Exception)
        {
            Console.Clear();
            // Console.WriteLine(ex);
            Console.WriteLine("Please enter a valid menu number.");
        }
    }
}

Console.Clear();
Main();
    


// if/else version of the menu select in Main()
    // try
    // {
    //     if (menuChoice == "1")
    //     {
    //         displayAllPlants();
    //     }
    //     else if (menuChoice == "2")
    //     {
    //         PostPlant();
    //     }
    //     else if (menuChoice == "3")
    //     {
    //         throw new NotImplementedException("you chose option 3");
    //     }
    //     else if (menuChoice == "4")
    //     {
    //         throw new NotImplementedException("you chose option 4");
    //     }
    //     else if (menuChoice == "5")
    //     {
    //         Console.WriteLine("SEEYUH");
    //     } else
    //     {
    //         throw new Exception();
    //     }
    // }
    // catch (NotImplementedException)
    // {
    //     Console.Clear();
    //     Console.WriteLine("menu option not yet set up");
    // }
    // catch (Exception)
    // {
    //     Console.Clear();
    //     Console.WriteLine("Please enter a valid menu number.");
    // }