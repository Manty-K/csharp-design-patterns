# Factory

Create objects without specifying the exact class. You ask the factory for an object; it decides which concrete type to give you.

## When to use it

- The caller should not need to know (or depend on) the concrete class it gets back.
- You want to centralise object creation so the logic is not scattered across callers.
- The type of object to create may vary at runtime based on configuration or input.

## How it works

1. Define a common interface (or abstract class) for the product.
2. Create a factory class (or factory method) that takes some input and returns the right concrete implementation.
3. Callers depend only on the interface, never on the concrete class.

```csharp
interface DocumentFactory {
    Document createDocument();
}

class TxtDocumentFactory : DocumentFactory { ... }
class PdfDocumentFactory : DocumentFactory { ... }

// Caller only knows about DocumentFactory and Document
DocumentFactory factory = new PdfDocumentFactory();
Document doc = factory.createDocument();
doc.addText("Hello");
doc.save("/tmp/output.pdf");
```

## Examples

| File | What it shows |
|---|---|
| `document/document.cs` | `DocumentFactory` with `TxtDocumentFactory` and `PdfDocumentFactory` — same interface, different output formats |
| `notification/notification.cs` | Notification factory variant |
