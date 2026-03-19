# D — Dependency Inversion Principle

> "High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details — details should depend on abstractions."

## What it means

High-level business logic should not be hard-wired to specific implementations. Depend on interfaces, not concrete classes. This makes behaviour swappable and code testable without changing the high-level module.

## Problem (`notification/bad.cs`, `file-system/bad.cs`)

`OrderService` directly instantiates `EmailService` and `SmsService`. It's tightly coupled to two concrete implementations. Adding a WhatsApp notification means editing `OrderService`, and testing it means real emails/SMS go out.

```csharp
public class OrderService {
    private EmailService _emailService = new EmailService(); // hard-wired
    private SmsService   _smsService   = new SmsService();   // hard-wired
}
```

## Solution (`notification/good.cs`, `file-system/good.cs`)

Introduce `INotificationService`. `OrderService` depends on the abstraction, not the concrete classes. Services are injected from outside — adding WhatsApp requires zero changes to `OrderService`.

```csharp
interface INotificationService { void Send(string message); }

class EmailService    : INotificationService { ... }
class SmsService      : INotificationService { ... }
class WhatsappService : INotificationService { ... }  // new — no edits to OrderService

// Inject via constructor
INotificationService[] services = [new EmailService(), new SmsService(), new WhatsappService()];
var orderService = new OrderService(services);
orderService.PlaceOrder("Laptop");
```
