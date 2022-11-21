using System.Text.RegularExpressions;
using System.Collections.Generic;

class NegativeWeightException : Exception
{

}

class InvalidWeightException : Exception
{

}

class UnknownUnitsException : Exception
{

}

class Product : IComparable<Product>
{
    private double weight;
    private string units;
    private string name;
    private static SortedDictionary<string, double> toKg;
    static Product() {
        toKg = new SortedDictionary<string, double>{
            {"кг", 1},
            {"л", 1},
            {"г", 0.001},
            {"т", 1000},
        };
    }
    public Product(string weightStr, string name) {
        if (weightStr.StartsWith("-")) {
            throw new NegativeWeightException();
        }

        this.name = name;

        Match match = Regex.Match(weightStr, @"([0-9]+(\.[0-9]+)?)(.+)");
        if (match.Success || match.Groups.Count < 3) {
            double.TryParse(match.Groups[1].Value, out this.weight);
            this.units = match.Groups[match.Groups.Count - 1].Value;
        } else {
            throw new InvalidWeightException();
        }

        if (!toKg.ContainsKey(this.units)) {
            throw new UnknownUnitsException();
        }
    }
    public void Show() {
        Console.WriteLine($"имя: {this.name}");
        Console.WriteLine($"вес: {this.weight}");
        Console.WriteLine($"единицы измерения: {this.units}");
        Console.WriteLine($"вес(кг): {this.ToKg}");
    }
    public double ToKg {
        get {
            return this.weight * toKg[units];
        }
    }
    public int CompareTo(Product? other) {
        if (other == null) {
            throw new NullReferenceException("unexpected product == null while comparing");
        }

        if (this.ToKg < other.ToKg) {
            return -1;
        } else if (this.ToKg == other.ToKg) {
            return 0;
        } else {
            return 1;
        }
    }
}

class Lab10
{
    public static void Main() {
        List<Product> products = new List<Product>();
        string[] lines = System.IO.File.ReadAllLines("products.txt");
        foreach (string line in lines) {
            string[] parts = line.Split(" ", 2);
            products.Add(new Product(parts[0], parts[1]));
        }

        Console.WriteLine();
        Console.WriteLine("Продукты до сортировки:");

        foreach (Product product in products) {
            product.Show();
        }

        products.Sort();

        Console.WriteLine();
        Console.WriteLine("Продукты после сортировки");

        foreach (Product product in products) {
            product.Show();
        }
    }
}