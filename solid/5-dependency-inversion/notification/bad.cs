public class EmailService {
    public void SendEmail(string message) {
        Console.WriteLine($"Sending Email: {message}");
    }
}

public class SmsService {
    public void SendSms(string message) {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

public class OrderService {
    private EmailService _emailService;
    private SmsService _smsService;

    public OrderService() {
        _emailService = new EmailService(); // 🚩
        _smsService   = new SmsService();   // 🚩
    }

    public void PlaceOrder(string product) {
        Console.WriteLine($"Order placed for {product}");

        _emailService.SendEmail($"Your order for {product} is confirmed!");
        _smsService.SendSms($"Order confirmed: {product}");
    }
}

public class Program {
    public static void Main() {
        OrderService orderService = new OrderService(); // 🚩
        orderService.PlaceOrder("Laptop");
    }
}