# Command

Encapsulate a request as an object, letting you parameterise clients with different requests, queue or log operations, and support undoable actions.

## When to use it

- You need undo/redo support — each command stores enough state to reverse itself.
- You want to decouple the object that triggers an action (invoker) from the object that carries it out (receiver).
- You need to queue, schedule, or log operations.

## How it works

1. Define a **Command** interface with `Execute()` and optionally `Undo()`.
2. Implement **concrete commands** — each wraps a receiver and a specific action.
3. The **Invoker** holds a reference to a command (or a history stack) and triggers `Execute`/`Undo` without knowing what the receiver does.
4. The **Receiver** contains the actual business logic.

```csharp
TextEditor editor = new();
Invoker invoker = new();

invoker.Execute(new WriteCommand(editor, "Hello"));
invoker.Execute(new WriteCommand(editor, " World"));
invoker.Execute(new DeleteCommand(editor, 6));
Console.WriteLine(editor.Content); // Hello

invoker.Undo();
Console.WriteLine(editor.Content); // Hello World

invoker.Undo();
Console.WriteLine(editor.Content); // Hello
```

## Examples

| File | What it shows |
|---|---|
| `text-editor/text-editor.cs` | `Invoker` maintains an undo stack; `WriteCommand` and `DeleteCommand` wrap `ITextEditor` — delete captures the removed text so it can be restored on undo |
| `remote/remote.cs` | `RemoteControl` (invoker) presses `TurnOnCommand`/`TurnOffCommand` against a `Light` (receiver); `PressUndo` walks the history stack in reverse |
