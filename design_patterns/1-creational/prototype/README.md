# Prototype

Clone an existing object instead of constructing a new one from scratch. Useful when object creation is expensive or when you want a slightly modified copy of an existing object.

## When to use it

- Creating an object is costly (heavy initialisation, DB lookups, file reads) and a copy is cheaper.
- You need many objects that differ only in a few fields — start from a prototype and tweak.
- The system should be independent of how its objects are created (e.g., when the class is only known at runtime).

## How it works

1. Define a cloning interface (e.g., `IClonable` with a `Clone()` method).
2. Each class implements `Clone()` by returning a new instance with the same field values.
3. Callers invoke `Clone()` rather than `new` — they get an independent copy they can modify freely.

```csharp
Graphic original = new Graphic(id: "g1", name: "Logo", position: "0,0");
Graphic copy     = (Graphic)original.Clone();

copy.Position = "100,200";  // original is unaffected

Console.WriteLine(original); // id: g1 | position: 0,0  | name: Logo
Console.WriteLine(copy);     // id: g1 | position: 100,200 | name: Logo
```

## Examples

| File | What it shows |
|---|---|
| `graphic/graphic.cs` | `Graphic` element in a design tool — clone and reposition without re-constructing |
| `job-posting/job_posting.cs` | Job posting cloned as a template for similar roles |
