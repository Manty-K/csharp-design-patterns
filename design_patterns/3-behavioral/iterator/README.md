# Iterator

Provide a way to sequentially access elements of a collection without exposing its underlying representation.

## When to use it

- You want a uniform way to traverse different collection types (list, tree, graph) without coupling client code to their internals.
- You need multiple independent traversals of the same collection simultaneously.
- The traversal algorithm is complex (e.g. in-order tree walk) and should be encapsulated separately from the collection.

## How it works

1. Define an **Iterator** interface with `HasNext()` and `Next()` methods.
2. Define a **Collection** interface with a `CreateIterator()` factory method.
3. **Concrete iterators** hold traversal state; **concrete collections** produce their matching iterator.
4. The client drives traversal through the iterator interface, unaware of the underlying structure.

```csharp
var iterator = playlist.CreateIterator();
while (iterator.HasNext())
    Console.WriteLine(iterator.Next());
// Bohemian Rhapsody
// Stairway to Heaven
// Hotel California
```

## Examples

| File | What it shows |
|---|---|
| `songs/songs.cs` | `SongPlaylist` (collection) produces a `SongIterator` that walks a `List<string>` sequentially; demonstrates the full `IIterableCollection` + `IIterator` pairing |
| `binary-tree/binary-tree.cs` | `InOrderIterator` uses an explicit stack to traverse a binary tree in-order without recursion; shows how iterators encapsulate complex traversal logic |
