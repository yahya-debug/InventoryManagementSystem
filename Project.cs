using System;
using System.Globalization;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

class Project {
    private readonly static DB<Product, string> Database;
    private readonly static Inventory Inventory;


    static Project() {
        IConfiguration config = Config();
        Database = new(config, "Products");
        Inventory = new(Database);
    }

    private readonly static string menu = """
    **Inventory Management System**
    1- Add a Product
    2- View All Products
    3- Edit a Product
    4- Delete a Product
    5- Search Products
    6- Exit
    """;
    public static void Main(string[] args) {
        bool boolean = true;
        while (boolean) {
            Console.Write($"{menu}\n\nPlease choose the number of the function you want: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice)) {
                Console.WriteLine("Please choose a number");
                continue;
            } else {
                try {
                    Console.Write("\n\n");
                    switch (choice) {
                        case 1:
                            AddProduct();
                            break;
                        case 2:
                            ShowAll();
                            break;
                        case 3:
                            EditProduct();
                            break;
                        case 4:
                            DeleteProduct();
                            break;
                        case 5:
                            SearchProducts();
                            break;
                        case 6:
                            boolean = false;
                            Console.WriteLine("Good bye!");
                            break;
                        default:
                            Console.WriteLine("no choice with this number");
                            break;
                    }
                } catch (Exception e) {
                    Console.WriteLine($"{e.Message}");
                }
            }
            
        }
    }

    private static void AddProduct() {
        Console.Write("Add Product:-\nProduct name: ");
        string? name = Console.ReadLine();

        if (Inventory.HasProduct(name ?? "")) {
            Console.Write("Already exists\n");
            return;
        }

        Console.Write("Product price: ");
        string? price = Console.ReadLine();
        Console.Write("Product quantity: ");
        string? quantity = Console.ReadLine();
        
        if (!double.TryParse(price, out double priceParsed) || !int.TryParse(quantity, out int quantityParsed))
            throw new DataFormatException("Wrong format in the fields you entered");
        else {
            Product product = new Product(name ?? "", priceParsed, quantityParsed);
            Inventory.AddProduct(product);

            Console.WriteLine($"Added the Item => {product}");
        }
    }

    private static void ShowAll() {
        Console.WriteLine("Products in Inventory:- ");

        if (!Inventory.GetAllProducts().Any()) {
            Console.WriteLine("Empty Inventory");
            return;
        }

        int counter = 1;
        foreach (var product in Inventory.GetAllProducts())
            Console.WriteLine($"{counter++}- {product}");
        Console.WriteLine();
    }

    private static void EditProduct() {
        Console.Write("Which Product you like to edit: ");
        string? originalName = Console.ReadLine();

        if (!Inventory.HasProduct(originalName ?? "")) {
            Console.WriteLine("no product with this name, try show command");
            return;
        }
        Console.Write("New Name: ");
        string? newName = Console.ReadLine();
        Console.Write("New price: ");
        string? price = Console.ReadLine();
        Console.Write("New quantity: ");
        string? quantity = Console.ReadLine();

        if (!double.TryParse(price, out double priceParsed) || !int.TryParse(quantity, out int quantityParsed))
            throw new Exception("Wrong format in the fields you entered");
        else {
            Inventory.EditProduct(originalName ?? "", newName ?? "", priceParsed, quantityParsed);
            Console.WriteLine($"Editted");
        }
    }

    private static void DeleteProduct() {
        Console.Write("Which Product yuu want to delete ");
        string? name = Console.ReadLine();

        if (!Inventory.HasProduct(name ?? "")) {
            Console.WriteLine("no product with this name, try show command");
            return;
        }

        Inventory.DeleteProduct(name ?? "");
        Console.WriteLine("Deleted");
    }

    private static void SearchProducts() {
        Console.Write("Product name: ");
        string? name = Console.ReadLine();

        Product? product = Inventory.GetProduct(name ?? "");
        if (product == null)
            Console.Write("Not found\n");
        else Console.WriteLine(product);
    }

    private static IConfiguration Config() {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
    }
}