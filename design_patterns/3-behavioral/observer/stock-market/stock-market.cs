public interface IObserver
{
    void Update(string stockName, double newPrice);

}

public interface ISubject
{
    void Subscribe(IObserver observer, string stockName);
    void Unsubscribe(IObserver observer, string stockName);
    void Notify(string stockName, double newPrice);

}

public class Stock
{
    public string Name { get; }
    public double Price { get; set; }

    public Stock(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

class StockMarket : ISubject
{
    private Dictionary<string, double> Stocks = new();
    private Dictionary<string, List<IObserver>> observers = new();

    public StockMarket(List<Stock> stocks)
    {
        foreach (var stock in stocks)
        {
            Stocks[stock.Name] = stock.Price;
            observers[stock.Name] = new();
        }

    }

    public void Subscribe(IObserver observer, string stockName)
    {
        if (observers.ContainsKey(stockName))
            observers[stockName].Add(observer);
    }

    public void Unsubscribe(IObserver observer, string stockName)
    {
        if (observers.ContainsKey(stockName))
            observers[stockName].Remove(observer);
    }


    public void Notify(string stockName, double newPrice)
    {
        foreach (var o in observers[stockName])
        {
            o.Update(stockName, newPrice);
        }
    }

    public void UpdatePrice(string stockName, double newPrice)
    {
        if (Stocks[stockName] == newPrice)
        {
            return;
        }

        Console.WriteLine($"Price update: {stockName} - {newPrice}");
        Stocks[stockName] = newPrice;
        Notify(stockName, newPrice);
    }

}

class MobileAlert : IObserver
{
    public void Update(string stockName, double newPrice)
    {
        Console.WriteLine($"Mobile: Stock: {stockName} : New Price {newPrice}");
    }

}

class EmailAlert : IObserver
{
    public void Update(string stockName, double newPrice)
    {
        Console.WriteLine($"Email: Stock: {stockName} : New Price {newPrice}");
    }

}

public class Program
{
    public static void Main()
    {
        Stock apple = new("AAPL", 20.0);
        Stock google = new("GOOGLE", 10.0);

        List<Stock> stocks = new() { apple, google };

        StockMarket market = new(stocks);

        MobileAlert mobile = new();
        EmailAlert email = new();

        market.Subscribe(mobile, "AAPL");
        market.Subscribe(email, "AAPL");

        market.UpdatePrice("AAPL", 25.0);  // notifies mobile + email

        market.Unsubscribe(mobile, "AAPL");

        market.UpdatePrice("AAPL", 30.0);  // notifies email only
        market.UpdatePrice("GOOGLE", 10.0); // no change, no notification
    }
}