public class PaymentProcessor {
    public void ProcessPayment(string paymentType, double amount) {
        if (paymentType == "creditcard") {
            Console.WriteLine($"Charging credit card: {amount}");
        }
        else if (paymentType == "upi") {  // you keep modifying this class
            Console.WriteLine($"Processing UPI: {amount}");
        }
        else if (paymentType == "crypto") { // again modifying...
            Console.WriteLine($"Processing Crypto: {amount}");
        }
    }
}