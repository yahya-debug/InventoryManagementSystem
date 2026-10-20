using System;
using System.Collections.Concurrent;

class Inventory {
    private readonly ConcurrentDictionary<string, Product> _products;
    private readonly DB<Product, string> _db;

    public Inventory(DB<Product, string> db) {
        _products = db.Read();
        _db = db;
    }

    public void AddProduct(Product product) {
        if (HasProduct(product.Name))
            throw new DuplicateProductsException($"Product with name {product.Name} already exists");

        _products[product.Name] = product;
        _db.Edit(_products);
    }

    public bool HasProduct(string name) {
        return _products.ContainsKey(name);
    }

    public Product? GetProduct(string name) {
        if (!HasProduct(name))
            return null;
        return _products[name];
    }

    public IEnumerable<Product> GetAllProducts() {
        return _products.Values;
    }


    public void EditProduct(string originalName, string newName, double price, int quantity) {
        _products.TryRemove(originalName, out _);
        AddProduct(new Product(newName, price, quantity));
    }


    public void DeleteProduct(string name) {
        _products.TryRemove(name, out _);
        _db.Edit(_products);
    }
}