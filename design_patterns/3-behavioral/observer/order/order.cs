public interface IObserver
{
    void Update(string eventMessage);

}

public interface ISubject
{
    void Subscribe(IObserver o);
    void Unsubscribe(IObserver o);
    void Notify(string message);

}


public class OrderService : ISubject
{
    private List<IObserver> observers = new();
    public void Subscribe(IObserver o) => observers.Add(o);
    public void Unsubscribe(IObserver o) => observers.Remove(o);
    public void Notify(string message)
    {
        foreach (var o in observers)
            o.Update(message);
    }

    public void PlaceOrder(string ordername)
    {
        Console.WriteLine($"Placing order {ordername}");
        Notify(ordername);
    }

}



public class EmailNotifier : IObserver
{
    public void Update(string eventMessage)
    {
        Console.WriteLine($"Notification received on email : {eventMessage}");
    }
}

public class SmsNotifier : IObserver
{
    public void Update(string eventMessage)
    {
        Console.WriteLine($"Notification received on sms : {eventMessage}");
    }
}

public class Program
{
    public static void Main()
    {
        EmailNotifier email = new();
        SmsNotifier sms = new();

        OrderService orderService = new();
        orderService.PlaceOrder("New order");

        orderService.Subscribe(sms);

        orderService.PlaceOrder("order 2");

        orderService.Subscribe(email);

        orderService.PlaceOrder("order 3");

        orderService.Unsubscribe(sms);

        orderService.PlaceOrder("order 4");

    }
}