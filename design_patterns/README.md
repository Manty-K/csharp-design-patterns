# Design Patterns

Gang of Four design patterns — reusable solutions to common object-oriented design problems. Patterns are grouped into three categories based on their purpose.

Each pattern has a dedicated folder with a `README.md` and one or more focused `.cs` examples.

## Categories

### [1. Creational](1-creational/README.md)
Deal with object creation. Decouple construction logic from the code that uses the object.

| Pattern | Intent |
|---|---|
| [Singleton](1-creational/singleton/README.md) | Ensure only one instance exists and provide a global access point |
| [Builder](1-creational/builder/README.md) | Construct complex objects step-by-step, separating construction from representation |
| [Factory](1-creational/factory/README.md) | Create objects without specifying the exact class |
| [Abstract Factory](1-creational/abstract-factory/README.md) | Create families of related objects without specifying concrete classes |
| [Prototype](1-creational/prototype/README.md) | Clone an existing object instead of constructing from scratch |

### [2. Structural](2-structural/README.md)
Deal with object composition. Build larger structures while keeping them flexible and efficient.

| Pattern | Intent |
|---|---|
| [Adapter](2-structural/adapter/README.md) | Make incompatible interfaces work together |
| [Bridge](2-structural/bridge/README.md) | Decouple abstraction from implementation so both vary independently |
| [Composite](2-structural/composite/README.md) | Treat individual objects and compositions uniformly via a shared interface |
| [Decorator](2-structural/decorator/README.md) | Add behaviour to objects dynamically without subclassing |
| [Facade](2-structural/facade/README.md) | Provide a simplified interface to a complex subsystem |
| [Flyweight](2-structural/flyweight/README.md) | Share common state across many fine-grained objects to reduce memory |
| [Proxy](2-structural/proxy/README.md) | Control access to an object by interposing a surrogate |

### [3. Behavioral](3-behavioral/README.md)
Deal with object communication. Define how objects interact and distribute responsibility.

| Pattern | Intent |
|---|---|
| [Command](3-behavioral/command/README.md) | Encapsulate a request as an object to support undo, queuing, and decoupled invocation |
| [Observer](3-behavioral/observer/README.md) | Notify dependents automatically when an object's state changes |
| [Strategy](3-behavioral/strategy/README.md) | Define a family of interchangeable algorithms and swap them at runtime |
