# Composite

Treat individual objects and compositions of objects uniformly. Build tree structures where leaves and branches share the same interface — callers never need to distinguish between them.

## When to use it

- You have a tree-like hierarchy (files/folders, employees/managers, UI components).
- You want client code to work the same way on a single item or a whole group.
- Operations should recurse naturally down the tree without branching logic in the caller.

## How it works

1. Define a common interface for both leaves and composites.
2. **Leaf** — implements the interface; has no children.
3. **Composite** — implements the interface; holds a list of children (which can be leaves or other composites); delegates operations to each child.

```csharp
var root = new Folder("root");
var src  = new Folder("src");
src.Add(new File("Program.cs"));
root.Add(src);
root.Add(new File("README.md"));

root.Print();
// 📁 root
//   📁 src
//     📄 Program.cs
//   📄 README.md
```

## Examples

| File | What it shows |
|---|---|
| `filesystem/filesystem.cs` | `Folder` (composite) and `File` (leaf) both implement `IFileSystemItem` — `Print` recurses through the whole tree with indentation |
| `company/company.cs` | `Manager` (composite) and `IndividualEmployee` (leaf) both implement `IEmployee` — org chart prints recursively from CEO down |
