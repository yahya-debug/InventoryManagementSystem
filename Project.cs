using System;
using System.Net.Http.Headers;

class Project {
    private static Inventory inventory = new Inventory();
    private static string menu = """
    **Inventory Management System**
    1- Add a Product
    2- View All Products
    3- Edit a Product
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
                    case 3:
                        editProduct();
                        break;

                }
            }
            
        }
    }

    private static void addProduct() {
        Console.Write("Add Product:-\nProduct name: ");
        string? name = Console.ReadLine();

        if (inventory.hasProduct(name ?? "")) {
            Console.Write("Already exists\n");
            return;
        }

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

        if (!inventory.getAllProducts().Any()) {
            Console.WriteLine("Empty Inventory");
            return;
        }
        int i = 1; // just a counter
        foreach (var product in inventory.getAllProducts()) {
            Console.WriteLine($"{i}- {product}");
            i++;
        }
        Console.WriteLine();

    }

    private static void editProduct() {
        Console.Write("Which Product you like to edit: ");
        string? original_name = Console.ReadLine();

        if (!inventory.hasProduct(original_name ?? "")) {
            Console.WriteLine("no product with this name, try show command");
            return;
        }
        Console.Write("New Name: ");
        string? new_name = Console.ReadLine();
        Console.Write("New price: ");
        string? price = Console.ReadLine();
        Console.Write("New quantity: ");
        string? quantity = Console.ReadLine();

        if (!double.TryParse(price, out double price_parsed) || !int.TryParse(quantity, out int quantity_parsed))
            throw new Exception("Wrong format in the fields you entered");
        else {
            inventory.editProduct(original_name ?? "", new_name ?? "", price_parsed, quantity_parsed);
            Console.WriteLine($"Editted");
        }
    }
}