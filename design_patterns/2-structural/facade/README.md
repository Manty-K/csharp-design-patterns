# Facade

Provide a simplified interface to a complex subsystem. The facade coordinates multiple components internally so the caller only needs to know one method.

## When to use it

- A subsystem has grown complex and callers shouldn't need to understand all its moving parts.
- You want to layer a clean API over a messy or tightly-coupled set of classes.
- You need to reduce dependencies between clients and subsystem internals.

## How it works

1. Identify the set of subsystem classes the caller would otherwise orchestrate.
2. Create a facade class that holds references to those subsystems.
3. Expose high-level methods on the facade that coordinate the subsystems in the right order.
4. Callers talk only to the facade — they never interact with the subsystems directly.

```csharp
// Without facade — caller must know all subsystems and their order
payment.ChargeCustomer(userId);
inventory.ReserveItem(itemId);
shipping.ScheduleDelivery(userId, itemId);
notification.SendConfirmation(userId);

// With facade — one call does it all
orderFacade.PlaceOrder(userId, itemId);
```

## Examples

| File | What it shows |
|---|---|
| `order/order.cs` | `OrderFacade.PlaceOrder` coordinates `PaymentService`, `InventoryService`, `ShippingService`, and `NotificationService` behind a single call |
| `home-theater/home-theater.cs` | `HomeTheaterFacade.WatchMovie` orchestrates `Projector`, `SoundSystem`, and `StreamingPlayer` so the caller sets up an entire home theatre with one method |
