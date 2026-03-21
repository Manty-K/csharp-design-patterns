// Complex subsystems
public class PaymentService {
    public void ChargeCustomer(string userId) =>
        Console.WriteLine($"[Payment] Charged user {userId}");
}

public class InventoryService {
    public void ReserveItem(string itemId) =>
        Console.WriteLine($"[Inventory] Reserved item {itemId}");
}

public class ShippingService {
    public void ScheduleDelivery(string userId, string itemId) =>
        Console.WriteLine($"[Shipping] Delivery scheduled for {userId} | item {itemId}");
}

public class NotificationService {
    public void SendConfirmation(string userId) =>
        Console.WriteLine($"[Notification] Order confirmation sent to {userId}");
}

// Facade — one simple interface over all of them
public class OrderFacade {
    private PaymentService _payment = new PaymentService();
    private InventoryService _inventory = new InventoryService();
    private ShippingService _shipping = new ShippingService();
    private NotificationService _notification = new NotificationService();

    public void PlaceOrder(string userId, string itemId) {
        Console.WriteLine("-- Placing Order --");
        _payment.ChargeCustomer(userId);
        _inventory.ReserveItem(itemId);
        _shipping.ScheduleDelivery(userId, itemId);
        _notification.SendConfirmation(userId);
        Console.WriteLine("-- Order Complete --");
    }
}

// Usage — caller does one thing
var order = new OrderFacade();
order.PlaceOrder("manthan_01", "item_42");
/*
-- Placing Order --
[Payment] Charged user manthan_01
[Inventory] Reserved item item_42
[Shipping] Delivery scheduled for manthan_01 | item item_42
[Notification] Order confirmation sent to manthan_01
-- Order Complete --
*