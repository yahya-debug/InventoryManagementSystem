using System;

class Project {
    private static string menu = """
    **Inventory Management System**
    
    """;
    public static void Main(string[] args) {
        Inventory inventory = new Inventory();

        while (true) {
            Console.WriteLine($"{menu}\n\nPlease choose the number of the function you want");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice)) {
                Console.WriteLine("Please choose a number");
                continue;
            } else {
                switch (choice) {
                    
                }
            }
            
        }
    }
}