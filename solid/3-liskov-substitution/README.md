# L — Liskov Substitution Principle

> "You should be able to use any derived class instead of a parent class and have it behave in the same manner without modification."

## What it means

Subclasses must honour the contract of the base class. If substituting a subclass breaks callers, the inheritance hierarchy is wrong. Throwing exceptions for inherited methods you can't support is a red flag.

## Problem (`bird/bad.cs`, `sql-manager/bad.cs`)

`Bird` has a `Fly()` method. `Penguin` extends `Bird` but throws an exception when `Fly()` is called. Any code that treats a `Penguin` as a `Bird` and calls `Fly()` will crash at runtime.

```csharp
public class Penguin : Bird {
    public override void Fly() {
        throw new Exception("Penguins can't fly!"); // LSP violation
    }
}

// BirdSanctuary.MakeBirdsFly() blows up when Penguin is in the list
```

## Solution (`bird/good.cs`, `sql-manager/good.cs`)

Move `Fly()` out of `Bird` into an `IFlyable` interface. Only birds that actually fly implement it. `BirdSanctuary` works with `IFlyable` — `Penguin` is never passed in.

```csharp
public interface IFlyable { void Fly(); }

public class Sparrow : Bird, IFlyable { public void Fly() { ... } }
public class Penguin : Bird { }          // no Fly() — that's correct

public class BirdSanctuary {
    private List<IFlyable> _birds;       // only flyable birds here
}
```
