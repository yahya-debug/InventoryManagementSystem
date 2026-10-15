using System;

class Inventory {
    private Dictionary<string, Product> _products;

    public Inventory() {
        _products = new Dictionary<string, Product>();
    }

    public void addProduct(Product product) {
        _products[product.name] = product;
    }

    public bool hasProduct(string name) {
        return _products.ContainsKey(name);
    }

    public Product? getProduct(string name) {
        if (!hasProduct(name))
            return null;
        return _products[name];
    }

    public IEnumerable<Product> getAllProducts() {
        return _products.Values;
    }


    public void editProduct(string original_name, string new_name, double price, int quantity) {
        addProduct(new Product(new_name, price, quantity));
        deleteProduct(original_name);
    }


    public void deleteProduct(string name) {
        _products.Remove(name);
    }
}