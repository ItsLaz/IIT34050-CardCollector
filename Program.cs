using Newtonsoft.Json;

namespace CardCollector
{
    class Program
    {
        static CardManager manager = new CardManager();
    
        static void Main(string[] args)
        {
            LoadCollectionsAtStart();
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t\t********** Welcome to the Card Collector **********");
                Console.ResetColor();
                Console.WriteLine("\n\tPlease select an option from the menu below:\n");
                Console.WriteLine("\t1. Add a new collection");
                Console.WriteLine("\t2. Select a collection");
                Console.WriteLine("\t3. Import collections from a JSON file");
                Console.WriteLine("\t4. Export collections to a JSON file");
                Console.WriteLine("\t5. Save collections to disk");
                Console.WriteLine("\t6. Load collections from disk");
                Console.WriteLine("\t0. Exit");
                Console.WriteLine("\n\t******************************************************");
                Console.Write("\n\tEnter your choice (0-6): ");
                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ShowErrorMessage("\n\tInvalid input. Please enter a number from 0 to 6. Press any key to continue...");
                    continue;
                }
    
                switch (choice)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\tThank you for using Card Collector. Goodbye!\n");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        return;
                    case 1:
                        CreateNewCardCollection();
                        break;
                    case 2:
                        SelectCardCollection();
                        break;
                    case 3:
                        ImportCollections();
                        break;
                    case 4:
                        ExportCollections();
                        break;
                    case 5:
                        SaveCollections();
                        break;
                    case 6:
                        LoadCollections();
                        break;
                    default:
                        ShowErrorMessage("\n\tInvalid choice. Please enter a number from 0 to 6. Press any key to continue...");
                        break;
                }
            }
        }
        
        static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
        }

        static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
        }
        
        static void CreateNewCardCollection()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Create a New Collection **********\n");
            Console.Write("\tEnter the name of the new collection: ");
            string collectionName = Console.ReadLine();

            CardCollection newCollection = new CardCollection(collectionName);
            manager.AddCollection(newCollection);

            ShowSuccessMessage($"\tCollection '{collectionName}' created successfully! Press any key to continue...");

        }
        
        static void SelectCardCollection()
        {
            if (manager.Collections.Count == 0)
            {
                ShowErrorMessage("\tNo collections available. Press any key to continue...");
                return;
            }

            Console.Clear();
            Console.WriteLine("\n\t\t********** Select a Collection **********\n");
            for (int i = 0; i < manager.Collections.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}. {manager.Collections[i].CollectionName}");
            }
            Console.Write("\n\tEnter the number of the collection: ");
            int selectedIndex;
            if (!int.TryParse(Console.ReadLine(), out selectedIndex))
            {
                ShowErrorMessage("\tInvalid input. Please enter a valid collection number. Press any key to continue...");
                return;
            }
            selectedIndex -= 1;

            // Check if the selected index is within the valid range
            if (selectedIndex < 0 || selectedIndex >= manager.Collections.Count)
            {
                ShowErrorMessage("\tInvalid collection number. Please enter a valid collection number. Press any key to continue...");
                return;
            }

            CardCollection selectedCollection = manager.Collections[selectedIndex];
            ManageCardCollection(selectedCollection);
        }
        
        static void ManageCardCollection(CardCollection collection)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\n\t\t********** Managing Collection: {collection.CollectionName} **********\n");
                Console.WriteLine("\t1. Add a new card");
                Console.WriteLine("\t2. List all cards");
                Console.WriteLine("\t3. Update card status");
                Console.WriteLine("\t4. Generate lists of cards");
                Console.WriteLine("\t5. Return to main menu");
                Console.Write("\n\tEnter your choice (1-5): ");
                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ShowErrorMessage("\tInvalid input. Please enter a number from 1 to 5. Press any key to continue...");
                    continue;
                }
                
                switch (choice)
                {
                    case 1:
                        AddCardToCollection(collection);
                        break;
                    case 2:
                        ListCardsInCollection(collection);
                        break;
                    case 3:
                        UpdateCardStatus(collection);
                        break;
                    case 4:
                        GenerateListsOfCards(collection);
                        break;
                    case 5:
                        return;
                    default:
                        ShowErrorMessage("\tInvalid choice. Please enter a number from 1 to 5. Press any key to continue...");
                        break;
                }
            }
        }
        
        static void AddCardToCollection(CardCollection collection)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Add New Card to Collection: {collection.CollectionName} **********\n");
            Console.Write("\tEnter the name of the new card: ");
            string cardName = Console.ReadLine();

            Card newCard = new Card(cardName);
            collection.AddCard(newCard);

            ShowSuccessMessage($"\tCard '{cardName}' added to collection '{collection.CollectionName}' successfully! Press any key to continue...");
        }
        
        static void ListCardsInCollection(CardCollection collection)
        {
            Console.Clear();
            Console.WriteLine($"\n\t\t********** Cards in Collection: {collection.CollectionName} **********\n");

            if (collection.Cards.Count == 0)
            {
                Console.WriteLine("\tNo cards in this collection. Press any key to continue...");
            }
            else
            {
                foreach (Card card in collection.Cards)
                {
                    Console.WriteLine($"\t- {card.Name}");
                }
            }

            Console.ReadKey();
        }
        
        static void UpdateCardStatus(CardCollection collection)
        {
            Console.Clear();
            Console.WriteLine($"\n\t\t********** Update Card Status in Collection: {collection.CollectionName} **********\n");

            if (collection.Cards.Count == 0)
            {
                Console.WriteLine("\tNo cards in this collection. Press any key to continue...");
            }
            else
            {
                for (int i = 0; i < collection.Cards.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {collection.Cards[i].Name} - {(collection.Cards[i].Have ? "Have" : "Need")}");
                }
                Console.Write("\tEnter the number of the card to update its status: ");
                int selectedIndex = int.Parse(Console.ReadLine()) - 1;

                if (selectedIndex < 0 || selectedIndex >= collection.Cards.Count)
                {
                    ShowErrorMessage("\tInvalid card number. Please enter a valid card number. Press any key to continue...");
                    return;
                }

                collection.Cards[selectedIndex].Have = !collection.Cards[selectedIndex].Have;
                ShowSuccessMessage($"\tCard status updated: {collection.Cards[selectedIndex].Name} - {(collection.Cards[selectedIndex].Have ? "Have" : "Need")}. Press any key to continue...");
            }
        }

        static void GenerateListsOfCards(CardCollection collection)
        {
            Console.Clear();
            Console.WriteLine($"\n\t\t********** Generate Lists of Cards in Collection: {collection.CollectionName} **********\n");
            Console.WriteLine("\tCards I have:");
            foreach (Card card in collection.Cards.Where(c => c.Have))
            {
                Console.WriteLine($"\t- {card.Name}");
            }

            Console.WriteLine("\n\tCards I need:");
            foreach (Card card in collection.Cards.Where(c => !c.Have))
            {
                Console.WriteLine($"\t- {card.Name}");
            }

            Console.WriteLine("\n\tPress any key to continue...");
            Console.ReadKey();
        }
        
        static void ExportCollections()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Export Collections **********\n");

            string json = JsonConvert.SerializeObject(manager, Formatting.Indented);
            File.WriteAllText("collections.json", json);

            ShowSuccessMessage("\tCollections exported successfully. Press any key to continue...");

        }

        static void ImportCollections()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Import Collections **********\n");

            if (!File.Exists("collections.json"))
            {
                ShowErrorMessage("\tNo collections.json file found. Press any key to continue...");
            }
            else
            {
                string json = File.ReadAllText("collections.json");
                try
                {
                    manager = JsonConvert.DeserializeObject<CardManager>(json);
                    ShowSuccessMessage("\tCollections imported successfully. Press any key to continue...");
                }
                catch (JsonException)
                {
                    ShowErrorMessage("\tFailed to import collections. Invalid JSON format. Press any key to continue...");
                }
            }
        }
        
        static void SaveCollections()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Save Collections **********\n");

            string json = JsonConvert.SerializeObject(manager, Formatting.Indented);
            File.WriteAllText("collections.json", json);

            ShowSuccessMessage("\tCollections saved successfully. Press any key to continue...");

        }

        static void LoadCollectionsAtStart()
        {
            if (File.Exists("collections.json"))
            {
                string json = File.ReadAllText("collections.json");
                try
                {
                    manager = JsonConvert.DeserializeObject<CardManager>(json);
                }
                catch (JsonException)
                {
                    //log this error
                }
            }
        }

        static void LoadCollections()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t********** Load Collections **********\n");

            if (File.Exists("collections.json"))
            {
                string json = File.ReadAllText("collections.json");
                try
                {
                    manager = JsonConvert.DeserializeObject<CardManager>(json);
                    ShowSuccessMessage("\tCollections loaded successfully. Press any key to continue...");
                }
                catch (JsonException)
                {
                    ShowErrorMessage("\tFailed to load collections. Invalid JSON format. Press any key to continue...");
                }
            }
            else
            {
                ShowErrorMessage("\tNo collections.json file found. Press any key to continue...");
            }
        }
        
    }
}
