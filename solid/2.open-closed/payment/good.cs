interface IPaymentMethod{
    void process(double amount);
}

public class CreditCardPayment : IPaymentMethod{

    double process(double amount){
        Console.WriteLine($"Charging credit card: {amount}");
    }
}

public class UPIPayment : IPaymentMethod{

    double process(double amount){
        Console.WriteLine($"Processing UPI: {amount}");
    }
}


public class CryptoPayment : IPaymentMethod{
    double process(double amount){
        Console.WriteLine($"Processing Crypto: {amount}");
    }
}

public class PaymentProcessor {
     public void ProcessPayment(IPaymentMethod payment , double amount) {
        payment.process(amount)
    }
}

var processor = new PaymentProcessor();

processor.ProcessPayment(new UPIPayment(), 499.00);
processor.ProcessPayment(new CryptoPayment(), 1999.00);