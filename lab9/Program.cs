using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

class ProductNotFoundException: Exception
{

}

class EmptyCustomerNameException: Exception
{
    
}

class Getter
{
    private static int GetInt(string prompt, string errMessage, Func<int?, bool> predicate) {
        Console.WriteLine(prompt);
        int res;
        while (!int.TryParse(Console.ReadLine(), out res) || !predicate(res)) {
            Console.WriteLine(errMessage);
            Console.WriteLine(prompt);
        }
        return res;
    }
    public static int GetInt(string prompt, string errMessage) {
        return GetInt(prompt, errMessage, (int? num) => true);
    }
    public static int GetNonNegativeInt(string prompt, string errMessage) {
        return GetInt(prompt, errMessage, (int? num) => num >= 0);
    }

    private static string GetString(string prompt, string errMessage, Func<string?, bool> predicate) {
        Console.WriteLine(prompt);
        string? res = Console.ReadLine();
        while (res == null || !predicate(res)) {
            Console.WriteLine(errMessage);
            Console.WriteLine(prompt);
            res = Console.ReadLine();
        }
        return res;
    }

    public static string GetString(string prompt, string errMessage) {
        return GetString(prompt, errMessage, (string? str) => true);
    }
    public static string GetNotEmptyString(string prompt, string errMessage) {
        return GetString(prompt, errMessage, (string? str) => !string.IsNullOrEmpty(str));
    }

}

class Customer
{
    public static double DefaultDiscount { get; }
    public string Name { get; }
    public string Address { get; }
    public double Discount { get; }
    
    static Customer() {
        DefaultDiscount = 0.05;
    }
    public Customer(string name, string address, double discount) {
        Name = name;
        Address = address;
        Discount = discount;
    }

    private static double GetDiscountFromConsole() {
        while (true) {
            string discountStr = Getter.GetNotEmptyString(
                "Введите скидку покупателя в процентах (если хотите использовать стандартную скидку, введите '-' без кавычек):",
                "Вы не ввели скидку"
            );

            if (discountStr == "-") {
                return DefaultDiscount;
            }

            int discountPercent;
            if (!int.TryParse(discountStr, out discountPercent)) {
                Console.WriteLine("Неверно введена скидка");
                continue;
            }

            return discountPercent / 100.0;
        }
    }

    public static Customer FromConsole() {
        string name = Getter.GetNotEmptyString("Введите имя покупателя:", "Имя покупателя не может быть пустым");
        string address = Getter.GetNotEmptyString("Введите адрес покупателя:", "Адрес покупателя не может быть пустым");
        double discount = GetDiscountFromConsole();

        return new Customer(name, address, discount);
    }
}

class Product
{
    public string Name { get; }
    public decimal Price { get; }

    public Product(string name, decimal price) {
        Name = name;
        Price = price;
    }
}

class ProductDB
{
    public SortedDictionary<int, Product> Products { get; }

    public ProductDB() {
        Products = new SortedDictionary<int, Product> {
            {1, new Product("батарейки", 79)},
            {2, new Product("компьютер", 54999)},
            {3, new Product("калькулятор", 299)},
            {4, new Product("носки", 319)},
            {5, new Product("наушники", 3999)},
        };
    }

    [JsonConstructor]
    public ProductDB(SortedDictionary<int, Product> products) {
        Products = products;
    }

    public Product GetProduct(int code) {
        try {
            return Products[code];
        } catch (KeyNotFoundException) {
            throw new ProductNotFoundException();
        }
    }

    public static async void ToFile(string fileName, ProductDB db) {
        string jsonString = JsonSerializer.Serialize(db);
        await File.WriteAllTextAsync(fileName, jsonString);
    }

    public static ProductDB? FromFile(string fileName) {
        return JsonSerializer.Deserialize<ProductDB>(File.ReadAllText(fileName));
    }
}

class OrderLine
{
    public Product Product { get; }
    public int Amount { get; }

    public OrderLine(Product product, int amount) {
        Product = product;
        Amount = amount;
    }
}

class Order
{
    public static int NextNumber { get; set; }
    public int Number { get; }
    public Customer Customer { get; }
    public decimal Discount { get; }
    public decimal TotalPrice { get; }
    public List<OrderLine> OrderLines { get; }

    static Order() {
        NextNumber = 1;
    }
    public Order(Customer customer, List<OrderLine> orderLines) {
        Number = NextNumber++;
        Customer = customer;
        Discount = (decimal)customer.Discount;
        OrderLines = orderLines;

        foreach (OrderLine orderLine in orderLines) {
            TotalPrice += orderLine.Product.Price * orderLine.Amount;
        }
        TotalPrice *= 1 - Discount;
    }

    public static Order FromConsole(Customer customer, ref ProductDB products) {
        List<OrderLine> orderLines = new List<OrderLine>();

        int amount = Getter.GetNonNegativeInt(
            "Введите количество видов товаров, которые заказал покупатель:",
            "Неверно введено количество товаров"
        );

        for (int i = 0; i < amount; ++i) {
            Product product;
            while (true) {
                int code = Getter.GetNonNegativeInt("Введите код товара:", "Неверно введен код товара");
                try {
                    product = products.GetProduct(code);
                    break;
                } catch (ProductNotFoundException) {
                    Console.WriteLine("Товар не найден");
                }
            }

            int currAmount = Getter.GetNonNegativeInt(
                "Введите количество единиц товара:", 
                "Неверно введено количество единиц товара"
            );
            
            orderLines.Add(new OrderLine(product, currAmount));
        }


        return new Order(customer, orderLines);
    }

    public static async void ToFile(string fileName, Order order) {
        string jsonString = JsonSerializer.Serialize(order, new JsonSerializerOptions(){ WriteIndented = true });
        await File.WriteAllTextAsync(fileName, jsonString);
    }
}




class Lab9
{
    private static void DescribeProducts(SortedDictionary<int, Product> products) {
        Console.WriteLine("Соответствие кодов продуктов с продуктами");
        foreach (KeyValuePair<int, Product> product in products) {
            Console.WriteLine($"{product.Key} - {product.Value.Name}");
        }
    }

    public static void Main() {
        ProductDB products = ProductDB.FromFile("db.json")!;
        DescribeProducts(products.Products);
        while (true) {
            Customer customer = Customer.FromConsole();

            List<OrderLine> orderLines = new List<OrderLine>();
            Order order = Order.FromConsole(customer, ref products);

            string fileName = Getter.GetNotEmptyString("Введите имя файла:", "Имя файла не может быть пустым");
            Console.WriteLine();

            Order.ToFile(fileName, order);
            Console.WriteLine("Заказ сохранен в файле с именем {0}", fileName);
        }
    }
}