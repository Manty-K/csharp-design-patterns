

public interface IMessageContent
{
    string BuildMessage();
}

public class OrderPlacedMessageContent : IMessageContent
{
    private string _foodItemName;

    public OrderPlacedMessageContent(string foodItemName)
    {
        _foodItemName = foodItemName;
    }

    public string BuildMessage()
    {
        return $"{DateTime.Now}: Order for {_foodItemName} placed.\n";
    }
}

public class OrderPlacedEmailContent : IMessageContent
{
    private string _foodItemName;

    public OrderPlacedEmailContent(string foodItemName)
    {
        _foodItemName = foodItemName;
    }

    public string BuildMessage()
    {
        return $"Order Confirmed, Your {_foodItemName} is on the way!";
    }
}

public interface ILogger
{
    void Log();
}

public class FileLogger : ILogger
{

    private readonly string _filename;
    private readonly IMessageContent _messageContent;

    public FileLogger(string filename, IMessageContent messageContent)
    {
        _filename = filename;
        _messageContent = messageContent;
    }

    public void Log()
    {
        string message = _messageContent.BuildMessage();
        File.AppendAllText(_filename, message);
    }
}
public interface IEmailClient
{
    void Send(string sender, string receiver, string payload);
}

public interface IEmailService
{
    void Send(string email);
}

public class MyEmailService : IEmailService
{
    private readonly IEmailClient _client;
    private readonly IMessageContent _content;
    private readonly string _sender = "noreply@food.com";
    public MyEmailService(IEmailClient client, IMessageContent content)
    {
        _client = client;
        _content = content;

    }
    public void Send(string email)
    {
        string message = _content.BuildMessage();
        _client.Send(_sender, email, message);

    }
}

public interface IPaymentService
{
    public void MakePayment(double price);
}

public class CreditCardPayment : IPaymentService
{
    public void MakePayment(double price)
    {
        Console.WriteLine($"Charging credit card: {price}");
    }
}
public class UPIPayment : IPaymentService
{
    public void MakePayment(double price)
    {
        Console.WriteLine($"Processing UPI: {price}");
    }
}

public interface IFoodItem
{
    string Name { get; }
    double Price { get; }
}

public class Pizza : IFoodItem
{
    public string Name => "Pizza";
    public double Price => 12.99;
}
public class Burger : IFoodItem
{
    public string Name => "Burger";
    public double Price => 8.99;
}

public class Pasta : IFoodItem
{
    public string Name => "Pasta";
    public double Price => 10.99;
}

public class OrderManager
{
    private readonly IEmailService _emailService;
    private readonly ILogger _logger;


    public OrderManager(IEmailService emailService, ILogger logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public void ProcessOrder(IFoodItem foodItem, string email, IPaymentService paymentService)
    {
        // get price
        double price = foodItem.Price;

        // process payment
        paymentService.MakePayment(price);

        //send notification
        _emailService.Send(email);

        // log
        _logger.Log();
    }
}


public class Program
{

    public void Main(string[] args)
    {

        IFoodItem foodItem = new Pizza();
        string email = "hello@mail.com";
        IPaymentService paymentType = UPIPayment();

        IEmailService emailService = MyEmailService(SmtpClient("smtp.gmail.com"), OrderPlacedEmailContent(foodItem.Name));
        ILogger logger = FileLogger("orders.log", OrderPlacedMessageContent(foodItem.Name));

        OrderManager orderManager = OrderManager(emailService, logger);

        orderManager.PlaceOrder(foodItem, email, paymentType);

    }
}
