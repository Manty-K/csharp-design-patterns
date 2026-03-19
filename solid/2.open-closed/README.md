# O — Open/Closed Principle

> "A software module/class is open for extension and closed for modification."

## What it means

You should be able to add new behaviour without editing existing, tested code. The typical tool is polymorphism — depend on an abstraction, then add new implementations instead of new `if/else` branches.

## Problem (`payment/bad.cs`, `shapes/bad.cs`)

`PaymentProcessor.ProcessPayment` uses a `if/else` chain keyed on a string. Every new payment type (UPI, crypto, …) requires cracking open the class and adding another branch — risking regressions in already-working paths.

```csharp
// Every new method = editing this class
if (paymentType == "creditcard") { ... }
else if (paymentType == "upi")   { ... }
else if (paymentType == "crypto"){ ... }
```

## Solution (`payment/good.cs`, `shapes/good.cs`)

Define an `IPaymentMethod` interface. Each payment type is its own class that implements it. `PaymentProcessor` never needs to change when a new method is added.

```csharp
interface IPaymentMethod { void Process(double amount); }

class CreditCardPayment : IPaymentMethod { ... }
class UPIPayment        : IPaymentMethod { ... }
class CryptoPayment     : IPaymentMethod { ... }

// Adding a new method = new class, zero edits here
processor.ProcessPayment(new CryptoPayment(), 1999.00);
```
