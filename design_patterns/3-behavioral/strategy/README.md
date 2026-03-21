# Strategy

Define a family of algorithms, encapsulate each one, and make them interchangeable. The strategy lets the algorithm vary independently from the clients that use it.

## When to use it

- You have multiple variants of an algorithm and want to switch between them at runtime.
- You want to eliminate conditionals that select different behaviours (`if isPremium … else if isSeasonal …`).
- Related classes differ only in their behaviour — extract the varying part into a strategy.

## How it works

1. Define a **Strategy** interface with the algorithm's method signature.
2. Implement **concrete strategies** — one class per algorithm variant.
3. The **Context** holds a reference to a strategy and delegates the work to it. Expose a setter so the strategy can be swapped at runtime.

```csharp
PriceCalculator calculator = new(new PercentageDiscount(50));
Console.WriteLine(calculator.Calculate(123));  // 61.5

calculator.SetDiscountStrategy(new FlatDiscount(20));
Console.WriteLine(calculator.Calculate(123));  // 103
```

## Examples

| File | What it shows |
|---|---|
| `discount/discount.cs` | `PriceCalculator` (context) delegates to `IDiscountStrategy`; `PercentageDiscount`, `FlatDiscount`, and `NoDiscount` are swappable strategies with their own validation logic |
| `sorting/sorting.cs` | `Sorter` (context) delegates to `ISortStrategy`; `BubbleSort`, `MergeSort`, and `QuickSort` can be set or swapped at runtime |
