# Abstract Factory

A factory of factories. Create a family of related objects — without specifying their concrete classes. All objects produced by one factory are guaranteed to be compatible with each other.

## When to use it

- Your system needs to work with multiple families of related objects (e.g., Light theme vs Dark theme).
- You want to enforce that products from the same family are always used together.
- You need to swap an entire product family at runtime without changing the consuming code.

## How it works

1. Define interfaces for each product in the family (`IButton`, `ICheckbox`).
2. Define a factory interface with a creation method for each product (`IUIFactory`).
3. Implement one concrete factory per family (`LightThemeFactory`, `DarkThemeFactory`).
4. The consumer accepts the factory interface — it never references concrete classes.

```csharp
interface IUIFactory {
    IButton   CreateButton();
    ICheckbox CreateCheckbox();
}

class LightThemeFactory : IUIFactory { ... }  // returns LightButton, LightCheckbox
class DarkThemeFactory  : IUIFactory { ... }  // returns DarkButton,  DarkCheckbox

// Switch themes by swapping the factory — no other code changes
IUIFactory factory = new DarkThemeFactory();
var ui = new UIRenderer(factory);
ui.RenderAll();
```

## Examples

| File | What it shows |
|---|---|
| `ui/ui.cs` | `IUIFactory` with `LightThemeFactory` and `DarkThemeFactory`, each producing a matching `IButton` and `ICheckbox` |
| `platform/platform.cs` | Platform-specific UI factory variant |

## Comparison with Factory

| Factory | Abstract Factory |
|---|---|
| Creates **one product** | Creates a **family of products** |
| One factory method | One factory interface with multiple methods |
| Use when creation of a single type varies | Use when families of related types must stay consistent |
