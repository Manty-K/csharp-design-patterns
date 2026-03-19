using System;
public interface INotification{
    void Send(string message);
}


public class SmsNotification : INotification{
    public void Send (string message){
        Console.WriteLine($"Sending SMS : {message}");
    }
}

public class EmailNotification : INotification{
    public void Send (string message){
        Console.WriteLine($"Sending Email : {message}");
    }
}


public class PushNotification : INotification{
    public void Send (string message){
        Console.WriteLine($"Sending Push : {message}");
    }
}

public class NotificationFactory{

    public static INotification Create(string type){
    return type switch 
        {
            "push"  => new PushNotification(),
            "sms"   => new SmsNotification(),
            "email" => new EmailNotification(),
            _       => throw new Exception("Undefined Type")
        };
    }
}