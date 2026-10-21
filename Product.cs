using System;
using System.Diagnostics.CodeAnalysis;

class Product {
    private string? _name;
    public required string Name {
        get {
            return _name!;
        }
        init {
            if (value.Trim().Length == 0)
                throw new UnNamedProductException("name is required");

            _name = value;
        }
    }

    private double _price;
    public required double Price {
        get {
            return _price;
        }
        set {
            if (value <= 0)
                throw new Exception("Price can't be 0 or less");
            else
                _price = value;
        }
    }

    private int _quantity;
    public required int Quantity {
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
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString() {
        return $"Product {{name: {Name}; Price: {Price}; quantity: {Quantity}}}";
    }
}