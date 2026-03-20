# Decorator

Add behaviour to objects dynamically without subclassing. Wrap an object in a decorator that implements the same interface and extends its behaviour by delegating to the wrapped object.

## When to use it

- You need to add responsibilities to individual objects, not to an entire class.
- Subclassing would lead to an explosion of combinations (e.g., Bold+Uppercase, Bold+Exclamation, etc.).
- Behaviours should be composable and stackable at runtime.

## How it works

1. Define a common interface for both the real object and decorators.
2. Implement the base (concrete) class that does the core work.
3. Create an abstract decorator that holds a reference to an `IComponent` and delegates to it.
4. Each concrete decorator extends the abstract one, calling `base` and adding its own behaviour.

```csharp
IText text = new PlainText("hello");
text = new BoldDecorator(text);
text = new UpperCaseDecorator(text);
text = new ExclamationDecorator(text);

Console.WriteLine(text.GetContent()); // **HELLO**!!!
```

## Examples

| File | What it shows |
|---|---|
| `formator/formator.cs` | `BoldDecorator`, `UpperCaseDecorator`, `ExclamationDecorator` stack on top of `PlainText` via `IText` |
| `coffee/coffee.cs` | `MilkDecorator`, `ChocoDecorator` wrap `PlainCoffee` via `ICoffee` — description and cost accumulate with each layer |
