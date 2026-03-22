# csharp-design-patterns

C# examples of SOLID principles and Gang of Four design patterns, each with focused implementations and explanations.

## Contents

### [SOLID Principles](solid/README.md)
Five principles for writing maintainable, extensible object-oriented code. Each principle has a `bad.cs` (the problem) and a `good.cs` (the solution).

| # | Principle |
|---|---|
| 1 | [Single Responsibility](solid/1-single-reponsibility/README.md) |
| 2 | [Open / Closed](solid/2-open-closed/README.md) |
| 3 | [Liskov Substitution](solid/3-liskov-substitution/README.md) |
| 4 | [Interface Segregation](solid/4-interface-segregation/README.md) |
| 5 | [Dependency Inversion](solid/5-dependency-inversion/README.md) |

### [Design Patterns](design_patterns/README.md)
Gang of Four patterns grouped by category. Each pattern has focused `.cs` examples and a README explaining intent, structure, and when to use it.

| Category | Patterns |
|---|---|
| [Creational](design_patterns/1-creational/README.md) | Singleton, Builder, Factory, Abstract Factory, Prototype |
| [Structural](design_patterns/2-structural/README.md) | Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy |
| [Behavioral](design_patterns/3-behavioral/README.md) | Command, Observer, Strategy |

### [References](references/)
Supplementary reference material for C# and .NET internals.

| File | Contents |
|---|---|
| [Advanced C#](references/advanced-csharp-reference.md) | Advanced C# language features |
| [.NET Internals](references/dotnet-internals-reference.md) | .NET runtime and internals |

## Notes

- No build system or external dependencies — all files are standalone C# meant to be read.
- Examples use modern C# syntax (global usings, target-typed `new`, expression bodies).
