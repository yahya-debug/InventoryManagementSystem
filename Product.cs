using System;
using System.Diagnostics.CodeAnalysis;

class Product {
    private string? _name;
    public required string name {
        get {
            return _name!;
        }
        init {
            if (value.Trim().Length == 0)
                throw new Exception("name is required");

            _name = value;
        }
    }

    private double _price;
    public required double price {
        get {
            return _price;
        }
        set {
            if (value <= 0)
                throw new Exception("price can't be 0 or less");
            else
                _price = value;
        }
    }

    private int _quantity;
    public required int quantity {
        get {
            return _quantity;
        }
        set {
            if (value < 0)
                throw new Exception("quantity can't be less than 0");
            else
                _quantity = value;
        }
    }

    [SetsRequiredMembers]
    public Product(string name, double price, int quantity) {
        this.name = name;
        this.price = price;
        this.quantity = quantity;
    }

    public override string ToString() {
        return $"Product {{name: {name}; price: {price}; quantity: {quantity}}}";
    }
}