using System;
using System.Net.Http.Headers;

class Project {
    private static Inventory inventory = new Inventory();
    private static string menu = """
    **Inventory Management System**
    1- Add a Product
    2- View All Products
    """;
    public static void Main(string[] args) {

        while (true) {
            Console.Write($"{menu}\n\nPlease choose the number of the function you want: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice)) {
                Console.WriteLine("Please choose a number");
                continue;
            } else {
                Console.Write("\n\n");
                switch (choice) {
                    case 1:
                        addProduct();
                        break;
                    case 2:
                        showAll();
                        break;
                }
            }
            
        }
    }

    private static void addProduct() {
        Console.Write("Add Product:-\nProduct name: ");
        string? name = Console.ReadLine();
        Console.Write("Product price: ");
        string? price = Console.ReadLine();
        Console.Write("Product quantity: ");
        string? quantity = Console.ReadLine();
        
        if (!double.TryParse(price, out double price_parsed) || !int.TryParse(quantity, out int quantity_parsed))
            throw new Exception("Wrong format in the fields you entered");
        else {
            Product product = new Product(name ?? "", price_parsed, quantity_parsed);
            inventory.addProduct(product);

            Console.WriteLine($"Added the Item => {product}");
        }
    }

    private static void showAll() {
        Console.WriteLine("Products in Inventory:- ");

        if (inventory.products.Count() == 0) {
            Console.WriteLine("Empty Inventory");
            return;
        }
        int i = 1; // just a counter
        foreach (var product in inventory.products) {
            Console.WriteLine($"{i}- {product}");
            i++;
        }
        Console.WriteLine();

    }
}