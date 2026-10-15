using System;

class Inventory {
    private Dictionary<string, Product> _products;
    public Dictionary<string, Product> products {get => _products;}

    public Inventory() {
        _products = new Dictionary<string, Product>();
    }

    public void addProduct(Product product) {
        _products[product.name] = product;
    }

}