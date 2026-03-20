// Old system — can't modify this
public class LegacySmsService {
    public void SendTextMessage(string phone, string content) {
        Console.WriteLine($"[Legacy SMS] To: {phone} | {content}");
    }
}

// Your app expects this interface
public interface INotification {
    void Send(string message);
}

// Adapter — wraps old, speaks new
public class SmsAdapter : INotification {
    private LegacySmsService _legacy;
    private string _phone;

    public SmsAdapter(LegacySmsService legacy, string phone) {
        _legacy = legacy;
        _phone = phone;
    }

    public void Send(string message) {
        _legacy.SendTextMessage(_phone, message); // translates the call
    }
}

// Usage
INotification notifier = new SmsAdapter(new LegacySmsService(), "+91-9999999999");
notifier.Send("Your OTP is 4521");
// [Legacy SMS] To: +91-9999999999 | Your OTP is 4521