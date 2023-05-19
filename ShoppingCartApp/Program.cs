using System;
using System.Collections.Generic;

namespace GroceryStoreInventory
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

    public class Inventory
    {
        private Dictionary<string, List<InventoryItem>> inventory;

        public Inventory()
        {
            inventory = new Dictionary<string, List<InventoryItem>>();
        }

        public void AddItem(InventoryItem item)
        {
            if (!inventory.ContainsKey(item.Category))
            {
                inventory[item.Category] = new List<InventoryItem>();
            }

            inventory[item.Category].Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            if (inventory.ContainsKey(item.Category))
            {
                inventory[item.Category].Remove(item);
            }
        }

        public void DisplayInventory()
        {
            foreach (var category in inventory.Keys)
            {
                Console.WriteLine($"Category: {category}");
                Console.WriteLine("----------------------------");

                foreach (var item in inventory[category])
                {
                    Console.WriteLine($"Name: {item.Name}, Price: {item.Price:C}, Quantity: {item.Quantity}");
                }

                Console.WriteLine();
            }
        }
    }

    public class GroceryStore
    {
        public Inventory Inventory { get; }

        public GroceryStore()
        {
            Inventory = new Inventory();
        }
    }

    public class InputValidator
    {
        public static bool ValidateItem(InventoryItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                Console.WriteLine("Item name is required.");
                return false;
            }

            if (item.Price <= 0)
            {
                Console.WriteLine("Item price must be greater than zero.");
                return false;
            }

            if (item.Quantity <= 0)
            {
                Console.WriteLine("Item quantity must be greater than zero.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.Category))
            {
                Console.WriteLine("Item category is required.");
                return false;
            }

            return true;
        }
    }

    public class ErrorHandler
    {
        public static void HandleError(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                GroceryStore store = new GroceryStore();

                while (true)
                {
                    Console.WriteLine("1. Add Item\n2. Remove Item\n3. Display Inventory\n4. Exit");
                    Console.WriteLine("Enter your choice:");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter item name:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter item price:");
                            decimal price = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Enter item quantity:");
                            int quantity = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter item category:");
                            string category = Console.ReadLine();

                            InventoryItem item = new InventoryItem
                            {
                                Name = name,
                                Price = price,
                                Quantity = quantity,
                                Category = category
                            };

                            if (InputValidator.ValidateItem(item))
                            {
                                store.Inventory.AddItem(item);
                                Console.WriteLine("Item added to inventory.");
                            }

                            break;

                        case 2:
                            Console.WriteLine("Enter item name:");
                            string itemName = Console.ReadLine();

                            Console.WriteLine("Enter item category:");
                            string itemCategory = Console.ReadLine();

                            InventoryItem itemToRemove = store.Inventory.GetInventoryItem(itemName, itemCategory);

                            if (itemToRemove != null)
                            {
                                store.Inventory.RemoveItem(itemToRemove);
                                Console.WriteLine("Item removed from inventory.");
                            }
                            else
                            {
                                Console.WriteLine("Item not found in inventory.");
                            }

                            break;

                        case 3:
                            store.Inventory.DisplayInventory();
                            break;

                        case 4:
                            Console.WriteLine("Exiting the program.");
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex);
            }
        }
    }
}
