# SOLID Principles

Five design principles that make software easier to understand, maintain, and extend. Coined by Robert C. Martin; the acronym was popularised by Michael Feathers.

Each principle has a dedicated folder with a `README.md`, a `bad.cs` showing the problem, and a `good.cs` showing the corrected design.

## The Principles

| # | Principle | One-line summary |
|---|---|---|
| 1 | **Single Responsibility** | A class should have only one reason to change |
| 2 | **Open / Closed** | Open for extension, closed for modification |
| 3 | **Liskov Substitution** | Subtypes must be substitutable for their base types |
| 4 | **Interface Segregation** | Clients should not be forced to depend on methods they don't use |
| 5 | **Dependency Inversion** | Depend on abstractions, not on concrete implementations |

## Principles

### 1. Single Responsibility
A class should do one thing and have only one reason to change. Bundling unrelated responsibilities — validation, persistence, email — into a single class creates a fragile, hard-to-test module.

→ [`1-single-reponsibility/`](1-single-reponsibility/README.md)

### 2. Open / Closed
New behaviour should be added by writing new code, not by editing existing code. `if/else` chains that grow with every new variant are a sign of OCP violation.

→ [`2-open-closed/`](2-open-closed/README.md)

### 3. Liskov Substitution
If `S` extends `T`, you should be able to replace `T` with `S` everywhere without breaking the program. Subclasses that throw `NotImplementedException` on inherited methods violate this rule.

→ [`3-liskov-substitution/`](3-liskov-substitution/README.md)

### 4. Interface Segregation
One fat interface forces classes to implement methods they don't need. Split interfaces into small, focused ones — each class implements only what it actually does.

→ [`4-interface-segregation/`](4-interface-segregation/README.md)

### 5. Dependency Inversion
High-level modules should not hard-wire concrete dependencies. Inject abstractions so implementations can change without touching the caller.

→ [`5-dependency-inversion/`](5-dependency-inversion/README.md)

## Challenges

[`challenges/`](challenges/) contains exercises to apply multiple principles together without a provided solution walkthrough — try them before reading the code.
