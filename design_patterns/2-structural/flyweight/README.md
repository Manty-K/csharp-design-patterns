# Flyweight

Share common state across many fine-grained objects to reduce memory usage. Instead of each object storing all its data, it holds only the **extrinsic** (unique, per-instance) state and references a shared **flyweight** object for the **intrinsic** (immutable, shared) state.

## When to use it

- You have a huge number of similar objects consuming too much memory.
- Most of the object's state can be made extrinsic (passed in at call time).
- The shared intrinsic state is immutable — flyweights must not be mutated after creation.

## How it works

1. Split object state into **intrinsic** (shared, stored in the flyweight) and **extrinsic** (unique, stored in the context/caller).
2. Create a **Flyweight** class holding only intrinsic state.
3. Create a **Flyweight Factory** that caches and reuses flyweight instances by key.
4. Create a **Context** class that stores extrinsic state and holds a reference to the shared flyweight.

```csharp
var factory = new TreeTypeFactory();

// 7 Tree objects, but only 2 TreeType flyweights allocated
var trees = new List<Tree>();
for (int i = 0; i < 5; i++)
    trees.Add(new Tree(i * 10, i * 5, factory.Get("Oak", "oak_texture")));

trees.Add(new Tree(99, 99, factory.Get("Pine", "pine_texture")));
trees.Add(new Tree(50, 50, factory.Get("Oak", "oak_texture"))); // reuses Oak

// [Factory] Created new TreeType: Oak    ← only once
// [Factory] Created new TreeType: Pine   ← only once
```

## Examples

| File | What it shows |
|---|---|
| `tree/tree.cs` | `TreeType` (flyweight) holds `Name` and `Texture`; `Tree` (context) holds `x,y`; `TreeTypeFactory` reuses flyweights by `name_texture` key |
| `character/character.cs` | `CharacterType` (flyweight) holds `Symbol`, `Font`, and `Size`; `Character` (context) holds `x,y`; `CharacterTypeFactory` reuses flyweights by `symbol_font_size` key |
