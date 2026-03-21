# Observer

Define a one-to-many dependency so that when one object (the **subject**) changes state, all its **observers** are notified and updated automatically. Also known as Publish-Subscribe.

## When to use it

- An event in one object needs to trigger reactions in other objects without tight coupling.
- The set of dependents is unknown at compile time or changes dynamically at runtime.
- You want to decouple the producer of events from the consumers.

## How it works

1. Define an **IObserver** interface with an `Update` method.
2. Define an **ISubject** interface with `Subscribe`, `Unsubscribe`, and `Notify` methods.
3. Implement the **Subject** — maintains a list of observers and calls `Notify` when its state changes.
4. Implement **concrete observers** — each reacts to the notification in its own way.

```csharp
OrderService orderService = new();

EmailNotifier email = new();
SmsNotifier sms = new();

orderService.PlaceOrder("order 1");  // no observers yet — silent

orderService.Subscribe(sms);
orderService.PlaceOrder("order 2");  // sms notified

orderService.Subscribe(email);
orderService.PlaceOrder("order 3");  // sms + email notified

orderService.Unsubscribe(sms);
orderService.PlaceOrder("order 4");  // email only
```

## Examples

| File | What it shows |
|---|---|
| `order/order.cs` | `OrderService` (subject) notifies `EmailNotifier` and `SmsNotifier` (observers) when an order is placed; observers can subscribe/unsubscribe at runtime |
| `stock-market/stock-market.cs` | `StockMarket` (subject) tracks per-stock observer lists; `MobileAlert` and `EmailAlert` subscribe to specific stocks and are notified only when a stock's price actually changes |
