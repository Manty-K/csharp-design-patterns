# Memento

Without violating encapsulation, capture and externalize an object's internal state so that the object can be restored to that state later.

## When to use it

- You need undo/redo functionality and don't want to expose the internals of the object being saved.
- You want to take snapshots of an object's state to restore it later (e.g. drafts, checkpoints, rollback).
- Storing the full history of changes is acceptable in terms of memory.

## How it works

1. The **Originator** is the object whose state you want to save. It creates a `Memento` via `Save()` and restores from one via `Restore()`.
2. The **Memento** is a snapshot — it holds the saved state and exposes nothing else.
3. The **Caretaker** (e.g. `History`) stores and manages mementos using a stack, but never inspects their contents.

```csharp
history.Push(editor.Save());   // snapshot "Hello"
editor.Write(" World");        // Content: Hello World
editor.Restore(history.Pop()); // Restored: Hello
```

## Examples

| File | What it shows |
|---|---|
| `text-editor/text-editor.cs` | `TextEditor` (originator) appends text and saves snapshots; `History` (caretaker) holds a `Stack<EditorMemento>`; each `Pop` + `Restore` undoes the last write |
| `form/form.cs` | Generic `Originator<T>` and `History<T>` work with any state type; uses a `record Form` with `with`-expressions to produce immutable snapshots of form field changes |
