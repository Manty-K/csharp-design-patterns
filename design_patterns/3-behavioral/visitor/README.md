# Visitor

Represent an operation to be performed on elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates.

## When to use it

- You need to perform many distinct, unrelated operations on an object structure and don't want to pollute their classes with that logic.
- The object structure is stable but you frequently add new operations.
- You want to gather related behaviour into a single visitor class rather than spreading it across many element classes.

## How it works

1. Each **element** class implements `Accept(IVisitor visitor)` — it just calls `visitor.Visit(this)`.
2. The **Visitor** interface declares a `Visit` overload for each concrete element type.
3. Each **concrete visitor** implements the full set of `Visit` overloads, containing the logic for one operation across all element types.
4. Adding a new operation means adding a new visitor — existing element classes are untouched.

```csharp
circle.Accept(new AreaCalculator());      // Area Circle: 78.54
circle.Accept(new PerimeterCalculator()); // Perimeter Circle: 31.42
```

## Examples

| File | What it shows |
|---|---|
| `shapes/shapes.cs` | `Circle` and `Rectangle` implement `IShape`; `AreaCalculator` and `PerimeterCalculator` are two visitors that compute different metrics without touching the shape classes |
| `document/document.cs` | `Heading`, `Paragraph`, and `Image` implement `IDocumentElement`; `HtmlExporter` and `PlainTextExporter` render the same document structure into different formats |
