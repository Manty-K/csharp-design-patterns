# Mediator

Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly and lets you vary their interaction independently.

## When to use it

- Many objects communicate in complex, tangled ways — extracting the interaction into a mediator simplifies the dependencies.
- Reusing a component is hard because it carries references to many other components.
- You find yourself subclassing just to vary the coordination logic between objects.

## How it works

1. Define a **Mediator** interface with methods for colleagues to call (e.g. `SendMessage`, `RequestLanding`).
2. The **concrete mediator** knows all colleagues and implements the coordination logic.
3. **Colleagues** hold only a reference to the mediator — they never call each other directly.

```csharp
alice.Send("Hey everyone!");
// [Alice] sends: Hey everyone!
// [Bob]   received from Alice: Hey everyone!
// [Carol] received from Alice: Hey everyone!
```

## Examples

| File | What it shows |
|---|---|
| `chat/chat.cs` | `ChatRoom` (mediator) fans out messages from one `ChatUser` to all others; colleagues only call `_mediator.SendMessage` — no user holds a reference to another user |
| `aircraft/aircraft.cs` | `AirTrafficControl` (mediator) manages a single runway with a `Queue<Aircraft>`; grants landing when free, queues aircraft when busy, and auto-clears the next in queue after each landing |
