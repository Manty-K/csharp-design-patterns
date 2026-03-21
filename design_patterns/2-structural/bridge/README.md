# Bridge

Decouple abstraction from implementation so both can vary independently. Instead of a class hierarchy that multiplies combinations (e.g., SmartRemote×SamsungTV, SmartRemote×SonyTV, …), the abstraction holds a reference to the implementation and delegates to it.

## When to use it

- You want to avoid a combinatorial explosion of subclasses when two dimensions vary independently.
- You need to switch implementations at runtime.
- Both the abstraction and the implementation should be extensible via subclassing without affecting each other.

## How it works

1. Split the class into two hierarchies: **abstraction** (high-level control) and **implementation** (low-level work).
2. The abstraction holds a reference to an implementation interface.
3. Refined abstractions extend behaviour; concrete implementations provide platform-specific logic.
4. Mix and match any abstraction with any implementation at runtime.

```csharp
Remote r1 = new BasicRemote(new SamsungTV());
r1.Power();    // [Samsung] TV On

Remote r2 = new SmartRemote(new SonyTV());
r2.Power();    // [Smart] Launching smart menu... [Sony] TV On
```

## Examples

| File | What it shows |
|---|---|
| `tv/tv.cs` | `Remote` (abstraction) × `IDevice` (implementation) — `BasicRemote`/`SmartRemote` work with any `SamsungTV`/`SonyTV` without new subclasses per combination |
| `channel/channel.cs` | `Notification` (abstraction) × `IChannel` (implementation) — `AlertNotification`/`PromoNotification` send via any `EmailChannel`/`SmsChannel` independently |
