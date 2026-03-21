public interface IChannel{
    void SendMessage(string message);
}

public class EmailChannel: IChannel {
    public void SendMessage(string message){
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsChannel: IChannel {
    public void SendMessage(string message){
        Console.WriteLine($"SMS: {message}");
    }
}

public abstract class Notification{
    protected IChannel _channel;
    public Notification(IChannel channel)=> _channel = channel;

    public abstract void SendNotification(string message);
}

public class AlertNotification:Notification {
    public AlertNotification(IChannel channel) : base(channel){}

    public override void SendNotification(string message){
        _channel.SendMessage(message);
    }

}

public class PromoNotification:Notification {
    public PromoNotification(IChannel channel) : base(channel){}

    public override void SendNotification(string message){
        Console.Write("🎉 PROMO: ");
        _channel.SendMessage(message);
    }
}
