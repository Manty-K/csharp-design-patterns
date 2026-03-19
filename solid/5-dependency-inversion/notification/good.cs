public interface INotificationService{
    void Send(string message);
}

public class EmailService : INotificationService {
    public void Send(string message) {
        Console.WriteLine($"Sending Email: {message}");
    }
}

public class SmsService : INotificationService{
    public void Send(string message) {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

public class WhatsappService : INotificationService{
    public void Send(string message) {
        Console.WriteLine($"Sending WhatsApp: {message}");
    }
}

public class OrderService {
    private readonly INotificationService[] _services;

    public OrderService(INotificationService[] services) {
        _services = services;
    }

    public void PlaceOrder(string product) {
        Console.WriteLine($"Order placed for {product}");

        foreach(INotificationService service in _services){
            service.Send($"Order confirmed: {product}");
        }
    }
}

public class Program {
    public static void Main() {
        INotificationService[] services = [new EmailService(),new SmsService(),new WhatsappService()];
        OrderService orderService = new OrderService(services);
        orderService.PlaceOrder("Laptop");
    }
}