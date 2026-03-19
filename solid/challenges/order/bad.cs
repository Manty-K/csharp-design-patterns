public class OrderManager
{
    public void ProcessOrder(string foodItem, string customerEmail, string paymentType)
    {
        // Validate order
        if (string.IsNullOrEmpty(foodItem))
            throw new Exception("Food item cannot be empty");

        // Calculate price
        double price = 0;
        if (foodItem == "Pizza")  price = 12.99;
        if (foodItem == "Burger") price = 8.99;
        if (foodItem == "Pasta")  price = 10.99;

        // Process payment
        if (paymentType == "creditcard")
            Console.WriteLine($"Charging credit card: {price}");
        else if (paymentType == "upi")
            Console.WriteLine($"Processing UPI: {price}");

        // Send notification
        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Send("noreply@food.com", customerEmail, "Order Confirmed", $"Your {foodItem} is on the way!");

        // Log
        File.AppendAllText("orders.log", $"{DateTime.Now}: Order for {foodItem} placed.\n");
    }
}

/// sip - multiple responsibilities in single class
/// 