using System;

class Inventory {
    private List<Product> _products;

    public Inventory() {
        _products = new List<Product>();
    }

    public void addProduct(Product product) {
        _products.Add(product);
    }

}